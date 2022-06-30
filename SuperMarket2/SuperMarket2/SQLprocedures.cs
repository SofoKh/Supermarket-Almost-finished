using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket2
{
    internal class SQLprocedures
    {
        public static string conn= "Data Source=DESKTOP-VQ33BL3\\SQLEXPRESS;Initial Catalog=SuperMarket;Integrated Security=True";
        public static SqlConnection connection = new SqlConnection(conn);
        public static SqlCommand cmd;
        public static SqlDataAdapter dataAdapter;

        public static void InsertUsers(string Name,string Surname,string Email, string Password, int Age,string Address,int PhoneNumber)
        {
            cmd = new SqlCommand("InsertUsers", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter[] SQLp = new SqlParameter[7];
            SQLp[0] = new SqlParameter("@UserName", SqlDbType.NVarChar,(50));
            SQLp[0].Value = Name;
            SQLp[1] = new SqlParameter("@UserSurname", SqlDbType.NVarChar,(50));
            SQLp[1].Value = Surname;
            SQLp[2] = new SqlParameter("@UserEmail", SqlDbType.NVarChar,(50));
            SQLp[2].Value = Email;
            SQLp[3] = new SqlParameter("@UserPassword", SqlDbType.NVarChar, (50));
            SQLp[3].Value = Password;
            SQLp[4] = new SqlParameter("@UserAge", SqlDbType.Int);
            SQLp[4].Value = Age;
            SQLp[5] = new SqlParameter("@UserAddress", SqlDbType.NVarChar,(50));
            SQLp[5].Value = Address;
            SQLp[6] = new SqlParameter("@UserPhoneNumber", SqlDbType.Int);
            SQLp[6].Value = PhoneNumber;
            cmd.Parameters.AddRange(SQLp);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }
        public static DataTable SelectUsers()
        {
            cmd = new SqlCommand("SelectUsers", connection);
            dataAdapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            return dataTable;
        }
        public static void UpdateUsers(string Name, string Surname, string Email, int Age, string Address, int PhoneNumber,int ID)
        {
            cmd = new SqlCommand("UpdateUsers", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UserName", SqlDbType.NVarChar,(50)).Value = Name;
            cmd.Parameters.Add("@UserSurname", SqlDbType.NVarChar, (50)).Value = Surname;
            cmd.Parameters.Add("@UserEmail", SqlDbType.NVarChar, (50)).Value = Email;
            cmd.Parameters.Add("@UserAge", SqlDbType.Int).Value = Age;
            cmd.Parameters.Add("@UserAddress", SqlDbType.NVarChar, (50)).Value = Address;
            cmd.Parameters.Add("@UserPhoneNumber", SqlDbType.Int).Value = PhoneNumber;
            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = ID;
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }
        public static void UpdatePassword(string Password,string Email)
        {
            cmd = new SqlCommand("UpdatePassword", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UserPassword", SqlDbType.NVarChar, (50)).Value = Password;
            cmd.Parameters.Add("@UserEmail", SqlDbType.NVarChar, (50)).Value = Email;
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }
        
        public static void InsertProducts(string Name, string Descrption, int Price, int Quantity)
        {
            cmd = new SqlCommand("InsertUsers", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter[] SQLp = new SqlParameter[3];
            SQLp[0] = new SqlParameter("@ProductName", SqlDbType.NVarChar, (50));
            SQLp[0].Value = Name;
            SQLp[1] = new SqlParameter("@ProductDescription", SqlDbType.NVarChar, (50));
            SQLp[1].Value = Descrption;
            SQLp[2] = new SqlParameter("@ProductPrice", SqlDbType.Money);
            SQLp[2].Value = Price;
            SQLp[3] = new SqlParameter("@ProductQuantity", SqlDbType.Int);
            SQLp[3].Value = Quantity;
            cmd.Parameters.AddRange(SQLp);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }
        public static DataTable SelectProducts()
        {
            
            cmd = new SqlCommand("SelectProducts", connection);
            dataAdapter = new SqlDataAdapter(cmd);
            DataTable dataTable1 = new DataTable();
            dataAdapter.Fill(dataTable1);
            return dataTable1;
        }
    }
}
