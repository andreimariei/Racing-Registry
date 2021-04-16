using System;
using System.Collections.Generic;
using System.Text;

namespace curse.Networking.dto
{
	[Serializable]
	public class UserDTO
	{
		private string id;
		private string passwd;

		public UserDTO(string id) : this(id, "")
		{
		}

		public UserDTO(string id, string passwd)
		{
			this.id = id;
			this.passwd = passwd;
		}

		public virtual string Id
		{
			get
			{
				return id;
			}
			set
			{
				this.id = value;
			}
		}


		public virtual string Passwd
		{
			get
			{
				return passwd;
			}
		}
	}

}
