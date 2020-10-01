
using ParkingLot.CL.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingLot.BL.Interface
{
    public interface IUserBL
    {
        ResponseMessage<ShowUserInformation> RegisterUser(UserModel data);

        ResponseMessage<ShowUserInformation> LoginUser(LoginModel data);

        ResponseMessage<ShowUserInformation> UpdateUser(UpdateUserInformation data, int id);

        ResponseMessage<ShowUserInformation> ShowAllRegisteredUsers();

        ResponseMessage<ShowUserInformation> ShowUserInformationByEmail(string email);

        ResponseMessage<ShowUserInformation> DeleteUser(int id);
    }
}
