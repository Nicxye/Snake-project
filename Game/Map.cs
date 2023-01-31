using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    public class Map
    {
        public int scale = 10;
        public int lengthMap = 90;
        public int heightMap = 60;
        private int[,] squares;
        private List<Snake> snake;

        public enum Direction { Right, Down, Left, Up }
        public Direction CurrentDirection = Direction.Right;
        private Food food = null;
        private Random randomNumber = new Random();
        private int score = 0;

        PictureBox oPictureBox;
        Label labelScore;

        private int InitialPositionX
        {
            get { return lengthMap / 2; }
        }
        private int InitialPositionY 
        { 
            get { return heightMap / 2; } 
        }

        public Map(PictureBox oPictureBox, Label labelScore)
        {
            this.oPictureBox = oPictureBox;
            this.labelScore = labelScore;
            Reset();
        }

        public void Next()
        {
            if (food == null)
                GetFood();

            switch(CurrentDirection)
            {
                case Direction.Right:
                    {
                        if (snake[0].X == (lengthMap - 1))
                            Death();
                        else
                            snake[0].X++;
                        break;
                    }
                case Direction.Left:
                    {
                        if (snake[0].X == 0)
                            Death();
                        else
                            snake[0].X--;
                        break;
                    }
                case Direction.Up:
                    {
                        if (snake[0].Y == 0)
                            Death();
                        else
                            snake[0].Y--;
                        break;
                    }
                case Direction.Down:
                    {
                        if (snake[0].Y == (heightMap - 1))
                            Death();
                        else
                            snake[0].Y++;
                        break;
                    }
            }

            Eating();

        }

        private void Eating()
        {
            if (snake[0].X == food.X && snake[0].Y == food.Y)   //If Snake's head touches the food, Snake eats.
            {
                food = null;
                score++;
            }
        }

        private void GetFood()
        {
            int X = randomNumber.Next(0, lengthMap - 1);
            int Y  = randomNumber.Next(0, heightMap - 1);

            food = new Food(X, Y);
        }

        private void Death()
        {
            Reset();
        }   

        public void Reset()
        {
            score = 0;
            snake = new List<Snake>();
            Snake head = new Snake(InitialPositionX, InitialPositionY);
            snake.Add(head);

            squares = new int[lengthMap, heightMap];

            for (int j = 0; j < heightMap; j++)
            {
                for (int i = 0; i < lengthMap; i++)
                {
                    squares[i, j] = 0;
                }
            }
        }

        public void ShowElements()
        {
            Bitmap bmp = new Bitmap(oPictureBox.Width, oPictureBox.Height);

            for (int j = 0; j < heightMap; j++)
            {
                for (int i = 0; i < lengthMap; i++)
                {
                    if (snake.Where(element => element.X == i && element.Y == j).Count() > 0)
                        PaintPixel(bmp, i, j, Color.DarkOliveGreen);
                    else
                        PaintPixel(bmp, i, j, Color.DarkSeaGreen);
                }       
            }

            //Show the food.
            if (food != null)
                PaintPixel(bmp, food.X, food.Y, Color.DarkRed);

            //Show the score on the Forms.
            labelScore.Text = score.ToString();

            //Paints the elements on the map.
            oPictureBox.Image = bmp;
        }

        private void PaintPixel(Bitmap bmp, int x, int y, Color color)
        {
            for (int j = 0; j < scale; j++)
            {
                for (int i = 0; i < scale; i++)
                {
                    bmp.SetPixel(i + (x * scale), j + (y * scale), color);
                }
            }
        }

    }
}
