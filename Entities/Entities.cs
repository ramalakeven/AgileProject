using System;
using System.Threading;

namespace ScrollShooter
{
    public abstract class Entity
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Entity(int x, int y)
        {
            X = x;
            Y = y;
        }

        public abstract void Update();
        public abstract void Draw();
    }
}
