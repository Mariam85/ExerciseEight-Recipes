using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeMigrations.Seeds
{
    public record CategoryRecord
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }

    [Migration(1004)]
    public class _1004_CategoryTableSeed : Migration
    {
        public static List<CategoryRecord> categories = new()
        {
            new CategoryRecord
            {
                Name = "Brunch",
                IsActive = true
            },
            new CategoryRecord
            {
                Name = "Dinner",
                IsActive = true
            },
            new CategoryRecord
            {
                Name = "Lunch",
                IsActive = true
            },
            new CategoryRecord
            {
                Name = "Appetizer",
                IsActive = true
            },
            new CategoryRecord
            {
                Name = "Desserts",
                IsActive = true
            },
            new CategoryRecord
            {
                Name = "Drinks",
                IsActive = true
            },
            new CategoryRecord
            {
                Name = "Vegan",
                IsActive = true
            }
        };
        public override void Up()
        {
            foreach (var category in categories)
            {

                Insert.IntoTable("category")
                    .Row(new
                    {
                        is_active = category.IsActive,
                        name = category.Name
                    });
            }
        }

        public override void Down()
        {
            //empty, not using
        }
    }
}
