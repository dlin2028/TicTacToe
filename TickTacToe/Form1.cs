using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        bool playerFirst;
        bool playerTurn;
        List<Button> buttons;
        List<Label> labels;

        //MinMax minMax;

        private void Form1_Load(object sender, EventArgs e)
        {
            //setting up buttons
            buttons = new List<Button> { button1, button2, button3, button4, button5, button6, button7, button8, button9};
            labels = new List<Label> { label2, label3, label4, label5, label6, label7, label8, label9, label10 };

            Random rng = new Random();
            playerTurn = 1 == rng.Next(2);
            playerFirst = !playerTurn;


            foreach (var button in buttons)
            {
                button.Text = "";

                button.Click += new System.EventHandler((sndr, evt) => 
                {
                    button.Text = playerFirst ? "X" : "O";
                });
                button.Click += new System.EventHandler(changeTurns);
            }

            changeTurns(sender, e);
        }

        private void changeTurns(object sender, EventArgs e)
        {
            playerTurn = !playerTurn;


            foreach (var button in buttons)
            {
                if (button.Text != "")
                {
                    button.Enabled = false;
                }
            }

            if (playerTurn)
            {
                label1.Text = "your turn";
            }
            else
            {
                label1.Text = "cpu turn";

                //cpu stuff
            }
        }
    }
}
