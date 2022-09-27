using FluentMigrator;

namespace RecipeMigrations.Migrations
{
    [Migration(0004)]
    public class _0004_CategoryTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("category")
                .WithColumn("name").AsString(100).PrimaryKey()
                .WithColumn("is_active").AsBoolean().WithDefaultValue(true);
        }
    }
}
