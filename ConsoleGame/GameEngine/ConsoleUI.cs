using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.GameEngine
{
    /// <summary>
    /// отрисовка UI
    /// </summary>
    internal abstract class ConsoleUI
    {
        /// <summary>
        /// Ссылка на GameWorld, которому принадлежит UI.
        /// </summary>
        protected GameWorld _gameWorld = new GameWorld();

        /// <summary>
        /// Буфер пользовательского интерфейса для отрисовки
        /// </summary>
        protected string _uiBuffer = String.Empty;

        /// <summary>
        /// Конструктор пользовательского интерфейса для создания буфера пользовательского интерфейса
        /// </summary>
        protected StringBuilder _uiBufferBuilder = new StringBuilder();

        /// <summary>
        /// Возвращает полный пользовательский интерфейс в GameWorld.
        /// </summary>
        /// <returns></returns>
        public string GetUI()
        {
            return _uiBuffer;
        }

        /// <summary>
        /// Создайте собственный пользовательский интерфейс с помощью этого переопределения.
        /// </summary>
        public abstract void Update();

        /// <summary>
        /// Передача gameWorld в ConsoleUI
        /// </summary>
        /// <param name="gameWorld"></param>
        internal void RegisterGameWorld(GameWorld gameWorld)
        {
            _gameWorld = gameWorld;
        }
    }
}
