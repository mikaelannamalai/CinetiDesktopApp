using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_LoginForm.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using WPF_LoginForm.Repositories;
using System.Linq.Expressions;
using System.Windows.Controls;

namespace WPF_LoginForm.ViewModels
{
    public class ClientViewModel : ViewModelBase
    {
        private string _clientId;
        private string _name;
        private string _language;
        private string _dob;
        private string _phone;
        private string _email;
        private string _address;


        private ClientModel _selectedClient;
        private List<ClientModel> _client = new List<ClientModel>();
        private List<string> _strClient = new List<string>();
        private ObservableCollection<string> _stringCollection = new ObservableCollection<string>();
        private ICommand _addClientCommand;
        private ICommand _deleteClientCommand;
        private ICommand _editClientCommand;
        private ICommand _updateClientCommand;
        private ICommand _showClientCommand;
        private IClientRepository clientRepository;

        private ObservableCollection<ClientModel> _dbClient = new ObservableCollection<ClientModel>();
        private delegate ObservableCollection<string> UserDelegate(IReadOnlyCollection<ClientModel> dbClient);

        public SqlConnection GetConnection()
        {
            return new SqlConnection("Server=(local); Database=MVVMLoginDb; Integrated Security=true");
        }



        public ClientModel SelectedClient
        {
            get => _selectedClient; set
            {
                if (value != _selectedClient)
                {
                    _selectedClient = value;
                    OnPropertyChanged("SelectedClient");
                }
            }
        }
        public List<ClientModel> Client
        {
            get { return _client; }
            set
            {
                if (value != _client)
                {
                    _client = value;
                    OnPropertyChanged("Client");
                }
            }
        }

        public List<string> strClient
        {
            get { return _strClient; }
            set
            {
                if (value != _strClient)
                {
                    _strClient = value;
                    OnPropertyChanged("strClient");
                }
            }
        }
        public ObservableCollection<ClientModel> DbClients
        {
            get { return _dbClient; }
            set
            {
                if (value != _dbClient)
                {
                    _dbClient = value;
                    OnPropertyChanged("DbClients");
                }
            }
        }

        public ObservableCollection<string> StrCollection
        {
            get { return _stringCollection; }
            set
            {
                if (value != _stringCollection)
                {
                    _stringCollection = value;
                    OnPropertyChanged("StrCollection");
                }
            }
        }

        public ICommand AddClientCommand
        {
            get;
        }


        public ICommand DeleteClientCommand
        {
            get;
        }


        public ICommand EditClientCommand
        {
            get;


        }


        public ICommand UpdateClientCommand
        {
            get;
        }


        public ICommand ShowClientCommand
        {
            get;
        }
        public ClientViewModel()
        {


            clientRepository = new ClientRepository();
            clientRepository.GetByAll();
            AddClientCommand = new ViewModelCommand(ExecuteAddCommand);
            ShowClientCommand = new ViewModelCommand(ExecuteShowCommand);
            DeleteClientCommand = new ViewModelCommand(ExecuteDeleteCommand);
            EditClientCommand = new ViewModelCommand(ExecuteEditCommand);
            UpdateClientCommand = new ViewModelCommand(ExecuteUpdateCommand);

        }


        public string selectedKey;
        private void ExecuteEditCommand(object obj)
        {
            if (SelectedClient != null && SelectedClient.ClientId != null)
            {

                Name = SelectedClient.Name;
                Language = SelectedClient.Language;
                Dob = SelectedClient.DOB;
                Email = SelectedClient.Email;
                Address = SelectedClient.Address;
                Phone = SelectedClient.phone;
                selectedKey = SelectedClient.ClientId;
            }
        }
        private void ExecuteUpdateCommand(object obj)
        { //////migrate to repo later
            using (var connection = GetConnection())

                try
                {
                    {
                        //"UPDATE Student SET Address = @add, City = @cit Where FirstName = @fn and LastName = @add";
                        connection.Open();
                        SqlCommand updateCommand = new SqlCommand(
                          "UPDATE [client] SET Name = @name, Language = @language,DOB = @dob,email = @email,address = @address, phone = @phone where Id = @ID",
                         connection);
                        updateCommand.Parameters.Add("@name", SqlDbType.NVarChar).Value = Name;
                        updateCommand.Parameters.Add("@language", SqlDbType.NVarChar).Value = Language;
                        updateCommand.Parameters.Add("@dob", SqlDbType.NVarChar).Value = Dob;
                        updateCommand.Parameters.Add("@email", SqlDbType.NVarChar).Value = Email;
                        updateCommand.Parameters.Add("@address", SqlDbType.NVarChar).Value = Address;
                        updateCommand.Parameters.Add("@phone", SqlDbType.NVarChar).Value = Phone;
                        updateCommand.Parameters.Add("@ID", SqlDbType.NVarChar).Value = selectedKey;


                        updateCommand.ExecuteScalar();
                        Name = String.Empty;
                        Language = String.Empty;
                        Dob = String.Empty;
                        Email = String.Empty;
                        Address = String.Empty;
                        Phone = String.Empty;
                        selectedKey = String.Empty;

                        Client.Clear();
                        Client = (List<ClientModel>)clientRepository.GetByAll();
                    }
                }
                catch (Exception ex) { }
        }
    
                

           
        
