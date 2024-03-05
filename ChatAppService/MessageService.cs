using ChatApp.Contract;
using ChatApp.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;

namespace ChatAppService
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    public class MessageService : IMessageService
    {
        private IMessageServiceCallBack _callBack = null;
        private ObservableCollection<User> _users;
        private readonly Dictionary<string, IMessageServiceCallBack> _clients;

        public MessageService()
        {
            _users = new ObservableCollection<User>();
            _clients = new Dictionary<string, IMessageServiceCallBack>();
        }

        public void Connect(User user)
        {
            _callBack = OperationContext.Current.GetCallbackChannel<IMessageServiceCallBack>();
            if (_callBack != null)
            {
                _clients.Add(user.UserId, _callBack);
                _users.Add(user);
                _clients.ToList().ForEach(c => c.Value.UserConnected(new ObservableCollection<User>(_users.Where(x => x.UserId != c.Key))));
                Console.WriteLine($"{user.Name} just connected.");
            }
        }

        public ObservableCollection<User> GetConnectedUsers()
        {
            return _users;
        }

        public void SendMessage(Message message)
        {
            var sendto = _clients?.FirstOrDefault(c => c.Key == message.ToUserId).Value;
            if (sendto != null)
            {
                sendto.ForwardToClient(message);
            }
        }

    }
}
