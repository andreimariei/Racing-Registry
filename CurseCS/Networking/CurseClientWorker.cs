using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using curse.Services;
using curse.Model;
using curse.Networking.dto;
using curse.Networking.protocol;
using static curse.Networking.protocol.GetAllPiloti;

namespace curse.Networking
{



    ///
    /// <summary> * Created by IntelliJ IDEA.
    /// * User: grigo
    /// * Date: Mar 18, 2009
    /// * Time: 4:04:43 PM </summary>
    /// 
    public class CurseClientWorker : ICurseObserver //, Runnable
    {
		private ICurseServices server;
		private TcpClient connection;

		private NetworkStream stream;
		private IFormatter formatter;
		private volatile bool connected;
		public CurseClientWorker(ICurseServices server, TcpClient connection)
		{
			this.server = server;
			this.connection = connection;
			try
			{
				
				stream=connection.GetStream();
                formatter = new BinaryFormatter();
				connected=true;
			}
			catch (Exception e)
			{
                Console.WriteLine(e.StackTrace);
			}
		}

		public virtual void run()
		{
			while(connected)
			{
				try
				{
                    object request = formatter.Deserialize(stream);
					object response =handleRequest((Request)request);
					if (response!=null)
					{
					   sendResponse((Response) response);
					}
				}
				catch (Exception e)
				{
                    Console.WriteLine(e.StackTrace);
				}
				
				try
				{
					Thread.Sleep(1000);
				}
				catch (Exception e)
				{
                    Console.WriteLine(e.StackTrace);
				}
			}
			try
			{
				stream.Close();
				connection.Close();
			}
			catch (Exception e)
			{
				Console.WriteLine("Error "+e);
			}
		}

        public void updateInscrieri(Pilot pilot, string username)
        {
            PilotDTO dto=new PilotDTO(pilot.nume,pilot.numeEchipa,username);
            try
            {
				sendResponse(new NewInscriereResponse(dto));
            }
			catch(Exception e)
            {
				throw new CurseException("Sending error: " + e);
            }
        }

        private Response handleRequest(Request request)
		{
			Response response =null;
			if (request is LoginRequest)
			{
				Console.WriteLine("Login request ...");
				LoginRequest logReq =(LoginRequest)request;
				UserDTO udto =logReq.User;
				User user1 =DTOUtils.getFromDTO(udto);
				Angajat user = new Angajat(user1.Id, user1.Password);
				try
                {
                    lock (server)
                    {
                        server.login(user, this);
                    }
					return new OkResponse();
				}
				catch (CurseException e)
				{
					connected=false;
					return new ErrorResponse(e.Message);
				}
			}
			if (request is LogoutRequest)
			{
				Console.WriteLine("Logout request");
				LogoutRequest logReq =(LogoutRequest)request;
				UserDTO udto =logReq.User;
				User user1 = DTOUtils.getFromDTO(udto);
				Angajat user = new Angajat(user1.Id, user1.Password);
				try
				{
                    lock (server)
                    {

                        server.logout(user, this);
                    }
					connected=false;
					return new OkResponse();

				}
				catch (CurseException e)
				{
				   return new ErrorResponse(e.Message);
				}
			}
			if (request is AddInscriereRequest)
			{
				Console.WriteLine("SendInscriereRequest ...");
				AddInscriereRequest senReq =(AddInscriereRequest)request;
				InscriereDTO mdto =senReq.Inscriere;
				Inscriere inscriere = mdto.inscriere;
				Console.WriteLine(inscriere.Cursa);
				Console.WriteLine(mdto.inscriere.Cursa);
				try
				{
                    lock (server)
                    {
                        server.AddInscriere(inscriere,mdto.username);
						
                    }
                        return new OkResponse();
				}
				catch (CurseException e)
				{
					return new ErrorResponse(e.Message);
				}
			}

			if (request is GetCurseRequest)
			{
			
				
				try{
                    Cursa[] cursa;
                    lock (server)
                    {

						cursa = server.GetCurse();
                    }
					CursaDTO[] frDTO = DTOUtils.getDTO(cursa);
					return new GetAllCurse(frDTO);
				}
				catch (CurseException e)
				{
					return new ErrorResponse(e.Message);
				}
			}
			if (request is GetPilotiRequest)
			{

				GetPilotiRequest senReq = (GetPilotiRequest)request;
				GetCurseRequest sen2Req = (GetCurseRequest)request;
				CursaDTO dtoc = sen2Req.Cursa;
				PilotDTO dtop = senReq.Pilot;
				try
				{
					Pilot[] pilot;
					lock (server)
					{

						pilot = server.GetPilotiInscrisi(dtop.nume,dtop.numeEchipa,dtoc.capacitate) ;
					}
					PilotDTO[] frDTO = DTOUtils.getDTO(pilot);
					return new GetAllPiloti(frDTO);
				}
				catch (CurseException e)
				{
					return new ErrorResponse(e.Message);
				}
			}
			return response;
		}

	private void sendResponse(Response response)
		{
			Console.WriteLine("sending response "+response);
            formatter.Serialize(stream, response);
            stream.Flush();
			
		}

        
    }

}