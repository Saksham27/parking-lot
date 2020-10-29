using Microsoft.Extensions.Configuration;
using ParkingLot.CL.Model;
using ParkingLot.RL.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using ParkingLot.CL;

namespace ParkingLot.RL.Services
{
    public class UserRL : IUserRL
    {
        private readonly IConfiguration configuration;

        public UserRL(IConfiguration configure)
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



        public IEnumerable<ShowUserInformation> ShowAllRegisteredUsers()
        {
            List<ShowUserInformation> registeredUsersList = new List<ShowUserInformation>();
            try
            {
                SqlConnection connection = DatabaseConnection();

                SqlCommand command = StoredProcedureConnection("spGetUsers", connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ShowUserInformation user = new ShowUserInformation
                    {
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        EmailID = reader.GetString(3),
                        Address = reader.GetString(4),
                        UserName = reader.GetString(5),
                        UserRole = reader.GetString(6),
                        Handicapped = reader.GetBoolean(7),
                        RegistationDate = reader.GetDateTime(8).ToString()
                        
                    };
                    if(reader.IsDBNull(8) == false)
                    {
                        user.LastUpdated = reader.GetDateTime(8).ToString();
                    }
                    registeredUsersList.Add(user);
                }
                return registeredUsersList;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            

        }

        public int RegisterUser(UserModel data)
        {
            SqlConnection connection = DatabaseConnection();
            try
            {
                
                //password encrption
                string encryptedPassword = PasswordEncryptDecrypt.EncodePasswordToBase64(data.Password);
                //for store procedure and connection to database
                SqlCommand command = StoredProcedureConnection("spRegisterUser", connection);
                command.Parameters.AddWithValue("@FirstName", data.FirstName);
                command.Parameters.AddWithValue("@LastName", data.LastName);
                command.Parameters.AddWithValue("@Email", data.EmailID);
                command.Parameters.AddWithValue("@Address", data.Address);
                command.Parameters.AddWithValue("@UserName", data.UserName);
                command.Parameters.AddWithValue("@Password", encryptedPassword);
                command.Parameters.AddWithValue("@UserRole", data.UserRole);
                if(data.Handicapped == true)
                {
                    command.Parameters.AddWithValue("@Handicapped", 1);
                }
                else
                {
                    command.Parameters.AddWithValue("@Handicapped", 0);
                }
                
                command.Parameters.AddWithValue("@RegistrationDate", DateTime.Now);
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

        public ShowUserInformation LoginUser(LoginModel data)
        {
            SqlConnection connection = DatabaseConnection();
            try
            {
                SqlCommand command = StoredProcedureConnection("spLoginUser", connection);
                command.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = data.UserName;
                command.Parameters.Add("@Password", SqlDbType.VarChar, 50).Value = PasswordEncryptDecrypt.EncodePasswordToBase64(data.Password);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                
                if(reader.Read() == false)
                {
                    return null;
                }
                else
                {
                    return new ShowUserInformation
                    {
                        Id = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        EmailID = reader.GetString(3),
                        Address = reader.GetString(4),
                        UserName = reader.GetString(5),
                        UserRole = reader.GetString(7),
                        Handicapped = reader.GetBoolean(8),
                        RegistationDate = reader.GetDateTime(9).ToString()
                    };
                }
             
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public int UpdateUser(UpdateUserInformation data, int id)
        {
            SqlConnection connection = DatabaseConnection();
            try
            {
                UpdateUserInformation employee = new UpdateUserInformation();
                SqlCommand command = StoredProcedureConnection("spUpdateUserDetails", connection);
                command.Parameters.AddWithValue("@Id", id);
                if(data.FirstName != null)
                {
                    command.Parameters.AddWithValue("@FirstName", data.FirstName);
                }
                if(data.LastName != null)
                {
                    command.Parameters.AddWithValue("@LastName", data.LastName);

                }
                if(data.EmailID != null)
                {
                    command.Parameters.AddWithValue("@Email", data.EmailID);

                }
                if (data.Address != null)
                {
                    command.Parameters.AddWithValue("@Address", data.Address);

                }
                if (data.UserName != null)
                {
                    command.Parameters.AddWithValue("@UserName", data.UserName);
                }
                if(data.Handicapped != null)
                {
                    command.Parameters.AddWithValue("@Handicapped", data.Handicapped);

                }
                command.Parameters.AddWithValue("@LastUpdated", DateTime.Now);
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

        public int DeleteUser(int id)
        {
            SqlConnection connection = DatabaseConnection();
            try
            {
                SqlCommand command = StoredProcedureConnection("spDeleteUserDetail", connection);
                command.Parameters.Add("@Id", SqlDbType.Int, 50).Value = id;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                return (int)reader[0];
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public IEnumerable<ShowUserInformation> ShowUserInformationByEmail(string email)
        {
            throw new NotImplementedException();
        }

        
    }
}
