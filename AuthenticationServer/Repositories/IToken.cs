using System.Threading.Tasks;

namespace NewProject.Authorization.Services
{
    public interface IToken
    {
        string GenerateRefreshToken();
    }
}