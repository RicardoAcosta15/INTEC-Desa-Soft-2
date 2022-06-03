using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Céudla_Windows_Forms
{
    public partial class Form1 : Form
    {
        List<Id> Ids = new List<Id>();
        
        public Form1()
        {
            InitializeComponent();
        }

        private void gbPanel_Enter(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            dtpDOB.Value = DateTime.Now.AddYears(-18);
            txtAge.Text = "18";

            btnAdd.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
            gbPanel.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void Save()
        {
            var id = new Id
            {
                ID = Guid.NewGuid(),
                ProfilePicture = txtProfilePicture.Text,
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                IdNumber = int.Parse(txtIdNumber.Text),
                BirthPlace = txtBirthPlace.Text,
                DOB = dtpDOB.Value,
                Country = cbCountry.Text,
                Sexo = rbMale.Checked ? "H" : "M",
                BloodType = cbBloodType.Text,
                Occupation = txtOccupation.Text,
                CivilState = cbCivilState.Text,
                ExpDate = dtpExpirationDate.Value,
                CreatedDate = DateTime.Now,
            };

            Ids.Add(id);

            MessageBox.Show("Cédula agregada");

            EmptyControls();

            btnAdd.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            gbPanel.Enabled = false;

            GetIds();

        }

        private void GetIds()
        {
            dgvIds.DataSource = null;
            dgvIds.DataSource = Ids;
        }

        private void EmptyControls()
        {
            txtProfilePicture.Text = string.Empty;
            pbProfilePicture.Image = null;
            
            foreach ( Control c in gbPanel.Controls)
            {
                if (c is TextBox)
                {
                    c.Text = string.Empty;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            EmptyControls();
        }

        private void dtpDOB_ValueChanged(object sender, EventArgs e)
        {
            txtAge.Text = (DateTime.Now.Year - dtpDOB.Value.Year).ToString();
        }

        private void btnUploadPhoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pbProfilePicture.Image = new Bitmap(openFileDialog.FileName);

                txtProfilePicture.Text = openFileDialog.FileName;
            }
        }
    }

    public class Id
    {
        public Guid ID { get; set; }
        public string ProfilePicture { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int IdNumber { get; set; }
        public string BirthPlace { get; set; }
        public DateTime DOB { get; set; }
        public string Country { get; set; }
        public string Sexo { get; set; }
        public string BloodType { get; set; }
        public string Occupation { get; set; }
        public string CivilState { get; set; }
        public DateTime ExpDate { get; set; }
        public DateTime CreatedDate { get; set; }

    }

}
