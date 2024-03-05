using ChatApp.Contract;
using ChatApp.Domain;
using ChatAppDesktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChatAppDesktop.Views
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : UserControl
    {
        private MainWindow _window;
        private ObservableCollection<Message> _messages;
        private readonly SolidColorBrush[] userBackground = new SolidColorBrush[4];
        private User _user;
        private MessageViewModel _viewModel = new MessageViewModel();
        private IMessageService _proxy;
        public Main(MainWindow window, User user,MessageViewModel viewModel, IMessageService proxy)
        {
            InitializeComponent();
            this._window = window;
            _user = user;
            _viewModel = viewModel;
            DataContext = viewModel;
            _proxy = proxy;
            _window.Width = 540;
            _window.Height = 400;

            _window.Background = new SolidColorBrush();
            userBackground[0] = new SolidColorBrush(Color.FromArgb(233, 108, 41, 239));
            userBackground[1] = new SolidColorBrush(Color.FromArgb(233, 239, 41, 210));
            userBackground[2] = new SolidColorBrush(Color.FromArgb(233, 73, 44, 130));
            userBackground[3] = new SolidColorBrush(Color.FromArgb(233, 115, 36, 103));

            Title.Content = $"Welcome to Chat App {_user.Name}";
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void ListBox_Loaded(object sender, RoutedEventArgs e)
        {


        }
        
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (ListBox.SelectedItem != null)
            {
               
                User selectedUser = (User)ListBox.SelectedItem;
                chatLayout.CurrentInterlocutor = selectedUser;
                chatLayout.User = _user;
                chatLayout.messageService = _proxy;
                chatLayout.Visibility = Visibility.Visible;
                chatLayout.ViewModel = _viewModel;
                _viewModel.UserId = selectedUser.UserId;

                if(_viewModel.IsA52 == true)
                {
                    chatLayout.IsA52 = true;
                    chatLayout.A52Key = _viewModel.A52Key;
                }
                else
                {
                    chatLayout.IsA52 = false;

                }

                CollectionViewSource.GetDefaultView(_viewModel.FilteredMessages).Refresh();
            }

        }
    }
}
