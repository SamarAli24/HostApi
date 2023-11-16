//using HostProject.Model;
using Services.Model;
using Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace HostProjectAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticateTokenController : ControllerBase
    {
        #region feilds
        private IAuthenticateUserToken _authenticateUserToken;
        #endregion

        #region ctor
        public AuthenticateTokenController(IAuthenticateUserToken authenticateUserToken)
        {
            _authenticateUserToken = authenticateUserToken;
        }
        #endregion

        #region methods
        [Route("AuthenticationToken")]
        //[RequireHttps]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] AuthernticateUserToken loginDetalhes)
        {
            var result = await _authenticateUserToken.ValidarUser(loginDetalhes);
            //var splitResult = result.Split(':');
            if (Convert.ToInt64(result)>0)
            {
                var tokenString = _authenticateUserToken.GenerateTokenJWT(loginDetalhes.AuthenticateName, result);
                return Ok(new { token = tokenString , userId = result });
            }
            else if (result == "-1")
            {
                return BadRequest("Password Not Valid");
            }
            else if (result == "-2")
            {
                return BadRequest("User Not Valid");
            }
            else if (result == "-3")
            {
                return BadRequest("User Inactive");
            }
            else
            {
                return NotFound(result);
            }
        }
        #endregion


    }

}
