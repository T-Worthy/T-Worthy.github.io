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
    public partial class SP2 : Form
    {
        public SP2()
        {
            InitializeComponent();
        }

        private void SP2_Load(object sender, EventArgs e)
        {
            
            
            // TODO: 这行代码将数据加载到表“myDB.Commodity”中。您可以根据需要移动或删除它。
           // this.TableAdapter.Fill(this.myDB.Commodity);

        }
        DataSet ds = new DataSet();
        SqlDataAdapter sda;
        DataTable DT = new DataTable();
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            

        SqlConnection objConnection = new SqlConnection();
            objConnection.ConnectionString = ("Server=.;database=mydtbs;integrated security=SSPI");               //链接
            objConnection.Open();
            try
            {
                //string str = "select   * from Commodity   ";
                string str = "select   * from table2";
                // SqlCommand objCommand =
                sda = new SqlDataAdapter(str, objConnection);
                sda .SelectCommand= new SqlCommand(str, objConnection);
                // DT  = new DataTable("Commodity");
                //ds.Tables.Add("Commodity");
                DT = new DataTable("table2");
                ds.Tables.Add("table2");
                //  objCommand.CommandType = CommandType.Text;
                // sda.SelectCommand = objCommand;
                sda.Fill(DT);
                //  sda.Fill(dt);"Commodity"
                sda.Fill(ds, "table2");
                dataGridView1.DataSource = ds.Tables ["table2"]  ;
            }
            catch (Exception)
            { MessageBox.Show("对不起，链接出错！"); }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("确定要将修改内容保存到数据库中吗？", "修改提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                /* SqlConnection objConnection = new SqlConnection();
                 objConnection.ConnectionString = ("Server=.;database=mydtbs;integrated security=SSPI");               //链接
                 objConnection.Open();
                 string str = "select   * from Commodity   ";
                 SqlCommand objCommand = new SqlCommand(str, objConnection);

                 sda = new SqlDataAdapter(str, objConnection);
                 sda.UpdateCommand = objCommand;



                 sda.Fill(ds, "Commodity");

                 SqlCommandBuilder  sb= new SqlCommandBuilder(sda);
                 sda.TableMappings.Add("AM","Commpdity");
                //sda.Update(DT );
                   sda.Update(ds);
                // sda.Update(DT.GetChanges());
                 //使DataTable保存更新
              //  DT.AcceptChanges();
                 MessageBox.Show("修改成功", "成功提示"); 
 */
                try
                {
                    DataSet changeData = this.ds .GetChanges();
                if (changeData != null)
                {
                    int changedRows = this.sda.Update(changeData);
                    MessageBox.Show("数据成功更新" + changedRows + "了条记录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ds .AcceptChanges();
                }
                else
                {
                    MessageBox.Show("没有要更新的记录", "没有改变", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception E)
            {
                MessageBox.Show("更新数据库时发生错误：" + E.Message + "", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.ds .RejectChanges();
            }

        }

    }

        private void myDBBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

    /* private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection objConnection = new SqlConnection();
            objConnection.ConnectionString = ("Server=.;database=mydtbs;integrated security=SSPI");               //链接
            objConnection.Open();
            string str = "delect *from Commodity where CommName='  "+ds.Tables [0].Rows[0][""].ToString ()  +"'";
            SqlCommand objCommand = new SqlCommand(str, objConnection);
            objCommand.ExecuteNonQuery();
            MessageBox.Show("删除成功");
            objConnection.Close();
          
         //////////////////////////////////////////////////////////////////////////////////////////////

       
            SqlConnection objConnection = new SqlConnection();
            objConnection.ConnectionString = ("Server=.;database=mydtbs;integrated security=SSPI");               //链接
            objConnection.Open();
            //从dataset中删除所选定行的数据：

            ds.Tables[0].Rows[this.dataGridView1.SelectedRows[0].Index].Delete();

           

            //第二步：［自动生成SQL语句］

            //自动生成sql语句且自动执行[靠update()]

            SqlCommandBuilder scb = new SqlCommandBuilder(sda);

            sda.Update(ds);

            MessageBox.Show("删除成功!");

            //第三步：［清空DataSet中的数据，因其与控件关联，所以控件中的数据也会被清除］

            //清空dataset数据集中表的所有数据

            ds.Tables[0].Clear();

            //第四步：重新加载数据

            //重新查询数据库，将数据重新绑定至dataset数据集

            sda = new SqlDataAdapter("select   * from Commodity   ", objConnection);

            sda.Fill(ds);

            this.dataGridView1.DataSource = ds.Tables[0];

        }
    }*/
}
