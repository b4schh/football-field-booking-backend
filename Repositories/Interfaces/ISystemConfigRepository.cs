using FootballField.API.Entities;

namespace FootballField.API.Repositories.Interfaces
{
    public interface ISystemConfigRepository : IGenericRepository<SystemConfig>
    {
        Task<SystemConfig?> GetByKeyAsync(string configKey);
        Task<string?> GetValueAsync(string configKey);
    }
}
