using ConsoleGame.GameEngine;
using ConsoleGame.GameObjects;

namespace ConsoleGame
{
    class Program
    {
        static void Main(string[] args)
        {

            /// <summary>
            /// Блокировка доступа к _entities (стены в игре)
            /// </summary>
            object _gameLock = new object();
            //Вам всегда нужен GameWorld и GraphicsManager
            var game = new GameWorld();
            var graphicsManager = new GraphicsManager(game);

            //var player = new Player();
            //var enemy = new Enemy();
            //var boolet = new Boolet();
            //var power = new Power();
            //var terrain = new Terrain();

            game.RegisterEntity(new Player());
            game.RegisterEntity(new Enemy());
            game.RegisterEntity(new Boolet());
            game.RegisterEntity(new Power());
            game.RegisterEntity(new Terrain());
            game.Start();

            while (true)
            {
                // Update the game
                game.Update();

                // Update & draw the scene
                graphicsManager.Update();
                graphicsManager.Draw();

                // Просто чтобы не забивать процессор, частота обнавления по сути
                Thread.Sleep(20);
            }
        }
    }
}


