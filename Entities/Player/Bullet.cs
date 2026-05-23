using ScrollShooter;
namespace Scroll_Shooter.Entities.Player
{
    class Bullet : Entity
    {
        public Bullet(int x, int y) : base(x, y) { }

        public override void Update()
        {
            Y--; // летит вверх
        }

        public override void Draw()
        {
            if (Y >= 0 && Y < Console.WindowHeight)
            {
                Console.SetCursorPosition(X, Y);
                Console.Write("|");
            }
        }
    }
}  