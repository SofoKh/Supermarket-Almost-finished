using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace SuperMarket2
{

    public partial class PasswordReset : Form
    {
        int passwordCode = 0;
        List<User> Users = new List<User>();
        public PasswordReset()
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


        //code
        private void button2_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            int randomcode = random.Next(1000, 9999);
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("diaxmasgisment@gmail.com", "aahxiqrahabgdthl");
                MailMessage msg = new MailMessage();
                msg.To.Add(textBoxEmail.Text);
                msg.From = new MailAddress("diaxmasgisment@gmail.com");
                msg.Subject = "Password Reset";
                msg.Body = Convert.ToString(randomcode);
                client.Send(msg);
                MessageBox.Show("Please, check your email");
                passwordCode = randomcode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex));
            }


            /* try
             {
                 using (MailMessage mail = new MailMessage())
                 {
                     mail.From = new MailAddress("yourmegamarket@gmail.com");
                     mail.To.Add(textBoxEmail.Text);
                     mail.Subject = "Password Reset";
                     mail.Body = randomcode.ToString();
                     mail.IsBodyHtml = true;
                     using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                     {
                         smtp.Credentials = new System.Net.NetworkCredential("ourmegamarket@gmail.com", "Olivi@2007");
                         smtp.EnableSsl = true;
                         smtp.Send(mail);
                         MessageBox.Show("Please, Check your email");
                     }
                 }

             }
             catch(Exception ex)
             {
                 MessageBox.Show(Convert.ToString(ex));
             }*/
            /*SmtpClient cv = new SmtpClient("smtp.live.com", 25);
            cv.EnableSsl = true;
            cv.Credentials = new NetworkCredential("yourmegamarket@gmail.com", "Olivi@2007");


            try
            {
                cv.Send("yourmegamarket@gmail.com" ,textBoxEmail.Text , "Recover Password", Convert.ToString(randomcode));
                MessageBox.Show("please, check your email");
            }
            catch(Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex));
            }*/


            /*try 
            {
                string from = "yourmegamarket@gmail.com";
                string password = "Olivi@2007";

                MailMessage msg = new MailMessage();
                msg.Subject = "Recover your password";
                msg.From = new MailAddress(from);
                msg.Body = Convert.ToString(randomcode);
                msg.To.Add(new MailAddress(textBoxEmail.Text));

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smpt.gmil.com";

                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = true;

                NetworkCredential nc = new NetworkCredential(from, password);

                smtp.Credentials = nc;

                smtp.Send(msg);
                MessageBox.Show("Please, check your email");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }*/

        }

        private void PasswordReset_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool notUser = true;
            LoadUsers();
            foreach (User user in Users)
            {
                if (user.Email == textBoxEmail.Text)
                {
                    notUser = false;
                    if (textBoxCode.Text == Convert.ToString(passwordCode))
                    {
                        if(textBoxPassword.Text.Length >=8 && textBoxPassword.Text.Length <= 20)
                        {
                            SQLprocedures.UpdatePassword(textBoxPassword.Text, textBoxEmail.Text);
                            MessageBox.Show("Password changed succesfully");
                        }
                        else
                        {
                            MessageBox.Show("Password length required: 8-20 ");
                        }
                        
                    }
                    else if (textBoxCode.Text != Convert.ToString(passwordCode))
                    {
                        MessageBox.Show("Code is incorrect");
                    }

                    else
                    {
                        notUser = true;
                    }
                }
                
            }
            if (notUser == true)
            {
                MessageBox.Show("User could not be found");
                
            }
        }

        private void Back_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 form = new Form1();
            form.Show();
        }
    }
}
