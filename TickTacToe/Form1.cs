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

        List<Button> buttons;
        List<Label> labels;

        MCGameTree gameTree;

        private void Form1_Load(object sender, EventArgs e)
        {
            //setting up buttons
            buttons = new List<Button> { button1, button2, button3, button4, button5, button6, button7, button8, button9};
            labels = new List<Label> { label2, label3, label4, label5, label6, label7, label8, label9, label10 };

            Random rng = new Random();
            bool humanFirst = 1 == rng.Next(2);


            for (int i = 0; i < buttons.Count; i++)
            {
                var button = buttons[i];
            
                button.Text = "";

                button.Click += new System.EventHandler((sndr, evt) => 
                {
                    if(gameTree.CurrentStatus.IsTerminal)
                    {
                        return;
                    }

                    button.Text = humanFirst ? "X" : "O";
                    gameTree.Move(buttons.IndexOf((Button)sndr));
                    button.Enabled = false;
                });
                button.Click += new System.EventHandler(cpuTurn);
            }

            gameTree = new MCGameTree(humanFirst);
            if(!humanFirst)
            {
                cpuTurn(sender, e);
            }
        }

        private void cpuTurn(object sender, EventArgs e)
        {
            for (int i = 0; i < gameTree.CurrentStatus.Board.Length; i++)
            {
                TileState tile = gameTree.CurrentStatus.Board[i];
                if(tile == TileState.X)
                {
                    buttons[i].Text = "X";
                    buttons[i].Enabled = false;
                }
                else if(tile == TileState.O)
                {
                    buttons[i].Text = "O";
                    buttons[i].Enabled = false;
                }
                else
                {
                    //enable button, clear text
                    ;
                }
            }
        }
    }
}
