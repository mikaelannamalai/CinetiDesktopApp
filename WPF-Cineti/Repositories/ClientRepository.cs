using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WPF_LoginForm.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace WPF_LoginForm.Repositories
{
    public class ClientRepository : RepositoryBase, IClientRepository
    {
        private List<ClientModel> _Clients = new List<ClientModel>();

        public void Add(ClientModel clientModel)
        {
            throw new NotImplementedException();
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

        public void Edit(ClientModel clientModel)
        {
            throw new NotImplementedException();
        }
        public List<ClientModel> Clients { get => _Clients; set => _Clients = value; }
        public IEnumerable<ClientModel> GetByAll()
        {
            {
                Clients.Clear();
                using (var connection = GetConnection())
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "select * from [Client]";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Clients.Add(new ClientModel()
                            {
                                ClientId = reader[0].ToString(),
                                Name = reader[1].ToString(),
                                Language = reader[2].ToString(),
                                DOB = reader[3].ToString(),
                                Email = reader[4].ToString(),
                                Address = reader[5].ToString(),
                                phone = reader[6].ToString(),
                            });
                        }
                        connection.Close();
                        return Clients;
                    }
                }
            }
        }
            

        public ClientModel GetByDOB(DateTimeKind dob)
        {
            ClientModel client = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select *from [Client] where DOB=@dob";
                command.Parameters.Add("@dob", SqlDbType.Date).Value = dob;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        client = new ClientModel()
                        {
                            ClientId = reader[0].ToString(),
                            Name = reader[1].ToString(),
                            DOB = reader[3].ToString(),
                            phone = reader[6].ToString(),
                      
                        };
                    }
                }
            return client;
        }
    }
        public string Name { get; set; }
        public string Language { get; set; }
        public DateTimeKind DOB { get; set; }
        public string phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
 



        public ClientModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
    }
