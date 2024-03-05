using ChatApp.Contract;
using ChatApp.Domain;
using ChatAppDesktop.ViewModels;
using ChatAppDesktop.A52;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    /// Interaction logic for ChatLayout.xaml
    /// </summary>
    public partial class ChatLayout : UserControl
    {
        private User _currentInterlocutor;
        private User _user;
        private IMessageService _messageService;
        private MessageViewModel _viewModel = new MessageViewModel();
        private string _codedMessage;
        private bool _isA52;
        private string _a52Key;

        public string A52Key
        {
            get { return _a52Key; }
            set
            {
                _a52Key = value;
            }
        }
        public bool IsA52
        {
            get { return _isA52; }
            set
            {
                _isA52 = value;
            }
        }
        public MessageViewModel ViewModel
        {
            get { return _viewModel; }
            set
            {
                _viewModel = value;
            }
        }
        public string CodedMessage
        {
            get { return _codedMessage; }
            set
            {
                _codedMessage = value;
            }
        }
        public IMessageService messageService
        {
            get { return _messageService; }
            set
            {
                _messageService = value;
            }
        }
        public User CurrentInterlocutor
        {
            get { return _currentInterlocutor; }
            set
            {
                _currentInterlocutor = value;
                Title.Content = $"Chat with {_currentInterlocutor?.Name}";
            }
        }
        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
            }
        }
        
        public ChatLayout()
        {
            InitializeComponent();
            _viewModel = (MessageViewModel)DataContext;
        }

        public void HandleReceivedMessage(Message message)
        {
            // Ovde implementirajte logiku za rukovanje primljenim porukama
            // Dodajte poruku u kolekciju poruka u ContentScroller-u
            Application.Current.Dispatcher.Invoke(() =>
            {
              
                _viewModel.ReceivedMessages.Add(message);
            });
        }

        public Label MessageTitle
        {
            get { return Title; }
            set { 
                Title = value;
               
            }

        }
        
        public Button SendMessageButton
        {
            get { return SendButton; }
            set { SendButton = value; }
        } 
        public ScrollViewer ContentScrollViewer
        {
            get { return ContentScroller; }
            set { ContentScroller = value; }
        }

        public TextBox MessageContainer
        {
            get { return MessageContent; }
            set { MessageContent = value; }
        }

        public ItemsControl MessageDisplay
        {
            get { return MessageTemplate; }
            set { MessageTemplate = value; }
        }
        private void ShowCodedMessageCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (MessageContent.Text != "")
            {
                if(IsA52 == false)
                {
                    if (MessageContent.Text != "" || CodedMessage == null)
                    {
                        string message = MessageContent.Text;
                        CodedMessage = CodeMessage(message);
                    }
                }
                if(IsA52 == true)
                {
                    if (MessageContent.Text != "" || CodedMessage == null)
                    {
                        string message = MessageContent.Text;
                        A52.A52 a52 = new A52.A52();

                        //string key = "HelloWor";
                        int keyForCFB = 85;

                        A52.CFB cFB = new A52.CFB(keyForCFB);
                        CodedMessage = cFB.Encryption(message, a52, A52Key);
                    }
                }

                CodedMessageTextBlock.Text = CodedMessage;
                CodedMessageTextBlock.Visibility = Visibility.Visible;
            }
        }
        private void ShowCodedMessageCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CodedMessageTextBlock.Visibility = Visibility.Collapsed;
        }
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            
            string message = MessageContent.Text;
            if (IsA52 == false)
            {
                CodedMessage = CodeMessage(message);
                //int m = (int)Math.Ceiling((double)Math.Sqrt(message.Length));
                //int n = (int)Math.Ceiling((double)message.Length / m);

                //int[] columnKey = new int[m];
                //int[] rowKey = new int[n];

                //ShuflleAlgorithm(columnKey, m);
                //ShuflleAlgorithm(rowKey, n);

                //char[,] matrixMessage = StringToMatrix(message, n, m);

                //char[,] codedMatrix = CodeMatrix(matrixMessage, columnKey, rowKey, n, m);
                //string codedMessage=MatrixToString(codedMatrix, n, m);
            }
            if(IsA52 == true)
            {
                A52.A52 a52 = new A52.A52();

                //string key = "HelloWor";
                int keyForCFB = 85;

                A52.CFB cFB = new A52.CFB(keyForCFB);
                CodedMessage = cFB.Encryption(message,a52, A52Key);
            }

            var sentMessage = new Message
            {
                FromUserId = User.UserId,
                FromUser = User.Name,
                ToUserId = CurrentInterlocutor.UserId,
                ToUser = CurrentInterlocutor.Name,
                MessageSent = CodedMessage,
                TimeSent = DateTime.Now
            };
            _viewModel.AddSentMessage(sentMessage);
            _messageService.SendMessage(sentMessage);
        }
        public string CodeMessage(string message)
        {
            int m = (int)Math.Ceiling((double)Math.Sqrt(message.Length));
            int n = (int)Math.Ceiling((double)message.Length / m);

            int[] columnKey = new int[m];
            int[] rowKey = new int[n];

            ShuflleAlgorithm(columnKey, m);
            ShuflleAlgorithm(rowKey, n);

            char[,] matrixMessage = StringToMatrix(message, n, m);

            char[,] codedMatrix = CodeMatrix(matrixMessage, columnKey, rowKey, n, m);
            string codedMessage = MatrixToString(codedMatrix, n, m);
            return codedMessage;
        }
        public static char[,] CodeMatrix(char[,] poruka, int[] columnKey, int[] rowKey, int n, int m)
        {
            char[,] kodiranaMatrica = new char[n, columnKey.Length];
            char[,] konacnaKodiranaMatrica = new char[n, columnKey.Length];


            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    kodiranaMatrica[j, i] = poruka[j, columnKey[i]];
                }
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    konacnaKodiranaMatrica[i, j] = kodiranaMatrica[rowKey[i], j];
                }
            }
            return konacnaKodiranaMatrica;
        }
        public char[,] StringToMatrix(string message, int n, int m)
        {
            char[,] matrixMessage = new char[n, m];

            if (message.Length % m != 0)
            {
                for (int i = message.Length; i < n * m; i++)
                {
                    message += " ";
                }
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    matrixMessage[i, j] = message[i * m + j];
                }
            }

            return matrixMessage;
        }
        public string MatrixToString(char[,] matrix,int n,int m)
        {


            char[] characters = new char[n * m];

            
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    characters[i * m + j] = matrix[i, j];
                }
            }

            
            return new string(characters);
        }
        public void ShuflleAlgorithm(int[] key, int length)
        {
            for (int i = 0; i < length; i++)
            {
                key[i] = i;
            }

            int seed = _viewModel.Key;
            Console.WriteLine("Ovo je key iz send-a"+seed);
            for (int i = 0; i < length ; i++)
            { 
                int index = (i * seed) % (length);
                int temp = key[i];
                key[i] = key[index];
                key[index] = temp;
            }
        }
        
    }
}

