using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ki_2
{
    public partial class OrderF : Form
    {
        private int waitingCount = 0;
        public OrderF()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void SetSumValue(double sum)
        {
            label2.Text = (sum / 2).ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                Credit credit = new Credit(double.Parse(label2.Text));
                credit.ShowDialog();
                if(credit != null)
                {
                    this.Close();
                }
            }
            if (radioButton2.Checked)
            {
                MessageBox.Show("결제 되었습니다.");
                this.Close();
            }
            
            
        }
    }
}