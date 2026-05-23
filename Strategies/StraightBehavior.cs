namespace ScrollShooter
{
    public class StraightBehavior : IEnemyBehavior
    {
        public void Move(Enemy enemy)
        {
            enemy.Y++;
        }
    }
}