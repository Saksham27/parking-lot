using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ParkingLot.BL.Interface;
using ParkingLot.CL.Model;

namespace ParkingLot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IConfiguration configuration;
        IUserBL userBusinessLayer;
        ResponseMessage<ShowUserInformation> response;

        public UserController(IUserBL userBL, IConfiguration config)
        {
            userBusinessLayer = userBL;
            configuration = config;
        }

        [Authorize(Roles = Role.Owner)]
        [HttpGet]
        [Route("allUsers")]
        public ActionResult<IEnumerable<string>> ShowRegisteredUsers()
        {
            try
            {
                response = userBusinessLayer.ShowAllRegisteredUsers();

                if(response.Status == true)
                {
                    return Ok(new { response.Status, response.Message, response.Data });
                }
                else
                {
                    return BadRequest(new { response.Status, response.Message });
                }
            }
            catch (Exception exception)
            {
                return BadRequest(new { error = exception.Message });
            }
        }

        [HttpPost]
        [Route("register")]
        public ActionResult UserRegistration([FromBody] UserModel model)
        {
            try
            {
                response = userBusinessLayer.RegisterUser(model);
                if (response.Status == true)
                {
                    
                    return this.Ok(new { response.Status, response.Message });
                }
                else
                {
                    return BadRequest(new { response.Status, response.Message });
                }
            }
            catch (Exception exception)
            {
                return BadRequest(new { error = exception.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public ActionResult UserLogin([FromBody] LoginModel model)
        {
            try
            {
                response = userBusinessLayer.LoginUser(model);
                if (response.Status == true)
                {
                    string token = GenerateToken(response.Data);
                    response.Data = token;
                    return this.Ok(new { response.Status, response.Message, response.Data });
                }
                else
                {
                    return BadRequest(new { response.Status, response.Message });
                }
            }
            catch (Exception exception)
            {
                return BadRequest(new { error = exception.Message });
            }
        }

        [HttpPatch]
        [Route("update/{id}")]

        public ActionResult UpdateUserDetails([FromBody] UpdateUserInformation data,[FromRoute] int id)
        {
            try
            {
                response = userBusinessLayer.UpdateUser(data,id);
                if (response.Status == true)
                {
                    return Ok(new { response.Status, response.Message });
                }
                else
                {
                    return BadRequest(new { response.Status, response.Message });
                }
            }
            catch (Exception exception)
            {
                return BadRequest(new { error = exception.Message });
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public ActionResult DeleteUser([FromRoute] int id)
        {
            try
            {
                response = userBusinessLayer.DeleteUser(id);
                if (response.Status == true)
                {
                    return Ok(new { response.Status, response.Message });
                }
                else
                {
                    return BadRequest(new { response.Status, response.Message });
                }
            }
            catch (Exception exception)
            {

                return BadRequest(new { error = exception.Message });

            }
        }

        /// <summary>
        /// Generates Token for Login
        /// </summary>
        /// <param name="responseData"></param>
        /// <returns></returns>
        private string GenerateToken(ShowUserInformation Info)
        {
            try
            {
                var symmetricSecuritykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

                var signingCreds = new SigningCredentials(symmetricSecuritykey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>
                {
                    
                    new Claim("FirstName", Info.FirstName.ToString()),
                    new Claim("LastName", Info.LastName.ToString()),
                    new Claim("Email", Info.EmailID.ToString()),
                    new Claim("Address", Info.Address.ToString()),
                    new Claim("UserName", Info.UserName.ToString()),
                    new Claim(ClaimTypes.Role, Info.UserRole.ToString()),
                    new Claim("IsHandicapped", Info.Handicapped.ToString()),

                };
                var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                    configuration["Jwt:Issuer"],
                    claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: signingCreds);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}