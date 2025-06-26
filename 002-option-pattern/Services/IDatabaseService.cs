namespace _002_option_pattern.Services
{
    public interface IDatabaseService
    {
        Task<string> GetConnectionInfoAsync();
    }
}