namespace ScrollShooter
{
    public class ZigZagBehavior : IEnemyBehavior
    {
        private bool moveRight = true;

        public void Move(Enemy enemy)
        {
            enemy.Y++;

            if (enemy.Y % 2 == 0)
            {
                if (enemy.X < Console.WindowWidth - enemy.Width)
                    enemy.X++;
            }
            else
            {
                if (enemy.X > 0)
                    enemy.X--;
            }

            moveRight = !moveRight;
        }
    }
}