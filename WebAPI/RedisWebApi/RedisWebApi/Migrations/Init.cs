//using FluentMigrator;
//using FluentMigrator.SqlServer;

//namespace test
//{
//    [Migration(20180430121800)]
//    public class InitMigration : Migration
//    {
//        public override void Up()
//        {
//            Create.Table("Category")
//                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
//                .WithColumn("Name").AsString(100).NotNullable();

//            Create.Table("Movie")
//                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
//                .WithColumn("Name").AsString(100).NotNullable()
//                .WithColumn("Describe").AsString()
//                .WithColumn("CategoryId").AsInt64().NotNullable();

//            Create.ForeignKey("fk_Movie_Category_CategoryId")
//                .FromTable("Movie").ForeignColumn("CategoryId")
//                .ToTable("Category").PrimaryColumn("Id");

//            Insert.IntoTable("Category").Row(new { Id = 1, Name = "Horror" }).WithIdentityInsert();
//            Insert.IntoTable("Category").Row(new { Id = 2, Name = "Comedy" }).WithIdentityInsert();
//            Insert.IntoTable("Movie").Row(new { Id = 1, Name = "Przyjaciele", Describe = "test", CategoryId = 2 }).WithIdentityInsert();
//            Insert.IntoTable("Movie").Row(new { Id = 2, Name = "Obecność", Describe = "test", CategoryId = 1 }).WithIdentityInsert();
//            Insert.IntoTable("Movie").Row(new { Id = 3, Name = "Annabele", Describe = "test", CategoryId = 1 }).WithIdentityInsert();
//        }

//        public override void Down()
//        {
//            Delete.Table("Movie");
//            Delete.Table("Category");
//        }
//    }
//}