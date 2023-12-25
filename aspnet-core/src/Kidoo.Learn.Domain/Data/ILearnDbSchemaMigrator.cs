using System.Threading.Tasks;

namespace Kidoo.Learn.Data;

public interface ILearnDbSchemaMigrator
{
    Task MigrateAsync();
}
