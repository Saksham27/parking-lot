using ParkingLot.CL;
using ParkingLot.CL.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingLot.RL.Interface
{
    public interface IUserRL
    {
        int RegisterUser(UserModel data);

        int LoginUser(LoginModel data);

        int UpdateUser(UpdateUserInformation data, int id);

        int DeleteUser(int id);

        IEnumerable<ShowUserInformation> ShowAllRegisteredUsers();

        IEnumerable<ShowUserInformation> ShowUserInformationByEmail(string email);
    }
}
