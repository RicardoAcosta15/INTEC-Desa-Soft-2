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
    public partial class CategoryForm : Form
    {
        public bool Adding { get; set; } = true;

        public CategoryForm()
        {
            InitializeComponent();

            GetRecords();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            gbPanel.Enabled = true;
            btnAdd.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
        }

        private void GenerateNewID()
        {
            ClearFields();

            //var Id = 1;
            //txtID.Text = Id.ToString();
        }

        private void ClearFields()
        {
            txtID.Text = string.Empty;
            txtName.Text = string.Empty;
            txtDescription.Text = string.Empty;
            chkIsEnabled.Checked = false;
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            SaveRecord();
        }

        private void SaveRecord()
        {
            var json = string.Empty;
            var categoryList = new List<Category>();
            var pathFile = $"{AppDomain.CurrentDomain.BaseDirectory}\\categories.json";

            if (File.Exists(pathFile))
            {
                json = File.ReadAllText(pathFile, Encoding.UTF8);
                categoryList = JsonConvert.DeserializeObject<List<Category>>(json);
            }

            var category = new Category();
            if (Adding) // Adding Record
            {
                category = new Category
                {
                    Id = int.Parse(txtID.Text),
                    Name = txtName.Text,
                    Description = txtDescription.Text,
                    IsEnabled = chkIsEnabled.Checked,
                    CreatedDate = DateTime.Now
                };
            }
            else // Update Record
            {
                var Id = int.Parse(txtID.Text);
                category = categoryList.FirstOrDefault(x => x.Id == Id);
                if (category != null)
                {
                    categoryList.Remove(category);

                    category.Name = txtName.Text;
                    category.Description = txtDescription.Text;
                    category.IsEnabled = chkIsEnabled.Checked;
                    category.ModifiedDate = DateTime.Now;
                }
            }

            categoryList.Add(category);

            json = JsonConvert.SerializeObject(categoryList);

            var sw = new StreamWriter(pathFile, false, Encoding.UTF8);
            sw.Write(json);
            sw.Close();


            MessageBox.Show("Registro almacenado", "INTEC", MessageBoxButtons.OK, MessageBoxIcon.Information);

            gbPanel.Enabled = false;
            btnAdd.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            ClearFields();

            GetRecords();
        }

        private void GetRecords()
        {
            var pathFile = $"{AppDomain.CurrentDomain.BaseDirectory}\\categories.json";
            var categoryList = new List<Category>();

            if (File.Exists(pathFile))
            {
                var json = File.ReadAllText(pathFile, Encoding.UTF8);
                categoryList = JsonConvert.DeserializeObject<List<Category>>(json);
            }

            txtID.Text = (categoryList.Count + 1).ToString();
            dgvCategories.DataSource = categoryList;
        }

        
    }
}
