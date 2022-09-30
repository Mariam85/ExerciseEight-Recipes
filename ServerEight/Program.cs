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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

var builder = WebApplication.CreateBuilder();
var securityScheme = new OpenApiSecurityScheme()
{
    Name = "Authorisation",
    Type = SecuritySchemeType.ApiKey,
    Scheme = "Bearer",
    BearerFormat = "JWT",
    In = ParameterLocation.Header,
    Description = "JWT authentication for MinimalAPI"
};

var securityRequirements = new OpenApiSecurityRequirement()
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type= ReferenceType.SecurityScheme,
                Id="Bearer"
            }
        },
        new string[] {}
    }
};

var contactInfo = new OpenApiContact()
{
    Name = "Mariam Mostafa",
    Email = "mariammostafa.493@gmail.com",
    Url = new Uri("https://github.com/Mariam85")
};

var license = new OpenApiLicense()
{
    Name = "Free License"
};

var info = new OpenApiInfo()
{
    Version = "V1",
    Title = "Recipes Api with JWT Authentication",
    Description = "Recipes Api with JWT Authentication",
    Contact = contactInfo,
    License = license
};

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy
                              .WithOrigins(config["Client"])
                              .AllowAnyHeader()
                              .AllowAnyMethod()
                              .WithExposedHeaders("IS-TOKEN-EXPIRED")
                              .AllowCredentials();
                      });
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    var Key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]);
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Key),
        ClockSkew = TimeSpan.FromSeconds(0)
    };
    o.Events = new JwtBearerEvents
    {

        OnAuthenticationFailed = context =>
        {
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
                context.Response.Headers.Add("IS-TOKEN-EXPIRED", "true");
            }
            return Task.CompletedTask;
        }
    };
});
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", info);
    options.AddSecurityDefinition("Bearer", securityScheme);
    options.AddSecurityRequirement(securityRequirements);
});

var connectionString = config["ConnectionString"];
RuntimeConfiguration.AddConnectionString("ConnectionString.PostgreSql (Npgsql)", connectionString);
RuntimeConfiguration.ConfigureDQE<PostgreSqlDQEConfiguration>(c =>
{
    c.AddDbProviderFactory(typeof(NpgsqlFactory));
    c.SetTraceLevel(System.Diagnostics.TraceLevel.Verbose);
});

WebApplication app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();

// Generating a random string for the refresh token.
string RandomString(int length)
{
    var random = new Random();
    var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    return new string(Enumerable.Repeat(chars, length).Select(x => x[random.Next(x.Length)]).ToArray());
}

// Validating the password.
bool ValidatePassword(string password)
{
    int validConditions = 0;
    foreach (char c in password)
    {
        if (c >= 'a' && c <= 'z')
        {
            validConditions++;
            break;
        }
    }
    foreach (char c in password)
    {
        if (c >= 'A' && c <= 'Z')
        {
            validConditions++;
            break;
        }
    }
    if (validConditions == 0) return false;
    foreach (char c in password)
    {
        if (c >= '0' && c <= '9')
        {
            validConditions++;
            break;
        }
    }
    if (validConditions == 1) return false;
    if (validConditions == 2)
    {
        char[] special = { '@', '#', '$', '%', '^', '&', '+', '=' };
        if (password.IndexOfAny(special) == -1) return false;
    }
    return true;
}

// Validating the username.
bool ValidateUsername(string username)
{
    if (username.Length > 30 || username.Length < 8)
    {
        return false;
    }
    int validConditions = 0;
    // Checking that at least 1 letter exists. 
    foreach (char c in username)
    {
        if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z'))
        {
            validConditions++;
            break;
        }
    }
    if (validConditions == 0)
    {
        return false;
    }
    // Checking that at least 1 number exists. 
    foreach (char c in username)
    {
        if (c >= '0' && c <= '9')
        {
            validConditions++;
            break;
        }
    }
    if (validConditions <= 1)
    {
        return false;
    }
    else
    {
        return true;
    }
}

