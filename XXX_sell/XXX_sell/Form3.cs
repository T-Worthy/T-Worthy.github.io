using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XXX_sell
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime DT = System.DateTime.Now;
            string dt = System.DateTime.Now.ToString();


            if (name0.Text.Trim() == "")
            { MessageBox.Show("请输入有效的用户信息", "提示"); }
            else
            {
                SqlConnection objConnection = new SqlConnection();
                objConnection.ConnectionString = ("Server=.;database=mydtbs;integrated security=SSPI");               //链接
                objConnection.Open();


                string str = "insert Users (Name ,Pwd,Tel) values('"     + name0.Text.ToString() +    "','"+ PWD0.Text.ToString() + "','" +Tel.Text.ToString ()+ "') ";
                SqlCommand objCommand = new SqlCommand(str, objConnection);
                objCommand.CommandText = str;
                objCommand.ExecuteNonQuery();
                MessageBox.Show("注册成功", "恭喜：");
                Form2 f2 = new XXX_sell.Form2();
                this.Close();
                f2.Show();


                /*   if (objCommand.ExecuteScalar() != null)
                   {
                       objConnection.Close();
                       Tag = 1;
                       //this.Close();
                       Form2 f2 = new XXX_sell.Form2();
                       f2.Show();

                   }
                   else
                   {
                       MessageBox.Show("用户信息有误，请重新输入！", "警告：");
                       objConnection.Close();

                   }*/



            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
