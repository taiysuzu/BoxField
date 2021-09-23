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

            Box playerBox = new Box(425, 440, 50, 0, 0);
            boxes.Add(playerBox);

            Box b = new Box(randGen.Next(100, 175), 0, 75, 8, randGen.Next(1, 9));
            boxes.Add(b);

            Box b2 = new Box(b.x + 525, 0, 75, 8, b.colour);
            boxes.Add(b2);



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

            //TODO - update location of all boxes (drop down screen)
            foreach (Box b in boxes)
            {
                b.y += b.speed;
            }
            foreach (Box b2 in boxes)
            {
                b2.y += b2.speed;
            }

            if (boxes[1].y >= this.Height)
            {
                boxes.RemoveAt(1);
            }


            //TODO - add new box if it is time
            if (newBoxCounter == 5)
            {
                Box b = new Box(randGen.Next(100, 200), 0, 75, 8, randGen.Next(1, 9));
                boxes.Add(b);

                Box b2 = new Box(b.x + 500, 0, 75, 8, b.colour);
                boxes.Add(b2);

                newBoxCounter = 0;
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
            foreach (Box playerBox in boxes)
            {
                e.Graphics.FillRectangle(brushArray[playerBox.colour], playerBox.x, playerBox.y, playerBox.size, playerBox.size);
            }
            foreach (Box b in boxes)
            {
                e.Graphics.FillRectangle(brushArray[b.colour], b.x, b.y, b.size, b.size);
            }

        }
    }
}
