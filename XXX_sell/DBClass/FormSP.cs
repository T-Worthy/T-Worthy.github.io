using System;
using System.Collections;
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
    public partial class FormSP : Form
    {
        public FormSP()
        {
            InitializeComponent();
        }

        private void FormSP_Load(object sender, EventArgs e)
        {
            SqlConnection objConnection = new SqlConnection();
            objConnection.ConnectionString = ("Server=.;database=mydtbs;integrated security=SSPI");               //链接
            objConnection.Open();
            string str = "select   * from [Commodity]   ";               string str2 = "select   * from [SELL]   ";
            SqlDataAdapter sda = new SqlDataAdapter(str ,objConnection ); SqlDataAdapter sda2 = new SqlDataAdapter(str2, objConnection);

            DataSet ds = new DataSet();                                  DataSet ds2 = new DataSet();
            sda.Fill(ds);                                                  sda2.Fill(ds2);
            DataView dv = ds.Tables[0].DefaultView;                   DataView dv2 = ds2.Tables[0].DefaultView;
            //   dv.Sort = "mid desc";
            listBox1.DataSource = ds.Tables[0];                         listBox2.DataSource = ds2.Tables[0];
            listBox1.DisplayMember = "CommName";                      listBox2.DisplayMember = "CommName"; 
            // listBox1.DisplayMember = "CommInput";
            objConnection.Close();  

        /*    Sp.DataSource = ds.();
            Sp.DisplayMember = "CommName";
            Sp.ValueMember = "CommName";
            */

           
        }

    public   class SpM {
           public  string Name;
            public string  Id;
            public string  Inprice;
            public string  Outprice;
            public string  Stock;
            public string Form;


        };

        public static ArrayList GetSp()
        {
            ArrayList al = new ArrayList();

            SqlConnection objConnection = new SqlConnection();
            objConnection.ConnectionString = ("Server=.;database=mydtbs;integrated security=SSPI");               //链接
            try
            {
                objConnection.Open();
                string str = "select   * from [Commodity]   ";
                SqlCommand objCommand = new SqlCommand(str, objConnection);
                using (SqlDataReader dr = objCommand.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        SpM s = new XXX_sell.FormSP.SpM();
                        s.Id = dr["Comnmd"].ToString();
                        s.Name = dr["CommName"].ToString();
                        s.Inprice = dr["CommInput"].ToString();
                        s.Outprice = dr["CommOutprice"].ToString();
                        s.Stock = dr["Stock"].ToString();
                        al.Add(s);
                    }
                }
                objConnection.Dispose();
                objConnection.Close();
            }
            catch (Exception)
            { MessageBox.Show("对不起，链接数据库失败！"); }

            return al;
///----------------------------------------------------------------






        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