// Signing up endpoint.
app.MapPost("/account/signup", [AllowAnonymous] async (string userName, string password) =>
{
    using (var adapter = new DataAccessAdapter(connectionString))
    {
        try
        {
            var metaData = new LinqMetaData(adapter);
            var usersList = await metaData.User.ToListAsync();
            if (usersList.Find((x) => x.Username == userName) != null)
            {
                return Results.BadRequest("Username already exists");
            }
            else if (String.IsNullOrEmpty(userName) || ValidateUsername(userName) == false)
            {
                return Results.BadRequest("Username is invalid");
            }
            else if (String.IsNullOrEmpty(password) || ValidatePassword(password) == false)
            {
                return Results.BadRequest("Password is invalid");
            }
            else
            {
                // add the user.
                UserEntity userEntity = new();
                PasswordHasher<string> pw = new();
                userEntity.Username = userName;
                userEntity.Password = pw.HashPassword(userName, password);
                await adapter.SaveEntityAsync(userEntity);
                return Results.Ok();
            }
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
});

// Refreshing the token.
app.MapPost("token/refresh-token", async (string refreshToken) =>
{
    using (var adapter = new DataAccessAdapter(connectionString))
    {
        try
        {
            var metaData = new LinqMetaData(adapter);
            UserEntity userFound = await metaData.User.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
            if (userFound is not null)
            {
                // Creating the token.
                var secureKey = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]);
                var securityKey = new SymmetricSecurityKey(secureKey);
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
                var jwtTokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new System.Security.Claims.ClaimsIdentity(new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Name,userFound.Username)
                    }),
                    Expires = DateTime.Now.AddMinutes(20),
                    SigningCredentials = credentials
                };
                var token = jwtTokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = jwtTokenHandler.WriteToken(token);
                if (jwtToken != null)
                {
                    var refresh = RandomString(35);
                    userFound.RefreshToken = refresh;
                    try
                    {
                        await adapter.SaveEntityAsync(userFound);
                        return Results.Ok(new { Token = jwtToken, Refresh = refresh });
                    }
                    catch
                    {
                        return Results.Unauthorized();
                    }
                }
                else
                {
                    return Results.Unauthorized();
                }
            }
            else
            {
                return Results.Unauthorized();
            }
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
});

// Login endpoint.
app.MapPost("/account/login", [AllowAnonymous] async (string userName, string password) =>
{
    using (var adapter = new DataAccessAdapter(connectionString))
    {
        try
        {
            var metaData = new LinqMetaData(adapter);
            UserEntity userFound = await metaData.User.FirstOrDefaultAsync(x => x.Username == userName);
            // Checking if the user exists.
            if (userFound is null)
            {
                return Results.BadRequest("This user does not exist.");
            }

            // Verifying the password.
            PasswordHasher<string> pw = new();
            if (pw.VerifyHashedPassword(userName, userFound.Password, password) != PasswordVerificationResult.Success)
            {
                return Results.BadRequest("The password entered is incorrect.");
            }

            // Creating the token.
            var secureKey = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]);
            var securityKey = new SymmetricSecurityKey(secureKey);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Name,userFound.Username),
                }),
                Expires = DateTime.Now.AddMinutes(20),
                SigningCredentials = credentials
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);
            if (jwtToken != null)
            {
                var refresh = RandomString(35);
                userFound.RefreshToken = refresh;
                await adapter.SaveEntityAsync(userFound);
                return Results.Ok(new { Token = jwtToken, Refresh = refresh });
            }
            else
            {
                return Results.Unauthorized();
            }
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
});

