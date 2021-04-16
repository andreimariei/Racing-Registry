
using curse.Model;
using System;
using System.Collections.Generic;
using System.Text;


namespace curse.Networking.dto
{

    public class DTOUtils
    {
        public static Cursa getFromDTO(CursaDTO cursa)
        {
			int capacitate = cursa.capacitate;
			return new Cursa(capacitate);
        }
		public static CursaDTO getDTO(Cursa cursa)
        {
			int capa = cursa.capacitate;
            return new CursaDTO(capa);
		}
		public static Pilot getFromDTO(PilotDTO pilot)
        {
			string n = pilot.nume;
			string ne = pilot.numeEchipa;
			return new Pilot(n, ne);
        }
		public static PilotDTO getDTO(Pilot pilot)
		{
			return null;
		}
		public static User getFromDTO(UserDTO usdto)
		{
			string id = usdto.Id;
			string pass = usdto.Passwd;
			return new User(id, pass);

		}
		public static UserDTO getDTO(Angajat user)
		{
			string id = user.Username;
			string pass = user.Password;
			return new UserDTO(id, pass);
		}

		public static Inscriere getFromDTO(InscriereDTO idto)
		{
			//long IDPilot = idto.IDPilot;
			//long IDCursa = idto.IDCursa;
			return null;

		}

		public static InscriereDTO getDTO(Inscriere inscriere)
		{

			/*long IDPilot = inscriere.Pilot;
			long IDCursa = inscriere.Cursa;
			return new InscriereDTO(IDPilot, IDCursa);*/
			return null;
		}

		public static UserDTO[] getDTO(Angajat[] users)
		{
			UserDTO[] frDTO = new UserDTO[users.Length];
			for (int i = 0; i < users.Length; i++)
			{
				frDTO[i] = getDTO(users[i]);
			}
			return frDTO;
		}

		public static User[] getFromDTO(UserDTO[] users)
		{
			User[] friends = new User[users.Length];
			for (int i = 0; i < users.Length; i++)
			{
				friends[i] = getFromDTO(users[i]);
			}
			return friends;
		}
		public static CursaDTO[] getDTO(Cursa[] users)
		{
			CursaDTO[] frDTO = new CursaDTO[users.Length];
			for (int i = 0; i < users.Length; i++)
			{
				frDTO[i] = getDTO(users[i]);
			}
			return frDTO;
		}

		public static Cursa[] getFromDTO(CursaDTO[] users)
		{
			Cursa[] friends = new Cursa[users.Length];
			for (int i = 0; i < users.Length; i++)
			{
				friends[i] = getFromDTO(users[i]);
			}
			return friends;
		}
		public static PilotDTO[] getDTO(Pilot[] users)
		{
			PilotDTO[] frDTO = new PilotDTO[users.Length];
			for (int i = 0; i < users.Length; i++)
			{
				frDTO[i] = getDTO(users[i]);
			}
			return frDTO;
		}

		public static Pilot[] getFromDTO(PilotDTO[] users)
		{
			Pilot[] friends = new Pilot[users.Length];
			for (int i = 0; i < users.Length; i++)
			{
				friends[i] = getFromDTO(users[i]);
			}
			return friends;
		}

	}
}
