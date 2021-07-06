using System.Threading.Tasks;

namespace NewProject.Authorization.Services
{
    public interface IToken
    {
        Task<string> CreateHash(string password);
        string GenerateRefreshToken();
    }
}