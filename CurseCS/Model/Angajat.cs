using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace curse.Model
{
    [Serializable]
    public class Angajat : Entity<long>
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public Angajat(string username, string password)
        {
            Id = 0;
            Username = username;
            Password = password;
        }
        public Angajat(long id, string username, string password)
        {
            Id = id;
            Username = username;
            Password = password;
        }
    }
}
