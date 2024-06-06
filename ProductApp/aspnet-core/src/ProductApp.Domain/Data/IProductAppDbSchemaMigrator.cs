using System.Threading.Tasks;

namespace ProductApp.Data;

public interface IProductAppDbSchemaMigrator
{
    Task MigrateAsync();
}
