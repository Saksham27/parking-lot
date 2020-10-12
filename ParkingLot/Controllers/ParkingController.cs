﻿using System;
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
    }
}