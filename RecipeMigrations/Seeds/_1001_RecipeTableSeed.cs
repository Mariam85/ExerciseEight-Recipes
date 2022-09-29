using FluentMigrator;
using RecipeMigrations.Migrations;

namespace RecipeMigrations.Seeds
{
    public record Recipe
    {
        public string Title { get; set; }
        public bool IsActive { get; set; }
    }

    [Migration(1001)]
    public class _1001_RecipeTableSeed : Migration
    {
        public static List<Recipe> recipes = new()
        {
            new Recipe
            {
                Title = "Miso Mushroom Pasta",
                IsActive = true
            },
            new Recipe
            {
                Title = "Shrimp Tacos",
                IsActive = true
            },
            new Recipe
            {
                Title = "Beef Bulgogi",
                IsActive = true
            },
            new Recipe
            {
                Title = "Pasta Carbonara",
                IsActive = true
            },
            new Recipe
            {
                Title = "Grilled Pork Chops",
                IsActive = true
            },
            new Recipe
            {
                Title = "Thai fried rice",
                IsActive = true
            },
            new Recipe
            {
                Title = "Vegan icecream",
                IsActive = true
            },
            new Recipe
            {
                Title = "Greek salad",
                IsActive = true
            }
        };

        public override void Up()
        {
            foreach (var recipe in recipes)
            {

                Insert.IntoTable("recipe")
                    .Row(new
                    {
                        is_active = recipe.IsActive,
                        title = recipe.Title
                    });
            }
        }

        public override void Down()
        {
            //empty, not using
        }
    }
}