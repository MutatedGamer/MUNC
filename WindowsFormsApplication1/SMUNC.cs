using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SMUNC;
using System.IO;
using System.Text.RegularExpressions;

namespace WindowsFormsApplication1
{

    public partial class Form1 : Form
    {
        bool first = true; //used to highlight textbox1 at first start
        int speakers = new int(); //sets the PSL speakers time
        int _speakers = new int(); //used to countdown PSL speakers time
        int speakers2 = new int(); //sets SPL speakers time
        int _speakers2 = new int(); //used to countdown SPL speakers time
        int caucustime = new int(); //sets time for caucus
        int ctst = new int(); //sets speakers time for caucus
        int _ctst = new int(); //used to countdown speakers time for caucus
        int countriespresent = new int(); //number of countries not absent
        bool timeset = new bool(); //checks to see if PSL speakers time was set
        bool timeset2 = new bool();//checks to see if SSL speakers time was set
        bool ctset = new bool(); //checks to see if caucus time speakers time was set
        List<string> _countries = new List<string>(); //used to feed PSL country list
        List<string> _countries2 = new List<string>(); //used to feed SPL country list
        List<string> attend = new List<string>(); 
        List<string> attendsave = new List<string>();
        public Form1()
        {
            InitializeComponent();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != (""))
            {
                if (tabControl1.SelectedTab == tabPage1)
                {
                    _countries.Add(textBox1.Text);
                    listBox1.DataSource = null;
                    listBox1.DataSource = _countries;
                    textBox1.Text = "";
                 }else if (tabControl1.SelectedTab == tabPage2)
                 {
                    _countries2.Add(textBox1.Text);
                    listBox2.DataSource = null;
                    listBox2.DataSource = _countries2;
                    textBox1.Text = "";
                 }
                 else if (tabControl1.SelectedTab == tabPage5)
                 {
                    attend.Add(textBox1.Text);
                    dataGridView1.Rows.Add(textBox1.Text);
                    dataGridView1.AutoResizeRows();
                     DataGridViewRow row = dataGridView1.Rows[0];
                    dataGridView1.RowTemplate.Height = row.Height;
                    textBox1.Text = "";
                 }
              }
         }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           

        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage1)
            {
                int selectedIndex = listBox1.SelectedIndex;

                try
                {
                    _countries.RemoveAt(selectedIndex);
                }
                catch
                {
                }
                listBox1.DataSource = null;
                listBox1.DataSource = _countries;
            } else if (tabControl1.SelectedTab == tabPage2)
            {
                int selectedIndex2 = listBox2.SelectedIndex;
                try
                {
                    _countries2.RemoveAt(selectedIndex2);
                }
                catch
                {
                }
                listBox2.DataSource = null;
                listBox2.DataSource = _countries2;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(tabControl1.SelectedTab==tabPage1&radioButton8.Checked==true)
            {
                if (textBox2.Text == "")
                { 
                    timeset = false;
                }
                else 
                { 
                    timeset = true;
                }
            }
            if (tabControl1.SelectedTab == tabPage2 & radioButton10.Checked == true)
            {
                if (textBox3.Text == "")
                { 
                    timeset2 = false;
                }
                else
                {
                    timeset2 = true;
                }
            }
            if (_countries.Count != 0 & tabControl1.SelectedTab == tabPage1) //check that there is a country to go up
            {
                if (timeset == true)
                {
                    label1.Text = _countries.First(); //puts current country name at top
                    _speakers = speakers;
                    timer1.Enabled = true; //enable timer
                    label3.Text = speakers.ToString();
                    if (radioButton8.Checked == true) //check if custom radio button time is checked
                    {
                      
                        if (textBox2.Text.Contains(":"))
                        {
                            string time = "00:" + textBox2.Text; //fetches mm:ss info to convert into seconds
                            var seconds = TimeSpan.Parse(time).TotalSeconds; //converts mm:ss to seconds
                            speakers = Convert.ToInt32(seconds); //stores seconds value in speakers integer
                            _speakers = speakers;
                        }
                        else
                        {
                            speakers = Convert.ToInt32(textBox2.Text);
                            _speakers = speakers;
                        }
                    }
                    label3.Text = speakers.ToString(); //shows initial time (timer takes care of decrmenting this)
                    listBox1.SelectedIndex = 0; //selects topmost country (the one that moved up to "currently speaking"
                    button2.PerformClick(); //removes top country from list
                    listBox1.ClearSelected(); //deselects new top country
                }
                else if (timeset == false)
                {
                    System.Media.SystemSounds.Beep.Play();
                    MessageBox.Show("Please set a speaking time!");

                }
            }
            else if (_countries.Count == 0 & tabControl1.SelectedTab == tabPage1)
            {
                label1.Text = "";
            }

            if (_countries2.Count != 0 & tabControl1.SelectedTab == tabPage2) //if crisis speakers list selected
            { //does same thing as above but with crisis speakers list and uses seperate timer (speakers2)
                if (timeset2 == true)
                {
                    label1.Text = _countries2.First();
                    _speakers2 = speakers2;
                    timer1.Enabled = true;
                    label3.Text = speakers2.ToString();
                    if (radioButton10.Checked == true)
                    {
                        if (textBox3.Text.Contains(":"))
                        {
                            string time = "00:" + textBox3.Text;
                            var seconds = TimeSpan.Parse(time).TotalSeconds;
                            speakers2 = Convert.ToInt32(seconds);
                            _speakers2 = speakers2;
                        }
                        else
                        {
                            speakers2 = Convert.ToInt32(textBox3.Text);
                            _speakers2 = speakers2;
                        }
                    }
                    label3.Text = speakers2.ToString();
                    listBox2.SelectedIndex = 0;
                    button2.PerformClick();
                    listBox2.ClearSelected();
                }
                else if (timeset2 == false)
                {
                    System.Media.SystemSounds.Beep.Play();
                    MessageBox.Show("Please set a speaking time!");

                }
            }
            else if (_countries2.Count == 0 & tabControl1.SelectedTab == tabPage2)
            {
                label1.Text = "";
            }
            button5.Text = "▌ ▌";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            if (textBox1.Text != (""))
            {
                if (tabControl1.SelectedTab == tabPage1)
                {
                    int selectedIndex = listBox1.SelectedIndex;
                    _countries.Insert(selectedIndex, textBox1.Text);
                    listBox1.DataSource = null;
                    listBox1.DataSource = _countries;
                    textBox1.Text = "";
                } 
                else if (tabControl1.SelectedTab == tabPage2)
                {
                    int selectedIndex = listBox2.SelectedIndex;
                    _countries2.Insert(selectedIndex, textBox1.Text);
                    listBox2.DataSource = null;
                    listBox2.DataSource = _countries2;
                    textBox1.Text = "";
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (tabControl1.SelectedTab == tabPage1)
            {
                if (_speakers > 1)
                {
                    _speakers = _speakers - 1;
                    label3.Text = _speakers.ToString();
                }
                else
                {
                    _speakers = _speakers - 1;
                    label3.Text = _speakers.ToString();
                    label1.Text = "";
                    System.Media.SystemSounds.Beep.Play();
                    timer1.Enabled = false;
                }
            } 
            else if (tabControl1.SelectedTab == tabPage2)
            {
                if (_speakers2 > 1)
                {
                    _speakers2 = _speakers2 - 1;
                    label3.Text = _speakers2.ToString();
                }
                else
                {
                    timer1.Enabled = false;
                    _speakers2 = _speakers2 - 1;
                    label3.Text = _speakers2.ToString();
                    label1.Text = "";

                    System.Media.SystemSounds.Beep.Play();
                }
                } 
            else if (tabControl1.SelectedTab == tabPage4)
            {
                if (_ctst > 1)
                {

                    _ctst = _ctst - 1;
                    label3.Text = _ctst.ToString();
                 }
                 else if(_ctst==1)
                 {
                    _ctst = _ctst - 1;
                    label3.Text = _ctst.ToString();
                    label1.Text = "";
                    System.Media.SystemSounds.Beep.Play();
                     if(timer2.Enabled==false)
                     {
                         button6.Text = "Start";
                     }

                  }
              }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(timer1.Enabled==true)
            {
                timer1.Enabled = false;
                button5.Text = "▶";
            }
            else if(timer1.Enabled==false & label3.Text!="0")
            {
                timer1.Enabled = true;
                button5.Text = "▌ ▌";
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            speakers = 15;
            timeset = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            speakers = 30;
            timeset = true;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            speakers = 45;
            timeset = true;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            speakers = 60;
            timeset = true;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            speakers = 90;
            timeset = true;
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            speakers = 120;
            timeset = true;
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            speakers = 180;
            timeset = true;
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            timeset = true;
        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            radioButton8.Checked = true;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage2)
            {
                groupBox1.Enabled = false;
                groupBox1.Visible = false;
                groupBox1.SendToBack();
                groupBox2.Enabled = true;
                groupBox2.Visible = true;
                groupBox2.BringToFront();
                textBox1.Enabled = true;
                button1.Enabled = true;
                button2.Enabled = true;
                button4.Enabled = true;
                label5.Text="Crisis";
                label9.Visible = false;
                label8.Visible = false;
                button4.Enabled = true;
                button3.Enabled = true;
                button5.Enabled = true;
                button10.Visible = false;
                if (timeset2==false)
                {
                    label3.Text = "00";
                }
                else
                {
                    label3.Text = speakers2.ToString();
                }
            }else if (tabControl1.SelectedTab == tabPage1)
            {
                groupBox1.Enabled = true;
                groupBox1.Visible = true;
                groupBox2.SendToBack();
                groupBox2.Enabled = false;
                groupBox2.Visible = false;
                groupBox1.BringToFront();
                textBox1.Enabled = true;
                button1.Enabled = true;
                button2.Enabled = true;
                button4.Enabled = true;
                label5.Text = "General";
                label9.Visible = false;
                label8.Visible = false;
                button4.Enabled = true;
                button3.Enabled = true;
                button5.Enabled = true;
                button10.Visible = false;
                if (timeset==false)
                {
                    label3.Text = "00";
                }else 
                {
                    label3.Text=speakers.ToString();
                }
            }
            else if (tabControl1.SelectedTab == tabPage4)
            {
                label5.Text = "Caucus";
                label9.Visible = true;
                label8.Visible = true;
                label3.Text = _ctst.ToString();
                button10.Visible = false;
                textBox1.Enabled = false;
                button1.Enabled = false;
                button2.Enabled = false;
                button4.Enabled = false;
                button10.Visible = false;
                button3.Enabled = false;
                button5.Enabled = false;

            }
            else if (tabControl1.SelectedTab == tabPage5)
            {
                textBox1.Enabled = true;
                button1.Enabled = true;
                button2.Enabled = false;
                button4.Enabled = false;
                button3.Enabled = false;
                button5.Enabled = false;
                label9.Visible = false;
                label8.Visible = false;
                button10.Visible = false;
            }
            else if (tabControl1.SelectedTab == tabPage6)
            {
                textBox1.Enabled = false;
                button1.Enabled = true;
                button2.Enabled = false;
                button4.Enabled = false;
                button3.Enabled = false;
                button5.Enabled = false;
                label9.Visible = false;
                label8.Visible = false;
                button10.Visible = true;
                button1.Enabled = false;

            }
            else
            {
                textBox1.Enabled = false;
                button1.Enabled = false;
                button2.Enabled = false;
                label9.Visible = false;
                label8.Visible = false;
                button4.Enabled = false;
                button10.Visible = false;
                button3.Enabled = false;
                button5.Enabled = false;
            }
            if (tabControl1.SelectedTab == tabPage6)
            {
                while (dataGridView2.Rows.Count > 0)
                {

                    dataGridView2.Rows.RemoveAt(0);
                    countriespresent--;

                }
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!Convert.ToBoolean(row.Cells[3].Value))
                    {
                        dataGridView2.RowTemplate.Height = dataGridView1.RowTemplate.Height;
                        DataGridViewRow row2 = (DataGridViewRow)dataGridView2.RowTemplate.Clone();
                        row2.CreateCells(dataGridView2, row.Cells[0].Value);
                        dataGridView2.Rows.Add(row2);
                        dataGridView2.AutoResizeRows();


                        if(Convert.ToBoolean(row.Cells[2].Value))
                        {
                            row2.Cells[3].ReadOnly = true;
                            row2.Cells[3].Style.BackColor = Color.Gray;
                        }
                    }
                }


            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
            radioButton1.Checked = false;
            timeset = false;
            timeset2 = false;
            ctset = false;
            dataGridView1.RowsDefaultCellStyle.BackColor = Color.White;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.SkyBlue;
            dataGridView2.RowsDefaultCellStyle.BackColor = Color.White;
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.SkyBlue;
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            speakers2 = 15;
            timeset2 = true;
        }

        private void radioButton11_CheckedChanged(object sender, EventArgs e)
        {
            speakers2 = 30;
            timeset2 = true;
        }

        private void radioButton13_CheckedChanged(object sender, EventArgs e)
        {
            speakers2 = 45;
            timeset2 = true;
        }

        private void radioButton15_CheckedChanged(object sender, EventArgs e)
        {
            speakers2 = 60;
            timeset2 = true;
        }

        private void radioButton16_CheckedChanged(object sender, EventArgs e)
        {
            speakers2 = 90;
            timeset2 = true;
        }

        private void radioButton14_CheckedChanged(object sender, EventArgs e)
        {
            speakers2 = 120;
            timeset2 = true;
        }

        private void radioButton12_CheckedChanged(object sender, EventArgs e)
        {
            speakers2 = 180;
            timeset2 = true;
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {

            timeset2 = true;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            if (first==true)
            {
                textBox1.SelectAll();
                first=false;
            }
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            timer1.Enabled = false;
            button5.Text = "▌ ▌";

        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            radioButton10.Checked = true;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            const char Delete = (char)8;
            e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != Delete && e.KeyChar != ':';
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            const char Delete = (char)8;
            e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != Delete && e.KeyChar != ':';
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            const char Delete = (char)8;
            e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != Delete && e.KeyChar != ':';
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != "" & textBox4.Text.Contains("1") == true || textBox4.Text.Contains("2") == true || textBox4.Text.Contains("3") == true || textBox4.Text.Contains("4") == true || textBox4.Text.Contains("5") == true || textBox4.Text.Contains("6") == true || textBox4.Text.Contains("7") == true || textBox4.Text.Contains("8") == true || textBox4.Text.Contains("9") == true)
            {
                if (radioButton17.Checked == true)
                {
                    if (ctset == true)
                    {
                        if (button6.Text == "Start")
                        {
                            button6.Text = "Stop";
                            button8.Enabled = true;
                            button9.Enabled = true;
                            //start caucus stuff
                            timer2.Enabled = true;
                            timer1.Enabled = true;
                            _ctst = ctst;
                            label3.Text = ctst.ToString();
                        }
                        else if (button6.Text == "Stop")
                        {
                            button6.Text = "Start";
                            button8.Enabled = false;
                            button9.Enabled = false;
                            timer2.Enabled = false;
                            label8.Text = "0:00";
                            label3.Text = "00";
                            //stop caucus stuff
                            timer1.Enabled = false;
                        }

                        if (textBox4.Text.Contains(":") & button6.Text=="Stop")
                        {
                            string ct = "00:" + textBox4.Text; //fetches mm:ss info to convert into seconds
                            var secondsct = TimeSpan.Parse(ct).TotalSeconds; //converts mm:ss to seconds
                            caucustime = Convert.ToInt32(secondsct); //stores seconds value in speakers integer
                            TimeSpan ts = TimeSpan.FromSeconds(caucustime);
                            label8.Text = ts.ToString(@"mm\:ss");
                        }
                        else if (textBox4.Text.Contains(":")==false & button6.Text == "Stop")
                        {
                            caucustime = Convert.ToInt32(textBox4.Text) * 60;
                            TimeSpan ts = TimeSpan.FromSeconds(caucustime);
                            label8.Text = ts.ToString(@"mm\:ss");
                        }
                        if (radioButton20.Checked == true & textBox5.Text != "" & button6.Text == "Stop")
                        {

                            if (textBox5.Text.Contains(":"))
                            {
                                string time = "00:" + textBox5.Text; //fetches mm:ss info to convert into seconds
                                var seconds = TimeSpan.Parse(time).TotalSeconds; //converts mm:ss to seconds
                                ctst = Convert.ToInt32(seconds); //stores seconds value in speakers integer
                                _ctst = ctst;
                                label3.Text = ctst.ToString();

                            }
                            else
                            {
                                ctst = Convert.ToInt32(textBox5.Text);
                                _ctst = ctst;
                                label3.Text = ctst.ToString();
                            }
                        }
                    } else if(ctset==false)
                    { MessageBox.Show("Please enter a speakers time!"); }
                } else if(radioButton18.Checked==true)
                {
                    if (textBox4.Text != "")
                    {
                        if (button6.Text == "Start")
                        {
                            button6.Text = "Stop";
                            button8.Enabled = true;
                            button9.Enabled = true;
                            //start caucus stuff
                            timer2.Enabled = true;
                        }
                        else if (button6.Text == "Stop")
                        {
                            button6.Text = "Start";
                            button8.Enabled = false;
                            button9.Enabled = false;
                            timer2.Enabled = false;
                            label8.Text = "0:00";
                            label3.Text = "00";
                            //stop caucus stuff
                        }
                        if (textBox4.Text.Contains(":") & button6.Text=="Stop")
                        {
                            string ct = "00:" + textBox4.Text; //fetches mm:ss info to convert into seconds
                            var secondsct = TimeSpan.Parse(ct).TotalSeconds; //converts mm:ss to seconds
                            caucustime = Convert.ToInt32(secondsct); //stores seconds value in speakers integer
                            TimeSpan ts = TimeSpan.FromSeconds(caucustime);
                            label8.Text = ts.ToString(@"mm\:ss");
                        }
                        else if (textBox4.Text.Contains(":")==false & button6.Text=="Stop")
                        {
                            caucustime = Convert.ToInt32(textBox4.Text) * 60;
                            TimeSpan ts = TimeSpan.FromSeconds(caucustime);
                            label8.Text = ts.ToString(@"mm\:ss");
                        }
                    }
                    else if (textBox4.Text == "")
                    {
                        MessageBox.Show("Please enter a caucus time!");
                    }
                    else
                    {
                        caucustime = Convert.ToInt32(textBox4.Text) * 60;
                    }
                }
            }else
            { MessageBox.Show("Please enter a caucus time!");
            }
        }

        private void radioButton18_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton18.Checked == true)
            {
                radioButton19.Enabled = false;
                radioButton20.Enabled = false;
                radioButton21.Enabled = false;
                radioButton22.Enabled = false;
                radioButton23.Enabled = false;
                radioButton23.Enabled = false;
                radioButton24.Enabled = false;
                radioButton25.Enabled = false;
                radioButton26.Enabled = false;
                textBox5.Enabled = false;
                button7.Enabled = false;
                //unmod caucus stuff
            }
        }

        private void radioButton17_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton17.Checked == true)
            {
                radioButton19.Enabled = true;
                radioButton20.Enabled = true;
                radioButton21.Enabled = true;
                radioButton22.Enabled = true;
                radioButton23.Enabled = true;
                radioButton23.Enabled = true;
                radioButton24.Enabled = true;
                radioButton25.Enabled = true;
                radioButton26.Enabled = true;
                textBox5.Enabled = true;
                //mod caucus stuff
            }
        }

        private void radioButton25_CheckedChanged(object sender, EventArgs e)
        {
            ctst = 60;
            ctset = true;
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            const char Delete = (char)8;
            e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != Delete && e.KeyChar != ':';
           
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (caucustime > 1)
            {
                caucustime = caucustime - 1;
                TimeSpan ts = TimeSpan.FromSeconds(caucustime);
                label8.Text = ts.ToString(@"mm\:ss");
            }
            else
            {
                caucustime = caucustime - 1;
                TimeSpan ts = TimeSpan.FromSeconds(caucustime);
                label8.Text = ts.ToString(@"mm\:ss");

                System.Media.SystemSounds.Beep.Play();
                timer2.Enabled = false;
                if (timer1.Enabled ==false)
                {
                    button6.Text = "Start";
                }
            }
        }

        private void radioButton19_CheckedChanged(object sender, EventArgs e)
        {
            ctst = 15;
            ctset = true;
        }

        private void radioButton21_CheckedChanged(object sender, EventArgs e)
        {
            ctst = 30;
            ctset = true;
        }

        private void radioButton23_CheckedChanged(object sender, EventArgs e)
        {
            ctst = 45;
            ctset = true;
        }

        private void radioButton26_CheckedChanged(object sender, EventArgs e)
        {
            ctst = 90;
            ctset = true;
        }

        private void radioButton24_CheckedChanged(object sender, EventArgs e)
        {
            ctst = 120;
            ctset = true;
        }

        private void radioButton22_CheckedChanged(object sender, EventArgs e)
        {
            ctst = 180;
            ctset = true;
        }

        private void radioButton20_CheckedChanged(object sender, EventArgs e)
        {
            ctset = true;
        }

        private void textBox5_ClientSizeChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_Click(object sender, EventArgs e)
        {
            radioButton20.Checked = true;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if(timer2.Enabled==true & caucustime!=0)
            {
                button8.Text = "▶";
                timer2.Enabled = false;
                button6.Enabled = false;
            }else if(timer2.Enabled==false & caucustime!=0)
            {
                timer2.Enabled = true;
                button8.Text = "▌ ▌";
                button6.Enabled = true;
            }
            if(timer1.Enabled==true & _ctst!=0)
            {
                button8.Text = "▶";
                timer1.Enabled = false;
                button6.Enabled = false;
            }
            else if (timer1.Enabled == false & _ctst != 0)
            {
                timer1.Enabled = true;
                button8.Text = "▌ ▌";
                button6.Enabled = true;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (textBox4.Text.Contains(":"))
            {
                string ct = "00:" + textBox4.Text; //fetches mm:ss info to convert into seconds
                var secondsct = TimeSpan.Parse(ct).TotalSeconds; //converts mm:ss to seconds
                caucustime = caucustime+Convert.ToInt32(secondsct); //stores seconds value in speakers integer
                TimeSpan ts = TimeSpan.FromSeconds(caucustime);
                label8.Text = ts.ToString(@"mm\:ss");
            }
            else
            {
                caucustime = caucustime+Convert.ToInt32(textBox4.Text) * 60;
                TimeSpan ts = TimeSpan.FromSeconds(caucustime);
                label8.Text = ts.ToString(@"mm\:ss");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            _ctst = ctst;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
            
       

        private void dataGridView1_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            
        }

        private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((e.RowIndex >= 0) && (e.ColumnIndex >= 0))
            {
                DataGridViewCell _changedCell = (sender as DataGridView).Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (_changedCell is DataGridViewCheckBoxCell)
                    if (_changedCell.EditedFormattedValue.Equals(true))
                        foreach (DataGridViewCell _otherCell in dataGridView1.Rows[e.RowIndex].Cells)
                            if ((_otherCell is DataGridViewCheckBoxCell) &&
                                (_otherCell != _changedCell) &&
                                (_otherCell.Value != null) &&
                                (_otherCell.Value.Equals(true)))
                                _otherCell.Value = false;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            dataGridView2.AutoResizeRows();
        }

        private void dataGridView2_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((e.RowIndex >= 0) && (e.ColumnIndex >= 0))
            {
                DataGridViewCell _changedCell = (sender as DataGridView).Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (_changedCell is DataGridViewCheckBoxCell)
                    if (_changedCell.EditedFormattedValue.Equals(true))
                        foreach (DataGridViewCell _otherCell in dataGridView2.Rows[e.RowIndex].Cells)
                            if ((_otherCell is DataGridViewCheckBoxCell) &&
                                (_otherCell != _changedCell) &&
                                (_otherCell.Value != null) &&
                                (_otherCell.Value.Equals(true)))
                                _otherCell.Value = false;
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "" & openFileDialog1.FileName.Contains(".txt"))
            {
                tabControl1.SelectedTab = tabPage5;
                while (dataGridView1.Rows.Count > 0)
                {

                    dataGridView1.Rows.RemoveAt(0);

                }
                int part = 1;
                richTextBox2.Clear();
                richTextBox3.Clear();
                string[] fileLines = System.IO.File.ReadAllLines(openFileDialog1.FileName);
                foreach (string readingline in fileLines)
                {
                    if(readingline.Contains("<<<<PART 2>>>>"))
                    {
                        part = 2;
                    }
                    else if(readingline.Contains("<<<<PART 3>>>>"))
                    { part = 3;
                    }
                    else if (readingline.Contains("<<<<PART 4>>>>"))
                    {
                        part = 4;
                    }
                    else if(!readingline.Contains("<<<<PART"))
                    {
                        if(part==1)
                        {
                            dataGridView1.Rows.Add(readingline);
                            dataGridView1.AutoResizeRows();
                            DataGridViewRow row = dataGridView1.Rows[0];
                            dataGridView1.RowTemplate.Height = row.Height;
                        }
                        else if (part==2)
                        {
                            if (richTextBox2.Text == "")
                            {
                                richTextBox2.Text = readingline;
                            }
                            else
                            {
                                richTextBox2.Text = richTextBox2.Text + Environment.NewLine + readingline;
                            }
                        }
                        else if (part == 3)
                        {
                            if (richTextBox3.Text == "")
                            {
                                richTextBox3.Text = readingline;
                            }
                            else
                            {
                                richTextBox3.Text = richTextBox3.Text + Environment.NewLine + readingline;
                            }
                        }
                        else if (part == 4)
                        {
                            if (richTextBox1.Text == "")
                            {
                                richTextBox1.Text = readingline;
                            }
                            else
                            {
                                richTextBox1.Text = richTextBox1.Text + Environment.NewLine + readingline;
                            }
                        }
                    }
                }
                

            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    attendsave.Add(row.Cells[0].Value.ToString());
         
                }




                TextWriter tw = new StreamWriter(saveFileDialog1.FileName);
                foreach (String s in attendsave)
                {
                    tw.WriteLine(s);
                }
                tw.WriteLine("<<<<PART 2>>>>");
                tw.WriteLine(richTextBox2.Text);
                tw.WriteLine("<<<<PART 3>>>>");
                tw.WriteLine(richTextBox3.Text);
                tw.WriteLine("<<<<PART 4>>>>");
                tw.WriteLine(richTextBox1.Text);
                tw.Close();
                attendsave.Clear();
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            countriespresent = 0;
            label7.Text = "Countries Present: 0";
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!Convert.ToBoolean(row.Cells[3].Value))
                {
                    countriespresent++;
                    label7.Text = "Countries Present: " + countriespresent;
                }
            }
            int remainder=new int();
            int majority=new int();
            majority = Math.DivRem(countriespresent, 2, out remainder);
            if(remainder!=0)
            { 
            label10.Text="Simple Majority: "+ (majority+1);
            }
            else if (remainder==0)
            {
                label10.Text = "Simple Majority: " + (majority+1);
            }

            int thirds = Math.DivRem(countriespresent * 2, 3, out remainder);
            if (remainder != 0)
            {
                label11.Text = "Two Thirds: " + (thirds + 1);
            }
            else if (remainder == 0)
            {
                label11.Text = "Two Thirds: " + (thirds);
            }
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            int countriesvoted = new int();
            countriesvoted = 0;
            int countriespassed = new int();
            countriespassed = 0;
            int voteremainder = new int();
            int requiredvote = new int();
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (!Convert.ToBoolean(row.Cells[3].Value))
                {
                    countriesvoted++;

                }
                if(Convert.ToBoolean(row.Cells[1].Value)==true)
                {
                    countriespassed++;
                }
            }
            int thirds = Math.DivRem(countriesvoted * 2, 3, out voteremainder);

            if (voteremainder != 0)
            {
                requiredvote = (thirds + 1);
            }
            else if (voteremainder == 0)
            {
                requiredvote=(thirds);
            }



            if(countriespassed>=requiredvote)
            {
                System.Media.SystemSounds.Exclamation.Play();
                MessageBox.Show("This resolution PASSED!" + "\n" + "Information:" + "\n" + "Number of voters: " + countriesvoted.ToString() + "\n" + "Two Thirds: " + requiredvote.ToString() + "\n" + "Number of yays: " + countriespassed.ToString() + "\n" + "Vote passed by: " + (countriespassed - requiredvote));
            }
            else
            {
                System.Media.SystemSounds.Exclamation.Play();
                MessageBox.Show("This resolution FAILED!" + "\n" + "Information:" + "\n" + "Number of voters: " + countriesvoted.ToString() + "\n" + "Two Thirds: " + requiredvote.ToString() + "\n" + "Number of yays: " + countriespassed.ToString() + "\n" + "Vote failed by: " + (requiredvote-countriespassed));
                
            }

        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            frm.Show();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            this.dataGridView1.ClearSelection();
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            this.dataGridView2.ClearSelection();
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text.Contains(":"))
            {
                var input = textBox2.Text; // or what ever your input is. 
                var regex = new Regex("^[0-9]:[0-9]{2}$");
                var regex2 = new Regex("^[0-9]{2}:[0-9]{2}$");
                var match = regex.Match(input);
                var match2 = regex2.Match(input);

                if (!match.Success & !match2.Success)
                {
                    MessageBox.Show("Please enter correct format (MM:SS or SS)!");
                    textBox2.Text = "";
                }
            }
            
        }
        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text.Contains(":"))
            {
            var input = textBox3.Text; // or what ever your input is. 
            var regex = new Regex("^[0-9]:[0-9]{2}$");
            var regex2 = new Regex("^[0-9]{2}:[0-9]{2}$");
            var match = regex.Match(input);
            var match2 = regex2.Match(input);

            if (!match.Success & !match2.Success)
            {
                MessageBox.Show("Please enter correct format (MM:SS or SS)!");
                textBox3.Text = "";
            }
            }
           
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (textBox4.Text.Contains(":"))
            {
                var input = textBox4.Text; // or what ever your input is. 
                var regex = new Regex("^[0-9]:[0-9]{2}$");
                var regex2 = new Regex("^[0-9]{2}:[0-9]{2}$");
                var match = regex.Match(input);
                var match2 = regex2.Match(input);

                if (!match.Success & !match2.Success)
                {
                    MessageBox.Show("Please enter correct format (MM:SS or MM)!");
                    textBox4.Text = "";
                }
            }
        }
    }
}
