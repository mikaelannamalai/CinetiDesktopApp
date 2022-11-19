using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WPF_LoginForm.Models
{
    public interface IClientRepository
    {
        bool AuthenticateUser(NetworkCredential credential);
        void Add(ClientModel clientModel);
        void Edit(ClientModel clientModel);
        void Remove(int id);
        ClientModel GetById(int id);
        ClientModel GetByDOB(DateTimeKind dob);
        IEnumerable<ClientModel> GetByAll();
        //...
    }
}
