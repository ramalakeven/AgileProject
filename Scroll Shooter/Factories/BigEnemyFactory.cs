namespace ScrollShooter
{
    class BigEnemyFactory : EnemyFactory
    {
        public override Enemy CreateEnemy(int x, int y, int speed, IEnemyBehavior behavior)
        {
            var enemy = new BigEnemy(x, y, speed, new ZigZagBehavior());
            enemy.HP = 5;
            enemy.Symbol = "[###]";
            return enemy;
        }
    }
}