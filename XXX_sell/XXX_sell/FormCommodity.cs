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
    public partial class FormCommodity : Form
    {
        public FormCommodity()
        {
            InitializeComponent();
        }

    

        private void FormCommodity_Load(object sender, EventArgs e)
        {
            ChaK();
            ZaiRu();
          
          

        }
        void ZaiRu()
        {
            string str = "Server=.;database=mydtbs;integrated security=SSPI";
            string Read_Class = "select distinct Class from Commodity";
            SqlConnection cn = new SqlConnection(str);
            cn.Open();

            SqlDataAdapter da2 = new SqlDataAdapter(Read_Class, cn);
            DataSet ds2 = new DataSet();
            da2.Fill(ds2, "Commodity");
            comboBoxClass.DataSource = ds2.Tables["Commodity"];
            comboBoxClass.DisplayMember = "Class";

        }



        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
           
        }

/// <summary>
/// 添加商品
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
        private void AddCom_Click(object sender, EventArgs e)
        {
            if (ComName.Text == "" || ComInprice.Text == "" || ComOutprice.Text == "")
            { MessageBox.Show("请填写完整信息！", "系统提示");    return; }
            DateTime DT = System.DateTime.Now;
            string dt = System.DateTime.Now.ToString();

          double  ComIn;
            ComIn = Convert.ToDouble(ComInprice.Text.Trim ());
            if (checkBox1.Checked)
                ComIn = ComIn/Convert.ToDouble(Stock.Text.Trim()) ;



            if (ComName.Text.Trim() == "" && ComOutprice.Text.Trim() == "")
                MessageBox.Show("商品名称与售价不可为空", "注意：");
            else
            {
                SqlConnection objConnection = new SqlConnection();
                objConnection.ConnectionString = ("Server=.;database=mydtbs;integrated security=SSPI");               //链接
                objConnection.Open();

                string Class;
                if (textBoxClass.Text == "")
                    Class = comboBoxClass.Text;
                else Class = textBoxClass.Text;
                                       //录入
                string str = "insert into Commodity (LastDateTime,CommName ,CommInprice ,CommOutprice,Stock,CommForm,Class) values('" + dt +"','" + ComName.Text.Trim()    + "','" + ComIn  + "','" + ComOutprice .Text.Trim() + "','" + Stock .Text.Trim() + "','" +ComForm .Text.Trim () + "','" +Class + "') ";
                SqlCommand objCommand = new SqlCommand(str, objConnection);
                objCommand.CommandText = str;
                objCommand.ExecuteNonQuery();
                //数据备份
                str = "insert into Commodity2 (CommName  ,CommOutprice,Stock) values('"  + ComName.Text.Trim() + "','"  + ComOutprice.Text.Trim() + "','" + Stock.Text.Trim()  + "') ";
                objCommand = new SqlCommand(str, objConnection);
                objCommand.CommandText = str;
                objCommand.ExecuteNonQuery();

                ComName.Clear();
                ComOutprice.Clear();
                ComInprice.Clear();
                ComForm.Clear();
                Stock.Clear();
                MessageBox.Show("商品添加成功", "恭喜：");
            }

        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            ChaK();
        }
/// <summary>
/// 更改
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
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
                da.SelectCommand = new SqlCommand("select * from Commodity", con);

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
                ZaiRu();
            }
        }
/// <summary>
/// 删除
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)                           //删除选定行
        {

               SqlConnection con = new SqlConnection("Server=.;database=mydtbs;integrated security=SSPI");

            DialogResult dr=MessageBox.Show("信息一旦删除无法恢复，确定删除吗？", "删除警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if(dr == DialogResult.OK)
          { try
                {

                    con.Open();
                    int i = this.dataGridView1.CurrentRow.Index;
                    string select_id = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();//选择的当前行第一列的值，也就是ID
                    string delete_by_id = "delete from Commodity where ID='" + select_id + "'";//sql删除语句
                    SqlCommand cmd = new SqlCommand(delete_by_id, con);
                    cmd.ExecuteNonQuery();
                    // cmd.EndExecuteNonQuery();
                    MessageBox.Show("删除成功！");
                }
                catch
                {
                    MessageBox.Show("请正确选择行!", "系统提示");
                }
                finally
                {
                    con.Dispose();
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
        void ChaK()
        {
            string str = "Server=.;database=mydtbs;integrated security=SSPI";
            string sql = "select * from Commodity";

            SqlConnection cn = new SqlConnection(str);
            cn.Open();
            SqlDataAdapter da = new SqlDataAdapter(sql, cn);
            DataSet ds = new DataSet();
            da.Fill(ds, "Commodity");
            dataGridView1.DataSource = ds.Tables["Commodity"];
            ZaiRu();
        }
    }
}
