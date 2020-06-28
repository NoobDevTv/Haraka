using Haraka.WebApp.Shared.Information;
using Microsoft.IdentityModel.Tokens;

namespace Haraka.WebApp.Shared.Services
{
    public interface IUserSessionService
    {
        SymmetricSecurityKey Key { get; }
        string Issuer { get; }

        string CreateToken(LoginInfo loginInfo);
        void LoadOrCreateKey();
    }
}