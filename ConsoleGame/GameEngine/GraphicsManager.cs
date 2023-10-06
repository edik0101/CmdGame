using System.Text;

namespace ConsoleGame.GameEngine
{
    /// <summary>
    ///  Натройка окна консоли, отрисовка сцены и обнавление её
    /// </summary>
    internal class GraphicsManager
    {
        #region Поля 
        /// <summary>
        /// Ссылка на отображаемый игровой мир
        /// </summary>
        private GameWorld gameWorld;

        /// <summary>
        /// Буфер сцены для отрисовки
        /// </summary>
        private string gameScene;

        /// <summary>
        /// Построитель строк для создания буфера сцены
        /// </summary>
        private StringBuilder gameSceneBuilder;
        #endregion

        #region Конструктор
        /// <summary>
        /// Стандартный Конструктор должен принимать ссылку на GameWorld.
        /// </summary>
        /// <param name="argGameWorld">GameWorld</param>
        public GraphicsManager(GameWorld argGameWorld, string title = "СonsoleGameEngine")
        {
            Console.Title = title;
            gameWorld = argGameWorld;
            Console.CursorVisible = false;
            Console.SetWindowSize(gameWorld.X + 2, gameWorld.Y + 5);
            Console.BufferWidth = gameWorld.X + 2;
            Console.BufferHeight = gameWorld.Y + 5;
            gameSceneBuilder = new StringBuilder(gameWorld.X * gameWorld.Y + 1);//в скобочках передаем емкость стрингбилдера
        }
        #endregion

        #region Публичные методы
        /// <summary>
        /// обнавление текущей сцены
        /// </summary>
        public void Update()
        {
            //TODO найти частоту обнавления
            gameSceneBuilder.Clear();
            for (int y = 0; y < gameWorld.Y; y++)
            {
                // Draw World
                for (int x = 0; x < gameWorld.X; x++)
                {
                    gameSceneBuilder.Append(gameWorld.GetCellContents(x, y));
                }

                gameSceneBuilder.Append(Environment.NewLine);
            }

            // Draw UI
            gameSceneBuilder.Append(gameWorld.GetUIBuffer());

            gameScene = gameSceneBuilder.ToString();
        }

        /// <summary>
        /// Draws the current scene
        /// </summary>
        public void Draw()
        {
            Console.SetCursorPosition(0, 0);
            Console.Write(gameScene);
        }
        #endregion
    }
}