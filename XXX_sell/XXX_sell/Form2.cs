using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XXX_sell
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
/// <summary>
/// 管理
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
           

            FormCommodity fc = new XXX_sell.FormCommodity  ();
            fc.Show();
        }
/// <summary>
/// 销售链接
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
     private void button1_Click(object sender, EventArgs e)
        {
          FormManager  sm = new XXX_sell.FormManager ();
            sm.Show();
        }

        private void 进货ToolStripMenuItem_Click(object sender, EventArgs e)
        {
 FormCommodity fc = new XXX_sell.FormCommodity();
            fc.Show();
        }

        private void 查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void 销售记录查改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SELL se = new XXX_sell.SELL();
            se.Show();
        }
    }
}
