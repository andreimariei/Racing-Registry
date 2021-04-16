using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace curse.Client
{
    public enum CurseUserEvent
    {
        updateInscriere
    } ;
    public class CurseUserEventArgs : EventArgs
    {
        private readonly CurseUserEvent userEvent;
        private readonly Object data;

        public CurseUserEventArgs(CurseUserEvent userEvent, object data)
        {
            this.userEvent = userEvent;
            this.data = data;
        }

        public CurseUserEvent UserEventType
        {
            get { return userEvent; }
        }

        public object Data
        {
            get { return data; }
        }
    }
}
