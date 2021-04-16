using System;
using System.Collections.Generic;
using System.Text;

namespace curse.Model
{
    public class User : Indentifiable<string>
    {
        private String username, pass;


        public User(string id)
        {
            this.username = id;
            this.pass = "";

        }

        public User(string id,string pass)
        {
            this.username = id;
            this.pass = pass;
        }
        public string Id
        {
            get { return username; }
            set { username = value; }
        }

        public override string ToString()
        {
            return string.Format("Id:{0}", username);
        }

        public string Password
        {
            get { return pass; }
            set { pass = value; }
        }
        public bool Equals(User obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj.username, username);
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(User)) return false;
            return Equals((User) obj);
        }

        public override int GetHashCode()
        {
            return username.GetHashCode();
        }





    }
    
}
