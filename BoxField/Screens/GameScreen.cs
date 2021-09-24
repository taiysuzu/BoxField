using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoxField
{
    public partial class GameScreen : UserControl
    {
        //player1 button control keys
        Boolean leftArrowDown, rightArrowDown;

        //used to draw boxes on screen
        SolidBrush[] brushArray = new SolidBrush[10];

        //create a list to hold a column of boxes   
        List<Box> boxes = new List<Box>();

        int newBoxCounter = 0;
        Random randGen = new Random();
        int boxX = 150;
        int gap = 400;
        //randGen.Next(100, 175)

        //pattern values
        bool moveRight = true;
        int patternLength = 10;
        int xChange = 20;

        bool patternStage = true;
        int patternStageCounter = 0;
        //hero values
        Box playerBox;

        public GameScreen()
        {
            InitializeComponent();
            OnStart();
        }

        /// <summary>
        /// Set initial game values here
        /// </summary>
        public void OnStart()
        {
            //TODO - set game start values
            //color array
            brushArray[0] = new SolidBrush(Color.White);
            brushArray[1] = new SolidBrush(Color.Red);
            brushArray[2] = new SolidBrush(Color.Blue);
            brushArray[3] = new SolidBrush(Color.Yellow);
            brushArray[4] = new SolidBrush(Color.Purple);
            brushArray[5] = new SolidBrush(Color.Orange);
            brushArray[6] = new SolidBrush(Color.Green);
            brushArray[7] = new SolidBrush(Color.Black);
            brushArray[8] = new SolidBrush(Color.Pink);
            brushArray[9] = new SolidBrush(Color.SkyBlue);

            CreateBox(boxX);
            CreateBox(boxX + gap);

            playerBox = new Box(425, 440, 50, 5, 0);
            Refresh();
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //player 1 button presses
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
            }
        }


        private void gameLoop_Tick(object sender, EventArgs e)
        {
            newBoxCounter++;
            patternStageCounter++;
            //TODO - update location of all boxes (drop down screen)
            foreach (Box b in boxes)
            {
                b.Move();
            }
            if (boxes[0].y >= this.Height)
            {
                boxes.RemoveAt(0);
            }

            if (rightArrowDown == true)
            {
                playerBox.Move("right");
            }
            else if (leftArrowDown == true)
            {
                playerBox.Move("left");
            }

            if (patternStageCounter == 150)
            {
                patternStage = !patternStage;
                patternStageCounter = 0;
            }
            //TODO - add new box if it is time
            if (patternStage == true)
            {
                if (newBoxCounter == 10)
                {

                    if (boxX < 0)
                    {
                        boxX = 0;
                    }
                    if (boxX > this.Width - gap)
                    {
                        boxX = this.Width - gap;
                    }
                    if (moveRight == true)
                    {
                        boxX += xChange;
                    }
                    else
                    {
                        boxX -= xChange;
                    }

                    CreateBox(boxX);
                    CreateBox(boxX + gap);

                    patternLength--;

                    if (patternLength == 0)
                    {
                        moveRight = !moveRight;
                        patternLength = randGen.Next(5, 10);
                        xChange = randGen.Next(15, 40);
                    }

                    newBoxCounter = 0;
                }
            }
            else
            {
                if (newBoxCounter == 10)
                {
                    boxX = randGen.Next(0, this.Width);
                    CreateBox(boxX);
                    newBoxCounter = 0;
                }
            }
            foreach (Box b in boxes)
            {
                if (playerBox.Collision(b) == true)
                {
                    gameLoop.Enabled = false;
                }
            }
            Refresh();
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
            }
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {

            //TODO - draw boxes to screen

            e.Graphics.FillRectangle(brushArray[playerBox.colour], playerBox.x, playerBox.y, playerBox.size, playerBox.size);

            foreach (Box b in boxes)
            {
                e.Graphics.FillRectangle(brushArray[b.colour], b.x, b.y, b.size, b.size);
            }

        }

        public void CreateBox(int x)
        {
            int colorValue = randGen.Next(1, 9);

            Box b = new Box(x, 0, 70, 10, colorValue);
            boxes.Add(b);
        }
    }
}
