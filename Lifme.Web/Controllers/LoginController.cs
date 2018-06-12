using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Lifme.Domain.Entity;
using Lifme.Domain.Model;
using Lifme.Repository.Context;
using Lifme.Security;
using Lifme.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Lifme.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {

        private readonly UserService _userService;
        private readonly BadgeService _badgeService;

        public LoginController(DatabaseContext context)
        {
            _userService = new UserService(context);
            _badgeService = new BadgeService(context);
        }

        [HttpPost]
        public async Task<IActionResult> Login
           (
            [FromBody]LoginModel loginModel,
            [FromServices]LoginConfiguration loginConfiguration,
            [FromServices]TokenConfiguration tokenConfiguration
            )
        {
            loginModel.Validation();

            var user = await _userService.GetOneByEmail(loginModel.Email);


            if (user != null && CryptographyService.VerifyPassword(loginModel.Email, loginModel.Password, user.Password))
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(loginModel.Email, "Login"),
                    new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N"))
                    }
                );

                DateTime creationDate = DateTime.Now;
                DateTime expirationDate = creationDate +
                    TimeSpan.FromSeconds(tokenConfiguration.Seconds);

                //Verifica Badges
                await _badgeService.VerifyNewBadges(user);

                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = tokenConfiguration.Issuer,
                    Audience = tokenConfiguration.Audience,
                    SigningCredentials = loginConfiguration.SigningCredentials,
                    Subject = identity,
                    NotBefore = creationDate,
                    Expires = expirationDate
                });
                var token = handler.WriteToken(securityToken);

                return Ok(new TokenModel
                {
                    Token = token,
                    CreationDate = creationDate,
                    ExpirationDate = expirationDate
                });

            }
            else
            {
                return Unauthorized();
            }
        }

        }
    }