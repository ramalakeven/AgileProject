using System;
using System.Threading;
using Microsoft.Data.Sqlite; // если используете Microsoft.Data.Sqlite

namespace ScrollShooter
{
    class GameManager
    {
        private static GameManager instance;
        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new GameManager();
                return instance;
            }
        }

        public string PlayerName { get; private set; }
        public int MapWidth { get; private set; }
        public int MapHeight { get; private set; }
        public Difficulty Difficulty { get; private set; }

        private bool isRunning = true;
        private Level level;
        private GameFacade facade;

        private GameManager()
        {
            Console.Write("Enter your name: ");
            PlayerName = Console.ReadLine();

            Console.WriteLine("Select difficulty:");
            Console.WriteLine("1 - Easy");
            Console.WriteLine("2 - Normal");
            Console.WriteLine("3 - Hard");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": Difficulty = Difficulty.Easy; break;
                case "3": Difficulty = Difficulty.Hard; break;
                default: Difficulty = Difficulty.Normal; break;
            }

            MapWidth = 30;
            MapHeight = 30;
            Console.SetWindowSize(MapWidth, MapHeight);
            Console.SetBufferSize(MapWidth, MapHeight);

            level = new Level(Difficulty, MapWidth, MapHeight);
            facade = new GameFacade(level);

            level.OnPlayerDeath += OnPlayerDied;
        }

        public void Run()
        {
            Console.CursorVisible = false;

            // Инициализация БД
            DatabaseManager.Initialize();

            // Показываем таблицу лидеров перед началом игры
            ShowTopRecords();

            Console.Clear();
            Console.WriteLine($"Game Started with difficulty: {Difficulty}");
            Thread.Sleep(1500);

            while (isRunning)
            {
                HandleInput();
                facade.UpdateGame();
                Draw();
                Thread.Sleep(50);
            }

            Console.Clear();
            Console.WriteLine("Game closed. Goodbye!");
        }

        private void HandleInput()
        {
            while (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Escape)
                {
                    SaveGameAndExit();
                    isRunning = false;
                }
                level.HandleInput(key);
            }
        }

        private void Draw()
        {
            Console.Clear();
            level.Draw();
        }

        private void OnPlayerDied()
        {
            SaveGameAndExit();
            isRunning = false;
        }

        private void SaveGameAndExit()
        {
            ScoreManager.SaveGameState(PlayerName, level.Score, level.Quest.Completed);
            DatabaseManager.SaveRecord(PlayerName, level.Score, level.Quest.Completed);
        }

        private void ShowTopRecords()
        {
            var top = DatabaseManager.GetTopRecords(5);
            if (top.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("=== No records yet ===");
                Console.WriteLine("Press any key to start...");
                Console.ReadKey(true);
                return;
            }

            Console.Clear();
            Console.WriteLine("========== TOP 5 LEADERBOARD ==========");
            Console.WriteLine("{0,-20} {1,-10} {2,-15}", "Name", "Score", "Quest Completed");
            Console.WriteLine(new string('-', 50));

            foreach (var record in top)
            {
                string questMark = record.QuestCompleted ? "✓" : "✗";
                Console.WriteLine("{0,-20} {1,-10} {2,-15}",
                    record.Name.Length > 20 ? record.Name.Substring(0, 17) + "..." : record.Name,
                    record.Score,
                    questMark);
            }
            Console.WriteLine("========================================");
            Console.WriteLine("Press any key to start the game...");
            Console.ReadKey(true);
        }
    }
}