using System.Threading.Tasks;

namespace NewProject.AuthenticationServer.Repositories
{
    public interface IToken
    {
        string GenerateRefreshToken();
    }
}