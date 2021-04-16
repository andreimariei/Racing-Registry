using System;
using System.Net.Sockets;

using System.Threading;
using curse.Server;
namespace curse
{
    using curse.Model;
    using curse.Networking;
    using curse.Persistance;
    using curse.ServerTemplate;
    using curse.Services;
    using System.Collections.Generic;

    class StartServer
    {
        static void Main(string[] args)
        {
            
          
            IRepoAngajat userRepo=new RepoAngajat();
            IRepoInscriere messageRepository=new RepoInscriere();
            IRepoPilot pilotRepository = new RepoPilot();
            ICurseServices serviceImpl = new CurseServerImpl(userRepo, messageRepository,pilotRepository);

            IEnumerable<Angajat> ang = userRepo.GetAll();
            foreach (var el in ang)
                Console.WriteLine(el);
            
			SerialChatServer server = new SerialChatServer("127.0.0.1", 55555, serviceImpl);
            server.Start();
            Console.WriteLine("Server started ...");
            //Console.WriteLine("Press <enter> to exit...");
            Console.ReadLine();
            
        }
    }

    public class SerialChatServer: ConcurrentServer 
    {
        private ICurseServices server;
        private CurseClientWorker worker;
        public SerialChatServer(string host, int port, ICurseServices server) : base(host, port)
            {
                this.server = server;
                Console.WriteLine("SerialChatServer...");
        }
        protected override Thread createWorker(TcpClient client)
        {
            worker = new CurseClientWorker(server, client);
            return new Thread(new ThreadStart(worker.run));
        }
    }
    
}
