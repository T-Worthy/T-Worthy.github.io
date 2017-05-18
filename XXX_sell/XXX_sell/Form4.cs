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
//using XXX_sell.myDBTableAdapters;

namespace XXX_sell
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();

        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            
            
            string str = "Server=.;database=mydtbs;integrated security=SSPI";
            string sql = "select * from table12";

            SqlConnection cn = new SqlConnection(str);
            cn.Open();
            SqlDataAdapter da = new SqlDataAdapter(sql, cn);
            DataSet ds = new DataSet();
            da.Fill(ds, "table12");
            dataGridView1.DataSource = ds.Tables["table12"];

        }

        private void button2_Click(object sender, EventArgs e)
        {


            dataGridView1.EndEdit();
            //重新用表格数据填充数据容器
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable table = (DataTable)dataGridView1.DataSource;
            //重新启动连接
            string str = "Server=.;database=mydtbs;integrated security=SSPI";
            //用Buider方法更新数据
            using (SqlConnection con = new SqlConnection(str))
            {
                da.SelectCommand = new SqlCommand("select * from table12", con);

                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                da.UpdateCommand = builder.GetUpdateCommand();
                try
                {
                    //更新数据表数据时
                    da.Update(table);
                    table.AcceptChanges();
                    MessageBox.Show("更新成功", "系统消息");

                }

                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
            }

        }
    }
}
