using System;
using System.Collections.Generic;
using System.Text;

namespace curse.Services
{
    public class CurseException:Exception
    {
        public CurseException() : base() { }
        public CurseException(String msg) : base(msg) { }
        public CurseException(String msg, Exception e) : base(msg, e) { }
    }
}
