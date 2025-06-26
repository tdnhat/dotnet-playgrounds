
using _002_option_pattern.Models.Options;
using Microsoft.Extensions.Options;

namespace _002_option_pattern.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly DatabaseOptions _options;
        public DatabaseService(IOptions<DatabaseOptions> options)
        {
            _options = options.Value;
        }
        public Task<string> GetConnectionInfoAsync()
        {
            return Task.FromResult($"Connection: {_options.ConnectionString}, Timeout: {_options.TimeoutSeconds} seconds");
        }
    }
}