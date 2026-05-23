using ScrollShooter;
using System;

namespace Scroll_Shooter.Entities.Player
{
    class Player : Entity
    {
        private int mapWidth;

        public Player(int x, int y, int mapWidth) : base(x, y)
        {
            this.mapWidth = mapWidth;
        }

        public void HandleInput(ConsoleKey key)
        {
            if (key == ConsoleKey.LeftArrow && X > 0)
                X--;

            if (key == ConsoleKey.RightArrow && X < mapWidth - 1)
                X++;
        }

        public override void Update()
        {

        }

        public override void Draw()
        {
            if (Y >= 0 && Y < Console.WindowHeight)
            {
                Console.SetCursorPosition(X, Y);
                Console.Write("A");
            }
        }
    }
}