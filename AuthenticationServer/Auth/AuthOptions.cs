using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace NewProject.Auth
{
    public class AuthOptions
    {
        /// <summary>
        /// Издатель токена
        /// </summary>
        public const string ISSUER = "MyAuthServer"; 

        /// <summary>
        /// Потребитель токена
        /// </summary>
        public const string AUDIENCE = "MyAuthClient"; 
        
        /// <summary>
        /// Ключ для шифрации
        /// </summary>
        const string KEY = "mysupersecret_secretkey!123"; 

        /// <summary>
        /// Время жизни токена в минутах
        /// </summary>
        public const int LIFETIME = 5;
    }
}
