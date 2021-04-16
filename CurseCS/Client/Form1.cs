using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace curse.Client
{
    public partial class Form1 : Form
    {
        private CurseClientCtrl ctrl;
        public Form1(CurseClientCtrl ctrl)
        {
            InitializeComponent();
            this.ctrl = ctrl;
        }

        private void LogInButton_Click(object sender, EventArgs e)
        {
            var username = UsernameBox.Text;
            var password = PasswordBox.Text;
            if(username=="" || password=="")
            {
                MessageBox.Show("Username sau parola invalida!");
            }
            else
            {
                try { 
                    ctrl.login(username, password);
                    var mainMenuForm = new MainMenu(ctrl,new Model.Angajat(username,password));
                    mainMenuForm.Show();
                    this.Hide();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Username sau parola gresite!");
                }
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
