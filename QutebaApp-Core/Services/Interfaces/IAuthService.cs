using QutebaApp_Data.ViewModels;
using System.Collections.Generic;
using System.Security.Claims;

namespace QutebaApp_Core.Services.Interfaces
{
    public interface IAuthService
    {
        UserVM Register(AuthenticateUserVM authenticateUser, string role, string createdAccountWith);
        UserVM Login(AuthenticateUserVM authenticateUser);
        IEnumerable<Claim> SetCustomClaims(int id, string role);
        AuthenticatedUserVM GetToken(UserVM user, IEnumerable<Claim> claims);
        string Encrypt(string stringToEncrypt);
        bool DoesUserExist(string email);
    }
}
