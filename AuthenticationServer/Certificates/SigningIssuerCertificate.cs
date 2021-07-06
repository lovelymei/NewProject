using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace AuthenticationServer.Certificates
{
    public class SigningIssuerCertificate : IDisposable
    {
        private readonly RSA _rsa;

        public SigningIssuerCertificate()
        {
            _rsa = RSA.Create();
        }

        public RsaSecurityKey GetIssuerSigningKey()
        {
            string publicXmlKey = File.ReadAllText("./public_key.xml");
            _rsa.FromXmlString(publicXmlKey);

            return new RsaSecurityKey(_rsa);
        }

        public void Dispose()
        {
            _rsa?.Dispose();
        }
    }
}
