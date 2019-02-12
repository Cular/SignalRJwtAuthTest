using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SignalRAuthTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChatRoomController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetRoomId()
        {
            var array = new[] { new { Id = "3a50ca2c-ca2a-47e5-8de3-05392cc50d81" }, new { Id = "27176365-ad37-4790-a015-cb10572c04c4" } };
            return this.Ok(array);
        }
    }
}