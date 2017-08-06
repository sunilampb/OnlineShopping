using DataLayer.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class MessageRepository:IMessageRepository
    {
        private string conString;
        public MessageRepository()
        {
            conString = ConfigurationManager.ConnectionStrings["myConString"].ConnectionString;
        }
         public MessageRepository(string connectionString)
        {
            conString = connectionString;
        }
        public int AddMessage(DTO.Message msg)
        {
            try
            {
                using (var con = new SqlConnection(conString))
                {
                    con.Open();
                    var cmd = new SqlCommand("AddMessage", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("MessageText", msg.MessageText);
                    cmd.Parameters.AddWithValue("UserId", msg.FromUserId);
                    cmd.Parameters.AddWithValue("AdId", msg.AdId);
                    decimal newMsgId = (decimal)cmd.ExecuteScalar();
                    return Convert.ToInt32(newMsgId);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<DTO.Message> GetMessageForUser(int userId)
        {
            try
            {
                var list = new List<DTO.Message>();
                using (var con = new SqlConnection(conString))
                {
                    con.Open();
                    var cmd = new SqlCommand("GetMessageForUser", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("UserId", userId);
                    var rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        var msg = new Message();
                        msg.MessageId = (int)rdr["MessageId"];
                        msg.MessageText = (string)rdr["MessageText"];
                        msg.MessageDate = (DateTime)rdr["MessageDate"];
                        msg.FromUserId = (int)rdr["FromUserId"];
                        msg.ToUserId = (int)rdr["ToUserId"];
                        msg.To= (string)rdr["To"];
                        msg.From = (string)rdr["From"];
                        msg.AdTitle = (string)rdr["AdTitle"];
                        msg.AdId = (int)rdr["AdId"];
                        list.Add(msg);
                    }
                }
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
