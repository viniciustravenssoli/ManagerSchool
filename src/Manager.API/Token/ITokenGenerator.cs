using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Manager.API.Token{
    public interface ITokenGenerator{
        Task<AuthResult> GenerateJwtToken(IdentityUser user);

        Task<List<Claim>> GetAllValidClaims(IdentityUser user);

        Task<AuthResult> VerifyAndGenerateToken(TokenRequest tokenRequest);
    }
}