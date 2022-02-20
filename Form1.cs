using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MemoryGame
{
    public partial class Form1 : Form
    {
        Random random = new Random();
        public string _currentPath = Environment.CurrentDirectory;
        public List<string> _loadWordsList;
        int Counter;
        Label label;
        int randomNumber;

        Label firstClicked, secondClicked;
        public Form1()
        {
            InitializeComponent();
            tableLayoutPanel1.Visible = false;
            tableLayoutPanel2.Visible = false;
        }

        private void label_Click(object sender, EventArgs e)
        {
            if (firstClicked != null && secondClicked != null)
                return;

            Label clickedLabel = sender as Label;
            if (clickedLabel == null)
                return;

            if (clickedLabel.ForeColor == Color.Black)
                return;

            if (firstClicked == null)
            {
                firstClicked = clickedLabel;
                firstClicked.ForeColor = Color.Black;
                return;
            }

            secondClicked = clickedLabel;
            secondClicked.ForeColor = Color.Black;

            CheckForWinner();
            if (firstClicked.Text == secondClicked.Text)
            {
                firstClicked = null;
                secondClicked = null;
            }
            else
            {    
                timer1.Start();
                label11.Text = Counter--.ToString();
                if (Counter == -1)
                {
                    CheckForLooser();
                }
            }
        }

        private void HideEasy()
        {
            Label[] hideall = { label20, label21, label22, label23, label24, label25, label26, label27 };
            foreach (var hidden in hideall)
            {
                hidden.ForeColor = hidden.BackColor;
            }
        }

        private void HideHard()
        {
            Label[] hideall = { label3, label4, label5, label6, label12, label13, label14, label15, label7, label8, label9, label10, label16, label17, label18, label19 };
            foreach (var hidden in hideall)
            {
                hidden.ForeColor = hidden.BackColor;
            }
        }

        private void CheckForWinner()
        {
            Label label;

            if (label1.Text == "Level: Easy")
            {
                for (int i = 0; i < tableLayoutPanel2.Controls.Count; i++)
                {
                    label = tableLayoutPanel2.Controls[i] as Label;

                    if (label != null && label.ForeColor == label.BackColor)
                        return;
                }
            }
            else
            {
                for (int i = 0; i < tableLayoutPanel1.Controls.Count; i++)
                {
                    label = tableLayoutPanel1.Controls[i] as Label;

                    if (label != null && label.ForeColor == label.BackColor)
                        return;
                }
            }

            DialogResult dialogResult = MessageBox.Show("You matched all the icons. Congrats!", "Try again?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (label1.Text == "Level: Easy")
                {
                    Counter = 9;
                    label11.Text = "10";
                    AssingWordsToSquares1();
                    HideEasy();
                }
                else
                {
                    Counter = 14;
                    label11.Text = "15";
                    AssingWordsToSquares();
                    HideHard();
                }
             }
             else if (dialogResult == DialogResult.No)
             {
                Close();
              }
            
        }

        private void CheckForLooser()
        {
            Label label;
            if (label1.Text == "Level: Easy")
            {
                for (int i = 0; i > tableLayoutPanel2.Controls.Count; i++)
                {
                    label = tableLayoutPanel2.Controls[i] as Label;

                    if (label != null && label.ForeColor == label.BackColor)
                        return;
                }
            }
            else
            {
                for (int i = 0; i > tableLayoutPanel1.Controls.Count; i++)
                {
                    label = tableLayoutPanel1.Controls[i] as Label;

                    if (label != null && label.ForeColor == label.BackColor)
                        return;
               }
            }

            DialogResult dialogResult = MessageBox.Show("You loose. Try again!" , "Restart game?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {   
                if (label1.Text == "Level: Easy")
                {
                    AssingWordsToSquares1();
                    Counter = 9;
                    label11.Text = "10";
                    HideEasy();
                }
                else
                {
                    AssingWordsToSquares();
                    Counter = 14;
                    label11.Text = "15";
                    HideHard();
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                Close();
            }
        }

        private void bEasy_Click(object sender, EventArgs e)
        {
            AssingWordsToSquares1();
            label1.Text = "Level: Easy";
            label2.Text = "Guess chances:";
            Counter = 9;
            label11.Text = "10";
            tableLayoutPanel1.Visible = false;
            tableLayoutPanel2.Visible = true;
            tableLayoutPanel2.Location = new Point(443, 29);
        }

        private void bHard_Click(object sender, EventArgs e)
        {
            AssingWordsToSquares();
            label1.Text = "Level: Hard";
            label2.Text = "Guess chances:";
            Counter = 14;
            label11.Text = "15";
            tableLayoutPanel1.Visible = true;
            tableLayoutPanel2.Visible = false;
        }

        private void bExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            firstClicked = null;
            secondClicked = null;
        }

        private void AssingWordsToSquares()
        {
            string path = $"{_currentPath}\\Resources\\Words.txt";
            _loadWordsList = File.ReadAllLines(path).ToList();
            
            for (int i = 0; i < tableLayoutPanel1.Controls.Count; i++)
            {
               
                if (tableLayoutPanel1.Controls[i] is Label)
                {
                    label = (Label)tableLayoutPanel1.Controls[i];
                }
                else
                {
                    continue;
                }
                
                randomNumber = random.Next(0, _loadWordsList.Count);
                label.Text = _loadWordsList[randomNumber];
                
                _loadWordsList.RemoveAt(randomNumber);
            }
            string[] duplicate = { label3.Text, label4.Text, label5.Text, label6.Text, label12.Text, label13.Text, label14.Text, label15.Text };

            var shuffle = duplicate.OrderBy(c => random.NextDouble()).ToArray();

            label7.Text = shuffle[3];
            label8.Text = shuffle[1];
            label9.Text = shuffle[0];
            label10.Text = shuffle[2];
            label16.Text = shuffle[7];
            label17.Text = shuffle[4];
            label18.Text = shuffle[6];
            label19.Text = shuffle[5];
        }


        private void AssingWordsToSquares1()
        {
            string path = $"{_currentPath}\\Resources\\Words.txt";
            _loadWordsList = File.ReadAllLines(path).ToList();

            for (int i = 0; i < tableLayoutPanel2.Controls.Count; i++)
            {

                if (tableLayoutPanel1.Controls[i] is Label)
                {
                    label = (Label)tableLayoutPanel2.Controls[i];
                }
                else
                {
                    continue;
                }

                randomNumber = random.Next(0, _loadWordsList.Count);
                label.Text = _loadWordsList[randomNumber];

                _loadWordsList.RemoveAt(randomNumber);
            }
            string[] duplicate = { label20.Text, label21.Text, label22.Text, label23.Text };

            string[] shuffle = duplicate.OrderBy(c => random.NextDouble()).ToArray();

            label25.Text = shuffle[2];
            label26.Text = shuffle[1];
            label27.Text = shuffle[0];
            label24.Text = shuffle[3];
        }
    }
}
