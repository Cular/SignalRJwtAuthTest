using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SignalRAuthTest.Controllers
{
    [Route("token")]
    [AllowAnonymous]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly TokenConfiguration tokenConfiguration;

        public TokenController(TokenConfiguration tokenConfiguration)
        {
            this.tokenConfiguration = tokenConfiguration;
        }

        /// <summary>
        /// Generates token by users form data.
        /// </summary>
        /// <returns>Token</returns>
        [HttpPost]
        public IActionResult Post()
        {
            if (!this.HttpContext.Request.Form.TryGetValue("grant_type", out var grantType))
            {
                return this.BadRequest("Request does not contains grant_type parameter.");
            }

            if (grantType == "password" && this.HttpContext.Request.Form.TryGetValue("username", out var username))
            {
                var reponse = JwtAuthorization.GenerateToken(username, tokenConfiguration);

                return this.Ok(reponse);
            }

            return this.BadRequest("Wrong grant_type parameter or user parameters for recognizing.");
        }
    }
}