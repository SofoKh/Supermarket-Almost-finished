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
    public partial class Form1 : Form
    {
        List<User> Users = new List<User>();

        public Form1()
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

        private void SignUp_Click(object sender, EventArgs e)
        {
            bool noIssues = true;
            LoadUsers();
            foreach (User user in Users)
            {
                if (SignUpEmail.Text == user.Email)
                {
                    MessageBox.Show("This email is already in use");
                    noIssues = false;
                    break;
                }
            }
            if (SignUpName.Text == "" || SignUpSurname.Text == "" || SignUpEmail.Text == "" || SignUpPassword.Text == "" || SignUpAge.Text == "" || SignUpAddress.Text == "" || SignUpPhoneNumber.Text == "")
            {
                MessageBox.Show("Please,don't leave anything blank");
                noIssues = false;
                
            }
            if (!SignUpEmail.Text.Contains("@"))
            {
                noIssues = false;
                MessageBox.Show("Email does not exist");
            }
            if (SignUpPhoneNumber.Text.Length != 9)
            {
                noIssues = false;
                MessageBox.Show("Phone Number is invalid");
            }
            if (SignUpPassword.Text.Length < 8 || SignUpPassword.Text.Length > 20)
            {
                noIssues = false;
                MessageBox.Show("Please change your password");
            }
            bool isIntString = true;
            if (isIntString = SignUpAge.Text.All(char.IsDigit) == false)
            {
                noIssues = false;
                MessageBox.Show("Age must be a number");

            }
            if(SignUpAge.Text != "")
            {
                if (isIntString = SignUpAge.Text.All(char.IsDigit) == true)
                {
                    if (Convert.ToInt32(SignUpAge.Text) < 18)
                    {
                        noIssues = false;
                        MessageBox.Show("User must be at least 18 years old");
                    }
                }
            }
            
            if (noIssues == true)
            {
                SQLprocedures.InsertUsers(SignUpName.Text, SignUpSurname.Text, SignUpEmail.Text, SignUpPassword.Text, Convert.ToInt32(SignUpAge.Text), SignUpAddress.Text, Convert.ToInt32(SignUpPhoneNumber.Text));
                MessageBox.Show("Account created successfully:)");
                SignUpAddress.Clear();
                SignUpAge.Clear();
                SignUpEmail.Clear();
                SignUpName.Clear();
                SignUpPassword.Clear();
                SignUpPhoneNumber.Clear();
                SignUpSurname.Clear();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            SignUpPassword.PasswordChar = '*';
            LogInPassword.PasswordChar = '*';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public static string LabelName = "";
        public static string LabelID = "";
        private void LogIn_Click(object sender, EventArgs e)
        {

            bool isLoggedIn = false;
            bool mightBeAdmin = false;
            LoadUsers();
            foreach (User user in Users)
            {
                    if (user.Email == LogInEmail.Text && user.Password == LogInPassword.Text)
                    {
                    if(LogInEmail.Text == user.Email && user.ID == 6)
                    {
                        isLoggedIn = true;
                        MessageBox.Show("Welcome back,Admin");
                        Admin1 admin = new Admin1();
                        admin.Show();
                        this.Hide();
                        LabelID = Convert.ToString(user.ID);
                        break;
                    }
                        /*
                         * done!if (LogInEmail.Text == "Admin@gmail.com")
                        {
                            isLoggedIn = true;
                            MessageBox.Show("Welcome back,Admin");
                            Admin1 admin = new Admin1();
                            admin.Show();
                            this.Hide();
                            LabelID = Convert.ToString(user.ID);
                        break;
                        }*/
                        else
                        {
                            LabelName = user.Name;
                            LabelID = Convert.ToString(user.ID);
                            isLoggedIn = true;
                            MessageBox.Show("Logged in successfully!");
                            ShopForm shop = new ShopForm();
                            shop.Show();
                            this.Hide();
                        break;
                        }
                    }
            }
            if (!isLoggedIn)
            {
                MessageBox.Show("Email or Password incorrrect");
            }



        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            PasswordReset reset = new PasswordReset();
            reset.Show();
            this.Hide();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void buttonfree_Click(object sender, EventArgs e)
        {
            ShopForm shop = new ShopForm();
            shop.Show();
            this.Hide();
        }
    }
                
        }
        
    

