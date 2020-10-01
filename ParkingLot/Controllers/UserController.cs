using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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

        [HttpPost]
        [Route("login")]
        public ActionResult UserLogin([FromBody] LoginModel model)
        {
            try
            {
                response = userBusinessLayer.LoginUser(model);
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

    }
}