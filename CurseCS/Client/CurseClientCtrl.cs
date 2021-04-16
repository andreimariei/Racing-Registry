using System;
using System.Collections.Generic;
using curse.Model;
using curse.Persistance;
using curse.Services;
using curse.Server;

namespace curse.Client{
    

    public class CurseClientCtrl: ICurseObserver
    {
        public event EventHandler<CurseUserEventArgs> updateEvent;
        private ICurseServices server;
        private Angajat currentUser;
        public CurseClientCtrl(ICurseServices server)
        {
            this.server = server;
            currentUser = null;
        }

        public void login(String userId, String pass)
        {
            Angajat user=new Angajat(userId,pass);
            server.login(user,this);
            Console.WriteLine("Login succeeded ....");
            currentUser = user;
            Console.WriteLine("Current user {0}", user);
        }

        public void logout()
        {
            Console.WriteLine("Ctrl logout");
            server.logout(currentUser, this);
            currentUser = null;
        }

        protected virtual void OnUserEvent(CurseUserEventArgs e)
        {
            if (updateEvent == null) return;
            updateEvent(this, e);
            Console.WriteLine("Update Event called");
        }
        public IEnumerable<Cursa> GetAllCurse()
        {
            RepoCursa repoCursa = new RepoCursa();
            return repoCursa.GetAll();
        }
        public IEnumerable<Pilot> GetAllPiloti()
        {
            RepoPilot repo = new RepoPilot();
            return repo.GetAll();
        }
        public IEnumerable<Pilot> GetInscrisiEchipa(string nume, int capacitate)
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
                Console.WriteLine(rez1.ToArray().ToString());
                foreach (var c in rez1)
                {
                    foreach (var el in pil)
                    {
                        if (c.Pilot == el.Id && el.numeEchipa == nume)
                            rez2.Add(el);
                    }
                }
            }
            return rez2;
        }
        internal void addInscriere(long id, string nume, string numeEchipa, int capacitate,string username)
        {
            RepoInscriere repoInscriere = new RepoInscriere();
            RepoPilot repoPilot = new RepoPilot();
            RepoCursa repoCursa = new RepoCursa();
            Cursa c = repoCursa.FindByCapacitate(capacitate);
            IEnumerable<Pilot> pil = repoPilot.GetAll();
            bool ok = false;
            foreach (var el in pil)
            {
                
                if (el.numeEchipa == numeEchipa && el.nume == nume)
                {
                    Console.Write("inscrere ctrl @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
                    server.AddInscriere(new Inscriere(el.Id, c.Id),username);
                    ok = true;
                    
                    CurseUserEventArgs userEventArgs = new CurseUserEventArgs(CurseUserEvent.updateInscriere, new Pilot(id, nume, numeEchipa));
                }
            }
            if(ok==false)
            {
                repoPilot.Add(new Pilot(id, nume, numeEchipa));
                server.AddInscriere(new Inscriere(id, c.Id), username);
                CurseUserEventArgs userEventArgs = new CurseUserEventArgs(CurseUserEvent.updateInscriere, new Pilot(id,nume,numeEchipa));
            }

        }

        public void updateInscrieri(Pilot pilot, string username)
        {
            CurseUserEventArgs userEventArgs = new CurseUserEventArgs(userEvent: CurseUserEvent.updateInscriere, username);
            OnUserEvent(userEventArgs);

        }

       
    }
}
