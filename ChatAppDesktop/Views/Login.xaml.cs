using ChatApp.Contract;
using ChatApp.Domain;
using ChatAppDesktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        private MainWindow _window;
        public Login()
        {
            InitializeComponent();
            _window = (MainWindow)Application.Current.MainWindow;
            if (_window != null)
            {
                _window.Width = 540;
                _window.Height = 420;
                _window.MinWidth = 540;
                _window.MinHeight = 420;
                _window.MaxHeight = 540;
                _window.MaxWidth = 420;
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (Key.Value != null)
            {
                if (!string.IsNullOrEmpty(Username.Text))
                {
                    MessageViewModel viewModel = new MessageViewModel();
                    User user = new User
                    {
                        TimeCreated = DateTime.UtcNow,
                        Name = Username.Text
                    };
                    viewModel.Key = (int)Key.Value;
                    viewModel.IsA52 = false;
                    var uri = "net.tcp://localhost:8080/MessageService";
                    var callBack = new InstanceContext(new MessageCallBack(viewModel));
                    var binding = new NetTcpBinding(SecurityMode.None);
                    var channel = new DuplexChannelFactory<IMessageService>(callBack, binding);
                    var endPoint = new EndpointAddress(uri);
                    var proxy = channel.CreateChannel(endPoint);
                    _window.MainView = new Main(_window, user, viewModel, proxy);
                    if (proxy != null)
                    {
                        proxy.Connect(user);
                    }
                    _window.Main.Children.Clear();
                    _window.Main.Children.Add(_window.MainView);
                }
            }
            if(!string.IsNullOrEmpty(A52Key.Text) && A52Key.Text.Length==8)
            {
                if (!string.IsNullOrEmpty(Username.Text))
                {
                    MessageViewModel viewModel = new MessageViewModel();
                    User user = new User
                    {
                        TimeCreated = DateTime.UtcNow,
                        Name = Username.Text
                    };
                    viewModel.A52Key = A52Key.Text;
                    viewModel.IsA52 = true;
                    var uri = "net.tcp://localhost:8080/MessageService";
                    var callBack = new InstanceContext(new MessageCallBack(viewModel));
                    var binding = new NetTcpBinding(SecurityMode.None);
                    var channel = new DuplexChannelFactory<IMessageService>(callBack, binding);
                    var endPoint = new EndpointAddress(uri);
                    var proxy = channel.CreateChannel(endPoint);
                    _window.MainView = new Main(_window, user, viewModel, proxy);
                    if (proxy != null)
                    {
                        proxy.Connect(user);
                    }
                    _window.Main.Children.Clear();
                    _window.Main.Children.Add(_window.MainView);
                }
            }
        }

        private void AlgorithmRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            DoubleTranspositionPanel.Visibility = Visibility.Collapsed;
            A52Panel.Visibility = Visibility.Collapsed;
            LoginButton.Visibility = Visibility.Collapsed;
            // Prikazi odabrani panel
            if (radioButton.Content.ToString() == "Double Transposition")
            {
                DoubleTranspositionPanel.Visibility = Visibility.Visible;
                LoginButton.Visibility = Visibility.Visible;
            }
            else if (radioButton.Content.ToString() == "A5/2")
            {
                A52Panel.Visibility = Visibility.Visible;
                LoginButton.Visibility = Visibility.Visible;
            }
        }
 
    }
}
