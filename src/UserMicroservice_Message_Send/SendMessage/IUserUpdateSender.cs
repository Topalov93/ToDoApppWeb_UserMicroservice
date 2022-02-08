using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Models.Users;

namespace UserMicroservice_Message_Send.SendMessage
{
    public interface IUserUpdateSender
    {
        void Send(User user);
    }
}
