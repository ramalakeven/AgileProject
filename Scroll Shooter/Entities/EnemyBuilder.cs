namespace ScrollShooter
{
    class EnemyBuilder
    {
        private Enemy enemy;

        public EnemyBuilder CreateSmallEnemy(int x, int y, int speed, IEnemyBehavior behavior)
        {
            enemy = new SmallEnemy(x, y, speed, behavior);

            return this;
        }

        public EnemyBuilder CreateBigEnemy(int x, int y, int speed, IEnemyBehavior behavior)
        {
            enemy = new BigEnemy(x, y, speed, behavior);

            return this;
        }

        public EnemyBuilder SetHP(int hp)
        {
            enemy.HP = hp;

            return this;
        }

        public EnemyBuilder SetSymbol(string symbol)
        {
            enemy.Symbol = symbol;

            return this;
        }

        public Enemy Build()
        {
            return enemy;
        }
    }
}