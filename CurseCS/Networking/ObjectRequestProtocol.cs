using System;

namespace curse.Networking.protocol
{
	using UserDTO = curse.Networking.dto.UserDTO;
	using InscriereDTO = curse.Networking.dto.InscriereDTO;
	using CursaDTO = curse.Networking.dto.CursaDTO;
	using PilotDTO = curse.Networking.dto.PilotDTO;

	public interface Request 
	{
	}


	[Serializable]
	public class LoginRequest : Request
	{
		private UserDTO user;

		public LoginRequest(UserDTO user)
		{
			this.user = user;
		}

		public virtual UserDTO User
		{
			get
			{
				return user;
			}
		}
	}

	[Serializable]
	public class LogoutRequest : Request
	{
		private UserDTO user;

		public LogoutRequest(UserDTO user)
		{
			this.user = user;
		}

		public virtual UserDTO User
		{
			get
			{
				return user;
			}
		}
	}

	[Serializable]
	public class AddInscriereRequest : Request
	{
		public InscriereDTO inscriere;

		public AddInscriereRequest(InscriereDTO message)
		{
			this.inscriere = message;
		}

		public virtual InscriereDTO Inscriere
		{
			get
			{
				return inscriere;
			}
		}
	}

	[Serializable]
	public class GetCurseRequest : Request
	{
		public CursaDTO cursa;

        public GetCurseRequest(CursaDTO cursa)
        {
			this.cursa = cursa;
		}

		public virtual CursaDTO Cursa
		{
			get
			{
				return cursa;
			}
		}
	}

	[Serializable]
	public class GetPilotiRequest : Request
	{
		public PilotDTO pilot;

		public GetPilotiRequest(PilotDTO pilot)
		{
			this.pilot = pilot;
		}

		public virtual PilotDTO Pilot
		{
			get
			{
				return pilot;
			}
		}
	}
}