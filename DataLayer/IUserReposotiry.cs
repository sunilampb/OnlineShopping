using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DTO;

namespace DataLayer
{
    public interface IUserReposotiry
    {
        int AddUser(User user);
        User GetUserByEmail(string email);
        User GetUserById(int userId);
        bool ValidateUser(string email, string password);
    }
}
