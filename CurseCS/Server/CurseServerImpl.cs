using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using curse.Model;
using curse.Services;
using System;
using curse.Persistance;

namespace curse.Server
{
    public class CurseServerImpl: ICurseServices
    {
        public IRepoAngajat userRepository;
        public IRepoInscriere inscriereRepository;
        public IRepoPilot pilotRepository;
        private readonly IDictionary <String, ICurseObserver> loggedClients;



    public CurseServerImpl(IRepoAngajat repo, IRepoInscriere messageRepo, IRepoPilot pilotRepository) {
        this.userRepository = repo;
        this.inscriereRepository = messageRepo;
        this.loggedClients=new Dictionary<String, ICurseObserver>();
        this.pilotRepository = pilotRepository;
    }

    public  void login(Angajat user, ICurseObserver client)  {
        Angajat userOk = userRepository.FindByUsername(user.Username);
            
        if (userOk!=null){
            if(loggedClients.ContainsKey(user.Username))
                throw new CurseException("User already logged in.");
            loggedClients[user.Username]= client;
            
        }else
            throw new CurseException("Authentication failed.");
    }


       public void notifyClients(String username, Pilot pilot)
       {
           ICurseObserver client = loggedClients[username];
           client.updateInscrieri(pilot,username);
           foreach (String us in loggedClients.Keys)
           {
               if (!us.Equals(username))
               {
                    ICurseObserver concursClient = loggedClients[us];
                    Task.Run(() => concursClient.updateInscrieri(pilot,us));
               }
           }
       }

        public  void logout(Angajat user, ICurseObserver client) 
        {
        ICurseObserver localClient=loggedClients[user.Username];
        if (localClient==null)
            throw new CurseException("User "+user.Id+" is not logged in.");
        loggedClients.Remove(user.Username);
        
        }

        public void AddInscriere(Inscriere insc,string username)
        {
            Console.WriteLine("INTRAT IN SERVER");
            try
            {
                if (loggedClients.ContainsKey(username))
                {
                    
                    inscriereRepository.Add(insc);
                    long id = insc.getPilot();
                    Pilot pilot = pilotRepository.FindOne(id);
                    notifyClients(username, pilot);
                    Console.WriteLine("afiseaza mesaj fara exceptie");

                }
                else
                    throw new CurseException("User not logged in!");
            }
            catch(Exception e)
            {
                throw new CurseException(e.Message);
            }
        }

        public Cursa[] GetCurse()
        {
            RepoCursa repo = new RepoCursa();
            IEnumerable<Cursa> curse = repo.GetAll();
            List<Cursa> rez=new List<Cursa>();
            foreach(var el in curse)
            {
                rez.Add(el);
            }
            return rez.ToArray();
        }
        public Pilot[] GetPiloti()
        {
            RepoPilot repo = new RepoPilot();
            IEnumerable<Pilot> piloti = repo.GetAll();
            List<Pilot> rez = new List<Pilot>();
            foreach(var el in piloti)
            {
                rez.Add(el);
            }
            return rez.ToArray();
        }
        public Pilot[] GetPilotiInscrisi(string nume, string numeEchipa, int capacitate)
        {
            RepoInscriere repoInscriere = new RepoInscriere();
            RepoPilot repoPilot = new RepoPilot();
            RepoCursa repoCursa = new RepoCursa();
            IEnumerable<Inscriere> insc = repoInscriere.GetAll();
            IEnumerable<Pilot> pil = repoPilot.GetAll();
            Cursa curse = repoCursa.FindByCapacitate(capacitate);

            List<Inscriere> rez1 = new List<Inscriere>();
            List<Pilot> rez2 = new List<Pilot>();
            if (curse != null)
            {
                foreach (var el in insc)
                {
                    if (curse.Id == el.Cursa)
                        rez1.Add(el);


                }
                foreach (var c in rez1)
                {
                    foreach (var el in pil)
                    {
                        if (c.Pilot == el.Id && el.numeEchipa == numeEchipa)
                            rez2.Add(el);
                    }
                }
            }
            return rez2.ToArray();
        }
    }
}
