using FluentMigrator;

namespace RecipeMigrations.Migrations
{
    [Migration(0003)]
    public class _0003_InstructionTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("instruction")
                .WithColumn("recipe_id").AsInt32().NotNullable().ForeignKey("recipe", "id").OnDeleteOrUpdate(System.Data.Rule.Cascade)
                .WithColumn("step").AsString(1000).NotNullable();

            Create.PrimaryKey().OnTable("instruction")
                .Columns("recipe_id", "step");

        }
    }
}

