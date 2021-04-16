using curse.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace curse.Networking.dto
{
	[Serializable]
	public class InscriereDTO
    {
		public Inscriere inscriere { get; set; }
		public string username { get; set; }

		public InscriereDTO(Inscriere inscriere, string username)
        {
			this.inscriere = inscriere;
			this.username = username;
        }
		

		/*public long IDPilot;
		public long IDCursa;
		
		public string user;

        public InscriereDTO(long iDPilot, long iDCursa)
        {
            IDPilot = iDPilot;
            IDCursa = iDCursa;
        }

        public InscriereDTO(long IdPilot,long IdCursa,string username)
			{
				this.IDPilot = IdPilot;
				this.IDCursa = IdCursa;
				this.user = username;
			}
		*/





	}
}
