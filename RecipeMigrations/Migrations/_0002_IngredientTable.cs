using FluentMigrator;

namespace RecipeMigrations.Migrations
{
    [Migration(0002)]
    public class _0002_IngredientTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("ingredient")
                .WithColumn("recipe_id").AsInt32().NotNullable().ForeignKey("recipe", "id").OnDeleteOrUpdate(System.Data.Rule.Cascade)
                .WithColumn("component").AsString(1000).NotNullable();

            Create.PrimaryKey().OnTable("ingredient")
                .Columns("recipe_id", "component");

        }
    }
}
