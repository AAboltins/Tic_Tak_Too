using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class Form1 : Form
    {
        bool xPlayerTurn = true;
        bool xwin = true;
        bool Win = false;
        int turnCount = 0;
        int pictureCounter = 1;
        int pictureCounter2 = 1;
        string onePic = null;
        string twoPic = null;
        string threePic = null;


        PictureBox picture;
        public Form1()
        {
            InitializeComponent();
            InitializeGrid();
            InitializeCells();
        }

        private void InitializeGrid()
        {
            Grid.BackColor = Color.Black;
            Grid.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;

        }

        private void RestartGame()
        {
            InitializeCells();
            turnCount = 0;
            Win = false;
        }


        private void InitializeCells()
        {
            string labelName;
            
            for (int i = 1; i <= 9; i++)
            {
                labelName = "pictureBox" + i;
                PictureBox picture;
                picture = (PictureBox)Grid.Controls[labelName];
                picture.Tag = String.Empty;
                picture.BackColor = Color.Transparent;
                picture.Image = null;
            }            
        }

        private void Player_Click(object sender, EventArgs e)
        {
            PictureBox pic = (PictureBox)sender;

            if (pictureCounter == 1)
            {
                if (pic.Tag != String.Empty)
                {
                    return;
                }

                if (xPlayerTurn)
                {
                    pic.Tag = "X";
                    picture = pic;
                    timer1.Start();
                }
                else
                {
                    pic.Tag = "O";
                    picture = pic;
                    timer1.Start();
                }
                turnCount++;
                PlaySound("click2_sound_wav");
                WinnerCellsChangeColor();
                CheckForDraw();
                xPlayerTurn = !xPlayerTurn;
            }
        }

        private void WinnerCellsChangeColor()
        {
            if (pictureBox1.Tag == pictureBox2.Tag && pictureBox1.Tag == pictureBox3.Tag && pictureBox1.Tag != "")
            {
                ChangeImage(pictureBox1, pictureBox2, pictureBox3);
                
            }
            else if (pictureBox4.Tag == pictureBox5.Tag && pictureBox4.Tag == pictureBox6.Tag && pictureBox4.Tag != "")
            {
                ChangeImage(pictureBox4, pictureBox5, pictureBox6);
                
            }
            else if (pictureBox7.Tag == pictureBox8.Tag && pictureBox7.Tag == pictureBox9.Tag && pictureBox7.Tag != "")
            {
                ChangeImage(pictureBox7, pictureBox8, pictureBox9);
                
            }
            else if (pictureBox1.Tag == pictureBox4.Tag && pictureBox1.Tag == pictureBox7.Tag && pictureBox1.Tag != "")
            {
                ChangeImage(pictureBox1, pictureBox4, pictureBox7);
                
            }
            else if (pictureBox2.Tag == pictureBox5.Tag && pictureBox2.Tag == pictureBox8.Tag && pictureBox2.Tag != "")
            {
                ChangeImage(pictureBox2, pictureBox5, pictureBox8);
                
            }
            else if (pictureBox3.Tag == pictureBox6.Tag && pictureBox3.Tag == pictureBox9.Tag && pictureBox3.Tag != "")
            {
                ChangeImage(pictureBox3, pictureBox6, pictureBox9);
                
            }
            else if (pictureBox1.Tag == pictureBox5.Tag && pictureBox1.Tag == pictureBox9.Tag && pictureBox1.Tag != "")
            {
                ChangeImage(pictureBox1, pictureBox5, pictureBox9);
                
            }
            else if (pictureBox3.Tag == pictureBox5.Tag && pictureBox3.Tag == pictureBox7.Tag && pictureBox3.Tag != "")
            {
                ChangeImage(pictureBox3, pictureBox5, pictureBox7);
               
            }

        }
        private void ChangeImage(PictureBox one, PictureBox two, PictureBox three)
        {
            onePic = one.Name;
            twoPic = two.Name;
            threePic = three.Name;
            if (one.Tag == "O")
            {
                xwin = false;
            }
            else
            {             
                xwin = true;
            }
            Win = true;
        }
        private void PlaySound(string soundName)
        {
            System.IO.Stream str = (System.IO.Stream)Properties.Resources.ResourceManager.GetObject(soundName);
            System.Media.SoundPlayer snd = new System.Media.SoundPlayer(str);
            snd.Play();
        }

        private void CheckForDraw() 
        { 

            if(turnCount == 9 && Win == false)
            {
                
                PlaySound("beep_sound");
                MessageBox.Show("Draw!");                
                RestartGame();
            }
        }

        private void GameOver()
        {
            string winner;
            if (xwin == true)
            {
                winner = "X";
            }
            else
            {
                winner = "O";
            }
            
            MessageBox.Show(winner  + " wins!");
            RestartGame();
        }
        private void Animate()
        {
         
            string pictureName;
            
            string turn;
            timer1.Start();
            turn = picture.Tag.ToString();
            pictureName = turn.ToLower() + "_frame_0" + pictureCounter.ToString("00");
            picture.Image = (Image)Properties.Resources.ResourceManager.GetObject(pictureName);
            picture.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureCounter += 1;
            if (pictureCounter > 20)
            {         
                timer1.Stop();
                if (Win == true)
                {
                    PictureBox onePicture;
                    onePicture = (PictureBox)Grid.Controls[onePic];
                    PictureBox twoPicture;
                    twoPicture = (PictureBox)Grid.Controls[twoPic];
                    PictureBox threePicture;
                    threePicture = (PictureBox)Grid.Controls[threePic];

                    if (xwin == false)
                    {
                        onePicture.Image = (Image)Properties.Resources.ResourceManager.GetObject("o_win");
                        twoPicture.Image = (Image)Properties.Resources.ResourceManager.GetObject("o_win");
                        threePicture.Image = (Image)Properties.Resources.ResourceManager.GetObject("o_win");
                    }
                    else
                    {
                        onePicture.Image = (Image)Properties.Resources.ResourceManager.GetObject("x_win");
                        twoPicture.Image = (Image)Properties.Resources.ResourceManager.GetObject("x_win");
                        threePicture.Image = (Image)Properties.Resources.ResourceManager.GetObject("x_win");
                    }

                    GameOver();
                }
                pictureCounter = 1;
            } 
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Animate();
        }
    }
}
