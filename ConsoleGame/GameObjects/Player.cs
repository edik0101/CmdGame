namespace ConsoleGame.GameEngine
{
    /// <summary>
    ///  Персонаж и Управление по умолчанию
    /// </summary>
    internal class Player : GameEntity
    {
        /// <summary>
        /// Чем мы будем управлять
        /// </summary>
        //         o
        //        /O\
        //        | |
        public List<Cell> Model;
        /// <summary>
        /// Получения кординат нашего объекта управления по Х
        /// </summary>
        public int X { get { return Model.FirstOrDefault()!.X; } }
        /// <summary>
        /// Получения кординат нашего объекта управления по У
        /// </summary>
        public int Y { get { return Model.FirstOrDefault()!.Y; } }

        /// <summary>
        /// Возвращаем Player Cells
        /// </summary>
        /// <returns></returns>
        public override List<Cell> GetCells()
        {
            return Model ;
        }

        /// <summary>
        /// Вызывает метод контроллера в своем собственном потоке
        /// </summary>
        public override void Start()
        {
            Model = new List<Cell> { new Cell(3,15," "), new Cell(4,15,"o"), new Cell(5,15," "),
            new Cell(3,16, "/"), new Cell(4,16, "O"), new Cell(5,16, "\\"),
            new Cell(3,17,"|"), new Cell(4,17, " "),new Cell(5,17, "|") };
            Task playerControllerThread = new(Controller);
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

            while (true)
            {

                keypress = Console.ReadKey(true);

                if (keypress.KeyChar == 'a' && Model.FirstOrDefault()!.X != 2)
                {
                    foreach (Cell cell in Model)
                    {
                        cell.X -= 1;
                    }
                }

                if (keypress.KeyChar == 'd' && Model.FirstOrDefault()!.X != _gameWorld.X - 28)
                {
                    foreach (Cell cell in Model)
                    {
                        cell.X += 1;
                    }
                }

                if (keypress.KeyChar == ' ')
                    Jump();
            }
        }
      
        /// <summary>
        /// Прыжок
        /// </summary>
        private void Jump()
        {
            
            foreach (Cell cell in Model)
            {
                cell.Y -= 2;
                if (cell.Contents == "|")
                    cell.Contents = ">";
            }

            Thread.Sleep(1200);
            foreach (Cell cell in Model)
            {
                cell.Y += 2;
                if (cell.Contents == ">")
                    cell.Contents = "|";
            }
        }
    }
}