namespace ScrollShooter
{
    class GameFacade
    {
        private Level level;

        public GameFacade(Level level)
        {
            this.level = level;
        }

        public void UpdateGame()
        {
            level.UpdateEntities();

            level.SpawnEnemies();

            level.HandleBulletCollisions();

            level.HandlePlayerCollisions();

            level.RemoveOffscreenEntities();
        }
    }
}