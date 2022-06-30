using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperMarket2
{
    public partial class ShopForm : Form
    {
        List<Product> Products = new List<Product>();
        int lastProductID = 0;
        int pageCount = 1;
        int productCount = 3;
        int actuallastproductID = 0;
        int count = 0;
        int lastpage = 0;
        int productLlastPage = 0;
        int boxesShown = 0;
        bool box2hidden = false;
        bool box3hidden = false;
        public ShopForm()
        {
            InitializeComponent();
        }
        private void LoadProducts()
        {
            Products.Clear();
            DataTable table = SQLprocedures.SelectProducts();
            for (int i = 0; i < table.Rows.Count; i++)
            {

                var row = table.Rows[i];
                Product product = new Product();
                product.ID = Convert.ToInt32(row["ProductID"]);
                product.Name = Convert.ToString(row["ProductName"]);
                product.Description = Convert.ToString(row["ProductDescription"]);
                product.Price = Convert.ToInt32(row["ProductPrice"]);
                product.Quantity = Convert.ToInt32(row["ProductQuantity"]);
                product.Image = (byte[])row["ProductImage"];
                Products.Add(product);
            }
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {

        }
        private void ShopForm_Load(object sender, EventArgs e)
        {
            labelint.Text = Convert.ToString(productCount);
            
             Loadnext_Product(0);
             Username.Text = Form1.LabelName;
        

        }

        private void Loadnext_Product(int offset)

        {
            LoadProducts();

            for (int i = offset; i < Products.Count && i - offset < 3; i++)
            {
                groupBox2.Show();
                groupBox3.Show();
                actuallastproductID = Products[Products.Count-1].ID;
                labelCount.Text = Convert.ToString(Products.Count);
                labelpage.Text = Convert.ToString(pageCount);
                Product product = Products[i];
                if ((i + 1) % 3 == 1)
                {
                    boxesShown = 1;
                    box2hidden = false;
                    product = Products[i];
                    Product1Name.Text = product.Name;
                    Description1.Text = product.Description;
                    Price1.Text = Convert.ToString(product.Price);
                    Quantity1.Text = Convert.ToString(product.Quantity);
                    lastProductID = product.ID;
                    if(lastProductID == actuallastproductID)
                    {
                        box2hidden = true;
                        box3hidden = true;
                        groupBox2.Hide();
                        groupBox3.Hide();
                    }
                }
                if ((i + 1) % 3 == 2)
                {
                    box3hidden = false;
                    if (box2hidden == true)
                    {
                        groupBox2.Hide();
                    }
                    else
                    {
                        boxesShown = 2;
                        product = Products[i];
                        Product2Name.Text = product.Name;
                        Description2.Text = product.Description;
                        Price2.Text = Convert.ToString(product.Price);
                        Quantity2.Text = Convert.ToString(product.Quantity);
                        lastProductID = product.ID;
                        if(lastProductID == actuallastproductID)
                        {
                            box3hidden = true;
                            groupBox3.Hide();
                        }
                    }
                }
                if ((i + 1) % 3 == 0)
                {
                    
                    if(box3hidden == true)
                    {
                        groupBox3.Hide();
                    }
                    else
                    {
                        boxesShown = 3;
                        product = Products[i];
                        Product3Name.Text = product.Name;
                        Description3.Text = product.Description;
                        Price3.Text = Convert.ToString(product.Price);
                        Quantity3.Text = Convert.ToString(product.Quantity);
                        lastProductID = product.ID;
                    }
                    
                }
                if(lastProductID == actuallastproductID)
                {
                    buttonNext.Hide();
                    if (Products.Count > 3)
                    {
                        buttonPrev.Show();
                    }
                    else
                    {
                        buttonPrev.Hide();
                    }
                }
                if(lastProductID < actuallastproductID)
                {
                    buttonNext.Show();
                    if(lastProductID == Products[0].ID || lastProductID == Products[1].ID || lastProductID == Products[2].ID)
                    {
                        buttonPrev.Hide();
                    }
                    else
                    {
                        buttonPrev.Show();
                    }
                }
                
            }
        }
            private void LogOutButton_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            Loadnext_Product(lastProductID);
            productCount += 3;
            labelint.Text = Convert.ToString(productCount);
            labelpage.Text = Convert.ToString(pageCount);
            pageCount += 1;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void buttonCart_Click(object sender, EventArgs e)
        {

        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            SettingsForm form = new SettingsForm();
            form.Show();
            this.Hide();
        }

        private void ShopForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void buttonPrev_Click(object sender, EventArgs e)
        {
            
            Loadnext_Product(Products.IndexOf((Product)Products.FindAll(p => p.ID == lastProductID)[0]) - (boxesShown + 2));
            productCount -= 3;
            labelint.Text = Convert.ToString(productCount);
            labelpage.Text = Convert.ToString(pageCount);
            pageCount -= 1;
        }
    }
}
