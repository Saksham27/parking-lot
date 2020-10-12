using ParkingLot.BL.Interface;
using ParkingLot.CL.Model;
using ParkingLot.RL.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingLot.BL.Services
{
    public class ParkingBL : IParkingBL
    {
        private readonly IParkingRL parkingRepository;

        public ParkingBL(IParkingRL parkingRepo)
        {
            this.parkingRepository = parkingRepo;
        }


        public ResponseMessage<ParkingTicket> ParkVehicle(ParkVehicleModel vehicleDetails)
        {
            ResponseMessage<ParkingTicket> response = new ResponseMessage<ParkingTicket>();
            try
            {
                Tuple<string,ParkingTicket> parkVehicleResponse = parkingRepository.ParkVehicle(vehicleDetails);
                if(parkVehicleResponse.Item2 == null)
                {
                    response.Status = true;
                    response.Message = parkVehicleResponse.Item1;
                    response.Data = null;
                }
                else
                {
                    response.Status = true;
                    response.Message = parkVehicleResponse.Item1;
                    response.Data = parkVehicleResponse.Item2;
                }
            }
            catch (Exception exception)
            {
                response.Status = false;
                response.Message = "Server error. Error : " + exception.Message;
                response.Data = null;
            }
            return response;
        }

        public ResponseMessage<ParkingBill> UnparkVehicle(int ticketId)
        {
            ResponseMessage<ParkingBill> response = new ResponseMessage<ParkingBill>();
            try
            {
                Tuple<string, ParkingBill> parkVehicleResponse = parkingRepository.UnparkVehicle(ticketId);
                if (parkVehicleResponse.Item2 == null)
                {
                    response.Status = true;
                    response.Message = parkVehicleResponse.Item1;
                    response.Data = null;
                }
                else
                {
                    response.Status = true;
                    response.Message = parkVehicleResponse.Item1;
                    response.Data = parkVehicleResponse.Item2;
                }
            }
            catch (Exception exception)
            {
                response.Status = false;
                response.Message = "Server error. Error : " + exception.Message;
                response.Data = null;
            }
            return response;
        }
    }
}
