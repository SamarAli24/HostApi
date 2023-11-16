using Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public interface IAuthenticateUserToken
    {
        /// <summary>
        /// Create a User Based Token 
        /// </summary>
        /// <returns>JWT Token on Authenticate User</returns>
        public string GenerateTokenJWT(string userName, string Id);

        /// <summary>
        /// Check Valid User 
        /// </summary>
        /// <param name="loginDetalhes"></param>
        /// <returns>Return Authenticate User/Password</returns>
        Task<string> ValidarUser(AuthernticateUserToken loginDetails);

    }
}
