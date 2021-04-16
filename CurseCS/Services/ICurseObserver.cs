using curse.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace curse.Services
{
    public interface ICurseObserver
    {
        void updateInscrieri(Pilot pilot,string username);
    }
}
