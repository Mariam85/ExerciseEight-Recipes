using FluentMigrator;
using RecipeMigrations.Migrations;

namespace RecipeMigrations.Seeds
{
    public record RecipeIngredient
    {
        public int RecipeId { get; set; }
        public string Component { get; set; }
    }

    [Migration(1002)]
    public class _1002_IngredientTableSeed : Migration
    {
        public static List<RecipeIngredient> recipeIngredients = new()
        {
            new RecipeIngredient
            {
                RecipeId = 1,
                Component = "1 pound spaghetti, fettuccine, or pasta of choice"
            },
            new RecipeIngredient
            {
                RecipeId = 1,
                Component = "Salt, for the pasta water"
            },
            new RecipeIngredient
            {
                RecipeId = 1,
                Component = "1 tablespoon olive oil"
            },
            new RecipeIngredient
            {
                RecipeId = 1,
                Component = "6 ounces cremini mushrooms, sliced"
            },
            new RecipeIngredient
            {
                RecipeId = 1,
                Component = "4 tablespoons unsalted butter"
            },
            new RecipeIngredient
            {
                RecipeId = 1,
                Component = "2 cloves garlic, minced"
            },
            new RecipeIngredient
            {
                RecipeId = 1,
                Component = "1/3 cup freshly grated Parmesan cheese"
            },
            new RecipeIngredient
            {
                RecipeId = 1,
                Component = "2 tablespoons white miso paste"
            },
            new RecipeIngredient
            {
                RecipeId = 1,
                Component = "Optional: Fresh sage, parsley, or thyme, for garnish"
            },
            new RecipeIngredient
            {
                RecipeId = 2,
                Component = "2 pounds extra-large shrimp (26-30 count), peeled, cleaned, and deveined"
            },
            new RecipeIngredient
            {
                RecipeId = 2,
                Component = "2 teaspoons blackened seasoning"
            },
            new RecipeIngredient
            {
                RecipeId = 2,
                Component = "1/2 teaspoon salt, if needed"
            },
            new RecipeIngredient
            {
                RecipeId = 2,
                Component = "8 corn or flour tortillas"
            },
            new RecipeIngredient
            {
                RecipeId = 2,
                Component = "2 cups thinly sliced red cabbage"
            },
            new RecipeIngredient
            {
                RecipeId = 2,
                Component = "1/4 cup mayonnaise"
            },
            new RecipeIngredient
            {
                RecipeId = 2,
                Component = "1/2 tablespoon granulated sugar"
            },
            new RecipeIngredient
            {
                RecipeId = 2,
                Component = "1/4 teaspoon black pepper"
            },
            new RecipeIngredient
            {
                RecipeId = 2,
                Component = "1/2 tablespoon white vinegar"
            },
            new RecipeIngredient
            {
                RecipeId = 2,
                Component = "1 tablespoon vegetable oil, avocado oil, or grapeseed oil"
            },
            new RecipeIngredient
            {
                RecipeId = 2,
                Component = "1/2 cup crumbled Mexican queso fresco"
            },
            new RecipeIngredient
            {
                RecipeId = 2,
                Component = "1/2 cup chopped cilantro"
            },
            new RecipeIngredient
            {
                RecipeId = 2,
                Component = "Lime wedges"
            }
        };
        public override void Up()
        {
            foreach (var ing in recipeIngredients)
            {
                Insert.IntoTable("ingredient")
                    .Row(new
                    {
                        recipe_id = ing.RecipeId,
                        component = ing.Component
                    });
            }
        }
        public override void Down()
        {
            //empty, not using
        }
    }
}