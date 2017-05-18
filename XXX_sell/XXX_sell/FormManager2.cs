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
    public partial class FormManager2 : Form 
    {
        private string buyer;

        public FormManager2()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buyer"></param>

        public FormManager2(string buyer)
        {
            this.buyer = buyer;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
    
      
        private void FormManager2_Load(object sender, EventArgs e)
        {
            string str = "Server=.;database=mydtbs;integrated security=SSPI";  
            //只读取最近一小时的购买清单
               string Read_buyer = "select distinct Buyer from ShoppingList where DATeDIFF(hh,SELLDatetime,'" + DateTime.Now.ToString ()+"')=0";
                 SqlConnection cn = new SqlConnection(str);
            cn.Open();
       
            SqlDataAdapter da2 = new SqlDataAdapter(Read_buyer, cn);
            DataSet ds2 = new DataSet();
            da2.Fill(ds2, "ShoppingList");
            comboBox1.DataSource = ds2.Tables["ShoppingList"];
            comboBox1.DisplayMember = "Buyer";

            //-----------------------------------------------------------
            //string Slist = "select id, CommName,SellPrice,SellCount from SELL " ;
            // string Slist = "select id, CommName,SellPrice,SellCount from ShoppingList where Buyer='" + comboBox1.Text + "'";
            string Slist = "select  CommName,SellPrice,SellCount from ShoppingList where Buyer='" + comboBox1.Text + "'";

            SqlDataAdapter da = new SqlDataAdapter(Slist, cn);
            DataSet ds = new DataSet();
            da.Fill(ds, "ShoppingList");
            dataGridView1.DataSource = ds.Tables["ShoppingList"];

            //-----------------------------------------------------
          
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
          /*   
           *   
           * string str = "Server=.;database=mydtbs;integrated security=SSPI";
             SqlConnection cn = new SqlConnection(str);
            cn.Open();   
             string Read_Buyer = "UPDATE Commodity SET stock -=( " + t2 + ") where ID =' " + select_id + "'";//修改语句
            SqlCommand cmd = new SqlCommand(Read_Buyer, cn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
              cmbChannelSelect.Items.Clear();//清空ComBox 
                while (dr.Read())
                {
                    cmbChannelSelect.Items.Add(dr[0].ToString());//循环读区数据 
                }
            }
             */
                


        }
        /// <summary>
        /// 总计
        /// </summary>
        /// <returns></returns>
        double Money()
        {
            double money;
            money = 0.0;
            for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
                money += (Convert.ToDouble( dataGridView1.Rows[i].Cells[1].Value.ToString())* Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value.ToString()) );
                ;


            return money;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       void  Money2()
        {
            double money2,m2;
            label5.Text = "总计应付：" + Money() + "元";
            if (textBox1.Text == "") m2 = 0.0;
            else   m2 = Convert.ToDouble(textBox1.Text);
            
            money2 = m2-Money();
            if(money2<0)
            label7.Text = "还差：" + -money2 + "元!";
else
            label7.Text = "需找零：" + money2 + "元";

        }



        private void button1_Click(object sender, EventArgs e)
        {
            ReNew();
            Money2();
            textBoxMount.Clear();
            textBoxPrice.Clear();
          
        }
      void ReNew()
        {
            string str = "Server=.;database=mydtbs;integrated security=SSPI";
            SqlConnection cn = new SqlConnection(str);
            cn.Open();
            string Slist = "select  CommName,SellPrice,sum ( SellCount )SellCount from ShoppingList where Buyer='" + comboBox1.Text + "' group by  CommName,SellPrice";

          //  string Buyer = comboBox1.Text, SellName = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();

            SqlDataAdapter da = new SqlDataAdapter(Slist, cn);
            DataSet ds = new DataSet();
            da.Fill(ds, "ShoppingList");
            dataGridView1.DataSource = ds.Tables["ShoppingList"];
            Money2();
        }
/// <summary>
/// 加减
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string Mount = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();//选择的当前行第3列的值，也就是数量
            dataGridView1.SelectedRows[0].Cells[2].Value = Convert.ToInt32  (Mount)+1;
            
            textBoxMount.Text =  dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            Money2();
        }
        private void buttonDed_Click(object sender, EventArgs e)
        {
            string Mount = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();//选择的当前行第4列的值，也就是数量
            dataGridView1.SelectedRows[0].Cells[2].Value = Convert.ToInt32(Mount) - 1;

            textBoxMount.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            Money2();
        }

 

    /// <summary>
    /// 
    /// </summary>
        void ChangeMount()
        {


        }
/// <summary>
/// 保存修改
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            string str = "Server=.;database=mydtbs;integrated security=SSPI";
           
            SqlConnection cn = new SqlConnection(str);
            try
            {
                cn.Open();
                string Buyer = comboBox1.Text, SellName = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();

                string Update_by_Stock = "UPDATE ShoppingList SET SellPrice= " + textBoxPrice.Text.ToString() + " where  CommName ='" + SellName + "'and Buyer='" + Buyer + "'";///修改语句
                string Update_by_Stock2 = "UPDATE ShoppingList SET SellCount= " + textBoxMount.Text.ToString() + " where  CommName ='" + SellName + "'and Buyer='" + Buyer + "'";///修改语句

                SqlCommand cmd = new SqlCommand(Update_by_Stock + Update_by_Stock2, cn);
                cmd.ExecuteNonQuery();
                ReNew();
                MessageBox.Show("修改成功");
                Money2();
            }
            catch { MessageBox.Show("修改出现异常！", "警告"); }
            finally { cn.Close();  }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            try
            {
                string Price = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();//选择的当前行第3列的值，也就是价格
                textBoxPrice.Text = Price;
                string Mount = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();//选择的当前行第4列的值，也就是数量
                textBoxMount.Text = Mount;
                Money2();
            }
            catch { MessageBox.Show("异常", " 警告："); }

        }

       private void textBoxPrice_TextChanged(object sender, EventArgs e)
        {
          //  dataGridView1.SelectedRows[0].Cells[2].Value = textBoxPrice.ToString();  
        }

        private void textBoxMount_TextChanged(object sender, EventArgs e)
        {
            //dataGridView1.SelectedRows[0].Cells[3].Value = textBoxMount.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string str = "Server=.;database=mydtbs;integrated security=SSPI";

            SqlConnection cn = new SqlConnection(str);
            cn.Open();

        
                try
                {
                    string Shanchu = "Delete From ShoppingList   where CommName ='" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + "'";///修改语句


                    SqlCommand cmd = new SqlCommand(Shanchu, cn);
                    cmd.ExecuteNonQuery();
                    ReNew();
                    MessageBox.Show("删除成功");
                    Money2();
                }
                catch { MessageBox.Show("无数据可删", " 警告："); }
                finally { cn.Close(); }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormManager sm = new XXX_sell.FormManager();
            sm.Show();
        }