        private void ExecuteDeleteCommand(object obj)
        { //////migrate to repo later
            if (SelectedClient != null && SelectedClient.ClientId != null)
            {
                using (var connection = GetConnection())
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "Delete from [client] where Id=@Id";
                    command.Parameters.Add("@Id", SqlDbType.NVarChar).Value = SelectedClient.ClientId;
                    command.ExecuteScalar();
                    Client.Clear();
                    Client = (List<ClientModel>)clientRepository.GetByAll();
                }
            }
            else
            {

            }
        }

        private void ExecuteShowCommand(object obj)
        {
        
            Client.Clear();
            Client = (List<ClientModel>)clientRepository.GetByAll();
        }

        private void ExecuteAddCommand(object obj)
        {//////migrate to repo later


            using (var connection = GetConnection())
                try
                {
                    {

                        connection.Open();
                        SqlCommand createLogin = new SqlCommand(
                          "Insert into [Client] values (NEWID(), @name, @language, @dob, @email, @address, @phone)",
                         connection);
                        createLogin.Parameters.Add("@name", SqlDbType.NVarChar).Value = Name;
                        createLogin.Parameters.Add("@language", SqlDbType.NVarChar).Value = Language;
                        createLogin.Parameters.Add("@dob", SqlDbType.NVarChar).Value = Dob;
                        createLogin.Parameters.Add("@email", SqlDbType.NVarChar).Value = Email;
                        createLogin.Parameters.Add("@address", SqlDbType.NVarChar).Value = Address;
                        createLogin.Parameters.Add("@phone", SqlDbType.NVarChar).Value = Phone;



                        createLogin.ExecuteScalar();
                        Name = String.Empty;
                        Language= String.Empty;
                        Dob= String.Empty;
                        Email = String.Empty;
                        Address = String.Empty;
                        Phone = String.Empty;
                        Client.Clear();
                        Client = (List<ClientModel>)clientRepository.GetByAll();
                    }
                } 
                catch (Exception ex)
                {

                }

        }

        public string ClientId {
            get
            {
                return _clientId;
            }

            set
            {
                _clientId = value;
                OnPropertyChanged(nameof(ClientId));
            }
        }
        public string Name {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        
        public string Language
        {
            get
            {
                return _language;
            }

            set
            {
                _language = value;
                OnPropertyChanged(nameof(Language));
            }
        }
        
        public string Dob {
            get
            {
                return _dob;
            }

            set
            {
                _dob = value;
                OnPropertyChanged(nameof(Dob));
            }
        }
        
        public string Phone
        {
            get
            {
                return _phone;
            }

            set
            {
                _phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }
        
        public string Email
        {
            get
            {
                return _email;
            }

            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        
        public string Address
        {
            get
            {
                return _address;
            }

            set
            {
                _address = value;
                OnPropertyChanged(nameof(Address));
            }
        }
       

        private ClientModel GetClient()
        {
            ClientModel client = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select * from [client]";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        client = new ClientModel()
                        {
                            ClientId = reader[0].ToString(),
                            Name = reader[1].ToString(),
                            Language= reader[2].ToString(),
                            DOB = reader[3].ToString(),
                            Email = reader[4].ToString(),
                            Address = reader[5].ToString(),
                            phone= reader[6].ToString(),
                        };
                    }
                }
            }
            return client;
        }

        private ObservableCollection<ClientModel> GetAllClients()
        {
            {
                ClientModel client = null;
                using (var connection = GetConnection())
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "select * from [client]";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            client = new ClientModel()
                            {
                                ClientId = reader[0].ToString(),
                                Name = reader[1].ToString(),
                                Language = reader[2].ToString(),
                                DOB = reader[3].ToString(),
                                Email = reader[4].ToString(),
                                Address = reader[5].ToString(),
                                phone = reader[6].ToString(),
                            };
                        }
                    }
                }
                DbClients.Add(client);
            }
            return DbClients;

        }



    }
}
/*
 *         private async void AddClient()
        {
            ClientModel newClient = new ClientModel();
            newClient.ClientId = ClientId;
            newClient.Name = Name;
            Name = string.Empty;
            newClient.DOB = DOB;
            DOB = string.Empty;
            newClient.phone = phone;
            phone = string.Empty;
            newClient.Address = Address;
            Address = string.Empty;

             
 * */