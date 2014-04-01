using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MineSweeper
{
    public partial class EndForm : Form
    {
        public EndForm(int X, int Y)
        {
            InitializeComponent();
            this.Location= new Point(X, Y);
        }

        public bool Won
        {
            set { if (value) { lbl_won.Visible = true; } else { lbl_lost.Visible = true; } }
        }

        public int Time
        {
            set { lbl_time.Text += value.ToString() + "s"; }
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btn_retry_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Retry;
            this.Close();
        }        
    }
}
