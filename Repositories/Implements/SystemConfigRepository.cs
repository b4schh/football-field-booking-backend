using FootballField.API.DbContexts;
using FootballField.API.Entities;
using FootballField.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FootballField.API.Repositories.Implements
{
    public class SystemConfigRepository : GenericRepository<SystemConfig>, ISystemConfigRepository
    {
        public SystemConfigRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<SystemConfig?> GetByKeyAsync(string configKey)
        {
            return await _dbSet.FirstOrDefaultAsync(c => c.ConfigKey == configKey);
        }

        public async Task<string?> GetValueAsync(string configKey)
        {
            var config = await GetByKeyAsync(configKey);
            return config?.ConfigValue;
        }
    }
}
