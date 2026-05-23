using Xunit;
using ScrollShooter;

namespace ScrollShooter.Tests
{
    public class HealthSystemTests
    {
        [Fact]
        public void TakeDamage_ShouldReduceHP()
        {
            HealthSystem health = new HealthSystem(10);

            health.TakeDamage(3);

            Assert.Equal(7, health.CurrentHP);
        }

        [Fact]
        public void TakeDamage_ShouldNotGoBelowZero()
        {
            HealthSystem health = new HealthSystem(5);

            health.TakeDamage(10);

            Assert.Equal(0, health.CurrentHP);
        }

        [Fact]
        public void Heal_ShouldRestoreHP()
        {
            HealthSystem health = new HealthSystem(10);

            health.TakeDamage(5);
            health.Heal(3);

            Assert.Equal(8, health.CurrentHP);
        }

        [Fact]
        public void Heal_ShouldNotExceedMaxHP()
        {
            HealthSystem health = new HealthSystem(10);

            health.Heal(100);

            Assert.Equal(10, health.CurrentHP);
        }

        [Fact]
        public void ZeroDamage_ShouldNotChangeHP()
        {
            HealthSystem health = new HealthSystem(10);

            health.TakeDamage(0);

            Assert.Equal(10, health.CurrentHP);
        }
    }
}