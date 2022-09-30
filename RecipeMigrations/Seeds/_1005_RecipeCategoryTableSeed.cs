using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeMigrations.Seeds
{
    public record RecipeCategory
    {
        public int RecipeId { get; set; }
        public string Name { get; set; }
    }

    [Migration(1005)]
    public class _1005_RecipeCategoryTableSeed : Migration
    {
        public static List<RecipeCategory> recipeCategories = new()
        {
            new RecipeCategory
            {
               RecipeId = 1,
               Name="Lunch"
            },
            new RecipeCategory
            {
               RecipeId = 1,
               Name="Dinner"
            },
            new RecipeCategory
            {
               RecipeId = 2,
               Name="Brunch"
            },
            new RecipeCategory
            {
               RecipeId = 2,
               Name="Lunch"
            },
            new RecipeCategory
            {
               RecipeId = 3,
               Name="Dinner"
            },
            new RecipeCategory
            {
               RecipeId = 4,
               Name="Dinner"
            },
            new RecipeCategory
            {
               RecipeId = 5,
               Name="Lunch"
            },
            new RecipeCategory
            {
               RecipeId = 5,
               Name="Dinner"
            },
            new RecipeCategory
            {
               RecipeId = 5,
               Name="Brunch"
            },
            new RecipeCategory
            {
               RecipeId = 6,
               Name="Lunch"
            },
            new RecipeCategory
            {
               RecipeId = 7,
               Name="Vegan"
            },
            new RecipeCategory
            {
               RecipeId = 7,
               Name="Desserts"
            },
            new RecipeCategory
            {
               RecipeId = 8,
               Name="Appetizer"
            }
        };
        public override void Up()
        {
            foreach (var category in recipeCategories)
            {

                Insert.IntoTable("recipe_category")
                    .Row(new
                    {
                        recipe_id=category.RecipeId,
                        category_name=category.Name
                    });
            }
        }

        public override void Down()
        {
            //empty, not using
        }
    }
}
