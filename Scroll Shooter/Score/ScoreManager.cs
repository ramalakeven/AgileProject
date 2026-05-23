using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ScrollShooter
{
    public static class ScoreManager
    {
        private static string statePath = "lastgame.json";

        // Сохраняет текущее состояние игры (имя, счёт, статус квеста)
        public static void SaveGameState(string playerName, int score, bool questCompleted)
        {
            var state = new GameState
            {
                PlayerName = playerName,
                Score = score,
                QuestCompleted = questCompleted,
                Timestamp = DateTime.Now
            };
            string json = JsonSerializer.Serialize(state, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(statePath, json);
        }

        // Загружает последнее состояние 
        public static GameState LoadGameState()
        {
            if (!File.Exists(statePath))
                return null;
            string json = File.ReadAllText(statePath);
            return JsonSerializer.Deserialize<GameState>(json);
        }
    }

    // Класс для сериализации состояния
    public class GameState
    {
        public string PlayerName { get; set; }
        public int Score { get; set; }
        public bool QuestCompleted { get; set; }
        public DateTime Timestamp { get; set; }
    }
}