/// <summary>
/// 确认支付
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection("Server=.;database=mydtbs;integrated security=SSPI");
            try
            {
                con.Open();
                DateTime DT = System.DateTime.Now;          //时间
                string dt = System.DateTime.Now.ToString();
                string Buyer = comboBox1.Text;
               //存入数据
                for (int i=0;i<dataGridView1.RowCount-1;i++   )
                {
                   // string select_id = dataGridView1.Rows[i].Cells[0].Value.ToString();//选择的当前行第一列的值，也就是ID
                    string count = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    string op = dataGridView1.Rows[i].Cells[1].Value.ToString();
                    string na = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    string Update_by_Stock = "UPDATE Commodity SET stock -=( "+ count +" ) where CommName ='" + na + "'";//修改语句
                    SqlCommand cmd = new SqlCommand(Update_by_Stock, con);
                    cmd.ExecuteNonQuery();
                    //记录销售                    //////////////////////////////////////////////////////////
                    //  Jl(na, op, t2);

                    string str = "insert into SELL(SellDatetime,CommName ,SellPrice ,SellCount,Buyer) values('" + dt + "','" + na + "','" + op + "','"+count+" ','" + Buyer + "') ";
                    SqlCommand objCommand = new SqlCommand(str, con);
                    objCommand.ExecuteNonQuery();

             }
                string str2 = " Delete  From ShoppingList  where Buyer='"+Buyer+"'";
                SqlCommand objCommand3 = new SqlCommand(str2, con);
                objCommand3.ExecuteNonQuery();


            }
            catch
            {
                MessageBox.Show("请正确选择行!");
            }
            finally
            {
                con.Dispose();
                MessageBox.Show("购买成功，欢迎下次光临！","恭喜");
                this.Close();
            }


      


        }

        private void label5_Click(object sender, EventArgs e)
        {
            Money2();
        }

        //DataGridViewCell的OnLeave
    }
}
