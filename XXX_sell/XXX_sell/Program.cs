using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace XXX_sell
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
    //  Application.Run(new Form1());
          Application.Run(new Form2());

        
        }
    }
    /// <summary>
    /// 等待加载
    /// </summary>
    public partial class WaitForm : Form
    {
        public string sValue = "加载数据 请稍等.......";
        private Cursor currentCursor;
        private Form FatherForm;

        public WaitForm()
        {
            //InitializeComponent();  
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new System.Drawing.Size(440, 80);
            this.ControlBox = false;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.Paint += new PaintEventHandler(WaitDialogPaint);
        }


        public void Show(string sValue)
        {
            this.sValue = sValue;
            this.Show();

        }
        public void Show()
        {
            currentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            base.Show();
            this.Refresh();
        }
        public void Refresh(string sValue)
        {
            this.sValue = sValue;
            this.Refresh();
        }
        public void Close()
        {
            Cursor.Current = currentCursor;
            base.Close();
        }
        private void WaitDialogPaint(object sender, PaintEventArgs e)
        {
            Rectangle r = e.ClipRectangle;
            r.Inflate(-1, -1);
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            ControlPaint.DrawBorder3D(e.Graphics, r, Border3DStyle.RaisedInner);
            e.Graphics.DrawString(sValue, new Font("Arial", 9, FontStyle.Regular), SystemBrushes.WindowText, r, sf);
        }
    }


}

