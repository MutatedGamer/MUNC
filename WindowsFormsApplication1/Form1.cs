using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{

    public partial class Form1 : Form
    {
        int speakers = new int();
        int speakers2 = new int();
        List<string> _countries = new List<string>();
        List<string> _countries2 = new List<string>();
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
                }else
                    if (tabControl1.SelectedTab == tabPage2)
                    {
                        _countries2.Add(textBox1.Text);
                        listBox2.DataSource = null;
                        listBox2.DataSource = _countries2;
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
            } else
                if (tabControl1.SelectedTab == tabPage2)
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
            if (_countries.Count != 0)
            {
                if (tabControl1.SelectedTab == tabPage1)
                {
                    label1.Text = _countries.First();
                    timer1.Enabled = true;
                    if (radioButton8.Checked == true)
                    {
                        string time = "00:" + textBox2.Text;
                        var seconds = TimeSpan.Parse(time).TotalSeconds;
                        speakers = Convert.ToInt32(seconds);
                    }
                    label3.Text = speakers.ToString();
                    listBox1.SelectedIndex = 0;
                    button2.PerformClick();
                    listBox1.ClearSelected();
                } else
                    if (tabControl1.SelectedTab == tabPage2)
                    {
                        label1.Text = _countries2.First();
                        timer1.Enabled = true;
                        if (radioButton10.Checked == true)
                        {
                            string time = "00:" + textBox2.Text;
                            var seconds = TimeSpan.Parse(time).TotalSeconds;
                            speakers2 = Convert.ToInt32(seconds);
                        }
                        label3.Text = speakers.ToString();
                        listBox1.SelectedIndex = 0;
                        button2.PerformClick();
                        listBox1.ClearSelected();
                    }
            } else
            {
                label1.Text = "";
            }
            button5.Text = "Pause Time";

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
                } else
                    if (tabControl1.SelectedTab == tabPage2)
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
                if (speakers > 1)
                {

                    speakers = speakers - 1;
                    label3.Text = speakers.ToString();
                }
                else
                {
                    speakers = speakers - 1;
                    label3.Text = speakers.ToString();

                    System.Media.SystemSounds.Beep.Play();
                    timer1.Enabled = false;
                }
            } else
                if (tabControl1.SelectedTab == tabPage2)
                {
                    if (speakers2 > 1)
                    {

                        speakers2 = speakers2 - 1;
                        label3.Text = speakers2.ToString();
                    }
                    else
                    {
                        speakers2 = speakers2 - 1;
                        label3.Text = speakers2.ToString();

                        System.Media.SystemSounds.Beep.Play();
                        timer1.Enabled = false;
                    }
                }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(timer1.Enabled==true)
            {
                timer1.Enabled = false;
                button5.Text = "Resume Timer";
            }
            else
            {
                timer1.Enabled = true;
                button5.Text = "Pause Timer";
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            speakers = 15;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            speakers = 30;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            speakers = 45;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            speakers = 60;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            speakers = 90;
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            speakers = 120;
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            speakers = 180;
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {




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
            }else if (tabControl1.SelectedTab == tabPage1)
            {
                groupBox1.Enabled = true;
                groupBox1.Visible = true;
                groupBox2.SendToBack();
                groupBox2.Enabled = false;
                groupBox2.Visible = false;
                groupBox1.BringToFront();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            groupBox2.Visible = false;
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            speakers2 = 15;
        }

        private void radioButton11_CheckedChanged(object sender, EventArgs e)
        {
            speakers2 = 30;
        }

        private void radioButton13_CheckedChanged(object sender, EventArgs e)
        {
            speakers2 = 45;
        }

        private void radioButton15_CheckedChanged(object sender, EventArgs e)
        {
            speakers2 = 60;
        }

        private void radioButton16_CheckedChanged(object sender, EventArgs e)
        {
            speakers2 = 90;
        }

        private void radioButton14_CheckedChanged(object sender, EventArgs e)
        {
            speakers2 = 120;
        }

        private void radioButton12_CheckedChanged(object sender, EventArgs e)
        {
            speakers2 = 180;
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
