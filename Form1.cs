using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EFCRUDAPP
{
    public partial class Form1 : Form
    {
        Customer  model = new Customer();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();

        }

        public void Clear()
        {
            txtFirstName.Text = txtLastName.Text  = txtAddress.Text = "";
            btnSave.Text = "Save";
            btnDelete.Enabled = false;
            model.CustomerID = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Clear();
            PopulateDataGirdView();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            model.FirstName = txtFirstName.Text.Trim();
            model.LastName = txtLastName.Text.Trim();
            model.Address = txtAddress.Text.Trim();
            
            using (DBEntities db= new DBEntities())
            {
                if(model.CustomerID== 0)
                db.Customers.Add(model);
                else
                {
                    db.Entry(model).State = EntityState.Modified;
                }
                db.SaveChanges();

            }
            Clear();
            PopulateDataGirdView();
            MessageBox.Show("Submitted Succesfully");
        }

        public void PopulateDataGirdView()
        {
           
            using (DBEntities db= new DBEntities())
            {
                dgbCustomer.DataSource = db.Customers.ToList < Customer >();
            }
        }

        private void dgbCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgbCustomer_DoubleClick(object sender, EventArgs e)
        {
            if (dgbCustomer.CurrentRow.Index != -1)
            {
                model.CustomerID = Convert.ToInt32(dgbCustomer.CurrentRow.Cells["CustomerID"].Value);

                using (DBEntities db= new DBEntities())
                {
                    model = db.Customers.Where(x => x.CustomerID == model.CustomerID).FirstOrDefault();
                    txtFirstName.Text = model.FirstName;
                    txtLastName.Text = model.LastName;
                    txtAddress.Text = model.Address;
                }

                btnSave.Text = "Update";
                btnDelete.Enabled = true;
            }
        }
    }
}
