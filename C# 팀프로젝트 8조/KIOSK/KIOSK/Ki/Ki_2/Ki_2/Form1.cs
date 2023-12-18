using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Ki_2
{
    public partial class Form1 : Form
    {
        private OrderF orderf;
        public Form1()
        {
            InitializeComponent();
        }

        public double Cost_of_Item()
        {
            // 주문 내역의 총 가격을 계산하여 반환하는 메서드입니다.
            double sum = 0;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                sum += Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value);
            }

            return sum;
        }

        Bitmap bitmap;

        private void button17_Click(object sender, EventArgs e) // 영수증 출력 버튼
        {
            try
            {
                // 주문 내역을 비트맵 이미지로 그려서 출력하는 메서드입니다.
                int height = dataGridView1.Height;
                dataGridView1.Height = dataGridView1.RowCount * dataGridView1.RowTemplate.Height * 2;
                bitmap = new Bitmap(dataGridView1.Width, dataGridView1.Height);
                dataGridView1.DrawToBitmap(bitmap, new Rectangle(0, 0, dataGridView1.Width, dataGridView1.Height));
                printPreviewDialog1.PrintPreviewControl.Zoom = 1;
                printPreviewDialog1.ShowDialog();
                dataGridView1.Height = height;
                dataGridView1.Rows.Clear(); //데이터 초기화
                //int height = dataGridView1.Height;: 현재 DataGridView의 높이를 저장합니다. 나중에 인쇄 이후에 이 높이로 다시 설정하기 위해 필요합니다.
                //dataGridView1.Height = dataGridView1.RowCount * dataGridView1.RowTemplate.Height * 2;: DataGridView의 높이를 행(row)의 개수와 행 템플릿(row template)의 높이를 곱한 값으로 설정합니다.이렇게 함으로써 DataGridView의 모든 행이 인쇄되도록 높이를 조정합니다.
                //bitmap = new Bitmap(dataGridView1.Width, dataGridView1.Height);: DataGridView와 동일한 크기의 비트맵(Bitmap) 객체를 생성합니다. 이 비트맵은 DataGridView를 그리기 위해 사용될 것입니다.
                //dataGridView1.DrawToBitmap(bitmap, new Rectangle(0, 0, dataGridView1.Width, dataGridView1.Height));: DataGridView를 이전에 생성한 비트맵에 그립니다.이를 통해 DataGridView의 내용을 비트맵으로 캡처할 수 있습니다.
                //printPreviewDialog1.PrintPreviewControl.Zoom = 1;: 인쇄 미리보기 대화상자의 줌(확대/ 축소) 수준을 1로 설정합니다. 이는 기본적으로 100 % 크기로 인쇄 미리보기를 표시하도록 합니다.
                //printPreviewDialog1.ShowDialog();: 인쇄 미리보기 대화상자를 표시합니다. 사용자는 이 대화상자에서 인쇄 설정을 확인하고 인쇄 작업을 시작할 수 있습니다.
                //dataGridView1.Height = height;: 이전에 저장한 DataGridView의 원래 높이로 다시 설정합니다.인쇄 작업 후에 DataGridView를 원래 상태로 복원하기 위해 사용됩니다.
                //마지막으로, catch (Exception ex)와 MessageBox.Show(ex.Message); 는 예외(Exception)가 발생할 경우 해당 예외 메시지를 보여주는 부분입니다. 이를 통해 예외가 발생한 경우 사용자에게 오류 메시지를 표시할 수 있습니다.

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                // 영수증을 인쇄할 때 호출되는 이벤트 핸들러입니다.
                e.Graphics.DrawImage(bitmap, 0, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button19_Click(object sender, EventArgs e) // 전체 초기화 버튼
        {
            // 주문 내역을 모두 삭제하는 메서드입니다.
            dataGridView1.Rows.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // "라면" 항목을 주문하는 버튼의 클릭 이벤트 핸들러입니다.
            // 이미 주문 내역에 "라면"이 존재하는 경우 수량을 1 증가시키고 가격을 업데이트하며,
            // 존재하지 않는 경우 새로운 행을 추가합니다.
            bool itemExists = false;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == "라면")
                {
                    double quantity = Convert.ToDouble(row.Cells[1].Value) + 1;
                    row.Cells[1].Value = quantity;
                    row.Cells[2].Value = quantity * 2500; // 가격을 수량 * 2500으로 설정
                    itemExists = true;
                    break;
                }//dataGridView1의 각 행을 반복하면서 foreach 루프가 실행됩니다.
                //현재 반복 중인 행의 첫 번째 셀 값이 null이 아니고 "라면"과 같은지 확인합니다.   
                //"라면" 항목이 있을 경우, 해당 행의 수량을 가져와 1을 더한 후 quantity 변수에 할당합니다.
                //해당 행의 수량 셀(row.Cells[1])에 quantity 값을 설정하여 수량을 업데이트합니다.
                //가격 셀(row.Cells[2])에 quantity * 2500 값을 설정하여 가격을 업데이트합니다. (가격은 수량에 2500을 곱한 값으로 설정됩니다.)
                //itemExists 변수를 true로 설정하여 "라면" 항목이 이미 존재함을 나타냅니다.
                //break 문을 사용하여 더 이상 반복하지 않고 루프를 종료합니다.
                //만약 "라면" 항목이 없는 경우(!itemExists), 새로운 행을 추가합니다.
                //"라면", 수량(1), 초기 가격(2500) 값을 갖는 새로운 행을 dataGridView1에 추가합니다.
                //초기에 itemExists 변수는 false로 설정됩니다.이 변수는 "라면" 항목이 이미 주문 내역에 존재하는지 여부를 나타냅니다.
                //foreach 문을 사용하여 dataGridView1의 각 행을 반복합니다.
                //반복 중인 행의 첫 번째 셀 값이 null이 아니고 "라면"과 같은지 확인합니다.즉, "라면" 항목이 이미 주문 내역에 있는지를 검사합니다.
                //만약 "라면" 항목이 이미 존재한다면, 해당 행의 수량을 가져와 1을 증가시키고, 가격을 업데이트합니다.이후 itemExists 변수를 true로 설정하고 break 문을 사용하여 더 이상 반복하지 않고 루프를 종료합니다.
                //만약 "라면" 항목이 존재하지 않는다면, 새로운 행을 dataGridView1에 추가합니다. "라면", 수량(1), 초기 가격(2500) 값을 갖는 새로운 행이 추가됩니다.
                //AddCost() 메서드를 호출하여 가격을 업데이트합니다. 이 부분의 코드는 주어지지 않았으므로, AddCost() 메서드의 내용에 따라 가격이 업데이트됩니다.
            }

            if (!itemExists)
            {
                double quantity = 1;
                double itemCost = 2500; // 초기 가격을 2500으로 설정
                dataGridView1.Rows.Add("라면", quantity, itemCost);
            }

            AddCost();
        }

        public double TotalAmount()
        {
            double total = 0;

            // 데이터 그리드 뷰의 각 행에서 가격 정보를 가져와 총 가격을 계산합니다.
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                total += Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value);
            }

            return total;
        }

        private void AddCost()
        {
            // 주문 내역의 가격을 업데이트하는 메서드입니다.

            double totalCost = TotalAmount();

            if (dataGridView1.Rows.Count > 0)
            {
                // 마지막 행의 가격 셀을 업데이트합니다.
                int lastRowIndex = dataGridView1.Rows.Count - 1;
                dataGridView1.Rows[lastRowIndex].Cells[2].Value = totalCost;
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            // "카레라면"을 주문하는 버튼의 클릭 이벤트 핸들러입니다.

            bool itemExists = false;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == "카레라면")
                {
                    // 이미 주문 내역에 "카레라면"이 존재하는 경우 수량을 증가시키고 가격을 업데이트합니다.
                    double quantity = Convert.ToDouble(row.Cells[1].Value) + 1;
                    row.Cells[1].Value = quantity;
                    row.Cells[2].Value = quantity * 3000;
                    itemExists = true;
                    break;
                }
            }

            if (!itemExists)
            {
                // 주문 내역에 "카레라면"이 존재하지 않는 경우 새로운 행을 추가합니다.
                double quantity = 1;
                double itemCost = 3000;
                dataGridView1.Rows.Add("카레라면", quantity, itemCost);
            }

            // 주문 내역의 가격을 업데이트합니다.
            AddCost();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            // "치즈라면"을 주문하는 버튼의 클릭 이벤트 핸들러입니다.

            bool itemExists = false;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == "치즈라면")
                {
                    // 이미 주문 내역에 "치즈라면"이 존재하는 경우 수량을 증가시키고 가격을 업데이트합니다.
                    double quantity = Convert.ToDouble(row.Cells[1].Value) + 1;
                    row.Cells[1].Value = quantity;
                    row.Cells[2].Value = quantity * 3000;
                    itemExists = true;
                    break;
                }
            }

            if (!itemExists)
            {
                // 주문 내역에 "치즈라면"이 존재하지 않는 경우 새로운 행을 추가합니다.
                double quantity = 1;
                double itemCost = 3000;
                dataGridView1.Rows.Add("치즈라면", quantity, itemCost);
            }

            // 주문 내역의 가격을 업데이트합니다.
            AddCost();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // "제육덮밥"을 주문하는 버튼의 클릭 이벤트 핸들러입니다.

            bool itemExists = false;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == "제육덮밥")
                {
                    // 이미 주문 내역에 "제육덮밥"이 존재하는 경우 수량을 증가시키고 가격을 업데이트합니다.
                    double quantity = Convert.ToDouble(row.Cells[1].Value) + 1;
                    row.Cells[1].Value = quantity;
                    row.Cells[2].Value = quantity * 4500;
                    itemExists = true;
                    break;
                }
            }

            if (!itemExists)
            {
                // 주문 내역에 "제육덮밥"이 존재하지 않는 경우 새로운 행을 추가합니다.
                double quantity = 1;
                double itemCost = 4500;
                dataGridView1.Rows.Add("제육덮밥", quantity, itemCost);
            }

            // 주문 내역의 가격을 업데이트합니다.
            AddCost();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // "치킨마요 덮밥"을 주문하는 버튼의 클릭 이벤트 핸들러입니다.

            bool itemExists = false;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == "치킨마요 덮밥")
                {
                    // 이미 주문 내역에 "치킨마요 덮밥"이 존재하는 경우 수량을 증가시키고 가격을 업데이트합니다.
                    double quantity = Convert.ToDouble(row.Cells[1].Value) + 1;
                    row.Cells[1].Value = quantity;
                    row.Cells[2].Value = quantity * 4500;
                    itemExists = true;
                    break;
                }
            }

            if (!itemExists)
            {
                // 주문 내역에 "치킨마요 덮밥"이 존재하지 않는 경우 새로운 행을 추가합니다.
                double quantity = 1;
                double itemCost = 4500;
                dataGridView1.Rows.Add("치킨마요 덮밥", quantity, itemCost);
            }

            // 주문 내역의 가격을 업데이트합니다.
            AddCost();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // "참치마요 덮밥"을 주문하는 버튼의 클릭 이벤트 핸들러입니다.

            bool itemExists = false;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == "참치마요 덮밥")
                {
                    // 이미 주문 내역에 "참치마요 덮밥"이 존재하는 경우 수량을 증가시키고 가격을 업데이트합니다.
                    double quantity = Convert.ToDouble(row.Cells[1].Value) + 1;
                    row.Cells[1].Value = quantity;
                    row.Cells[2].Value = quantity * 4500;
                    itemExists = true;
                    break;
                }
            }

            if (!itemExists)
            {
                // 주문 내역에 "참치마요 덮밥"이 존재하지 않는 경우 새로운 행을 추가합니다.
                double quantity = 1;
                double itemCost = 4500;
                dataGridView1.Rows.Add("참치마요 덮밥", quantity, itemCost);
            }

            // 주문 내역의 가격을 업데이트합니다.
            AddCost();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            // "돌솥 비빔밥"을 주문하는 버튼의 클릭 이벤트 핸들러입니다.

            bool itemExists = false;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == "돌솥 비빔밥")
                {
                    // 이미 주문 내역에 "돌솥 비빔밥"이 존재하는 경우 수량을 증가시키고 가격을 업데이트합니다.
                    double quantity = Convert.ToDouble(row.Cells[1].Value) + 1;
                    row.Cells[1].Value = quantity;
                    row.Cells[2].Value = quantity * 5000;
                    itemExists = true;
                    break;
                }
            }

            if (!itemExists)
            {
                // 주문 내역에 "돌솥 비빔밥"이 존재하지 않는 경우 새로운 행을 추가합니다.
                double quantity = 1;
                double itemCost = 5000;
                dataGridView1.Rows.Add("돌솥 비빔밥", quantity, itemCost);
            }

            // 주문 내역의 가격을 업데이트합니다.
            AddCost();
        }


        private void button8_Click(object sender, EventArgs e)
        {
            // "간장 계란밥"을 주문하는 버튼의 클릭 이벤트 핸들러입니다.

            bool itemExists = false;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == "간장 계란밥")
                {
                    // 이미 주문 내역에 "간장 계란밥"이 존재하는 경우 수량을 증가시키고 가격을 업데이트합니다.
                    double quantity = Convert.ToDouble(row.Cells[1].Value) + 1;
                    row.Cells[1].Value = quantity;
                    row.Cells[2].Value = quantity * 4000;
                    itemExists = true;
                    break;
                }
            }

            if (!itemExists)
            {
                // 주문 내역에 "간장 계란밥"이 존재하지 않는 경우 새로운 행을 추가합니다.
                double quantity = 1;
                double itemCost = 4000;
                dataGridView1.Rows.Add("간장 계란밥", quantity, itemCost);
            }

            // 주문 내역의 가격을 업데이트합니다.
            AddCost();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // "고추장 계란밥"을 주문하는 버튼의 클릭 이벤트 핸들러입니다.

            bool itemExists = false;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == "고추장 계란밥")
                {
                    // 이미 주문 내역에 "고추장 계란밥"이 존재하는 경우 수량을 증가시키고 가격을 업데이트합니다.
                    double quantity = Convert.ToDouble(row.Cells[1].Value) + 1;
                    row.Cells[1].Value = quantity;
                    row.Cells[2].Value = quantity * 4000;
                    itemExists = true;
                    break;
                }
            }

            if (!itemExists)
            {
                // 주문 내역에 "고추장 계란밥"이 존재하지 않는 경우 새로운 행을 추가합니다.
                double quantity = 1;
                double itemCost = 4000;
                dataGridView1.Rows.Add("고추장 계란밥", quantity, itemCost);
            }

            // 주문 내역의 가격을 업데이트합니다.
            AddCost();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            // "돈까스"를 주문하는 버튼의 클릭 이벤트 핸들러입니다.

            bool itemExists = false;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == "돈까스")
                {
                    // 이미 주문 내역에 "돈까스"가 존재하는 경우 수량을 증가시키고 가격을 업데이트합니다.
                    double quantity = Convert.ToDouble(row.Cells[1].Value) + 1;
                    row.Cells[1].Value = quantity;
                    row.Cells[2].Value = quantity * 3500;
                    itemExists = true;
                    break;
                }
            }

            if (!itemExists)
            {
                // 주문 내역에 "돈까스"가 존재하지 않는 경우 새로운 행을 추가합니다.
                double quantity = 1;
                double itemCost = 3500;
                dataGridView1.Rows.Add("돈까스", quantity, itemCost);
            }

            // 주문 내역의 가격을 업데이트합니다.
            AddCost();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            // "치즈 돈까스"를 주문하는 버튼의 클릭 이벤트 핸들러입니다.

            bool itemExists = false;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == "치즈 돈까스")
                {
                    // 이미 주문 내역에 "치즈 돈까스"가 존재하는 경우 수량을 증가시키고 가격을 업데이트합니다.
                    double quantity = Convert.ToDouble(row.Cells[1].Value) + 1;
                    row.Cells[1].Value = quantity;
                    row.Cells[2].Value = quantity * 4000;
                    itemExists = true;
                    break;
                }
            }

            if (!itemExists)
            {
                // 주문 내역에 "치즈 돈까스"가 존재하지 않는 경우 새로운 행을 추가합니다.
                double quantity = 1;
                double itemCost = 4000;
                dataGridView1.Rows.Add("치즈 돈까스", quantity, itemCost);
            }

            // 주문 내역의 가격을 업데이트합니다.
            AddCost();
        }


        private void button10_Click(object sender, EventArgs e)
        {
            // "고구마 돈까스"를 주문하는 버튼의 클릭 이벤트 핸들러입니다.

            bool itemExists = false;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == "고구마 돈까스")
                {
                    // 이미 주문 내역에 "고구마 돈까스"가 존재하는 경우 수량을 증가시키고 가격을 업데이트합니다.
                    double quantity = Convert.ToDouble(row.Cells[1].Value) + 1;
                    row.Cells[1].Value = quantity;
                    row.Cells[2].Value = quantity * 4000;
                    itemExists = true;
                    break;
                }
            }

            if (!itemExists)
            {
                // 주문 내역에 "고구마 돈까스"가 존재하지 않는 경우 새로운 행을 추가합니다.
                double quantity = 1;
                double itemCost = 4000;
                dataGridView1.Rows.Add("고구마 돈까스", quantity, itemCost);
            }

            // 주문 내역의 가격을 업데이트합니다.
            AddCost();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            // "콜라"를 주문하는 버튼의 클릭 이벤트 핸들러입니다.

            bool itemExists = false;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == "콜라")
                {
                    // 이미 주문 내역에 "콜라"가 존재하는 경우 수량을 증가시키고 가격을 업데이트합니다.
                    double quantity = Convert.ToDouble(row.Cells[1].Value) + 1;
                    row.Cells[1].Value = quantity;
                    row.Cells[2].Value = quantity * 1000;
                    itemExists = true;
                    break;
                }
            }

            if (!itemExists)
            {
                // 주문 내역에 "콜라"가 존재하지 않는 경우 새로운 행을 추가합니다.
                double quantity = 1;
                double itemCost = 1000;
                dataGridView1.Rows.Add("콜라", quantity, itemCost);
            }

            // 주문 내역의 가격을 업데이트합니다.
            AddCost();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            // "사이다"를 주문하는 버튼의 클릭 이벤트 핸들러입니다.

            bool itemExists = false;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == "사이다")
                {
                    // 이미 주문 내역에 "사이다"가 존재하는 경우 수량을 증가시키고 가격을 업데이트합니다.
                    double quantity = Convert.ToDouble(row.Cells[1].Value) + 1;
                    row.Cells[1].Value = quantity;
                    row.Cells[2].Value = quantity * 1000;
                    itemExists = true;
                    break;
                }
            }

            if (!itemExists)
            {
                // 주문 내역에 "사이다"가 존재하지 않는 경우 새로운 행을 추가합니다.
                double quantity = 1;
                double itemCost = 1000;
                dataGridView1.Rows.Add("사이다", quantity, itemCost);
            }

            // 주문 내역의 가격을 업데이트합니다.
            AddCost();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            // "물"을 주문하는 버튼의 클릭 이벤트 핸들러입니다.

            bool itemExists = false;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == "물")
                {
                    // 이미 주문 내역에 "물"이 존재하는 경우 수량을 증가시키고 가격을 업데이트합니다.
                    double quantity = Convert.ToDouble(row.Cells[1].Value) + 1;
                    row.Cells[1].Value = quantity;
                    row.Cells[2].Value = quantity * 500;
                    itemExists = true;
                    break;
                }
            }

            if (!itemExists)
            {
                // 주문 내역에 "물"이 존재하지 않는 경우 새로운 행을 추가합니다.
                double quantity = 1;
                double itemCost = 500;
                dataGridView1.Rows.Add("물", quantity, itemCost);
            }

            // 주문 내역의 가격을 업데이트합니다.
            AddCost();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            // 선택된 행을 삭제
            foreach (DataGridViewRow item in dataGridView1.SelectedRows)
            {
                if (!item.IsNewRow)
                {
                    dataGridView1.Rows.RemoveAt(item.Index);
                }
            }

            AddCost(); // 삭제 후 Total 값 업데이트
        }


        private void button16_Click(object sender, EventArgs e)// 결제폼 띄우는 버튼
        {
            
            double sum = Cost_of_Item(); //Cost_of_Item의 값을 sum에 저장
            OrderF order = new OrderF();
            Credit credit= new Credit(sum);
            order.SetSumValue(sum);
            order.Show();
            //Cost_of_Item() 메서드를 호출하여 항목의 총 가격을 계산하고, 그 값을 sum 변수에 저장합니다.
            //OrderF 객체와 Credit 객체를 생성합니다.OrderF는 주문 폼을 나타내는 클래스이고, Credit는 결제에 필요한 정보를 입력받는 폼을 나타내는 클래스입니다.
            //order.SetSumValue(sum)을 사용하여 OrderF 객체의 SetSumValue() 메서드를 호출하여 총 가격을 설정합니다.이를 통해 주문 폼에 총 가격 정보가 전달됩니다.
            //order.Show()를 호출하여 OrderF 객체를 화면에 표시합니다. 이를 통해 주문 폼이 사용자에게 보여집니다.
        }
            
        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }
    }
}