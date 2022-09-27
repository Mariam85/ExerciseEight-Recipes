using FluentMigrator;

namespace RecipeMigrations.Migrations
{
    [Migration(0005)]
    public class _0005_RecipeCategoryTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("recipe_category")
                .WithColumn("recipe_id").AsInt32().NotNullable().ForeignKey("recipe", "id").OnDeleteOrUpdate(System.Data.Rule.Cascade)
                .WithColumn("category_name").AsString(100).NotNullable().ForeignKey("category", "name").OnDeleteOrUpdate(System.Data.Rule.Cascade);

            Create.PrimaryKey().OnTable("recipe_category")
                .Columns("recipe_id", "category_name");

        }
    }
}