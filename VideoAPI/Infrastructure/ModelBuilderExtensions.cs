using Microsoft.EntityFrameworkCore;

namespace VideoAPI.Infrastructure
{
    public static class ModelBuilderExtensions
    {
        public static void CustomNamingConvention(this ModelBuilder modelBuilder)
        {

            // Prefix column names with table name
            // Id => Blog_Id
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    property.SetColumnName(property.GetColumnName().ToLower());
                }
            }

            // Rename Foreign Key
            // FK_Post_Blog_BlogId => FK_Post_Blog_BlogId_Test
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    foreach (var fk in entityType.FindForeignKeys(property))
                    {
                        fk.SetConstraintName(property.Name.ToLower().Replace("id", "_id"));
                    }
                }
            }
        }
    }
}