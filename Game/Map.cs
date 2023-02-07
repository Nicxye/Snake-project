using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Game
{
    public class Map
    {
        public int scale = 20;
        public int lengthMap = 45;
        public int heightMap = 30;
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

            GetHistorySnakeMovement();

            switch(CurrentDirection)
            {
                case Direction.Right:
                    {
                        snake[0].X++;                        
                        break;
                    }
                case Direction.Left:
                    {
                        snake[0].X--;
                        break;
                    }
                case Direction.Up:
                    {
                        snake[0].Y--;
                        break;
                    }
                case Direction.Down:
                    {
                        snake[0].Y++;
                        break;
                    }
            }

            GetNextSnakeMovement();

            Eating();

        }
        private void GetNextSnakeMovement()
        {
            if (snake.Count > 1)
                for (int i = 1; i < snake.Count; i++)
                {
                    snake[i].X = snake[i - 1].formerX;
                    snake[i].Y = snake[i - 1].formerY;
                }
        }

        private void GetHistorySnakeMovement()
        {
            foreach (var square in snake)
            {
                square.formerX = square.X;
                square.formerY = square.Y;
            }
        }

        private void Eating()
        {
            if (snake[0].X == food.X && snake[0].Y == food.Y)   //If Snake's head touches the food, Snake eats.
            {
                food = null;
                score++;

                //Add new element to the snake.
                Snake lastBodyPart = snake[snake.Count - 1];
                Snake newBodyPart = new Snake(lastBodyPart.formerX, lastBodyPart.formerY);
                snake.Add(newBodyPart);
            }
        }

        private void GetFood()
        {
            int X = randomNumber.Next(0, lengthMap - 1);
            int Y  = randomNumber.Next(0, heightMap - 1);

            food = new Food(X, Y);
        }

        public bool Death()
        {
            foreach (var body in snake)
            {
                if (snake.Where(element => element.X == body.X && element.Y == body.Y && body != element).Count() > 0) //Checks if there is at least one part of the Snake
                    return true;                                                                                       //touching its body.
                if (snake[0].X == 0 || snake[0].X == (lengthMap - 1) || snake[0].Y == 0 || snake[0].Y == (heightMap - 1))   //Checks if Snake touched an edge.
                    return true;                                                                                            
            }
            return false;
        }   

        public void Reset()
        {
            score = 0;
            food = null;
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
                    {
                        PaintPixel(bmp, i, j, Color.DarkOliveGreen);

                        if (Death())
                        {
                            PaintPixel(bmp, i, j, Color.Gray);
                            PaintPixel(bmp, snake[0].X, snake[0].Y, Color.Gray);
                        }
                    }
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
