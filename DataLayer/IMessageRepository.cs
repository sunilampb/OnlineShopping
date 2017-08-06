using DataLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
   public interface IMessageRepository
    {
       int AddMessage(Message msg);
       List<Message> GetMessageForUser(int userId);
    }
}
