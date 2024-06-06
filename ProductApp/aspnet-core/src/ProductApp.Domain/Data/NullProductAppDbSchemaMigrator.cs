using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace ProductApp.Data;

/* This is used if database provider does't define
 * IProductAppDbSchemaMigrator implementation.
 */
public class NullProductAppDbSchemaMigrator : IProductAppDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
