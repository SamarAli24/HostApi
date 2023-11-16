using AutoMapper;
using Services.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Data.Models;

namespace Services.Services.Authenticate
{
    public class AuthenticateUserToken : IAuthenticateUserToken
    {
        #region Fields
        private readonly hosteduContext _aplicationdbContext;
        private readonly IMapper _mapper;
        private IConfiguration _config;
        #endregion
        #region ctor
        public AuthenticateUserToken
            (
            IMapper mapper,
            IConfiguration Configuration,
            hosteduContext aplicationdbContext
            )
        {
            _mapper = mapper;
            this._aplicationdbContext = aplicationdbContext;
            this._config = Configuration;
        }
        #endregion

        #region Methods

        public string GenerateTokenJWT(string userName, string Id)
        {
            var authClaims = new List<Claim>
                {
                    new Claim("UserId",Id),
                    new Claim("UserName", userName),
                    new Claim(JwtRegisteredClaimNames.UniqueName, userName)
                };
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];
            var expiry = DateTime.Now.AddMinutes(1440);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(issuer: issuer, audience: audience,
                expires: expiry, claims:authClaims, signingCredentials: credentials);
            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }
        public async Task<string> ValidarUser(AuthernticateUserToken loginDetails)
        {
            var user = new User();
            var getAuthenticate = await Task.Run(() => _aplicationdbContext.User.Where(x => x.Email == loginDetails.AuthenticateName).SingleOrDefault());
            if (getAuthenticate != null && getAuthenticate.UserType == 4)
            {
                return "-3";//"User InActive";
            }
            user = _mapper.Map<User>(getAuthenticate);
            if (user != null)
            {
                HashingHelper hashHelper = HashingHelper.GetInstance();

                string pwdHash = hashHelper.ComputeHash(loginDetails.AuthenticatePassword, user.PassSalt);
                if (pwdHash != user.PassHash)
                    return "-1";//"Password Not Valid";
                else
                    return $"{getAuthenticate.Id.ToString()}";
            }

            return "-2";//"User Not Valid";
        }

        #endregion
    }
}
