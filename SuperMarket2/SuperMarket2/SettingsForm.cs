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
    public partial class SettingsForm : Form
    {
        List<User> Users = new List<User>();
        public SettingsForm()
        {
            InitializeComponent();
        }
        private void LoadUsers()
        {
            DataTable table = SQLprocedures.SelectUsers();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                var row = table.Rows[i];
                User user = new User();
                user.ID = Convert.ToInt32(row["UserID"]);
                user.Name = Convert.ToString(row["UserName"]);
                user.Surname = Convert.ToString(row["UserSurname"]);
                user.Email = Convert.ToString(row["UserEmail"]);
                user.Password = Convert.ToString(row["UserPassword"]);
                user.Age = Convert.ToInt32(row["UserAge"]);
                user.Address = Convert.ToString(row["UserAddress"]);
                user.PhoneNumber = Convert.ToInt32(row["UserPhoneNumber"]);
                Users.Add(user);

            }
        }
        private void SettingsForm_Load(object sender, EventArgs e)
        {
            foreach (User user in Users)
            {

                if (user.ID == Convert.ToInt32(userID))
                {

                    textBoxName.Text = user.Name;
                    textBoxSurname.Text = user.Surname;
                    textBoxEmail.Text = user.Email;
                    textBoxAge.Text = Convert.ToString(user.Age);
                    textBoxAddress.Text = user.Address;
                    textBoxPhone.Text = Convert.ToString(user.PhoneNumber);
                    
                }
            }
        }
        private void Namelbl_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
       
        int userID = Convert.ToInt32(Form1.LabelID);
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            bool noIssues = true;
            LoadUsers();
            foreach (User user in Users)
            {
                if (textBoxName.Text == "" || textBoxSurname.Text == "" || textBoxEmail.Text == "" || textBoxAge.Text == "" || textBoxPassword.Text == "" || textBoxAddress.Text == "" || textBoxPhone.Text == "")
                {
                    noIssues = false;
                }
                if (!textBoxEmail.Text.Contains("@"))
                {
                    noIssues = false;
                    MessageBox.Show("Email does not exist");
                }
                if (textBoxPhone.Text.Length != 9)
                {
                    noIssues = false;
                    MessageBox.Show("Phone Number is invalid");
                    
                }
                
                bool isIntString = true;
                if (isIntString = textBoxAge.Text.All(char.IsDigit) == false)
                {
                    noIssues = false;
                    MessageBox.Show("Age must be a number");
                    break;
                }
                if (textBoxAge.Text != "")
                {
                    if (isIntString = textBoxAge.Text.All(char.IsDigit) == true)
                    {
                        if (Convert.ToInt32(textBoxAge.Text) < 18)
                        {
                            noIssues = false;
                            MessageBox.Show("User must be at least 18 years old");
                            break;
                        }
                    }
                }
                if (noIssues == true)
                {
                    if(user.ID != userID)
                    {
                        while (user.ID != Convert.ToInt32(userID))
                        {
                            userID++;
                        }
                    }
                    if (user.ID == Convert.ToInt32(userID))
                    {

                       if(textBoxPassword.Text == user.Password)
                        {
                            SQLprocedures.UpdateUsers(textBoxName.Text, textBoxSurname.Text, textBoxEmail.Text, Convert.ToInt32(textBoxAge.Text), textBoxAddress.Text, Convert.ToInt32(textBoxPhone.Text), userID);

                            MessageBox.Show("Saving Changes...");
                            MessageBox.Show("Account information changed succesfully!");
                            break;
                        }
                        else
                        {
                            MessageBox.Show("Password is incorrect");
                            break;
                        }
                       
                    }
                }
                else
                {
                    MessageBox.Show("Don't Leave Anything Blank!");
                    break;
                }
                
                
            }
                
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            ShopForm form = new ShopForm();
            form.Show();
        }
    }
}
