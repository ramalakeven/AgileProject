namespace ScrollShooter
{
    public class HealthSystem
    {
        public int MaxHP { get; private set; }

        public int CurrentHP { get; private set; }

        public bool IsDead => CurrentHP <= 0;

        public HealthSystem(int maxHP)
        {
            MaxHP = maxHP;
            CurrentHP = maxHP;
        }

        public void TakeDamage(int damage)
        {
            if (IsDead)
                return;

            CurrentHP -= damage;

            if (CurrentHP < 0)
                CurrentHP = 0;
        }

        public void Heal(int amount)
        {

            CurrentHP += amount;

            if (CurrentHP > MaxHP)
                CurrentHP = MaxHP;
        }

        public void Reset()
        {
            CurrentHP = MaxHP;
        }
    }
}