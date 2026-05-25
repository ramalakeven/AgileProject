using Scroll_Shooter.Entities.Player;
using System;
using System.Collections.Generic;
using System.Threading;

namespace ScrollShooter
{
    public class Level
    {
        public event Action<Enemy> OnEnemyKilled;
        public event Action OnPlayerDeath;   
        private int mapWidth;
        private int mapHeight;
        private Difficulty difficulty;
        private List<Entity> entities = new List<Entity>();
        private Player player;
        private Quest quest;
        private HealthSystem health = new HealthSystem(3);
        private int score = 0;
        private Random random = new Random();
        private int enemySpawnTimer = 0;
        private int enemySpawnDelay = 10;

       
        private EnemyFactory smallEnemyFactory;
        private EnemyFactory bigEnemyFactory;

        public int Score => score;
        public Quest Quest => quest;

        public Level(Difficulty difficulty, int mapWidth, int mapHeight)
        {
            this.difficulty = difficulty;
            this.mapWidth = mapWidth;
            this.mapHeight = mapHeight;
            this.difficulty = difficulty;

           
            switch (difficulty)
            {
                case Difficulty.Easy:
                    enemySpawnDelay = 20;   
                    break;
                case Difficulty.Hard:
                    enemySpawnDelay = 5; 
                    break;
                default:
                    enemySpawnDelay = 10;
                    break;
            }
            player = new Player(mapWidth / 2, mapHeight - 4, mapWidth);
            quest = new Quest();
            quest.Subscribe(this);
            entities.Add(player);

            smallEnemyFactory = new SmallEnemyFactory();
            bigEnemyFactory = new BigEnemyFactory();
        }

        public void HandleInput(ConsoleKey key)
        {
            player.HandleInput(key);
            if (key == ConsoleKey.Spacebar)
            {
                entities.Add(new Bullet(player.X, player.Y - 1));
            }
        }



        public void UpdateEntities()
        {
            foreach (var e in entities)
                e.Update();
            enemySpawnTimer++;
        }

        public void SpawnEnemies()
        {
            if (enemySpawnTimer >= enemySpawnDelay)
            {
                Enemy enemy;
                if (random.Next(2) == 0)
                {
      IEnemyBehavior behavior;
                    if (difficulty == Difficulty.Hard)
                        behavior = new ZigZagBehavior();
                    else
                        behavior = new StraightBehavior();

                    enemy = smallEnemyFactory.CreateEnemy(
                        random.Next(0, mapWidth - 3), 0, 3, behavior);
                }
                else
                {
                    enemy = bigEnemyFactory.CreateEnemy(
                        random.Next(0, mapWidth - 5), 0, 4, new ZigZagBehavior());
                }
                entities.Add(enemy);
                enemySpawnTimer = 0;
            }
        }

        public void RemoveOffscreenEntities()
        {
            entities.RemoveAll(e =>
                e.Y < 0 || e.Y >= mapHeight - 2);
        }

        public void HandleBulletCollisions()
        {
            Entity bulletToRemove = null;
            Entity enemyToRemove = null;

            for (int i = 0; i < entities.Count; i++)
            {
                if (entities[i] is Bullet bullet)
                {
                    for (int j = 0; j < entities.Count; j++)
                    {
                        if (entities[j] is Enemy enemy)
                        {
                            if (Math.Abs(bullet.Y - enemy.Y) <= 1 &&
                                bullet.X >= enemy.X &&
                                bullet.X <= enemy.X + enemy.Width - 1)
                            {
                                bulletToRemove = bullet;
                                enemyToRemove = enemy;
                                OnEnemyKilled?.Invoke(enemy);
                                score++;
                                break;
                            }
                        }
                    }
                }
            }

            if (bulletToRemove != null)
                entities.Remove(bulletToRemove);
            if (enemyToRemove != null)
                entities.Remove(enemyToRemove);
        }

        public void HandlePlayerCollisions()
        {
            for (int i = 0; i < entities.Count; i++)
            {
                if (entities[i] is Enemy enemy)
                {
                    if (enemy.Y == player.Y &&
                        player.X >= enemy.X &&
                        player.X <= enemy.X + enemy.Width - 1)
                    {
                        entities.RemoveAt(i);
                        health.TakeDamage(1);
                        i--;

                        if (health.IsDead)
                        {
                          
                            OnPlayerDeath?.Invoke();
                        }
                    }
                }
            }
        }

        public void Draw()
        {
            foreach (var e in entities)
                e.Draw();

            Console.SetCursorPosition(0, 0);
            Console.Write(
                $"Quest: Small {quest.SmallEnemiesKilled}/3 " +
                $"Big {quest.BigEnemiesKilled}/3     "
            );

            if (quest.Completed)
            {
                Console.SetCursorPosition(0, 1);
                Console.Write("QUEST COMPLETED!          ");
            }

            int hudY = Console.WindowHeight - 2;
            Console.SetCursorPosition(0, hudY);
            Console.Write(new string('_', Console.WindowWidth));
            Console.SetCursorPosition(0, hudY + 1);
            Console.Write($"HP: {health.CurrentHP}  SCORE: {score}");
        }
    }
}
