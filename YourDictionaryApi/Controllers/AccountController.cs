using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinessLogic.Models;
using BusinessLogic.Service;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YourDictionaryApi.App_Start;
using YourDictionaryApi.Models;

namespace YourDictionaryApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IRefreshTokenService _refreshTokenService;
        private IUserService _userService;
        public AccountController(ITokenService tokenService, IRefreshTokenService refreshTokenService,IUserService userService)
        {
            _tokenService = tokenService;
            _refreshTokenService = refreshTokenService;
            _userService = userService;
        }
        [HttpGet("GetAccount")]
        [Authorize]
        public async Task<ActionResult<int>> GetAccount()
        {
            var bb = User;
            var a = 1;
            if (a == 1)
                throw new Exception("aaa");
            return Ok(32);
        }
        [HttpGet("GetUser")]
        [AllowAnonymous]
        public async Task<UserModelBL> GetUser()
        {
            var user = await this._userService.GetUsers();
            return user;
        }
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<Dictionary<string, string>> Login()
        {
            UserModel user = new UserModel
            {
                Id = 555,
                UserName = "hardav",
                FirstName = "Davit",
                LastName = "Harutyunyan",
                Password = "123qweASD"
            };
            string clientId = "company-employee";

            return await this.GenerateToken(user, clientId);
        }

        private async Task<Dictionary<string, string>> GenerateToken(UserModel user, string clientId, AccessTokenType accessTokenType = AccessTokenType.Reference)
        {
            var request = new TokenCreationRequest();
            var identityUser = new IdentityServerUser(user.UserName);
            Dictionary<string, string> tokenResponse = null;
            string accessToken = null;
            string identityToken = null;
            string refreshToken = null;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Id.ToString()),
                new Claim("FirstName",user.FirstName),
                new Claim("LastName",user.LastName),
                new Claim("UserName",user.UserName)
            };
            try
            {
                // Add Identity User information
                identityUser.DisplayName = user.FirstName + " " + user.LastName;
                identityUser.AuthenticationTime = DateTime.UtcNow;
                identityUser.IdentityProvider = IdentityServer4.IdentityServerConstants.LocalIdentityProvider;

                // Build Token Request information
                request.Subject = identityUser.CreatePrincipal();
                request.IncludeAllIdentityClaims = true;
                request.ValidatedRequest = new IdentityServer4.Validation.ValidatedRequest
                {
                    Subject = request.Subject,
                };

                var clients = AuthenticationConfig.GetClients();
                var client = clients.FirstOrDefault(item => item.ClientId == clientId) ?? throw new Exception("invalid client");

                request.ValidatedRequest.SetClient(client);

                request.ValidatedResources = new IdentityServer4.Validation.ResourceValidationResult
                {
                    Resources = new Resources(AuthenticationConfig.GetIdentityResources(), AuthenticationConfig.GetApiResources(), AuthenticationConfig.GetApiScopes())
                };

                Token accessTokenObj = null;
                if (accessTokenType == AccessTokenType.Reference)
                {
                    accessTokenObj = await _tokenService.CreateAccessTokenAsync(request);
                    accessTokenObj.Issuer = AuthenticationConfig.authority;
                    foreach (var claim in claims)
                    {
                        accessTokenObj.Claims.Add(claim);
                    }

                    accessToken = await _tokenService.CreateSecurityTokenAsync(accessTokenObj);
                }

                

                var principal = new ClaimsPrincipal();
                refreshToken = await _refreshTokenService.CreateRefreshTokenAsync(principal, accessTokenObj , client);

                tokenResponse = new Dictionary<string, string>
                {
                    { "access_token", accessToken },
                };

                if (refreshToken != null)
                {
                    tokenResponse.Add("refresh_token", refreshToken);
                }
            }
            catch (Exception e)
            {
                tokenResponse = null;
            }

            return tokenResponse;
        }
    }
}
