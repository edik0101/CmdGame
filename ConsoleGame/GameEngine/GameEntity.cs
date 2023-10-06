namespace ConsoleGame.GameEngine
{
    /// <summary>
    /// Игравая сущьность 
    /// отвечает за получение всех ячеек и старт
    /// поле GameWorld _gameWorld
    /// </summary>
    internal abstract class GameEntity
    {
        /// <summary>
        /// Ссылка на GameWorld, частью которого является сущность.
        /// </summary>
        public GameWorld _gameWorld;
        /// <summary>
        /// Верните все ячейки, которые контролирует этот GameEntity.
        /// </summary>
        /// <returns>все ячейки</returns>
        public abstract List<Cell> GetCells();
        /// <summary>
        /// С целью запуска потока управления и настройки объекта
        /// </summary>
        public abstract void Start();
    }
}