using curse.Services;
using curse.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace curse.Services
{
    public interface ICurseServices
    {
        void login(Angajat user, ICurseObserver client);
        void logout(Angajat user, ICurseObserver client);
        void AddInscriere(Inscriere insc,string username);
        Cursa[] GetCurse();
        Pilot[] GetPiloti();
        Pilot[] GetPilotiInscrisi(string nume, string numeEchipa, int capacitate);

    }
}
