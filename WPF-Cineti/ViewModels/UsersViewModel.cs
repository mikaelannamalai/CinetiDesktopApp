using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using WPF_LoginForm.Models;
using System.Windows.Input;
using System.Xml.Linq;
using WPF_LoginForm.Repositories;
using WPF_LoginForm.ViewModels;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Data.Common;
using System.Net;
using MySqlX.XDevAPI;
using System.Security.Principal;
using System.Threading;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using Org.BouncyCastle.Utilities.Collections;

namespace WPF_LoginForm.ViewModels
{
    public class UsersViewModel : ViewModelBase
    {
        private string _id;
        private string _username;
        private string _password;
        private string _confirmPassword;
        private string _name;
        private string _lastName;
        private string _email;
        public string _connectionString;
        private UserModel _selectedUser;
        private List<UserModel> _users = new List<UserModel>();
        private List<string> _strUsers = new List<string>();
        private IUserRepository userRepository;




        public SqlConnection GetConnection()
        {
            return new SqlConnection("Server=(local); Database=MVVMLoginDb; Integrated Security=true");
        }

                //Properties

        public string Id
        { get { return _id; } }

        private ObservableCollection<UserModel> _dbUsers = new ObservableCollection<UserModel>();
        private delegate ObservableCollection<string> UserDelegate(IReadOnlyCollection<UserModel> dbUsers);
        public ObservableCollection<UserModel> DbUsers
        {
            get { return _dbUsers; }
            set
            {
                if (value != _dbUsers)
                {
                    _dbUsers = (ObservableCollection<UserModel>)userRepository.GetByAll();

                    OnPropertyChanged("DbUsers");
                }
            }
        }
        public UserModel SelectedUser
        {
            get => _selectedUser; set
            {
                if (value != _selectedUser)
                {
                    _selectedUser = value;
                    OnPropertyChanged("SelectedUsers");
                }
            }
        }
        public List<UserModel> Users
        {
            get { return _users; }
            set
            {
                if (value != _users)
                {
                    _users = value;
                    OnPropertyChanged("Users");
                }
            }
        }

        public string Username
        {
            get
            {
                return _username;
            }

            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }

            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }

        }
        public string ConfirmPassword
        {
            get
            {
                return _confirmPassword;
            }
            set
            {
                _confirmPassword = value;
            OnPropertyChanged(nameof(ConfirmPassword));
            }
        }


        public string Name
        {
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


        public string LastName
        {
            get
            {
                return _lastName;
            }

            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
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

        public ICommand AddUserCommand
        { get; }
        public ICommand ShowUserCommand
        { get; }
        public ICommand DeleteUserCommand
        { get; }
        
        public ICommand EditUserCommand { get; }
        public ICommand UpdateUserCommand { get; }
        
        public UsersViewModel()
        {

            userRepository = new UserRepository();
            userRepository.GetALL();
            AddUserCommand = new ViewModelCommand(ExecuteAddCommand);
            ShowUserCommand = new ViewModelCommand(ExecuteShowCommand);
            DeleteUserCommand = new ViewModelCommand(ExecuteDeleteCommand);
            EditUserCommand = new ViewModelCommand(ExecuteEditCommand);
            UpdateUserCommand = new ViewModelCommand(ExecuteUpdateCommand);

        }
        public string selectedKey;

        UserModel editedUser;
        private void ExecuteEditCommand(object obj)
        {
            if (SelectedUser != null && SelectedUser.Id != null)
            {

                Username = SelectedUser.Username;
                Password = SelectedUser.Password;
                Name = SelectedUser.Name;
                LastName = SelectedUser.LastName;
                Email = SelectedUser.Email;
                selectedKey = SelectedUser.Id;
            }


        }
        private void ExecuteUpdateCommand(object obj)
        {//////migrate to repo later
            try
            {
                using (var connection = GetConnection())


                {
                    //"UPDATE Student SET Address = @add, City = @cit Where FirstName = @fn and LastName = @add";
                    connection.Open();
                    SqlCommand updateCommand = new SqlCommand(
                      "UPDATE [User] SET Username = @username, Password = @password, Name =  @Name, LastName = @LastName, Email = @Email where Id = @ID",
                     connection);
                    updateCommand.Parameters.Add("@username", SqlDbType.NVarChar).Value = Username;
                    updateCommand.Parameters.Add("@password", SqlDbType.NVarChar).Value = Password;
                    updateCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = Name;
                    updateCommand.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = LastName;
                    updateCommand.Parameters.Add("@Email", SqlDbType.NVarChar).Value = Email;
                    updateCommand.Parameters.Add("@ID", SqlDbType.NVarChar).Value = selectedKey;


                    updateCommand.ExecuteScalar();
                    Username = String.Empty;
                    Password = String.Empty;
                    ConfirmPassword = String.Empty;
                    Name = String.Empty;
                    LastName = String.Empty;
                    Email = String.Empty;
                    
                }
            }
            catch (Exception ex) { }
               

        }

        

        private void ExecuteDeleteCommand(object obj)
        {//////migrate to repo later

            if (SelectedUser != null && SelectedUser.Id != null)
            {
                using (var connection = GetConnection())
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "Delete from [User] where Id=@Id";
                    command.Parameters.Add("@Id", SqlDbType.NVarChar).Value = SelectedUser.Id;
                    command.ExecuteScalar();
                }
            }
            else
            {
                
            }  
        }

        private void ExecuteShowCommand(object obj)
        {
            Users.Clear();
            Users = userRepository.GetALL();
        }

      
      
        private void ExecuteAddCommand(object obj)
        {//////migrate to repo later


            using (var connection = GetConnection())
                try
                {
                    {

                        connection.Open();
                        SqlCommand createLogin = new SqlCommand(
                          "Insert into [User] values (NEWID(), @username, @password, @Name, @LastName, @Email)",
                         connection);
                        createLogin.Parameters.Add("@username", SqlDbType.NVarChar).Value = Username;
                        createLogin.Parameters.Add("@password", SqlDbType.NVarChar).Value = Password;
                        createLogin.Parameters.Add("@Name", SqlDbType.NVarChar).Value = Name;
                        createLogin.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = LastName;
                        createLogin.Parameters.Add("@Email", SqlDbType.NVarChar).Value = Email;


                        createLogin.ExecuteScalar();
                        Username = String.Empty;
                        Password = String.Empty;
                        Name = String.Empty;
                        LastName = String.Empty;
                        Email = String.Empty;
                       }
                }
                catch (Exception ex) { 
                
                        }
           

        }
    }
}


