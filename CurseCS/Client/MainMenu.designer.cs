
using System;

namespace curse.Client
{
    partial class MainMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.PilotiTabel = new System.Windows.Forms.DataGridView();
            this.ArataParticipantiButon = new System.Windows.Forms.Button();
            this.NumePilotBox = new System.Windows.Forms.TextBox();
            this.EchipaPilotBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CapacitateBox = new System.Windows.Forms.TextBox();
            this.CurseTabel = new System.Windows.Forms.DataGridView();
            this.ArataCurseButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.InscriereButton = new System.Windows.Forms.Button();
            this.LogoutButton = new System.Windows.Forms.Button();
            this.IdBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PilotiTabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CurseTabel)).BeginInit();
            this.SuspendLayout();
            // 
            // PilotiTabel
            // 
            this.PilotiTabel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PilotiTabel.Location = new System.Drawing.Point(12, 74);
            this.PilotiTabel.Name = "PilotiTabel";
            this.PilotiTabel.Size = new System.Drawing.Size(444, 339);
            this.PilotiTabel.TabIndex = 0;
            // 
            // ArataParticipantiButon
            // 
            this.ArataParticipantiButon.Location = new System.Drawing.Point(12, 25);
            this.ArataParticipantiButon.Name = "ArataParticipantiButon";
            this.ArataParticipantiButon.Size = new System.Drawing.Size(114, 23);
            this.ArataParticipantiButon.TabIndex = 12;
            this.ArataParticipantiButon.Text = "Arata Participanti";
            this.ArataParticipantiButon.Click += new System.EventHandler(this.ArataParticipantiButon_Click);
            // 
            // NumePilotBox
            // 
            this.NumePilotBox.Location = new System.Drawing.Point(471, 226);
            this.NumePilotBox.Multiline = true;
            this.NumePilotBox.Name = "NumePilotBox";
            this.NumePilotBox.Size = new System.Drawing.Size(317, 40);
            this.NumePilotBox.TabIndex = 2;
            // 
            // EchipaPilotBox
            // 
            this.EchipaPilotBox.Location = new System.Drawing.Point(471, 301);
            this.EchipaPilotBox.Multiline = true;
            this.EchipaPilotBox.Name = "EchipaPilotBox";
            this.EchipaPilotBox.Size = new System.Drawing.Size(317, 40);
            this.EchipaPilotBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(471, 328);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 4;
            // 
            // CapacitateBox
            // 
            this.CapacitateBox.Location = new System.Drawing.Point(471, 364);
            this.CapacitateBox.Multiline = true;
            this.CapacitateBox.Name = "CapacitateBox";
            this.CapacitateBox.Size = new System.Drawing.Size(317, 40);
            this.CapacitateBox.TabIndex = 5;
            // 
            // CurseTabel
            // 
            this.CurseTabel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CurseTabel.Location = new System.Drawing.Point(840, 57);
            this.CurseTabel.Name = "CurseTabel";
            this.CurseTabel.Size = new System.Drawing.Size(444, 339);
            this.CurseTabel.TabIndex = 6;
            // 
            // ArataCurseButton
            // 
            this.ArataCurseButton.Location = new System.Drawing.Point(1187, 25);
            this.ArataCurseButton.Name = "ArataCurseButton";
            this.ArataCurseButton.Size = new System.Drawing.Size(86, 23);
            this.ArataCurseButton.TabIndex = 7;
            this.ArataCurseButton.Text = "Arata Curse";
            this.ArataCurseButton.UseVisualStyleBackColor = true;
            this.ArataCurseButton.Click += new System.EventHandler(this.ArataCurseButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(474, 207);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Nume Pilot";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(474, 285);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Nume Echipa";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(477, 348);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Capacitate";
            // 
            // InscriereButton
            // 
            this.InscriereButton.Location = new System.Drawing.Point(480, 415);
            this.InscriereButton.Name = "InscriereButton";
            this.InscriereButton.Size = new System.Drawing.Size(75, 23);
            this.InscriereButton.TabIndex = 11;
            this.InscriereButton.Text = "Inscriere";
            this.InscriereButton.UseVisualStyleBackColor = true;
            this.InscriereButton.Click += new System.EventHandler(this.InscriereButton_Click);
            // 
            // LogoutButton
            // 
            this.LogoutButton.Location = new System.Drawing.Point(1224, 414);
            this.LogoutButton.Name = "LogoutButton";
            this.LogoutButton.Size = new System.Drawing.Size(75, 23);
            this.LogoutButton.TabIndex = 13;
            this.LogoutButton.Text = "LogOut";
            this.LogoutButton.UseVisualStyleBackColor = true;
            this.LogoutButton.Click += new System.EventHandler(this.LogoutButton_Click);
            // 
            // IdBox
            // 
            this.IdBox.Location = new System.Drawing.Point(474, 147);
            this.IdBox.Multiline = true;
            this.IdBox.Name = "IdBox";
            this.IdBox.Size = new System.Drawing.Size(123, 39);
            this.IdBox.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(480, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "IdPilot";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1296, 450);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.IdBox);
            this.Controls.Add(this.LogoutButton);
            this.Controls.Add(this.InscriereButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ArataCurseButton);
            this.Controls.Add(this.CurseTabel);
            this.Controls.Add(this.CapacitateBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.EchipaPilotBox);
            this.Controls.Add(this.NumePilotBox);
            this.Controls.Add(this.ArataParticipantiButon);
            this.Controls.Add(this.PilotiTabel);
            this.Name = "MainMenu";
            this.Text = "MainMenu";
            this.Load += new System.EventHandler(this.MainMenu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PilotiTabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CurseTabel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.DataGridView PilotiTabel;
        private System.Windows.Forms.Button ArataParticipantiButon;
        private System.Windows.Forms.TextBox NumePilotBox;
        private System.Windows.Forms.TextBox EchipaPilotBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox CapacitateBox;
        private System.Windows.Forms.DataGridView CurseTabel;
        private System.Windows.Forms.Button ArataCurseButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button InscriereButton;
        private System.Windows.Forms.Button LogoutButton;
        private System.Windows.Forms.TextBox IdBox;
        private System.Windows.Forms.Label label5;
    }
}