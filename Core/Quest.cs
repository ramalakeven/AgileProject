using System;

namespace ScrollShooter
{
    public class Quest
    {
        private int smallEnemiesKilled = 0;
        private int bigEnemiesKilled = 0;

        private bool completed = false;
        public int SmallEnemiesKilled => smallEnemiesKilled;

        public int BigEnemiesKilled => bigEnemiesKilled;

        public bool Completed => completed;
        public void Subscribe(Level level)
        {
            level.OnEnemyKilled += CheckEnemy;
        }

        private void CheckEnemy(Enemy enemy)
        {
            if (completed)
                return;

            if (enemy is SmallEnemy)
            {
                smallEnemiesKilled++;
            }

            if (enemy is BigEnemy)
            {
                bigEnemiesKilled++;
            }

            Console.SetCursorPosition(0, 0);

            Console.Write(
                $"Quest: Small {smallEnemiesKilled}/3 Big {bigEnemiesKilled}/3     "
            );

            if (smallEnemiesKilled >= 3 &&
                bigEnemiesKilled >= 3)
            {
                completed = true;

                Console.SetCursorPosition(0, 1);

                Console.Write("QUEST COMPLETED!");
            }
        }
    }
}