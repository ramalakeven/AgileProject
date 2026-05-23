using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;

namespace ScrollShooter
{
    public static class DatabaseManager
    {
        private static string connectionString = "Data Source=records.db";

        public static void Initialize()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string sql = @"
                    CREATE TABLE IF NOT EXISTS Records (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        Score INTEGER NOT NULL,
                        QuestCompleted INTEGER NOT NULL,
                        Date TEXT NOT NULL
                    )";
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void SaveRecord(string name, int score, bool questCompleted)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string sql = @"
                    INSERT INTO Records (Name, Score, QuestCompleted, Date)
                    VALUES ($name, $score, $completed, $date)";
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.Parameters.AddWithValue("$name", name);
                    command.Parameters.AddWithValue("$score", score);
                    command.Parameters.AddWithValue("$completed", questCompleted ? 1 : 0);
                    command.Parameters.AddWithValue("$date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    command.ExecuteNonQuery();
                }
            }
        }

        public static List<ScoreEntry> GetTopRecords(int topCount = 5)
        {
            var result = new List<ScoreEntry>();
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string sql = @"
                    SELECT Name, Score, QuestCompleted
                    FROM Records
                    ORDER BY Score DESC
                    LIMIT $top";
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.Parameters.AddWithValue("$top", topCount);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new ScoreEntry
                            {
                                Name = reader.GetString(0),
                                Score = reader.GetInt32(1),
                                QuestCompleted = reader.GetInt32(2) == 1
                            });
                        }
                    }
                }
            }
            return result;
        }
    }
}