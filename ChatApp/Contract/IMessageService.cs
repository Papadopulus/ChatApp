using ChatApp.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Contract
{
    [ServiceContract(CallbackContract =typeof(IMessageServiceCallBack),SessionMode =SessionMode.Required)]
    public interface IMessageService
    {
        [OperationContract(IsOneWay = true)]
        void Connect(User user);
        [OperationContract(IsOneWay = true)]
        void SendMessage(Message message);
        [OperationContract(IsOneWay = false)]
        ObservableCollection<User> GetConnectedUsers();
    }
}
