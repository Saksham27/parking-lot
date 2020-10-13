using Microsoft.Extensions.Configuration;
using ParkingLot.CL.Model;
using ParkingLot.RL.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ParkingLot.RL.Services
{
    public class ParkingRL : IParkingRL
    {
        private readonly IConfiguration configuration;

        public ParkingRL(IConfiguration configure)
        {
            configuration = configure;
        }

        private SqlConnection DatabaseConnection()
        {
            string connectionString = configuration.GetSection("ConnectionString").GetSection("ParkingLotDB").Value;
            return new SqlConnection(connectionString);
        }

        private SqlCommand StoredProcedureConnection(string storedProcedure, SqlConnection connection)
        {
            SqlCommand command = new SqlCommand(storedProcedure, connection);
            command.CommandType = CommandType.StoredProcedure;
            return command;
        }

        /// <summary>
        /// Method to avoid duplicate parking
        /// </summary>
        /// <param name="vehicleNumber"></param>
        /// <returns></returns>
        private bool IsVehicleAlreadyParked(string vehicleNumber)
        {
            SqlConnection connection = DatabaseConnection();

            try
            {
                SqlCommand command = StoredProcedureConnection("spCheckIfVehicleParkedOrNot", connection);
                command.Parameters.AddWithValue("@VehicleNumber", vehicleNumber);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                int value = (int)reader[0];
                if (value == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e) { 
                throw new Exception(e.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Method to Generate parking slot
        /// </summary>
        /// <param name="isDriverHandicapped"></param>
        /// <returns></returns>
        private Tuple<string,int> GenerateSlot(bool isDriverHandicapped)
        {
            SqlConnection connection = DatabaseConnection();
            try
            {
                int getSelectedParkingLot = SelectParkingLot();
                string selectedParkingLot = getSelectedParkingLot == 1 ? "A" : "B";


                SqlCommand command = StoredProcedureConnection("spGenerateSlot", connection);
                command.Parameters.AddWithValue("@Lot", getSelectedParkingLot);
                command.Parameters.AddWithValue("@isDriverHandicapped", isDriverHandicapped);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                return new Tuple<string, int>(selectedParkingLot + "-" + (int)reader[0], getSelectedParkingLot);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Method to check if driver handicapped
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private bool IsDriverHandicapped(string userName)
        {
            SqlConnection connection = DatabaseConnection();

            try
            {
                SqlCommand command = StoredProcedureConnection("spCheckIfDriverHandicapped", connection);
                command.Parameters.AddWithValue("@userName", userName);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                int result = (int)reader[0];
                if ( result == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private bool IsUserRegistered(string userName)
        {
            SqlConnection connection = DatabaseConnection();

            try
            {
                SqlCommand command = StoredProcedureConnection("spCheckIfUserRegistered", connection);
                command.Parameters.AddWithValue("@userName", userName);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                int result = (int)reader[0];
                if (result == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private bool IsTicketIdValid(int ticketId)
        {
            SqlConnection connection = DatabaseConnection();

            try
            {
                SqlCommand command = StoredProcedureConnection("spIsTicketValid", connection);
                command.Parameters.AddWithValue("@ticketId", ticketId);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                int result = (int)reader[0];
                if (result == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private bool IsVehicleStillParked(int ticketId)
        {
            SqlConnection connection = DatabaseConnection();

            try
            {
                SqlCommand command = StoredProcedureConnection("spCheckIfVehicleStillParked", connection);
                command.Parameters.AddWithValue("@ticketId", ticketId);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                int result = (int)reader[0];
                if (result == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Method to select between lot A and B
        /// </summary>
        /// <returns></returns>
        private int SelectParkingLot()
        {
            SqlConnection connection = DatabaseConnection();
            try
            {
                SqlCommand command = StoredProcedureConnection("spSelectLotToPark", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                return (int)reader[0];
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                connection.Close();
            }
        }


        private void UpdateSlotsAfterParking(int parkingId, int parkingLot)
        {
            SqlConnection connection = DatabaseConnection();
            try
            {
                SqlCommand command = StoredProcedureConnection("spUpdateSlotsAfterParking", connection);
                command.Parameters.AddWithValue("@parkingId", parkingId);
                command.Parameters.AddWithValue("@parkingLot", parkingLot);
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private double CalculateBill(DateTime entry, DateTime exit)
        {
            // charge per hour = Rs. 20
            return Math.Round((exit - entry).TotalMinutes/60 * 20,2);
        }

        

        /// <summary>
        /// Method to Park vehicle
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Tuple<string,ParkingTicket> ParkVehicle(ParkVehicleModel data)
        {
            ParkingTicket ticket = new ParkingTicket();
            if (IsVehicleAlreadyParked(data.VehicleNumber))
            {
                return new Tuple<string, ParkingTicket>("Vehicle already parked in lot",null);
            }
            if (IsUserRegistered(data.VehicleOwnerUserName) == false)
            {
                return new Tuple<string, ParkingTicket>("User is not registered. Please register first", null);
            }
            else
            {
               
                SqlConnection connection = DatabaseConnection();
                try
                {
                    Tuple<string, int> slot = GenerateSlot(IsDriverHandicapped(data.VehicleOwnerUserName));
                    string generatedSlot = slot.Item1;
                    int selectedParkingLot = slot.Item2;
                    //for store procedure and connection to database
                    SqlCommand command = StoredProcedureConnection("spParkVehicle", connection);
                    command.Parameters.AddWithValue("@VehicleOwnerUserName", data.VehicleOwnerUserName);
                    command.Parameters.AddWithValue("@VehicleNumber", data.VehicleNumber);
                    command.Parameters.AddWithValue("@VehicleBrand", data.VehicleBrand);
                    command.Parameters.AddWithValue("@VehicleColor", data.VehicleColor);
                    command.Parameters.AddWithValue("@ParkedInLot", selectedParkingLot);
                    command.Parameters.AddWithValue("@ParkingSlot", generatedSlot);
                    command.Parameters.AddWithValue("@ParkingStatus", "parked");
                    command.Parameters.AddWithValue("@EntryTime", DateTime.Now);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();


                    ticket.TicketId = reader.GetInt32(0);
                    ticket.VehicleNumber = reader.GetString(2);
                    ticket.ParkingSlot = reader.GetString(6);
                    ticket.EntryTime = reader.GetDateTime(8).ToString();

                    UpdateSlotsAfterParking(ticket.TicketId, selectedParkingLot);
                   
                    return new Tuple<string,ParkingTicket>("Vehicle successfully parked",ticket);

                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    connection.Close();

                }
            }
        }

        public Tuple<string,ParkingBill> UnparkVehicle(int ticketId)
        {
                SqlConnection connection = DatabaseConnection();

            try
            {
                if (IsTicketIdValid(ticketId) == false)
                {
                    return new Tuple<string, ParkingBill>("this ticket does not exists. Please check it again",null);
                }
                if (IsVehicleStillParked(ticketId) == false)
                {
                    return new Tuple<string, ParkingBill>("vehicle has been unparked already",null);
                }
                else
                {
                    ParkingBill bill = new ParkingBill();
                    SqlCommand command = StoredProcedureConnection("spUnparkVehicle", connection);
                    command.Parameters.AddWithValue("@ticketId", ticketId);
                    command.Parameters.AddWithValue("@exitTime", DateTime.Now);
;                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();

                    bill.VehicleNumber = reader.GetString(2);
                    bill.EntryTime = reader.GetDateTime(8).ToString();
                    bill.ExitTime = reader.GetDateTime(9).ToString();
                    bill.ParkingCost = "Rs. "+CalculateBill(reader.GetDateTime(8), reader.GetDateTime(9));

                    return new Tuple<string, ParkingBill>("vehicle successfully unparked", bill);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                connection.Close();

            }
        }

        public List<LocateVehicleModel> FindVehiclesByColor(string color)
        {
            SqlConnection connection = DatabaseConnection();
            try
            {
                List<LocateVehicleModel> vehicleList = new List<LocateVehicleModel>();
                SqlCommand command = StoredProcedureConnection("spFindVehiclesByColor", connection);
                command.Parameters.AddWithValue("@color", color);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    LocateVehicleModel vehicleDetails = new LocateVehicleModel
                    {
                        VehicleNumber = reader.GetString(0),
                        VehicleBrand = reader.GetString(1),
                        VehicleColor = reader.GetString(2),
                        ParkingSlot = reader.GetString(3),
                        ParkingStatus = reader.GetString(4),
                       
                        FirstName = reader.GetString(7),
                        LastName = reader.GetString(8),
                        UserName = reader.GetString(9),
                        EmailID = reader.GetString(10),
                        Address = reader.GetString(11),
                        UserRole = reader.GetString(12),
                        Handicapped = reader.GetBoolean(13),
                        
                    };
                    if (reader.IsDBNull(5) == false)
                    {
                        vehicleDetails.EntryTime = reader.GetDateTime(5).ToString();
                    }
                    if (reader.IsDBNull(6) == false)
                    {
                        vehicleDetails.ExitTime = reader.GetDateTime(6).ToString();
                    }
                    if (reader.IsDBNull(14) == false)
                    {
                        vehicleDetails.RegistationDate = reader.GetDateTime(14).ToString();
                    }

                    vehicleList.Add(vehicleDetails);
                }
                return vehicleList;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                connection.Close();

            }
        }

        public List<LocateVehicleModel> FindVehiclesByBrand(string brand)
        {
            SqlConnection connection = DatabaseConnection();
            try
            {
                List<LocateVehicleModel> vehicleList = new List<LocateVehicleModel>();
                SqlCommand command = StoredProcedureConnection("spFindVehiclesByBrand", connection);
                command.Parameters.AddWithValue("@brand", brand);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    LocateVehicleModel vehicleDetails = new LocateVehicleModel
                    {
                        VehicleNumber = reader.GetString(0),
                        VehicleBrand = reader.GetString(1),
                        VehicleColor = reader.GetString(2),
                        ParkingSlot = reader.GetString(3),
                        ParkingStatus = reader.GetString(4),

                        FirstName = reader.GetString(7),
                        LastName = reader.GetString(8),
                        UserName = reader.GetString(9),
                        EmailID = reader.GetString(10),
                        Address = reader.GetString(11),
                        UserRole = reader.GetString(12),
                        Handicapped = reader.GetBoolean(13),

                    };
                    if (reader.IsDBNull(5) == false)
                    {
                        vehicleDetails.EntryTime = reader.GetDateTime(5).ToString();
                    }
                    if (reader.IsDBNull(6) == false)
                    {
                        vehicleDetails.ExitTime = reader.GetDateTime(6).ToString();
                    }
                    if (reader.IsDBNull(14) == false)
                    {
                        vehicleDetails.RegistationDate = reader.GetDateTime(14).ToString();
                    }

                    vehicleList.Add(vehicleDetails);
                }
                return vehicleList;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                connection.Close();

            }
        }

        public List<LocateVehicleModel> FindVehiclesByNumberPlate(string vehicleNumber)
        {
            SqlConnection connection = DatabaseConnection();
            try
            {
                List<LocateVehicleModel> vehicleList = new List<LocateVehicleModel>();
                SqlCommand command = StoredProcedureConnection("spFindVehiclesByNumberPlate", connection);
                command.Parameters.AddWithValue("@vehicleNumber", vehicleNumber);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    LocateVehicleModel vehicleDetails = new LocateVehicleModel
                    {
                        VehicleNumber = reader.GetString(0),
                        VehicleBrand = reader.GetString(1),
                        VehicleColor = reader.GetString(2),
                        ParkingSlot = reader.GetString(3),
                        ParkingStatus = reader.GetString(4),

                        FirstName = reader.GetString(7),
                        LastName = reader.GetString(8),
                        UserName = reader.GetString(9),
                        EmailID = reader.GetString(10),
                        Address = reader.GetString(11),
                        UserRole = reader.GetString(12),
                        Handicapped = reader.GetBoolean(13),

                    };
                    if (reader.IsDBNull(5) == false)
                    {
                        vehicleDetails.EntryTime = reader.GetDateTime(5).ToString();
                    }
                    if (reader.IsDBNull(6) == false)
                    {
                        vehicleDetails.ExitTime = reader.GetDateTime(6).ToString();
                    }
                    if (reader.IsDBNull(14) == false)
                    {
                        vehicleDetails.RegistationDate = reader.GetDateTime(14).ToString();
                    }

                    vehicleList.Add(vehicleDetails);
                }
                return vehicleList;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                connection.Close();

            }
        }
    }
}
