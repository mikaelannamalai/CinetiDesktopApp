using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WPF_LoginForm.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace WPF_LoginForm.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        private List<UserModel> _Users = new List<UserModel>();

        private List<UserModel> _DBAllUsers = new List<UserModel>();



        public List<UserModel> Users { get => _Users; set => _Users = value; }
        public List<UserModel> DBAllUsers { get => _DBAllUsers; set => _DBAllUsers = value; }

        public void Add(UserModel userModel)
        {
            
                using (var connection = GetConnection())
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "Insert into [User] values (NEWID(), @username, @password, @Name, @LastName, @Email";
                    command.Parameters.Add("@username", SqlDbType.NVarChar).Value = userModel.Username;
                    command.Parameters.Add("@password", SqlDbType.NVarChar).Value = userModel.Password;
                    command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = userModel.Name;
                    command.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = userModel.LastName;
                    command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = userModel.Email;
                command.ExecuteNonQuery();
                }
            }
         

        public bool AuthenticateUser(NetworkCredential credential)
        {
            bool validUser;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select *from [User] where username=@username and [password]=@password";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = credential.UserName;
                command.Parameters.Add("@password", SqlDbType.NVarChar).Value = credential.Password;
                validUser = command.ExecuteScalar() == null ? false : true;
            }
            return validUser;
        }

        public void Edit(UserModel userModel)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Insert into [User] where Id=@Id, values (NEWID(), @username, @password, @Name, @LastName, @Email)";
                command.Parameters.Add("@Id", SqlDbType.NVarChar).Value = userModel.Id;
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = userModel.Username;
                command.Parameters.Add("@password", SqlDbType.NVarChar).Value = userModel.Password;
                command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = userModel.Name;
                command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = userModel.Email;
                command.ExecuteScalar();
            }
        }
        public IEnumerable<UserModel> GetByAll()
        {
            Users.Clear();
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
                            Password = reader[2].ToString(),
                            Name = reader[3].ToString(),
                            LastName = reader[4].ToString(),
                            Email = reader[5].ToString(),
                        });
                    }connection.Close();
                    return Users;
                }
            }
        }
        
        public List <UserModel> GetALL()
    
            {
            DBAllUsers.Clear();
            using (var connection = GetConnection())
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "select *from [User]";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DBAllUsers.Add(new UserModel()
                            {
                                Id = reader[0].ToString(),
                                Username = reader[1].ToString(),
                                Password = reader[2].ToString(),
                                Name = reader[3].ToString(),
                                LastName = reader[4].ToString(),
                                Email = reader[5].ToString(),
                            });
                        }
                        return DBAllUsers;
                    }
                
            }
            }
        


        public UserModel GetById(int id)
        {
            UserModel user = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select *from [User] where Id=@Id";
                command.Parameters.Add("@Id", SqlDbType.NVarChar).Value = id;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new UserModel()
                        {
                            Id = reader[0].ToString(),
                            Username = reader[1].ToString(),
                            Password = string.Empty,
                            Name = reader[3].ToString(),
                            LastName = reader[4].ToString(),
                            Email = reader[5].ToString(),
                        };
                    }
                }
            }
            return user;
        }
        public UserModel GetByUsername(string username)
        {
            UserModel user = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select *from [User] where username=@username";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new UserModel()
                        {
                            Id = reader[0].ToString(),
                            Username = reader[1].ToString(),
                            Password = string.Empty,
                            Name = reader[3].ToString(),
                            LastName = reader[4].ToString(),
                            Email = reader[5].ToString(),
                        };
                    }
                }
            }
            return user;
        }



        public void Remove(int id)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Delete from [User] where Id=@Id";
                command.Parameters.Add("@Id", SqlDbType.NVarChar).Value = id;
                command.BeginExecuteReader();
            }
        }
    }
}
