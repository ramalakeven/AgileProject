using System;

namespace ScrollShooter
{
    public abstract class Enemy : Entity, ICloneable
    {
        public abstract int Width { get; }

        private int moveDelay;
        private int moveTimer = 0;

        private IEnemyBehavior behavior;
        public Enemy(int x, int y, int moveDelay, IEnemyBehavior behavior) : base(x, y)
        {
            this.moveDelay = moveDelay;
            this.behavior = behavior;
        }

        public override void Update()
        {
            moveTimer++;

            if (moveTimer >= moveDelay)
            {
                behavior.Move(this);
                moveTimer = 0;
            }
        }
        public int HP { get; set; }

        public string Symbol { get; set; }
        public virtual object Clone()
        {
            return MemberwiseClone();
        }
    }
}