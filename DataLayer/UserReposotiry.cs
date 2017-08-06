using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DTO;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataLayer
{
    public class UserReposotiry:IUserReposotiry
    {
        private string conString;
        public UserReposotiry()
        {
            conString = ConfigurationManager.ConnectionStrings["myConString"].ConnectionString;
        }
        public UserReposotiry(string connectionString)
        {
            conString = connectionString;
        }
        public int AddUser(DTO.User user)
        {
            try
            {
                using (var con = new SqlConnection(conString))
                {
                    con.Open();
                    var cmd = new SqlCommand("AddUser",con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Name",user.Name);      
                    cmd.Parameters.AddWithValue("Email",user.Email);    
                    cmd.Parameters.AddWithValue("Password",user.Password); 
                    cmd.Parameters.AddWithValue("City",user.City);
                    cmd.Parameters.AddWithValue("PostalCode", user.PostalCode);
                    cmd.Parameters.AddWithValue("State",user.State);
                    cmd.Parameters.AddWithValue("Mobile", user.Mobile);
                    int newUserId = cmd.ExecuteNonQuery();
                    return newUserId;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DTO.User GetUserByEmail(string email)
        {
            try
            {
                var user = new DTO.User();
                using (var con = new SqlConnection(conString))
                {
                    con.Open();
                    var cmd = new SqlCommand("GetUserByEmail", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Email", email);
                    var rdr =cmd.ExecuteReader();
                   
                    while (rdr.Read())
                    {
                        user.Email =(string) rdr["Email"];
                        user.Name = (string)rdr["Name"];
                        user.City = (string)rdr["City"];
                        user.PostalCode = (string)rdr["PostalCode"];
                        user.State = (string)rdr["State"];
                        user.Mobile=(string)rdr["Mobile"];
                        user.UserId = (int)rdr["UserId"];
                    }
                }
                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DTO.User GetUserById(int userId)
        {
            try
            {
                var user = new DTO.User();
                using (var con = new SqlConnection(conString))
                {
                    con.Open();
                    var cmd = new SqlCommand("GetUserById", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("UserId", userId);
                    var rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        user.Email = (string)rdr["Email"];
                        user.Name = (string)rdr["Name"];
                        user.City = (string)rdr["City"];
                        user.PostalCode = (string)rdr["PostalCode"];
                        user.State = (string)rdr["State"];
                        user.Mobile = (string)rdr["Mobile"];
                        user.UserId = (int)rdr["UserId"];
                    }
                }
                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ValidateUser(string email, string password)
        {
            try
            {
                using (var con = new SqlConnection(conString))
                {
                    con.Open();
                    var cmd = new SqlCommand("ValidateUser", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Email", email);
                    cmd.Parameters.AddWithValue("Password", password);
                    int valid = (int)cmd.ExecuteScalar();
                    return valid==1;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
