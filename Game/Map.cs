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

        PictureBox oPictureBox;

        private int InitialPositionX
        {
            get { return lengthMap / 2; }
        }
        private int InitialPositionY 
        { 
            get { return heightMap / 2; } 
        }

        public Map(PictureBox pictureBox)
        {
            oPictureBox = pictureBox;
            Reset();
        }

        public void Next()
        {
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

            }
        }

        private void Death()
        {

        }   

        public void Reset()
        {
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
