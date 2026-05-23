using System;

namespace ScrollShooter
{
    abstract class EnemyFactory
    {
        public abstract Enemy CreateEnemy(int x, int y, int speed, IEnemyBehavior behavior);
    }
}