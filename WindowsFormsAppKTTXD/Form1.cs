using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace WindowsFormsAppKTTXD
{
    public partial class Form1 : Form
    {
        public string[] part;
        //public LinkedList<int> linkedList;
        public List<string> list;
        public Graph g;
        public string[] tempString;
        public int[] tempPosition;
        //public int[] graph;
        public int m_Row;
        public int m_Column;

        public Form1()
        {
            InitializeComponent();
        }
        public void generateNewForm()
        {
            Form1 NewForm = new Form1();
            NewForm.Show();
            this.Dispose(false);
        }
        //public void showLinkedList(LinkedList<int> linkedList)
        //{
        //    node = linkedList.First;
        //    MessageBox.Show(linkedList.Count.ToString()); //0-7 
        //    while (node != linkedList.Last)
        //    {
        //        MessageBox.Show(node.Value.ToString());
        //        node = node.Next;
        //    }
        //    //foreach (var i in linkedList)
        //    //{
        //    //    MessageBox.Show(i.ToString());
        //    //}
        //}
        public string stringReplace(string str) //HÀM XOÁ KHOẢNG TRẮNG,DẤU NGOẶC
        {
            str.Trim();
            str = Regex.Replace(str, " ", "");
            str = Regex.Replace(str, "r", "R");
            str = Regex.Replace(str, "w", "W");
            str = Regex.Replace(str, "[\\,]", ";");
            str = Regex.Replace(str, "[\\)]", "");
            str = Regex.Replace(str, "[\\(]", "");
            str = Regex.Replace(str, "[\\{]", "");
            str = Regex.Replace(str, "[\\}]", "");
            return str;
        }
        public void convertDataArray(string data) //CHUYỂN MẢNG SANG LIST
        {
            part = data.Split(new String[] { ";" }, StringSplitOptions.None);
            list = new List<string>(part);
        }
        public void getColumnAndRow(List<string> list) //LẤY GIÁ TRỊ CỘT VÀ HÀNG
        {
            List<string> tempList = list;
            int max = 0;
            char[] tempChar;
            int i;
            try
            {
                foreach (string str in tempList)
                {
                    tempChar = str.ToCharArray();
                    var isNumberic = Int32.TryParse(tempChar[2].ToString(), out int n);
                    if (isNumberic == true)
                    {
                        MessageBox.Show("Số Transaction giới hạn từ 1-->9");
                        generateNewForm();
                    }
                    else
                    {
                        i = Int32.Parse(tempChar[1].ToString());
                        if (i == 0)
                        {
                            MessageBox.Show("Không có giá trị 0");
                            generateNewForm();
                        }
                        else
                        {
                            if (i > max)
                            {
                                max = i;
                            }
                        }
                    }
                }
                m_Column = max;
                m_Row = tempList.Count;
            }
            catch (Exception ex)
            {
                if (ex is FormatException)
                {
                    MessageBox.Show("Nhập sai");
                }
            }
        }
        public void getStatusAndPosition(List<string> list)
        {
            List<string> tempList = list;
            //linkedList = new LinkedList<int>();

            int i = 0;
            int tempNumber;
            tempString = new string[m_Row]; //TẠO CHUỐI TẠM CÓ SỐ PHẦN TỬ = VỚI SỐ HÀNG,LƯU THỨ TỰ READ HOẶC WRITE R1A;R2A;W4A => READ,READ,WRITE
            tempPosition = new int[m_Row];  //TẠO MẢNG VỊ TRÍ LƯU THỨ TỰ READ,WRITE CỦA TỪNG CỘT: R1A LƯU VỊ TRÍ CỘT 1,R2B LƯU CỘT 2
            //MessageBox.Show(m_Row.ToString());
            char[] tempChar;
            try
            {
                foreach (string str in tempList)
                {
                    tempChar = str.ToCharArray();
                    tempNumber = Int32.Parse(tempChar[1].ToString()) - 1; //VỊ TRÍ CỘT BẮT ĐẦU TỪ 0, côt T1=>được lưu lại ở index 0 của mảng
                    tempPosition[i] = tempNumber;

                    //linkedList.AddLast(tempNumber + 1); //2 -1-2-3-1
                    //MessageBox.Show(tempPosition[i].ToString());
                    if (tempChar[0] == 'R' || tempChar[0] == 'r')
                    {
                        tempString[i] = "Read" + tempChar[2].ToString();
                        //MessageBox.Show(tempString[i].ToString()+i);

                    }
                    if (tempChar[0] == 'W' || tempChar[0] == 'w')
                    {
                        tempString[i] = "Write" + tempChar[2].ToString();
                        //MessageBox.Show(tempString[i]+i);
                    }
                    i++;
                }
            }
            catch (Exception ex)
            {
                if (ex is FormatException)
                {
                    generateNewForm();
                }
            }
            //MessageBox.Show(m_Column.ToString());
        }
        public void createDynamicTable()
        {
            //TẠO BẢNG CÓ CỘT,HÀNG = 0
            Form form = new Form() { AutoSize = true };
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;
            tableLayoutPanel.ColumnCount = 0;
            tableLayoutPanel.RowCount = 0;
            //XOÁ CÁC CONTROLS
            tableLayoutPanel.Controls.Clear();
            tableLayoutPanel.ColumnStyles.Clear();
            tableLayoutPanel.RowStyles.Clear();
            //this.panelTable.Controls.Add(tableLayoutPanel);
            form.Controls.Add(tableLayoutPanel);
            //SINH CỘT T1...TN
            for (int col = 0; col < m_Column; col++)
            {
                tableLayoutPanel.ColumnCount++;
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                tableLayoutPanel.Controls.Add(new Label() { Text = "T" + (col + 1).ToString() }, col, 0);
            }
            //SINH HÀNG
            try
            {
                for (int row = 0; row < m_Row; row++)
                {
                    tableLayoutPanel.RowCount++;
                    tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                    tableLayoutPanel.Controls.Add(new Label() { Text = tempString[row].ToString() }, tempPosition[row], row + 1);
                }
            }
            catch (Exception ex)
            {
                if (ex is null)
                    throw;
            }

            tableLayoutPanel.AutoSize = true;
            form.Show();
        }
        public int getNumber(string str)
        {
            int number;
            char[] tempChar = str.ToCharArray();
            number = Int32.Parse(tempChar[1].ToString());
            return number;
        }
        //public bool checkNumber(int x)
        //{
        //    LinkedListNode<int> temp = linkedList.Find(x);
        //    //MessageBox.Show("Find: "+temp.Value.ToString());
        //    //while (node != linkedList.Last)
        //    //{
        //    //    if (temp.Value==x)
        //    //    {
        //    //        //MessageBox.Show(" Có số trong linkdedList: " + node.Value);
        //    //        return true;
        //    //    }
        //    //    node = node.Next;
        //    //}
        //    //return false;
        //    if (temp == null) //không có
        //    {
        //        return false;
        //    }
        //    return true;
        //}
        public int checkConflict(string x, string y)
        {
            //int number=0;
            char[] tempCharX = x.ToCharArray();
            char[] tempCharY = y.ToCharArray();
            //string t;
            if (tempCharX[1] == tempCharY[1])
            { //CHECK CÓ CÙNG MỘT TRANSACTION HAY KHÔNG,cùng T1..
              //t = x + "cùng cột với" + y + "chuyển sang cột T khác";
              // t = "next";
              //number = 2;
                return 2;
            }
            else //TH T1 và T2
            {
                if ((tempCharX[0] == 'R' && tempCharY[0] == 'W') || (tempCharX[0] == 'W' && tempCharY[0] == 'W') || (tempCharX[0] == 'W' && tempCharY[0] == 'R'))
                {
                    if (tempCharX[2] == tempCharY[2])
                    {
                        return 0;
                    }
                }

                return 1;
            }
        }
        public void conflictSerializable(string[] part)
        {
            int number;
            g = new Graph(m_Column);
            //node = null;
            for (int i = 0; i < part.Length; i++) //0---8
            {
                //0-1 0-2 0-3 0-4 0-5 0-6 0-7 0-8
                //1-2 1-3 1-4 1-5 1-6 1-7 1-8
                //2-3 2-4 2-5 2-6 2-7 2-8
                for (int j = i + 1; j < part.Length; j++) //1---7      //0-không xung đột 1-xung đột 2-cùng một cột
                {
                    number = checkConflict(part[i], part[j]); //0 và 1 kt xung đột
                    int x = getNumber(part[i]);
                    int y = getNumber(part[j]);
                    if (number == 0) //KHÔNG XUNG ĐỘT
                    {
                        g.addEdge(x - 1, y - 1);
                    }
                    else if (number == 1)
                    {

                    }
                    else //number==2
                    {

                    }

                    //MessageBox.Show(number.ToString() + "- -" + part[i] + " - " + part[j]);
                }
            }
            if (g.isCyclic())
                MessageBox.Show("Có chu trình-Non Serializability-Không khả tuần tự xung đột");
            else
                MessageBox.Show("Không có chu trình- Conflict Serizalizability-Khả tuần tự xung đột");
        }
        public void initGraph()
        {
            Form form = new Form();
            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
            //Label label1 = new Label() { Text = pair.First.ToString(),Location=new Point(1,1) };
            //Label label2 = new Label() { Text = pair.Second.ToString(), Location = new Point(1, 1) };

            //flowLayoutPanel.Controls.Add(label1);
            //flowLayoutPanel.Controls.Add(label2);

            //foreach (var item in pairArray)
            //{
            //    Label label = new Label() {Text=pairArray. };
            //    flowLayoutPanel.Controls.Add(label);
            //}
            for (int i = 0; i < part.Length; i++)
            {
                //Label label = new Label() { Text = pairArray[i].Item1.ToString() + "=======>" + pairArray[i].Item2.ToString() };
                //flowLayoutPanel.Controls.Add(label);
            }
            form.Controls.Add(flowLayoutPanel);
            form.Show();
        }
        private void buttonResult_Click(object sender, EventArgs e)
        {
            //string temp = "R2(A)   ;R1(B)  ;   W2(A);  W2(A)   ;R3 (A);W1  (B);    W3(A)   ;R2 (B);W2(B)            ";
            string data = textBoxInput.Text.ToString();
            data = stringReplace(data);
            convertDataArray(data);
            getColumnAndRow(list);
            getStatusAndPosition(list);
            createDynamicTable();
            conflictSerializable(part);
            //initGraph();
            //showLinkedList(linkedList);
        }
        private void buttonReset_Click(object sender, EventArgs e)
        {
            generateNewForm();
        }
    }
}
