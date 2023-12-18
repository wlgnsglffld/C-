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
    public partial class Credit : Form
    {
        private double sum;
        private int waitNumber;

        public Credit(double sum, int waitNumber = 0)
        {
            InitializeComponent();
            this.sum = sum;
            this.waitNumber = waitNumber;
        }

        private void Credit_Load(object sender, EventArgs e)
        {
            label3.Text = sum.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 대기번호 카운트를 증가시키지 않고 폼을 닫습니다.
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double credit = double.Parse(textBox1.Text);
            if (credit >= sum)
            {
                MessageBox.Show("거스름돈: " + (credit - sum) + "\n결제가 완료되었습니다.");

                // 대기번호를 보여주는 MessageBox를 띄웁니다.
                MessageBox.Show("대기번호: " + waitNumber + "번입니다.");

                // 대기번호 카운트를 증가시킵니다.
                waitNumber++;

                // DialogResult를 OK로 설정하여 폼을 닫습니다.
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("입력한 값이 부족합니다.");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
