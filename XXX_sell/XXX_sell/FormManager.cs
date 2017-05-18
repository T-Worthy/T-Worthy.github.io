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
    public partial class FormManager : Form 
    {
        public FormManager()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ShoppingL();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormManager_Load(object sender, EventArgs e)
        {
          
            string str = "Server=.;database=mydtbs;integrated security=SSPI";
            string sql = "select ID, CommName,CommOutprice,Stock from Commodity";

            SqlConnection cn = new SqlConnection(str);
            cn.Open();
            SqlDataAdapter da = new SqlDataAdapter(sql, cn);
            DataSet ds = new DataSet();
            da.Fill(ds, "Commodity");
            dataGridView1.DataSource = ds.Tables["Commodity"];
           
        }
        /// <summary>
        ///   //1获取出售价格，数量， 2改变库存，3记录销售记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)             
        {

            SqlConnection con = new SqlConnection("Server=.;database=mydtbs;integrated security=SSPI");
            try
            {
                con.Open();
                                                                 //获取Stockg新值
               // int i = this.dataGridView1.CurrentRow.Index;
             string select_id = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();//选择的当前行第一列的值，也就是ID
                
                string t2 = textBox2.Text.ToString();
                if (t2 == "") t2  = "1";                 //默认一

                string op= dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                string na= dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                string stock= dataGridView1.SelectedRows[0].Cells[3].Value.ToString();

                string pr= textBox1.Text.ToString();
                if(pr=="")pr =op;

                // //确认售出？u

                if (MessageBox.Show("确定要以" + pr + "元的价格出" + t2 + "个售原价为" + op + "的" + na + "吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    int SellCount = Convert.ToInt16(t2);
                    int Stock = Convert.ToInt16(stock);
                    string Update_by_Stock = "";
                    if (SellCount < Stock&&Stock>0)
                        Update_by_Stock = "UPDATE Commodity SET stock -=( " + t2 + ") where ID =' " + select_id + "'";//修改语句
                    else { MessageBox.Show("库存不够！售出失败！", "警告：");  return; }

                    SqlCommand cmd = new SqlCommand(Update_by_Stock, con);
                cmd.ExecuteNonQuery();
                    Cz();
                   
                MessageBox.Show("售出成功！","恭喜：");
                    textBox1.Clear();
                    textBox2.Clear();
                    //记录销售                    //////////////////////////////////////////////////////////
                    //  Jl(na, op, t2);
                    DateTime DT = System.DateTime.Now;          //时间
                    string dt = System.DateTime.Now.ToString();

                    string str = "insert into SELL(SellDatetime,CommName ,SellPrice ,SellCount,Buyer) values('" + dt + "','" + na + "','" + pr + "','" + t2 + "','"  + "过客" + "') ";
                    SqlCommand objCommand = new SqlCommand(str, con );
                      objCommand.ExecuteNonQuery();

                   /* string Profits = "UPDATE SELL SET Profits -=  CommInprice from SELL,Commodity where SELL.CommName=Commodity.CommName and SELL.CommName='" + na + "' ";
                    SqlCommand objCommand2 = new SqlCommand(Profits, con);
                    objCommand2.ExecuteNonQuery();

               

                    MessageBox.Show("销售记录完成", "完成：");*/


                }



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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// //更新视图
        /// </summary>
        void Cz()                    
            {
            string str = "Server=.;database=mydtbs;integrated security=SSPI";
            string sql = "select ID, CommName,CommOutprice,Stock from Commodity ";

            SqlConnection cn = new SqlConnection(str);
            cn.Open();
            SqlDataAdapter da = new SqlDataAdapter(sql, cn);
            DataSet ds = new DataSet();
            da.Fill(ds, "Commodity");
            dataGridView1.DataSource = ds.Tables["Commodity"];

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="na"></param>
        /// <param name="op"></param>
        /// <param name="t2"></param>
        void Jl(string na,string op,string t2 ,string buyer)
        {
          

            DateTime DT = System.DateTime.Now;
            string dt = System.DateTime.Now.ToString();

            SqlConnection objConnection = new SqlConnection();
            objConnection.ConnectionString = ("Server=.;database=mydtbs;integrated security=SSPI");               //链接
            objConnection.Open();


           /* string SellProfit = "Insert into SELL (Profits) Select CommInprice from Commodity  where CommName='"+na+"'";//将利润初始化为进价
            SqlCommand cmd = new SqlCommand(SellProfit, con);
            cmd.ExecuteNonQuery();
            */

            //录入
            string str = "insert into SELL(SellDateTime,CommName ,SellPrice ,SellCount,Buyer) values('" + dt + "','" + na+ "','" +op + "','"+t2+"','"  + buyer + "') "; 
            SqlCommand objCommand = new SqlCommand(str, objConnection);
            objCommand.CommandText = str;
            objCommand.ExecuteNonQuery();
          

        }
   

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
          
            FormManager2 fm2 = new FormManager2();
              fm2.Show();
           
        }
/// <summary>
/// 加入购物车
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            ShoppingL();

        }
        /// <summary>
        /// 加入购物车
        /// </summary>
        /// <param name="id"></param>
        void ShoppingL()
        {
            SqlConnection con = new SqlConnection("Server=.;database=mydtbs;integrated security=SSPI");
            try
            {
                con.Open();
                //获取Stockg新值
                // int i = this.dataGridView1.CurrentRow.Index;
                string select_id = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();//选择的当前行第一列的值，也就是ID

                string t2 = textBox2.Text.ToString();
                if (t2 == "") t2 = "1";                 //默认一

                string op = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                string na = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                string pr = textBox1.Text.ToString();
                string stock = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();

                if (pr == "") pr = op;                //默认价格
                string buyer = textBox3.Text.ToString();
                if (buyer == "")
                { buyer = "匿名"; textBox3.Text = buyer; }//默认买家
                                                 /*   string Update_by_Stock = "UPDATE Commodity SET stock -=( " + t2 + ") where ID =' " + select_id + "'";//修改语句
                                                        SqlCommand cmd = new SqlCommand(Update_by_Stock, con);
                                                        cmd.ExecuteNonQuery();
                                                  */
                Cz();



                //记录销售                    //////////////////////////////////////////////////////////

                DateTime DT = System.DateTime.Now;          //时间
                string dt = System.DateTime.Now.ToString();
                int SellCount = Convert.ToInt16(t2);
                int Stock = Convert.ToInt16(stock);
                string str = "",str2,str3;
                if ((SellCount < Stock) && (Stock > 0))
                { 
                str = "insert into ShoppingList (SellDatetime,CommName ,SellPrice ,SellCount,Buyer) values ('" + dt + "','" + na + "','" + pr + "','" + t2 + "','" + buyer + "')";
                str2 = "   UPDATE ShoppingList SET   SellCount=( select  sum ( SellCount )SellCount from ShoppingList where Buyer='" + buyer + "'and  CommName ='" + na + "' group by  CommName,SellPrice)   where   id=( select min(id) id from ShoppingList where  CommName ='" + na + "'and Buyer='" + buyer + "' )";
                 str3 = "    delete From ShoppingList where not id = (select min(id) id from ShoppingList where  CommName = '" + na + "'and Buyer = '" + buyer + "' )and Buyer='" + buyer + "'and CommName='" + na + "' ";
                SqlCommand objCommand = new SqlCommand(str + str2 + str3, con);
                objCommand.ExecuteNonQuery();
                label3.Text = na + "*" + t2 + " 已加入购物车";
                     }
                else {
                    MessageBox.Show("库存不够！加入购物车失败！需要："+SellCount+" 库存："+stock," 警告：");   textBox1.Clear();
                textBox2.Clear(); return;
                    }
             
                    }
            catch
            {
                MessageBox.Show("请正确选择行!");
            }
            finally
            {
                con.Dispose();
            }
            ReNew2();



        }
  void ReNew2()

          {
            string str = "Server=.;database=mydtbs;integrated security=SSPI";
            SqlConnection cn = new SqlConnection(str);
            cn.Open();
            string Slist = "select  CommName ,SellCount from ShoppingList where Buyer='" + textBox3.Text + "'";
            SqlDataAdapter da = new SqlDataAdapter(Slist, cn);
            DataSet ds = new DataSet();
            da.Fill(ds, "ShoppingList");
            listBox1.DataSource = ds.Tables["ShoppingList"];
            listBox1.DisplayMember = "CommName";
            listBox2.DataSource = ds.Tables["ShoppingList"];
            listBox2.DisplayMember = "SellCount";



        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
  
   
}
