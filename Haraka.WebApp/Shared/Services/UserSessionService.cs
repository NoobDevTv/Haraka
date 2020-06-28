using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using Haraka.WebApp.Shared.Model;
using Haraka.WebApp.Shared.Information;

namespace Haraka.WebApp.Shared.Services
{

    public sealed class UserSessionService : JwtSecurityTokenHandler, IUserSessionService
    {
        public SymmetricSecurityKey Key { get; private set; }
        public string Issuer { get; }

        public UserSessionService(string issuer)
        {
            Issuer = issuer;
        }

        public void LoadOrCreateKey()
        {
            var key = new byte[128];
            var keyFile = new FileInfo(Path.Combine(".", "jwt", "issuer.key"));

            if (!keyFile.Exists)
            {
                if (!keyFile.Directory.Exists)
                    keyFile.Directory.Create();

                GenerateNewKey(ref key, 0, key.Length);
                File.WriteAllBytes(keyFile.FullName, key);
            }
            else
            {
                key = File.ReadAllBytes(keyFile.FullName);
            }

            Key = new SymmetricSecurityKey(key);
        }


        public string CreateToken(LoginInfo loginInfo)
        {
            //todo: pw check 
            //todo: user lookup
            return InternalCreate(new User { Username = loginInfo.Username });
        }


        private string InternalCreate(User user)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                            {
                                new Claim(ClaimTypes.Name, user.Username),
                                new Claim("session", new Guid().ToString())
                            }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha512Signature),
                Issuer = Issuer,
                NotBefore = DateTime.Now,
                IssuedAt = DateTime.Now
            };
            var token = CreateToken(tokenDescriptor);
            return WriteToken(token);
        }
        private void GenerateNewKey(ref byte[] key, int offset, int count)
        {
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(key, offset, count);
        }

        public override bool CanReadToken(string token) => base.CanReadToken(token);

        public override ClaimsPrincipal ValidateToken(string token, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
        {
            return base.ValidateToken(token, validationParameters, out validatedToken);
        }
    }

}
