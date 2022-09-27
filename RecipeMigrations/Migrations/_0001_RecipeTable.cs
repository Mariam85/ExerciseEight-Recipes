using FluentMigrator;

namespace RecipeMigrations.Migrations
{
    [Migration(0001)]
    public class _0001_RecipeTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("recipe")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("title").AsString(100).NotNullable().Unique()
                .WithColumn("is_active").AsBoolean().NotNullable().WithDefaultValue(true);
        }
    }
}
