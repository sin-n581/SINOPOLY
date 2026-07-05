using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;


namespace Monopoly
{




    public partial class StartScreen : Form
    {
        
        private string playerName;
        private int imageIndex;

        Random rnd = new Random();
        public StartScreen()
        {
            InitializeComponent();

            label1.Text = "SINOPOLY";


        }



        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void StartScreen_Load(object sender, EventArgs e)
        {



        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "")
            {
                MessageBox.Show("Please enter your Name!");
                return;
            }
            else if (imageIndex == null)
            {
                MessageBox.Show("Please Choose a token");

            }

            playerName = textBox1.Text;

            
            Form2 gameWindow = new Form2(playerName, imageIndex);
            gameWindow.Show();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            imageIndex = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            imageIndex = 1;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            imageIndex = 2;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            imageIndex = 3;
        }
    }
}
