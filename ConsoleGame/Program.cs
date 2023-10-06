using ConsoleGame.GameEngine;
using System.Numerics;

namespace ConsoleGame
{
    class Program
    {
        static void Main(string[] args)
        {
            //Вам всегда нужен GameWorld и GraphicsManager
            var game = new GameWorld();
            var graphicsManager = new GraphicsManager(game);

            // Кастомные сущности в игре сделанные нами
            //var player = new Player();
            var player = new СonsoleCursor();
            //var dog = new Dog();
            //player.RegisterDog(dog);
            //var walls = new Terrain();

            // Register your custom Entity
            //game.RegisterEntity(player);
            game.RegisterEntity(player);
            //game.RegisterEntity(dog);
            //game.RegisterEntity(walls);

            // Standard Game Loop
            game.Start();
            while (true)
            {
                // Update the game
                game.Update();

                // Update & draw the scene
                graphicsManager.Update();
                graphicsManager.Draw();

                // Просто чтобы не забивать процессор
                Thread.Sleep(200);
            }
        }
    }
}