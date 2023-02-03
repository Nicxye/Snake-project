using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    internal class Snake
    {
        public int X, Y, formerX, formerY;    //X and Y position of the Snake's body
                                              //and former X and Y positions.
        public Snake(int x, int y)  //Position of Snake.
        {
            X = x;
            Y = y;
            this.formerX = x;
            this.formerY = y;
        }
    }
}
