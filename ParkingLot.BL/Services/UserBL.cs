using ParkingLot.BL.Interface;
using ParkingLot.CL.Model;
using ParkingLot.RL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParkingLot.BL.Services
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL userRepository;

        public UserBL(IUserRL userRepo)
        {
            this.userRepository = userRepo;
        }

        

        public ResponseMessage<ShowUserInformation> LoginUser(LoginModel data)
        {
            ResponseMessage<ShowUserInformation> response = new ResponseMessage<ShowUserInformation>();
            try
            {

                ShowUserInformation loggedUserDetails = userRepository.LoginUser(data);
                if (loggedUserDetails != null)
                {
                    response.Status = true;
                    response.Message = "Login successful";
                    response.Data = loggedUserDetails;
                }
                else
                {
                    response.Status = false;
                    response.Message = "Login failed. Please enter correct username and password";
                    response.Data = loggedUserDetails;
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

        public ResponseMessage<ShowUserInformation> RegisterUser(UserModel data)
        {
            ResponseMessage<ShowUserInformation> response = new ResponseMessage<ShowUserInformation>();
            try
            {
                
                int registrationStatus = userRepository.RegisterUser(data);
                if(registrationStatus > 0)
                {
                    response.Status = true;
                    response.Message = "Registration successful";
                    response.Data = null;
                }
                else
                {
                    response.Status = false;
                    response.Message = "Registration failed. This Email Id or username already exists.";
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

        public ResponseMessage<ShowUserInformation> ShowAllRegisteredUsers()
        {
            ResponseMessage<ShowUserInformation> response = new ResponseMessage<ShowUserInformation>();
            try
            {
                List<ShowUserInformation> data = userRepository.ShowAllRegisteredUsers().ToList();
                if(data.Count == 0)
                {
                    response.Status = true;
                    response.Message = "No user has registred yet";
                    response.Data = null;   
                }
                else
                {
                    response.Status = true;
                    response.Message = "Here is list of registered users.";
                    response.Data = data;
                }
            }
            catch (Exception exception)
            {
                response.Status = false;
                response.Message = "Server error. Error : "+exception.Message;
                response.Data = null;
            }
            return response;
        }

        public ResponseMessage<ShowUserInformation> ShowUserInformationByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage<ShowUserInformation> UpdateUser(UpdateUserInformation data, int id)
        {
            ResponseMessage<ShowUserInformation> response = new ResponseMessage<ShowUserInformation>();
            try
            {
                
                int updationStatus = userRepository.UpdateUser(data, id);
                if (updationStatus == 1)
                {
                    response.Status = true;
                    response.Message = "User details updated.";
                }
                else
                {
                    response.Status = false;
                    response.Message = "Update failed";
                }

                
            }
            catch (Exception exception)
            {
                response.Status = false;
                response.Message = "Server error. Error : " + exception.Message;
            }
            return response;
        }

        public ResponseMessage<ShowUserInformation> DeleteUser(int id)
        {
            ResponseMessage<ShowUserInformation> response = new ResponseMessage<ShowUserInformation>();
            try
            {
                
                int deletionStatus = userRepository.DeleteUser(id);
                if (deletionStatus == 1)
                {
                    response.Status = true;
                    response.Message = "User successfully deleted.";
                }
                else
                {
                    response.Status = false;
                    response.Message = "No such user exists.";
                }

                
            }
            catch (Exception exception)
            {
                response.Status = false;
                response.Message = "Server error. Error : " + exception.Message;
            }
            return response;
        }

        
    }
}