/*  
 *  using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select *from [User]";
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Users.Add(new UserModel()
                        {
                            Id = reader[0].ToString(),
                            Username = reader[1].ToString(),
                            Password = string.Empty,
                            Name = reader[3].ToString(),
                            LastName = reader[4].ToString(),
                            Email = reader[5].ToString(),
                        });

                    }

                }
                         
            }
 *  
 *  
 *  
                if (SelectedUser != null && SelectedUser.Id!= null)
                {
                    _deleteUserCommand = new ViewModelCommand(
                        param => userRepository.Remove(Id));
                                        
                }
                return _deleteUserCommand;

 *  
 *  
 *  
 *  

using (var connection = GetConnection())
using (var command = new SqlCommand())
{
    connection.Open();
    command.Connection = connection;
    command.CommandText = "select *from [User]";
    using (var reader = command.ExecuteReader())
    {
        if (reader.Read())
        {
            Users.Add(new UserModel()
            {
                Id = reader[0].ToString(),
                Username = reader[1].ToString(),
                Password = string.Empty,
                Name = reader[3].ToString(),
                LastName = reader[4].ToString(),
                Email = reader[5].ToString(),
            });

        }

    }
 *  
 *  
 *  
 *  
 *  
 *  
 *  
 *  
 *  
 *  
 *  
 *  
 *  
 *  
 *  
 *  using (var connection = GetConnection())
    using (var command = new SqlCommand())
    {
        connection.Open();
        command.Connection = connection;
        command()




        command.CommandText = "Insert into [User] values (NEWID(), @username, @password, @Name, @LastName, @Email);";
        command.Parameters.Add("@username", SqlDbType.NVarChar).Value = Username;
        command.Parameters.Add("@password", SqlDbType.NVarChar).Value = Password;
        command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = Name;
        command.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = LastName;
        command.Parameters.Add("@Email", SqlDbType.NVarChar).Value =Email;
        command.ExecuteNonQuery();
    }
}*/

/*
 * SqlCommand createLogin = new SqlCommand(
             "INSERT INTO [dbo].[Users] 
              VALUES (@username, @password, @firstname, @lastname, @email",
             myConnection.SqlConnection);
 */




/*
 *                 if (_addUserCommand == null)
                {
                    _addUserCommand = new ViewModelCommand(
                        param => userRepository.Add(new UserModel(Username,
                                Password,Name,LastName, Email)));
                }
                return _addUserCommand;



   userModel.Username = Username;
                Username = String.Empty;
                userModel.Password = Password;
                Password = String.Empty;
                userModel.Name = Name;
                Name= String.Empty;
                userModel.LastName= LastName;
                LastName= String.Empty;
                userModel.Email = Email;
                Email = String.Empty;







            }
*/