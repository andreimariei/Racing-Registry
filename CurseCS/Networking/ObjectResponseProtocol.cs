using System;
namespace curse.Networking.protocol
{
	using UserDTO = curse.Networking.dto.UserDTO;
	using InscriereDTO = curse.Networking.dto.InscriereDTO;
	using CursaDTO = curse.Networking.dto.CursaDTO;
	using PilotDTO = curse.Networking.dto.PilotDTO;
	public interface Response
	{
	}

	[Serializable]
	public class OkResponse : Response
	{

	}

	[Serializable]
	public class ErrorResponse : Response
	{
		public string inscriere;

		public ErrorResponse(string inscriere)
		{
			this.inscriere = inscriere;
		}

		public virtual string Inscriere
		{
			get
			{
				return inscriere;
			}
		}
	}

	[Serializable]
	public class GetAllCurse : Response
	{
		private CursaDTO[] friends;

		public GetAllCurse(CursaDTO[] friends)
		{
			this.friends = friends;
		}

		public virtual CursaDTO[] Friends
		{
			get
			{
				return friends;
			}
		}
	}
	[Serializable]
	public class GetAllPiloti : Response
	{
		private PilotDTO[] piloti;
		public GetAllPiloti(PilotDTO[] piloti)
		{
			this.piloti = piloti;
		}
		public virtual PilotDTO[] Piloti

		{

			get
			{
				return piloti;

			}
		}
		public interface UpdateResponse : Response
		{
		}

	
		

		[Serializable]
		public class NewInscriereResponse : UpdateResponse
		{
			private PilotDTO dto;

			public NewInscriereResponse(PilotDTO dto)
			{
				this.dto = dto;
			}

			public virtual PilotDTO pilot
			{
				get
				{
					return dto;
				}
			}
		}

	}
}