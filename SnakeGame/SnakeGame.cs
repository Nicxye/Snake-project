using Game;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGame
{
    public partial class SnakeGame : Form
    {
        Map map;
        public SnakeGame()
        {
            InitializeComponent();

            map = new Map(pbxMap, lblScoreAmount);
        }

        private void SnakeGame_Load(object sender, EventArgs e)
        {
            gameTimer.Enabled = true;
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            map.Next();
            map.ShowElements();
        }

        private void SnakeGame_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
                map.CurrentDirection = Map.Direction.Up;
            if (e.KeyCode == Keys.Left)
                map.CurrentDirection = Map.Direction.Left;
            if (e.KeyCode == Keys.Down)
                map.CurrentDirection = Map.Direction.Down;
            if (e.KeyCode == Keys.Right)
                map.CurrentDirection = Map.Direction.Right;
        }
    }
}
