using Control_de_Gastos.Models;
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

namespace Control_de_Gastos
{
    public partial class Form1 : Form
    {
        public bool Adding { get; set; } = true;

        public Form1()
        {
            InitializeComponent();

            GetRecords();
        }

        private void btnOpenConcept_Click(object sender, EventArgs e)
        {
            var oForm = new ConceptForm();
            oForm.Show();
        }

        private void btnOpenCategory_Click(object sender, EventArgs e)
        {
            var oForm = new CategoryForm();
            oForm.Show();
        }

        private void cbConcept_Click(object sender, EventArgs e)
        {
            GetConcepts();
        }

        void GetConcepts()
        {
            var pathFile = $"{AppDomain.CurrentDomain.BaseDirectory}\\concepts.json";
            var conceptList = new List<Concept>();

            if (File.Exists(pathFile))
            {
                var json = File.ReadAllText(pathFile, Encoding.UTF8);
                conceptList = JsonConvert.DeserializeObject<List<Concept>>(json);
            }

            cbConcept.DataSource = conceptList.Where(x => x.IsEnabled).ToList();
            cbConcept.DisplayMember = "Name";
            cbConcept.ValueMember = "Id";
        }

        private void cbCategory_Click(object sender, EventArgs e)
        {
            GetCategories();
        }

        void GetCategories()
        {
            var pathFile = $"{AppDomain.CurrentDomain.BaseDirectory}\\categories.json";
            var categoryList = new List<Category>();

            if (File.Exists(pathFile))
            {
                var json = File.ReadAllText(pathFile, Encoding.UTF8);
                categoryList = JsonConvert.DeserializeObject<List<Category>>(json);
            }

            cbCategory.DataSource = categoryList.Where(x => x.IsEnabled).ToList();
            cbCategory.DisplayMember = "Name";
            cbCategory.ValueMember = "Id";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            gbMain.Enabled = true;
            btnAdd.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
        }

        private void ClearFields()
        {
            txtAmount.Text = string.Empty;
            cbConcept.SelectedValue = string.Empty;
            cbCategory.SelectedValue = string.Empty;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveRecord();
        }

        private void SaveRecord()
        {
            var json = string.Empty;
            var recordList = new List<Record>();
            var pathFile = $"{AppDomain.CurrentDomain.BaseDirectory}\\records.json";

            if (File.Exists(pathFile))
            {
                json = File.ReadAllText(pathFile, Encoding.UTF8);
                recordList = JsonConvert.DeserializeObject<List<Record>>(json);
            }

            var record = new Record();
            if (Adding) // Adding Record
            {
                record = new Record
                {
                    Id = int.Parse(txtID.Text),
                    Concept = cbConcept.SelectedValue,
                    Category = cbCategory.SelectedValue,
                    Amount = int.Parse(txtAmount.Text),
                    CreatedDate = DateTime.Now
                };
            }
            else // Update Record
            {
                var Id = int.Parse(txtID.Text);
                record = recordList.FirstOrDefault(x => x.Id == Id);
                if (record != null)
                {
                    recordList.Remove(record);

                    record.Concept = cbConcept.SelectedValue;
                    record.Category = cbCategory.SelectedValue;
                    record.Amount = int.Parse(txtAmount.Text);
                    record.ModifiedDate = DateTime.Now;
                }
            }

            recordList.Add(record);

            json = JsonConvert.SerializeObject(recordList);

            var sw = new StreamWriter(pathFile, false, Encoding.UTF8);
            sw.Write(json);
            sw.Close();


            MessageBox.Show("Registro almacenado", "INTEC", MessageBoxButtons.OK, MessageBoxIcon.Information);

            gbMain.Enabled = false;
            btnAdd.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            ClearFields();

            GetRecords();
        }

        private void GetRecords()
        {
            var pathFile = $"{AppDomain.CurrentDomain.BaseDirectory}\\records.json";
            var recordList = new List<Record>();

            if (File.Exists(pathFile))
            {
                var json = File.ReadAllText(pathFile, Encoding.UTF8);
                recordList = JsonConvert.DeserializeObject<List<Record>>(json);
            }

            txtID.Text = (recordList.Count + 1).ToString();
            dgvRecords.DataSource = recordList;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            gbMain.Enabled = false;
            btnAdd.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            ClearFields();
        }
    }
}
