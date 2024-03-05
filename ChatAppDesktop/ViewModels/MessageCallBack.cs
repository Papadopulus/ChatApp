using ChatApp.Contract;
using ChatApp.Domain;
using System;
using System.Collections.ObjectModel;

namespace ChatAppDesktop.ViewModels
{
    public class MessageCallBack : IMessageServiceCallBack
    {

        MessageViewModel _viewModel;
        public MessageCallBack() { }
        public MessageCallBack(MessageViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        public void ForwardToClient(Message message)
        {
            _viewModel.AddReceivedMessage(message);
        }

        public void UserConnected(ObservableCollection<User> users)
        {
            _viewModel.UpdateUsers(users);
        }
    }
}
