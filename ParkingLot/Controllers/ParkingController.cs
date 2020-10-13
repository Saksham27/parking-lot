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
    public class ParkingController : ControllerBase
    {
        private IConfiguration configuration;
        IParkingBL parkingBusinessLayer;
        

        public ParkingController(IParkingBL parkingBL, IConfiguration config)
        {
            parkingBusinessLayer = parkingBL;
            configuration = config;
        }

        [HttpPost]
        [Route("ParkVehicle")]
        public ActionResult ParkVehicle([FromBody] ParkVehicleModel vehicleDetails)
        {
            ResponseMessage<ParkingTicket> response;
            try
            {
                response = parkingBusinessLayer.ParkVehicle(vehicleDetails);
                if(response.Status == true)
                {
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

        [HttpPost]
        [Route("UnparkVehicle")]
        public ActionResult UnparkVehicle([FromForm] int ticketId)
        {
            ResponseMessage<ParkingBill> response;
            try
            {
                response = parkingBusinessLayer.UnparkVehicle(ticketId);
                if (response.Status == true)
                {
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

        [HttpGet]
        [Route("FindVehiclesByColor")]
        public ActionResult<IEnumerable<string>> FindVehiclesByColor(string color)
        {
            ResponseMessage<List<LocateVehicleModel>> response;

            try
            {

                response = parkingBusinessLayer.FindVehiclesByColor(color);

                if (response.Status == true)
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

        [HttpGet]
        [Route("FindVehiclesByBrand")]
        public ActionResult<IEnumerable<string>> FindVehiclesByBrand(string brand)
        {
            ResponseMessage<List<LocateVehicleModel>> response;

            try
            {

                response = parkingBusinessLayer.FindVehiclesByBrand(brand);

                if (response.Status == true)
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

        [HttpGet]
        [Route("FindVehiclesByNumberPlate")]
        public ActionResult<IEnumerable<string>> FindVehiclesByNumberPlate(string vehicleNumber)
        {
            ResponseMessage<List<LocateVehicleModel>> response;

            try
            {

                response = parkingBusinessLayer.FindVehiclesByNumberPlate(vehicleNumber);

                if (response.Status == true)
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
    }

    
}
