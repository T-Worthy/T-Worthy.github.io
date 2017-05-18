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
    public partial class SELL : Form
    {
        public SELL()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)  
        {

        }
/// <summary>
///  //查询记录
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            ChaK();
           

        }
/// <summary>
/// //修改记录
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
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
                da.SelectCommand = new SqlCommand("select * from SELL", con);

                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                da.UpdateCommand = builder.GetUpdateCommand();
                try
                {
                    //更新数据表数据时
                    da.Update(table);
                    table.AcceptChanges();
                    //重载
                    DataSet ds = new DataSet();
                    da.Fill(ds, "SELL");
                    dataGridView1.DataSource = ds.Tables["SELL"];
                    MessageBox.Show("更新成功", "系统消息");

                }

                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
/// <summary>
///  //删除记录
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)             
        {
            SqlConnection con = new SqlConnection("Server=.;database=mydtbs;integrated security=SSPI");

            DialogResult dr = MessageBox.Show("信息一旦删除无法恢复，确定删除吗？", "删除警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                try
                {

                    con.Open();
                    int i = this.dataGridView1.CurrentRow.Index;
                    string select_id = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();//选择的当前行第一列的值，也就是ID
                    string delete_by_id = "delete from SELL where ID='" + select_id + "'";//sql删除语句
                    SqlCommand cmd = new SqlCommand(delete_by_id, con);
                    cmd.ExecuteNonQuery();
                    // cmd.EndExecuteNonQuery();
                    MessageBox.Show("删除成功！");
                }
                catch
                {
                    MessageBox.Show("请正确选择行!");
                }
                finally
                {
                    con.Dispose();
                }
            }
        }
/// <summary>
/// 按日查询
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy-MM-dd";
            string t1 = dateTimePicker1.Text;
            string t2 = " 00:00:00";
            string tt = "dd";
            TM(t1 ,t2,tt);
        }
/// <summary>
/// 按月查询
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy-MM-dd";
            string t1 = dateTimePicker1.Text;
            string t2 = " 00:00:00";
            string tt = "MM";
            TM(t1, t2,tt );
        }
        /// <summary>
        /// 
        /// </summary>
        void TM(string t1, string t2,string tt)
        {
         
           
            // string t3 = " 23:59:59";



            string str = "Server=.;database=mydtbs;integrated security=SSPI";
            string sql = "SELECT *  from SELL  where DATEDIFF ("+tt+",Sell.SellDatetime,'" + t1 +t2+ "')=0";

            SqlConnection cn = new SqlConnection(str);
            cn.Open();
            SqlDataAdapter da = new SqlDataAdapter(sql, cn);
            DataSet ds = new DataSet();
            da.Fill(ds, "SELL");
            dataGridView1.DataSource = ds.Tables["SELL"];
        }
/// <summary>
/// 统计显示行的利润总数
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
           
            double b=0.0;
           double sum=0.0;
            int i=0;

           string c = dataGridView1.RowCount.ToString();
            double  d = Convert.ToDouble(c );
            int row = (int)d;



             for(;i<row-1;i++)
                { //string  a = dataGridView1.Rows[i].Cells[5].Value.ToString();
                    
                    sum += Convert.ToDouble(dataGridView1.Rows[i].Cells[5].Value.ToString());
            }

            //string T = dataGridView1.Rows[1].Cells[5].Value.ToString();
            //string T = sum.ToString();
            //  b = Convert.ToDouble(T);
            string T2 =sum .ToString ();
            MessageBox.Show("表中总利润为" + T2); 
           // profit.Text =sum.ToString ();

        }
        /// <summary>
        ///载入class格 
        /// </summary> distinct
        void ZaiRu()
        {
            string str = "Server=.;database=mydtbs;integrated security=SSPI";
            string Read_Class = "select distinct Class  from SELL";
            string Read_buyer= "select distinct buyer from SELL";
            string Read_Date = "select distinct SellDatetime from SELL";
            SqlConnection cn = new SqlConnection(str);
            cn.Open();

            SqlDataAdapter da2 = new SqlDataAdapter(Read_Class, cn);
            DataSet ds2 = new DataSet();
            da2.Fill(ds2, "SELL");
            comboBox1.DataSource = ds2.Tables["SELL"];
            comboBox1.DisplayMember = "Class";
           
            SqlDataAdapter dat3 = new SqlDataAdapter(Read_buyer, cn);
            DataSet dst3 = new DataSet();
            dat3.Fill(dst3, "SELL");
            comboBox2.DataSource = dst3.Tables["SELL"];
            comboBox2.DisplayMember = "buyer";

            SqlDataAdapter dat4 = new SqlDataAdapter(Read_Date, cn);
            DataSet dst4 = new DataSet();
            dat4.Fill(dst4, "SELL");
            comboBox3.DataSource = dst4.Tables["SELL"];
            comboBox3.DisplayMember = "SellDatetime";


        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            ZaiRu();
        }
        /// <summary>
        /// 类别查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            string str = "Server=.;database=mydtbs;integrated security=SSPI";
            string sql;

            sql = "select * from SELL where " ;
            string class_c = comboBox1.Text.ToString();
            string buyer = comboBox2.Text.ToString();
                
           if (class_c!=null)
            {sql = sql + " Class='" + class_c + "'"; if (buyer != "")  sql = sql + " and "; }
            if (buyer != "")  sql = sql +"  buyer='" + buyer  + "'";
          


            SqlConnection cn = new SqlConnection(str);
            cn.Open();

            try
            {
               
                SqlDataAdapter da = new SqlDataAdapter(sql, cn);
                DataSet ds = new DataSet();
                da.Fill(ds, "SELL");
                dataGridView1.DataSource = ds.Tables["SELL"];
                //获取原价
                ZaiRu();
            }
            catch
            { MessageBox.Show("请选择正确项"+sql , "系统提示"); }

            finally
            {
                cn.Dispose();
            }
        }
      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           /* switch (checkedListBox1.SelectedValue.ToString())
            {
                case "查询该日记录":;



            }*/
        }

        private void SELL_Load(object sender, EventArgs e)
        {
            ChaK();
        }
        void ChaK()
        {
            string str = "Server=.;database=mydtbs;integrated security=SSPI";
            string sql = "select * from SELL";

            SqlConnection cn = new SqlConnection(str);
            cn.Open();
            SqlDataAdapter da = new SqlDataAdapter(sql, cn);
            DataSet ds = new DataSet();
            da.Fill(ds, "SELL");
            dataGridView1.DataSource = ds.Tables["SELL"];
            //获取原价
            ZaiRu();
        }
    }
   
}
