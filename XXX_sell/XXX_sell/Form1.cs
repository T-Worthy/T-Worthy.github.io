using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XXX_sell
{

//窗体一 ---------------------登录界面  （Form1类）
    public partial class Form1 : Form
    {
        

        public Form1()
           {
            InitializeComponent();
            }
          
  

 /// <summary>
 /// 测试连接及连接模板
 /// </summary>
 /// <param name="sender"></param>
 /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)   //------------------------链接测试
          {
         //  private static string strC = "data source=localhost;uid=sa;pwd=sa;database=mydtbs";
              SqlConnection objConnection = new SqlConnection();
            objConnection .ConnectionString = ("Server=.;database=mydtbs;integrated security=SSPI");   //链接
            objConnection.Open();
            SqlCommand objCommand = new SqlCommand("update Users set Pwd='666' where UID='18' ", objConnection);
            objCommand.CommandText = "update Users set Pwd='668886' where UID='12' ";
            SqlDataReader DR = objCommand .ExecuteReader();
            MessageBox.Show("成功","链接成功");
            DR.Close();
            objConnection.Close();
            }
/// <summary>
/// 登录操作
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
        private void check_Click(object sender, EventArgs e)        //---------------------------登陆
        {
            if (name.Text.Trim() == "" && PWD.Text.ToString() == "wxyz")            //超级管理员登陆
            {
                SuperManager sf = new SuperManager();
                sf.Show();
                MessageBox.Show("超级管理员，欢迎您进入！", "SuperManager");
            }


          else  if (name.Text.Trim() == "")
            { MessageBox.Show("请输入有效的用户信息", "提示"); }
            else
            {
                SqlConnection objConnection = new SqlConnection();
                objConnection.ConnectionString = ("Server=.;database=mydtbs;integrated security=SSPI");               //链接
                objConnection.Open();

                string str = "select   * from Users where Name='" + name.Text.ToString() + "'and Pwd='" + PWD.Text.ToString() + "'     ";
                SqlCommand objCommand = new SqlCommand(str, objConnection);
                if (objCommand.ExecuteScalar() != null  )
                {
                    objConnection.Close();
                    Tag = 1;
                 
                   
                    WaitForm _waitform = new WaitForm();
                        _waitform.Show();
                    System.Threading.Thread.Sleep(3000);
                    _waitform.Hide();

                   Form2 f2 = new XXX_sell.Form2();
                    f2.Show();
                   
                }
                else
                {
                    MessageBox.Show("用户信息有误，请重新输入！", "警告：");
                    objConnection.Close();

                }
            }
        }

      
/// <summary>
/// 注册入口
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
    private void button2_Click(object sender, EventArgs e)
        {
            Form3 f3= new XXX_sell.Form3();
            f3.Show();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

  