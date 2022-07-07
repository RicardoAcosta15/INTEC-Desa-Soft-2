using CRUD_Cédulas.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_Cédulas
{
    public enum State
    {
        Normal,
        Adding,
        Updating,
        Deleting
    }

    public partial class Form1 : Form
    {
        State state = State.Normal;
        int Id;
        char Sex = 'M';
        string URLProfilePicture = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            state = State.Normal;
            ClearFields();
            gbPanel.Enabled = false;
            btnAdd.Enabled = true;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
            btnCancel.Enabled = false;
            btnSave.Enabled = false;
            GetUsers();
            cbBloodType.SelectedIndex = -1;
            cbCivilState.SelectedIndex = -1;
            cbCountry.SelectedIndex = -1;
            rbMale.Checked = false;
            rbFemale.Checked = false;
            dtpExpirationDate.Value = DateTime.Now;
            dtpDOB.Value = DateTime.Now;
            Id = 0;
        }

        void ClearFields()
        {
            pbProfilePicture.Image = null;
            txtProfilePicture.Text = string.Empty;

            foreach (Control c in gbPanel.Controls)
            {
                if (c is TextBox)
                {
                    c.Text = string.Empty;
                }
            }
        }

        void GetUsers()
        {
            dgvIds.DataSource = null;
            dgvIds.DataSource = ReadJson();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            state = State.Adding;
            btnAdd.Enabled = false;
            gbPanel.Enabled = true;
            btnCancel.Enabled = true;
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }
        
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            state = State.Updating;
            btnCancel.Enabled = true;
            btnAdd.Enabled = false;
            gbPanel.Enabled = true;
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            state = State.Deleting;
            gbPanel.Enabled = true;
            btnDelete.Enabled = false;
            btnAdd.Enabled = false;
            btnUpdate.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                switch (state)
                {
                    case State.Adding:
                    {
                        if (EmptyFields())
                        {
                            break;
                        }

                        var user = new User
                        {
                            Id = NextId(),
                            IdNumber = long.Parse(txtIdNumber.Text),
                            ProfilePicture = URLProfilePicture,
                            FirstName = txtFirstName.Text,
                            LastName = txtLastName.Text,
                            BirthPlace = txtBirthPlace.Text,
                            DOB = dtpDOB.Value,
                            Country = cbCountry.Text,
                            Sex = Sex,
                            BloodType = cbBloodType.Text,
                            Occupation = txtOccupation.Text,
                            CivilState = cbCivilState.Text,
                            ExpDate = dtpExpirationDate.Value,
                            CreatedDate = DateTime.Now
                        };

                        var list = ReadJson();
                        list.Add(user);
                        WriteJson(list);
                        MessageBox.Show("Cédula Agregada", "INTEC", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        state = State.Normal;
                        ClearFields();
                        gbPanel.Enabled = false;
                        btnAdd.Enabled = true;
                        btnUpdate.Enabled = true;
                        btnDelete.Enabled = true;
                        btnCancel.Enabled = false;
                        btnSave.Enabled = false;
                        GetUsers();
                        cbBloodType.SelectedIndex = -1;
                        cbCivilState.SelectedIndex = -1;
                        cbCountry.SelectedIndex = -1;
                        rbMale.Checked = false;
                        rbFemale.Checked = false;
                        dtpExpirationDate.Value = DateTime.Now;
                        dtpDOB.Value = DateTime.Now;
                        Id = 0;

                        break;
                    }
                    case State.Updating:
                    {
                        if (EmptyFields())
                        {
                            break;
                        }
                        var user = new User
                        {
                            Id = Id,
                            IdNumber = long.Parse(txtIdNumber.Text),
                            ProfilePicture = URLProfilePicture,
                            FirstName = txtFirstName.Text,
                            LastName = txtLastName.Text,
                            BirthPlace = txtBirthPlace.Text,
                            DOB = dtpDOB.Value,
                            Country = cbCountry.Text,
                            Sex = Sex,
                            BloodType = cbBloodType.Text,
                            Occupation = txtOccupation.Text,
                            CivilState = cbCivilState.Text,
                            ExpDate = dtpExpirationDate.Value,
                            CreatedDate = DateTime.Now
                        };

                        var list = ReadJson();
                        list.Remove(list.FirstOrDefault(x => x.Id == Id));
                        list.Add(user);
                        WriteJson(list);
                        MessageBox.Show("Cédula Actualizada", "INTEC", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        state = State.Normal;
                        ClearFields();
                        gbPanel.Enabled = false;
                        btnAdd.Enabled = true;
                        btnUpdate.Enabled = true;
                        btnDelete.Enabled = true;
                        btnCancel.Enabled = false;
                        btnSave.Enabled = false;
                        GetUsers();
                        cbBloodType.SelectedIndex = -1;
                        cbCivilState.SelectedIndex = -1;
                        cbCountry.SelectedIndex = -1;
                        rbMale.Checked = false;
                        rbFemale.Checked = false;
                        dtpExpirationDate.Value = DateTime.Now;
                        dtpDOB.Value = DateTime.Now;
                        Id = 0;

                        break;
                    }
                    case State.Deleting:
                    {
                        var list = ReadJson();
                        list.Remove(list.FirstOrDefault(x => x.Id == Id));
                        WriteJson(list);
                        MessageBox.Show("Cédula Eliminada", "INTEC", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        state = State.Normal;
                        ClearFields();
                        gbPanel.Enabled = false;
                        btnAdd.Enabled = true;
                        btnUpdate.Enabled = true;
                        btnDelete.Enabled = true;
                        btnCancel.Enabled = false;
                        btnSave.Enabled = false;
                        GetUsers();
                        cbBloodType.SelectedIndex = -1;
                        cbCivilState.SelectedIndex = -1;
                        cbCountry.SelectedIndex = -1;
                        rbMale.Checked = false;
                        rbFemale.Checked = false;
                        dtpExpirationDate.Value = DateTime.Now;
                        dtpDOB.Value = DateTime.Now;
                        Id = 0;

                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se han llenado todos los campos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        bool EmptyFields()
        {
            foreach (Control c in gbPanel.Controls)
            {
                if (c is TextBox)
                    if (string.IsNullOrWhiteSpace(c.Text))
                    {
                        MessageBox.Show("Hay campos vacios", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return true;
                    }
            }
            return false;
        }

        int NextId()
        {
            return ReadJson().Max(x => x.Id) + 1;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            state = State.Normal;
            ClearFields();
            gbPanel.Enabled = false;
            btnAdd.Enabled = true;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
            btnCancel.Enabled = false;
            btnSave.Enabled = false;
            GetUsers();
            cbBloodType.SelectedIndex = -1;
            cbCivilState.SelectedIndex = -1;
            cbCountry.SelectedIndex = -1;
            rbMale.Checked = false;
            rbFemale.Checked = false;
            dtpExpirationDate.Value = DateTime.Now;
            dtpDOB.Value = DateTime.Now;
            Id = 0;
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            Sex = rbMale.Checked ? 'M' : 'F';
        }

        List<User> ReadJson()
        {
            var json = string.Empty;
            var userList = new List<User>();
            var pathFile = $"{AppDomain.CurrentDomain.BaseDirectory}\\users.json";

            if (File.Exists(pathFile))
            {
                json = File.ReadAllText(pathFile, Encoding.UTF8);
                userList = JsonConvert.DeserializeObject<List<User>>(json);
            }
            return userList;
        }

        void WriteJson(List<User> userList)
        {
            var json = JsonConvert.SerializeObject(userList);
            var pathFile = $"{AppDomain.CurrentDomain.BaseDirectory}\\users.json";

            StreamWriter sw = new StreamWriter(pathFile, false, Encoding.UTF8);
            sw.Write(json);
            sw.Close();
        }

        private void btnUploadPhoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos de Imágenes(*.jpg; *.jpeg; *.gif; *.bmp) | *.jpg; *.jpeg; *.gif; *.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pbProfilePicture.Image = new Bitmap(openFileDialog.FileName);

                URLProfilePicture = openFileDialog.FileName;
            }
        }

        private void dgvIds_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (state == State.Updating || state == State.Deleting)
            {
                Id = int.Parse(dgvIds.CurrentRow.Cells[0].Value.ToString());
                var user = ReadJson().FirstOrDefault(x => x.Id == Id);
                txtFirstName.Text = user.FirstName;
                txtLastName.Text = user.LastName;
                txtBirthPlace.Text = user.BirthPlace;
                cbCountry.SelectedIndex = cbCountry.Items.IndexOf(user.Country);
                txtOccupation.Text = user.Occupation;
                txtIdNumber.Text = user.IdNumber.ToString();
                pbProfilePicture.ImageLocation = user.ProfilePicture;
                if (user.Sex == 'M')
                {
                    rbMale.Checked = true;
                }
                else
                {
                    rbFemale.Checked = true;
                }
                dtpDOB.Value = user.DOB;
                dtpExpirationDate.Value = user.ExpDate;
                cbCivilState.SelectedIndex = cbCivilState.Items.IndexOf(user.CivilState);
                cbBloodType.SelectedIndex = cbBloodType.Items.IndexOf(user.BloodType);
            }
        }
    }
}
