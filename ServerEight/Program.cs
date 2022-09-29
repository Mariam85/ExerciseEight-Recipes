using DBRecipes.DatabaseSpecific;
using DBRecipes.Linq;
using SD.LLBLGen.Pro.ORMSupportClasses;
using SD.LLBLGen.Pro.DQE.PostgreSql;
using Npgsql;
using DBRecipes.EntityClasses;
using DBRecipes.HelperClasses;
using DBRecipes;
using SD.LLBLGen.Pro.LinqSupportClasses;
using SD.LLBLGen.Pro.QuerySpec;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
    });
});

var connectionString = config["ConnectionString"];
RuntimeConfiguration.AddConnectionString("ConnectionString.PostgreSql (Npgsql)", connectionString);
RuntimeConfiguration.ConfigureDQE<PostgreSqlDQEConfiguration>(c =>
{
    c.AddDbProviderFactory(typeof(NpgsqlFactory));
    c.SetTraceLevel(System.Diagnostics.TraceLevel.Verbose);
});
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

// Getting recipes from the database.
static async Task<List<Recipe>> GetRecipes()
{
    var recipes = new EntityCollection<RecipeEntity>();
    var prefetchPath = new PrefetchPath2(EntityType.RecipeEntity)
    {
        RecipeEntity.PrefetchPathInstructions,
        RecipeEntity.PrefetchPathIngredients,
        RecipeEntity.PrefetchPathRecipeCategories
    };

    using (var adapter = new DataAccessAdapter())
    {
        var qp = new QueryParameters()
        {
            CollectionToFetch = recipes,
            FilterToUse = RecipeFields.IsActive == true,
            PrefetchPathToUse = prefetchPath
        };
        await adapter.FetchEntityCollectionAsync(qp, CancellationToken.None);
    }
    List<Recipe> recipeList = new();
    foreach (var recipeEntity in recipes)
    {
        Recipe recipe = new Recipe
        {
            Id = recipeEntity.Id,
            Title = recipeEntity.Title,
            Categories = new(),
            Ingredients = new(),
            Instructions = new()
        };

        foreach (var instruction in recipeEntity.Instructions)
        {
            recipe.Instructions.Add(instruction.Step);

        }

        foreach (var ingredient in recipeEntity.Ingredients)
        {
            recipe.Ingredients.Add(ingredient.Component);
        }

        foreach (var category in recipeEntity.RecipeCategories)
        {
            recipe.Categories.Add(category.CategoryName);
        }
        recipeList.Add(recipe);
    }
    return recipeList;
}

// List all categories.  
app.MapGet("/categories", async () =>
{
    using (var adapter = new DataAccessAdapter(connectionString))
    {
        try
        {
            var metaData = new LinqMetaData(adapter);
            var categories = await metaData.Category.Where(x => x.IsActive).ToListAsync();
            if (categories == null)
                return Results.BadRequest("There are no categories");
            else
            {
                List<Categories> categoriesList = new();
                foreach (var cat in categories)
                {
                    Categories category = new Categories
                    {
                        Name = cat.Name
                    };
                    categoriesList.Add(category);
                }
                return Results.Ok(categoriesList);
            }
        }
        catch (Exception e)
        {
            app.Logger.LogError(e.Message);
            return Results.BadRequest(e.Message);
        }
    }
});

// Deleting a category.
app.MapDelete("recipes/remove-category/{category}", async (string category) =>
{
    using (var adapter = new DataAccessAdapter(connectionString))
    {
        try
        {
            var metaData = new LinqMetaData(adapter);
            CategoryEntity categoryFetched = await metaData.Category.FirstOrDefaultAsync(x => x.Name == category);
            if (categoryFetched is null)
            {
                return Results.BadRequest("This category does not exist.");
            }
            else
            {
                // Deactivating the category with this name.
                categoryFetched.IsActive = false;
                await adapter.SaveEntityAsync(categoryFetched);

                // Deleting records in the RecipyCategory Entity with this category name.
                EntityCollection<RecipeCategoryEntity> recipeCategoryDictionaryEntities = new();
                var qp = new QueryParameters()
                {
                    CollectionToFetch = recipeCategoryDictionaryEntities,
                    FilterToUse = RecipeCategoryFields.CategoryName == category
                };
                await adapter.FetchEntityCollectionAsync(qp, CancellationToken.None);
                await adapter.DeleteEntityCollectionAsync(recipeCategoryDictionaryEntities);

                // Deactivating recipes with only this category as their category.
                var recipes = new EntityCollection<RecipeEntity>();
                var prefetchPath = new PrefetchPath2(EntityType.RecipeEntity)
                {
                    RecipeEntity.PrefetchPathInstructions,
                    RecipeEntity.PrefetchPathIngredients,
                    RecipeEntity.PrefetchPathRecipeCategories
                };
                var qpp = new QueryParameters()
                {
                    CollectionToFetch = recipes,
                    FilterToUse = RecipeFields.IsActive == true,
                    PrefetchPathToUse = prefetchPath
                };
                await adapter.FetchEntityCollectionAsync(qpp, CancellationToken.None);
                foreach (var r in recipes)
                {
                    if (r.RecipeCategories.Count == 0)
                    {
                        r.IsActive = false;
                    }
                }
                await adapter.SaveEntityCollectionAsync(recipes);
                return Results.Ok("Successfuly deleted.");
            }
        }
        catch (Exception e)
        {
            app.Logger.LogError(e.Message);
            return Results.BadRequest(e.Message);
        }
    }
});

