using Microsoft.AspNetCore.Identity;

using StoreApp.Entities.DTOs;

namespace StoreApp.Services.Abstract
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> RegisterUser(UserForRegistrationDto dto);
        Task<bool> ValidateUser(UserForAuthenticationDto dto);
        Task<TokenDto> CreateAccessToken(bool populateExp);
        Task<TokenDto> CreateRefreshToken(TokenDto tokenDto);

    }
}
