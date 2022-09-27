using FluentMigrator;

namespace RecipeMigrations.Migrations
{
    [Migration(0006)]
    public class _0006_UserTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("user")
                .WithColumn("username").AsString(100).PrimaryKey()
                .WithColumn("password").AsString(100).NotNullable()
                .WithColumn("refresh_token").AsString().Nullable();
        }
    }
}