// List all recipes.
app.MapGet("/recipes", async () =>
{
    var recipesList = await GetRecipes();
    return Results.Ok(recipesList);
});

// Listing a recipe.
app.MapGet("recipes/list-recipe/{id}", async (int id) =>
{
    var recipesList = await GetRecipes();
    var foundRecipe = recipesList.Find(r => r.Id == id);
    if (foundRecipe == null)
        return Results.NotFound();
    else
        return Results.Ok(foundRecipe);
});

// Deleting a recipe.
app.MapDelete("recipes/delete-recipe/{id}", async (int id) =>
{
    using (var adapter = new DataAccessAdapter(connectionString))
    {
        try
        {
            var metaData = new LinqMetaData(adapter);
            RecipeEntity recipeFetched = await metaData.Recipe.FirstOrDefaultAsync(x => x.Id == id);
            if (recipeFetched is null)
            {
                return Results.BadRequest("This recipe does not exist.");
            }
            else
            {
                recipeFetched.IsActive = false;
                await adapter.SaveEntityAsync(recipeFetched);
                return Results.Ok("Successfuly deleted");
            }
        }
        catch (Exception e)
        {
            app.Logger.LogError(e.Message);
            return Results.BadRequest(e.Message);
        }
    }
});

// Adding a category.
app.MapPost("recipes/add-category", async (Categories category) =>
{
    using (var adapter = new DataAccessAdapter(connectionString))
    {
        try
        {
            var metaData = new LinqMetaData(adapter);
            CategoryEntity categoryFetched = await metaData.Category.FirstOrDefaultAsync(x => x.Name == category.Name);

            // The category name does not exist, therefore we add it.
            if (categoryFetched is null)
            {
                var categoryToAdd = new CategoryEntity() { Name = category.Name, IsActive = true };
                await adapter.SaveEntityAsync(categoryToAdd);
                return Results.Created("Successfully added a category", category);
            }
            // The category name exists
            else
            {
                // The category name exists and is active.
                if (categoryFetched.IsActive == true)
                {
                    return Results.BadRequest("This category already exists");
                }
                // The category name exists but is inactive, therefore activate it.
                else
                {
                    categoryFetched.IsActive = true;
                    await adapter.SaveEntityAsync(categoryFetched);
                    return Results.Created("Successfully added a category", category);
                }

            }
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
});

// Renaming a category.
app.MapPut("categories/rename-category", async (string oldName, string newName) =>
{
    using (var adapter = new DataAccessAdapter(connectionString))
    {
        try
        {
            var metaData = new LinqMetaData(adapter);
            CategoryEntity categoryFetched = await metaData.Category.FirstOrDefaultAsync(x => x.Name == oldName);
            if (categoryFetched is null)
            {
                return Results.BadRequest("old category does not exist.");
            }
            else
            {
                // Checking that the new category name does not exist.
                CategoryEntity newCategory = await metaData.Category.FirstOrDefaultAsync(x => x.Name == newName);
                // The new name does not exist.
                if (newCategory is null)
                {
                    categoryFetched.Name = newName;
                    await adapter.SaveEntityAsync(categoryFetched);
                    return Results.Ok("Successfully updated");
                }
                // The new name exists.
                else
                {
                    return Results.BadRequest("new category name already exists");
                }
            }
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
});

app.UseCors();
app.Run();
