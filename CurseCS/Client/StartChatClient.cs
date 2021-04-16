using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using curse.Client;
using curse.Networking;
using curse.Services;
using Hashtable=System.Collections.Hashtable;


namespace chat.client
{
    static class StartChatClient
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
                
            //IChatServer server=new ChatServerMock();          
            ICurseServices server = new CurseClientProxy("127.0.0.1", 55555);
            CurseClientCtrl ctrl=new CurseClientCtrl(server);
            Form1 win=new Form1(ctrl);
            Application.Run(win);
        }
    }
}
