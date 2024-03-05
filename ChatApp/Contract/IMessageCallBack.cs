using ChatApp.Domain;
using System.Collections.ObjectModel;
using System.ServiceModel;

namespace ChatApp.Contract
{
    public interface IMessageServiceCallBack
    {
        [OperationContract(IsOneWay = true)]
        void ForwardToClient(Message message);
        [OperationContract(IsOneWay = true)]
        void UserConnected(ObservableCollection<User> users);
    }
}