// Adding a recipe.
app.MapPost("recipes/add-recipe", [Authorize] async (Recipe recipe) =>
{
    using (var adapter = new DataAccessAdapter(connectionString))
    {
        try
        {
            // Creating the recipe to add to the recipe table.
            var metaData = new LinqMetaData(adapter);
            RecipeEntity newRecipe = new();
            newRecipe.IsActive = true;
            newRecipe.Title = recipe.Title;
            adapter.SaveEntity(newRecipe);

            // Ingredients  to add to the ingredients table.
            var ingredientsCollection = new EntityCollection<IngredientEntity>();
            IngredientEntity ingredients = new();
            foreach (var ingredient in recipe.Ingredients)
            {
                ingredients.RecipeId = newRecipe.Id;
                ingredients.Component = ingredient;
                ingredientsCollection.Add(ingredients);
            }

            // Instructions to add to the instructions table.
            var instructionsCollection = new EntityCollection<InstructionEntity>();
            InstructionEntity instructions = new();
            foreach (var instruction in recipe.Instructions)
            {
                instructions.RecipeId = newRecipe.Id;
                instructions.Step = instruction;
                instructionsCollection.Add(instructions);
            }

            // Categories to add to the recipe_category table.
            var categoryCollection = new EntityCollection<RecipeCategoryEntity>();
            RecipeCategoryEntity categories = new();
            foreach (var category in recipe.Categories)
            {
                categories.RecipeId = newRecipe.Id;
                categories.CategoryName = category;
                categoryCollection.Add(categories);
            }
            await adapter.SaveEntityCollectionAsync(ingredientsCollection);
            await adapter.SaveEntityCollectionAsync(instructionsCollection);
            await adapter.SaveEntityCollectionAsync(categoryCollection);

            return Results.Created("Successfully added a recipe", recipe);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }

});

// Editing a recipe.
app.MapPut("recipes/edit-recipe/{id}", [Authorize] async (int id, Recipe editedRecipe) =>
{
    //search for recipe if not found,therefore return return Results.BadRequest();....
    using (var adapter = new DataAccessAdapter(connectionString))
    {
        try
        {
            var metaData = new LinqMetaData(adapter);
            // Search for the recipe with this id and update the title.
            RecipeEntity recipeFound = await metaData.Recipe.FirstOrDefaultAsync(x => x.Id == id);
            if (recipeFound is null)
            {
                return Results.BadRequest();
            }
            else
            {
                recipeFound.Title = editedRecipe.Title;
                await adapter.SaveEntityAsync(recipeFound);

                // Removing the old values for the ingredients,instructions,categories.
                await adapter.FetchEntityCollectionAsync(new()
                {
                    CollectionToFetch = recipeFound.Ingredients,
                    FilterToUse = IngredientFields.RecipeId == editedRecipe.Id
                },
                CancellationToken.None);
                await adapter.DeleteEntityCollectionAsync(recipeFound.Ingredients);
                await adapter.FetchEntityCollectionAsync(new()
                {
                    CollectionToFetch = recipeFound.Instructions,
                    FilterToUse = InstructionFields.RecipeId == editedRecipe.Id
                },
                CancellationToken.None);
                await adapter.DeleteEntityCollectionAsync(recipeFound.Instructions);
                await adapter.FetchEntityCollectionAsync(new()
                {
                    CollectionToFetch = recipeFound.RecipeCategories,
                    FilterToUse = RecipeCategoryFields.RecipeId == editedRecipe.Id
                },
                CancellationToken.None);
                await adapter.DeleteEntityCollectionAsync(recipeFound.RecipeCategories);

                // Adding the new fields for the recipe.
                // Ingredients  to add to the ingredients table.
                var ingredientsCollection = new EntityCollection<IngredientEntity>();
                IngredientEntity ingredients = new();
                foreach (var ingredient in editedRecipe.Ingredients)
                {
                    ingredients.RecipeId = recipeFound.Id;
                    ingredients.Component = ingredient;
                    ingredientsCollection.Add(ingredients);
                }

                // Instructions to add to the instructions table.
                var instructionsCollection = new EntityCollection<InstructionEntity>();
                InstructionEntity instructions = new();
                foreach (var instruction in editedRecipe.Instructions)
                {
                    instructions.RecipeId = recipeFound.Id;
                    instructions.Step = instruction;
                    instructionsCollection.Add(instructions);
                }

                // Categories to add to the recipe_category table.
                var categoryCollection = new EntityCollection<RecipeCategoryEntity>();
                RecipeCategoryEntity categories = new();
                foreach (var category in editedRecipe.Categories)
                {
                    categories.RecipeId = recipeFound.Id;
                    categories.CategoryName = category;
                    categoryCollection.Add(categories);
                }
                await adapter.SaveEntityCollectionAsync(ingredientsCollection);
                await adapter.SaveEntityCollectionAsync(instructionsCollection);
                await adapter.SaveEntityCollectionAsync(categoryCollection);
                return Results.Ok();
            }
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
});

// List all recipes.
app.MapGet("/recipes", [Authorize] async () =>
{
    var recipesList = await GetRecipes();
    return Results.Ok(recipesList);
});

// Listing a recipe.
app.MapGet("recipes/list-recipe/{id}", [Authorize] async (int id) =>
{
    var recipesList = await GetRecipes();
    var foundRecipe = recipesList.Find(r => r.Id == id);
    if (foundRecipe == null)
        return Results.NotFound();
    else
        return Results.Ok(foundRecipe);
});

// Deleting a recipe.
app.MapDelete("recipes/delete-recipe/{id}", [Authorize] async (int id) =>
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
app.MapGet("/categories", [Authorize] async () =>
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
app.MapDelete("recipes/remove-category/{category}", [Authorize] async (string category) =>
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

// Adding a category.
app.MapPost("recipes/add-category", [Authorize] async (Categories category) =>
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
app.MapPut("categories/rename-category", [Authorize] async (string oldName, string newName) =>
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
