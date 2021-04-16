using curse.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using curse.Services;

namespace curse.Client
{
    
    public partial class MainMenu : Form
    {
        private CurseClientCtrl ctrl;
        private Angajat user;
        private IEnumerable<Pilot> piloti;
        private IEnumerable<Cursa> curse;
        public MainMenu(CurseClientCtrl ctrl,Angajat user)
        {
            InitializeComponent();
            this.ctrl = ctrl;
            this.user = user;
            curse = ctrl.GetAllCurse();
            piloti = ctrl.GetAllPiloti();

            ctrl.updateEvent += userUpdate;
        }
        public void userUpdate(object sender,CurseUserEventArgs e)
        {
            BeginInvoke(new UpdateListBoxCallback(this.updateListView), new object[] { });
        }
        public delegate void UpdateListBoxCallback();
        private void updateListView()
        {
            CurseTabel.Rows.Clear();
            loadCurse();
            PilotiTabel.Rows.Clear();
            loadPiloti();
        }

        private void loadPiloti()
        {
            var source = new BindingSource();
            var piloti = ctrl.GetAllPiloti();
            source.DataSource = piloti;
            PilotiTabel.DataSource = source;
        }

        private void loadCurse()
        {
            var source = new BindingSource();
            var curse = ctrl.GetAllCurse();
            source.DataSource = curse;
            CurseTabel.DataSource = source;
        }

        private void ArataCurseButton_Click(object sender, EventArgs e)
        {
            var source = new BindingSource();
            var curse = ctrl.GetAllCurse();
            source.DataSource = curse;
            CurseTabel.DataSource = source;
        }

        private void ArataParticipantiButon_Click(object sender, EventArgs e)
        {
            var echipa = EchipaPilotBox.Text;
            var capacitate = CapacitateBox.Text;
            int capac = 125;
            if (capacitate != "")
            {
                capac = int.Parse(capacitate);
            }
            var source = new BindingSource();
            var part = ctrl.GetInscrisiEchipa(echipa,capac).ToList();
            source.DataSource = part;
            PilotiTabel.DataSource = source;
            

            
        }

        private void InscriereButton_Click(object sender, EventArgs e)
        {

            var nume = NumePilotBox.Text;
            var capacitate = CapacitateBox.Text;
            var numeEchipa = EchipaPilotBox.Text;
            var id1= IdBox.Text;
            int id = 0;
            int capac = 0;
            if (capacitate != "")
            {
                if (id1 !="")
                {
                    id = int.Parse(id1);
                }
                capac = int.Parse(capacitate);
                ctrl.addInscriere(id,nume, numeEchipa, capac,user.Username);
            }
            
           
            
            
            
        }

        private void LogoutButton_Click(object sender, EventArgs e)
        {
            this.Close();
            var form1 = new Form1(ctrl);
            form1.Show();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            loadCurse();
            loadPiloti();
        }
    }
}
