namespace ScrollShooter
{
    class SmallEnemyFactory : EnemyFactory
    {
        public override Enemy CreateEnemy(int x, int y, int speed, IEnemyBehavior behavior)
        {
            var actualBehavior = behavior ?? new StraightBehavior();
            var enemy = new SmallEnemy(x, y, speed, actualBehavior);
            enemy.HP = 1;
            enemy.Symbol = "[X]";
            return enemy;
        }
    }
}
