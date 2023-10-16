using ConsoleGame.GameEngine;
using ConsoleGame.GameObjects;

namespace ConsoleGame
{
    class Program
    {
        static void Main(string[] args)
        {

            //Вам всегда нужен GameWorld и GraphicsManager
            var game = new GameWorld();
            var graphicsManager = new GraphicsManager(game);
            //Добавляем объекты в игру
            game.RegisterEntity(new Player());
            game.RegisterEntity(new Enemy());
            game.RegisterEntity(new Boolet());
            game.RegisterEntity(new Power());
            game.RegisterEntity(new Terrain());
            // Стартуем их
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


