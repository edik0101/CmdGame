namespace ConsoleGame.GameEngine
{
    /// <summary>
    ///  Персонаж и Управление по умолчанию
    /// </summary>
    internal class СonsoleCursor : GameEntity
    {
        /// <summary>
        /// Чем мы будем управлять
        /// </summary>
        public Cell Model = new Cell(0, 0, "x");

        /// <summary>
        /// Получения кординат нашего объекта управления по Х
        /// </summary>
        public int X { get { return Model.X; } }
        /// <summary>
        /// Получения кординат нашего объекта управления по У
        /// </summary>
        public int Y { get { return Model.Y; } }

        /// <summary>
        /// Возвращаем Cursor Cell
        /// </summary>
        /// <returns></returns>
        public override List<Cell> GetCells()
        {
            return new List<Cell> { Model };
        }

        /// <summary>
        /// Вызывает метод контроллера в своем собственном потоке
        /// </summary>
        public override void Start()
        {
            Thread playerControllerThread = new Thread(Controller);
            playerControllerThread.Start();
        }

        /// <summary>
        /// Управляет курсором
        /// WASD    - movement
        /// SPACE   - select
        /// </summary>
        protected virtual void Controller()
        {
            ConsoleKeyInfo keypress;
            Cell currentCell;

            while (true)
            {

                keypress = Console.ReadKey(true);

                if (keypress.KeyChar == 'a' && Model.X != 0)
                    Model.X -= 1;

                if (keypress.KeyChar == 'd' && Model.X != _gameWorld.X - 1)
                    Model.X += 1;

                if (keypress.KeyChar == 'w' && Model.Y != 0)
                    Model.Y -= 1;

                if (keypress.KeyChar == 's' && Model.Y != _gameWorld.Y - 1)
                    Model.Y += 1;

                if (keypress.KeyChar == ' ')
                    currentCell = _gameWorld.GetCell(Model);
            }
        }
    }
}