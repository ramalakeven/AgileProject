namespace ScrollShooter
{
    class BigEnemyFactory : EnemyFactory
    {
        public override Enemy CreateEnemy(int x, int y, int speed, IEnemyBehavior behavior)
        {
            var actualBehavior = behavior ?? new ZigZagBehavior();
            var enemy = new BigEnemy(x, y, speed, actualBehavior);
            enemy.HP = 5;
            enemy.Symbol = "[###]";
            return enemy;
        }
    }
