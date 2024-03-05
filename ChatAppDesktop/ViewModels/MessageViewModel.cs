using ChatApp.Domain;
using ChatAppDesktop.A52;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ChatAppDesktop.ViewModels
{
    public class MessageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<User> _users;
        private ObservableCollection<Message> _receivedMessages = new ObservableCollection<Message>();
        private ObservableCollection<Message> _sentMessages = new ObservableCollection<Message>();
        private ObservableCollection<Message> _filteredMessages = new ObservableCollection<Message>();
        private string _userId;
        private int _key;
        private string _A52Key;
        private bool _isA52;
        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsA52
        {
            get => _isA52;
            set
            {
                _isA52 = value;
            }
        }
        public string A52Key
        {
            get => _A52Key;
            set
            {
                _A52Key = value;
            }
        }
        public int Key
        {
            get => _key;
            set
            {
                _key = value;
            }
        }
        public ObservableCollection<Message> SentMessages
        {
            get => _sentMessages;
            set
            {
                _sentMessages = value;
                OnPropertyChanged(nameof(SentMessages));
            }
        }
        public ObservableCollection<User> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }
        public ObservableCollection<Message> ReceivedMessages
        {
            get => _receivedMessages;
            set
            {
                _receivedMessages = value;
                OnPropertyChanged(nameof(ReceivedMessages));
            }
        }
        public ObservableCollection<Message> FilteredMessages
        {
            get => _filteredMessages;
            set
            {
                _filteredMessages = value;

                OnPropertyChanged(nameof(FilteredMessages));
            }
        }
        public string UserId
        {
            get => _userId;
            set
            {
                _userId = value;
                filter();
                OnPropertyChanged(nameof(FilteredMessages));
            }
        }

        public void UpdateUsers(ObservableCollection<User> users)
        {
            Users = users;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void AddReceivedMessage(Message message)
        {
            string receivedMessage = message.MessageSent;
            if(IsA52 == false)
            {
                int m = (int)Math.Ceiling((double)Math.Sqrt(receivedMessage.Length));
                int n = (int)Math.Ceiling((double)receivedMessage.Length / m);

                int[] columnKey = new int[m];
                int[] rowKey = new int[n];

                ShuflleAlgorithm(columnKey, m);
                ShuflleAlgorithm(rowKey, n);

                char[,] matrixMessage = StringToMatrix(receivedMessage, n, m);
                char[,] decodedMatrix = DecodeMatrix(matrixMessage, columnKey, rowKey, n, m);

                string decodedMessage = MatrixToString(decodedMatrix, n, m);

                message.MessageSent = decodedMessage;
            }
            if(IsA52 == true)
            {
                int keyForCFB = 85;
                A52.CFB cFB= new A52.CFB(keyForCFB);
                A52.A52 a52 = new A52.A52();
                //string key = "HelloWor";
                message.MessageSent = cFB.Decription(receivedMessage, a52, A52Key);
            }
           

            Application.Current.Dispatcher.Invoke(() => ReceivedMessages.Add(message));
            filter();

        }
        public void filter()
        {
            FilteredMessages = new ObservableCollection<Message>(ReceivedMessages.Where(x => x.FromUserId == _userId));
        }
        public void AddSentMessage(Message message)
        {
            Application.Current.Dispatcher.Invoke(() => { SentMessages.Add(message); OnPropertyChanged(nameof(SentMessages)); });
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
        static string MatrixToString(char[,] matrix, int n, int m)
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
        public static char[,] DecodeMatrix(char[,] poruka, int[] columnKey, int[] rowKey, int n, int m)
        {
            char[,] kodiranaMatrica = new char[n, columnKey.Length];
            char[,] konacnaKodiranaMatrica = new char[n, columnKey.Length];


            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    kodiranaMatrica[j, columnKey[i]] = poruka[j, i];
                }
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    konacnaKodiranaMatrica[rowKey[i], j] = kodiranaMatrica[i, j];
                }
            }
            return konacnaKodiranaMatrica;
        }
        public void ShuflleAlgorithm(int[] key, int length)
        {
            for (int i = 0; i < length; i++)
            {
                key[i] = i;
            }

            int seed = Key;
            Console.WriteLine("Ovo je key iz receive-a" + seed);

            for (int i = 0; i < length; i++)
            {
                int index = (i * seed) % (length);
                int temp = key[i];
                key[i] = key[index];
                key[index] = temp;
            }
        }
    }
}
