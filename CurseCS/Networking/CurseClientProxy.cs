using System;
using System.Collections.Generic;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using curse.Services;
using curse.Model;
using curse.Networking.protocol;
using curse.Networking.dto;
using static curse.Networking.protocol.GetAllPiloti;

namespace curse.Networking
{
	///
	/// <summary> * Created by IntelliJ IDEA.
	/// * User: grigo
	/// * Date: Mar 18, 2009
	/// * Time: 4:36:34 PM </summary>
	/// 
	public class CurseClientProxy : ICurseServices
	{
		private string host;
		private int port;

		private ICurseObserver client;

		private NetworkStream stream;
		
        private IFormatter formatter;
		private TcpClient connection;

		private Queue<Response> responses;
		private volatile bool finished;
        private EventWaitHandle _waitHandle;
		public CurseClientProxy(string host, int port)
		{
			this.host = host;
			this.port = port;
			responses=new Queue<Response>();
		}

		public virtual void login(Angajat user, ICurseObserver client)
		{
			initializeConnection();
			UserDTO udto = DTOUtils.getDTO(user);
			sendRequest(new LoginRequest(udto));
			Response response =readResponse();
			if (response is OkResponse)
			{
				this.client=client;
				return;
			}
			if (response is ErrorResponse)
			{
				ErrorResponse err =(ErrorResponse)response;
				closeConnection();
				throw new CurseException(err.inscriere);
			}
		}


	public virtual void logout(Angajat user, ICurseObserver client)
		{
			UserDTO udto =DTOUtils.getDTO(user);
			sendRequest(new LogoutRequest(udto));
			Response response =readResponse();
			closeConnection();
			if (response is ErrorResponse)
			{
				ErrorResponse err =(ErrorResponse)response;
				throw new CurseException(err.Inscriere);
			}
		}

		



		private void closeConnection()
		{
			finished=true;
			try
			{
				stream.Close();
				//output.close();
				connection.Close();
                _waitHandle.Close();
				client=null;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.StackTrace);
			}

		}

		private void sendRequest(Request request)
		{
			try
			{
                formatter.Serialize(stream, request);
                stream.Flush();
			}
			catch (Exception e)
			{
				throw new CurseException("Error sending object "+e);
			}

		}

		private Response readResponse()
		{
			Response response =null;
			try
			{
                _waitHandle.WaitOne();
				lock (responses)
				{
                    //Monitor.Wait(responses); 
                    response = responses.Dequeue();
                
				}
				

			}
			catch (Exception e)
			{
				Console.WriteLine(e.StackTrace);
			}
			return response;
		}
		private void initializeConnection()
		{
			 try
			 {
				connection=new TcpClient(host,port);
				stream=connection.GetStream();
                formatter = new BinaryFormatter();
				finished=false;
                _waitHandle = new AutoResetEvent(false);
				startReader();
			}
			catch (Exception e)
			{
                Console.WriteLine(e.StackTrace);
			}
		}
		private void startReader()
		{
			Thread tw =new Thread(run);
			tw.Start();
		}


		private void handleUpdate(UpdateResponse update)
		{
			if (update is NewInscriereResponse)
			{

				NewInscriereResponse req = (NewInscriereResponse)update;
				Pilot pilot = new Pilot(req.pilot.nume, req.pilot.NumeEchipa);

				try
				{
					client.updateInscrieri(pilot, req.pilot.username);
				}
				catch (CurseException e)
				{
					Console.WriteLine(e.StackTrace);
				}
			}
		}
		public virtual void run()
			{
				while(!finished)
				{
					try
					{
                        object response = formatter.Deserialize(stream);
						Console.WriteLine("response received "+response);
						if (response is UpdateResponse)
						{
							 handleUpdate((UpdateResponse)response);
						}
						else
						{
							
							lock (responses)
							{
                                					
								 
                                responses.Enqueue((Response)response);
                               
							}
                            _waitHandle.Set();
						}
					}
					catch (Exception e)
					{
						Console.WriteLine("Reading error "+e);
					}
					
				}
			}

       

        public void AddInscriere(Inscriere insc, string username)
        {
			InscriereDTO dto = new InscriereDTO(insc,username);
			sendRequest(new AddInscriereRequest(dto));
			Response response = readResponse();
			if(response is ErrorResponse)
            {
				ErrorResponse err = (ErrorResponse)response;
				throw new CurseException(err.Inscriere);
            }
        }

        public Cursa[] GetCurse()
        {
            throw new NotImplementedException();
        }

        public Pilot[] GetPilotiInscrisi(string nume, string numeEchipa, int capacitate)
        {
            throw new NotImplementedException();
        }

        public Pilot[] GetPiloti()
        {
            throw new NotImplementedException();
        }
    }

}