using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMcNabbAssignment1
{
    /// <summary>
    /// This application runs a classic Tic-Tac-Toe game; With a spooky twist. 
    /// Brittany McNabb (7853724).
    /// Sunday October 2nd, 2022. 
    /// </summary>
    /// This has been edited twice: March 24th, 2023. 

    //Image references: 
    //Chaos07. (2018-2022). Pumpkin clipart[PNG]. Creazilla. https://creazilla.com/nodes/3162124-pumpkin-clipart
    //Creazilla. (2018-2022).Haunted tree clipart[PNG]. Creazilla. https://creazilla.com/nodes/19020-haunted-tree-clipart
    //LATIN CAPITAL LETTER O [PNG]. (2022). Wiktionary. https://en.wiktionary.org/wiki/O
    //Bequw. (2009). Letter x [SVG]. Wikimedia. https://commons.wikimedia.org/wiki/File:Letter_x.svg

    public partial class TicTacToe : Form
    {

        //Image resources for the game.
        Image o = BMcNabbAssignment1.Properties.Resources.o;
        Image x = BMcNabbAssignment1.Properties.Resources.x;
        Image scary_Pumpkin = BMcNabbAssignment1.Properties.Resources.scary_pumpkin_3;
        Image scary_Tree = BMcNabbAssignment1.Properties.Resources.scary_tree;

        //Declared variables. 
        string the_Winner = "";
        bool players_Turn = true;
        bool did_Someone_Win = false;
        int grid_count = 0;

        public TicTacToe()
        {
            InitializeComponent();
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {

            //The Tic-Tac-Toe form has picture boxes which can be retrieved from the parameter "sender". 
            PictureBox grid = sender as PictureBox;

            //If the user clicks on the picture box and the bool (players_Turn) equals true:          
            if (players_Turn)
            {
                //X starts - The image "X" will appear in the picture box. 
                grid.Image = x;

                //The label at the bottom of the board will change to "Player 2's turn". 
                lblPlayersTurn.Text = "Player 2's turn";
            }
            else
            {
                //The same logic applies here. 
                grid.Image = o;
                lblPlayersTurn.Text = "Player 1's turn";
            }

            //In order to switch the players turn: 
            //If the bool (players_Turn) is true, set true to not true. 
            //If the bool (players_Turn) is false, set false to not false. 
            players_Turn = !players_Turn;

            //The picture box that was clicked will be disabled. 
            grid.Enabled = false;

            //1 will be added to the grid count. 
            grid_count++;

            //This method is called to see if there's a winner. 
            checkForWinner();
        }

        public void checkForWinner()
        {
            //Check horizontal rows
            if ((pBoxA1.Image == pBoxA2.Image) && (pBoxA2.Image == pBoxA3.Image) && (pBoxA1.Image != null)) { did_Someone_Win = true; }
            else if ((pBoxB1.Image == pBoxB2.Image) && (pBoxB2.Image == pBoxB3.Image) && (pBoxB1.Image != null)) { did_Someone_Win = true; }
            else if ((pBoxC1.Image == pBoxC2.Image) && (pBoxC2.Image == pBoxC3.Image) && (pBoxC1.Image != null)) { did_Someone_Win = true; }

            //Check vertical rows
            if ((pBoxA1.Image == pBoxB1.Image) && (pBoxB1.Image == pBoxC1.Image) && (pBoxA1.Image != null)) { did_Someone_Win = true; }
            else if ((pBoxA2.Image == pBoxB2.Image) && (pBoxB2.Image == pBoxC2.Image) && (pBoxA2.Image != null)) { did_Someone_Win = true; }
            else if ((pBoxA3.Image == pBoxB3.Image) && (pBoxB3.Image == pBoxC3.Image) && (pBoxA3.Image != null)) { did_Someone_Win = true; }

            //Check diagonal rows
            if ((pBoxA3.Image == pBoxB2.Image) && (pBoxB2.Image == pBoxC1.Image) && (pBoxA3.Image != null)) { did_Someone_Win = true; }
            if ((pBoxA1.Image == pBoxB2.Image) && (pBoxB2.Image == pBoxC3.Image) && (pBoxA1.Image != null)) { did_Someone_Win = true; }

            //If did_Someone_Win is equal to true:
            if (did_Someone_Win)
            {
                //If the bool (players_Turn) is equal to true:
                if (players_Turn)
                {
                    //Player 2 wins. 
                    the_Winner = "Player 2 - O";
                }
                else
                {
                    //Otherwise, Player 1 wins. 
                    the_Winner = "Player 1 - X";
                }

                //A message box appears declaring the winner. 
                var userInteraction = MessageBox.Show(the_Winner + " wins!\nPress OK to play again.", "Boo!", MessageBoxButtons.OK);

                //If the user clicks OK:
                if (userInteraction == DialogResult.OK)
                {
                    //This method is called to start a new game. 
                    NewGame();
                }           
            }
            else
            {
                //If the grid count reaches 9:
                if (grid_count == 9)
                {
                    //All the picture boxes have been selected and the game ends in a tie. 
                    MessageBox.Show("It's a tie!");
                    //This method is called to start a new game. 
                    NewGame();
                }
            }
        }

        public void NewGame()
        {
            //The bool (players_Turn) is set back to true:
            //X must start first. 
            players_Turn = true;

            //The grid count is set to 0 - keep track of a possible tie for the next game. 
            grid_count = 0;

            //The label shows which player will start the new game. 
            lblPlayersTurn.Text = "Player 1 Starts!";

            //This foreach statement loops throught the controls, in this case picture boxes, of the form: 
            foreach (var pictureBox in Controls.OfType<PictureBox>())
            {
                //Each picture box image is set to null - Erases the image. 
                pictureBox.Image = null;
                //All the picture boxes are enabled again - They can be clicked by the user to show an X or O. 
                pictureBox.Enabled = true;
            }

            //The scary pumpkin, tree, X, and O images will re-appear after all the picture boxes have been set to null.
            pBoxScaryPumpkin.Image = scary_Pumpkin;
            pBoxScaryTree.Image = scary_Tree;
            pBoxPlayer1.Image = x;
            pBoxPlayer2.Image = o;

            //The bool (did_Someone_Win) is set to false to check for a winner again. 
            did_Someone_Win = false; 
        }
    }
}
