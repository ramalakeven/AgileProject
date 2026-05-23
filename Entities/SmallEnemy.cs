using System;

namespace ScrollShooter
{
    class SmallEnemy : Enemy
    {
        public SmallEnemy(int x, int y, int speed, IEnemyBehavior behavior)
            : base(x, y, speed, behavior)
        {

        }
        public override int Width => 3;
        public override void Draw()
        {
            if (Y >= 0 && Y < Console.WindowHeight)
            {
                Console.SetCursorPosition(X, Y);
                Console.Write(Symbol);
            }
        }
    }
}