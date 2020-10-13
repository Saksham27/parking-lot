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

        public ResponseMessage<List<LocateVehicleModel>> FindVehiclesByColor(string color)
        {
            ResponseMessage<List<LocateVehicleModel>> response = new ResponseMessage<List<LocateVehicleModel>>();
            try
            {
                List<LocateVehicleModel> locateVehicleResponse = parkingRepository.FindVehiclesByColor(color);
                if (locateVehicleResponse.Count != 0)
                {
                    response.Status = true;
                    response.Message = "Here are the details of vehicles whose color is "+color ;
                    response.Data = locateVehicleResponse;
                }
                else
                {
                    response.Status = false;
                    response.Message = "there are no vehicles of color "+color+" found in parking lot" ;
                    response.Data = null;
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

        public ResponseMessage<List<LocateVehicleModel>> FindVehiclesByBrand(string brand)
        {
            ResponseMessage<List<LocateVehicleModel>> response = new ResponseMessage<List<LocateVehicleModel>>();
            try
            {
                List<LocateVehicleModel> locateVehicleResponse = parkingRepository.FindVehiclesByBrand(brand);
                if (locateVehicleResponse.Count != 0)
                {
                    response.Status = true;
                    response.Message = "Here are the details of vehicles whose brand is " + brand;
                    response.Data = locateVehicleResponse;
                }
                else
                {
                    response.Status = false;
                    response.Message = "there are no vehicles of brand " + brand + " found in parking lot";
                    response.Data = null;
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

        public ResponseMessage<List<LocateVehicleModel>> FindVehiclesByNumberPlate(string vehicleNumber)
        {
            ResponseMessage<List<LocateVehicleModel>> response = new ResponseMessage<List<LocateVehicleModel>>();
            try
            {
                List<LocateVehicleModel> locateVehicleResponse = parkingRepository.FindVehiclesByNumberPlate(vehicleNumber);
                if (locateVehicleResponse.Count != 0)
                {
                    response.Status = true;
                    response.Message = "Here are the details of vehicle whose number is " + vehicleNumber;
                    response.Data = locateVehicleResponse;
                }
                else
                {
                    response.Status = false;
                    response.Message = "there is no vehicle of number " + vehicleNumber + " found in parking lot";
                    response.Data = null;
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
