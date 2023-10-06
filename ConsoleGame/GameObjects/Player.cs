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
        public List<Cell> Model = new List<Cell> { new Cell(0,15," "), new Cell(1,15,"o"), new Cell(2,15," "), 
            new Cell(0,16, "/"), new Cell(1,16, "O"), new Cell(2,16, "\\"),
            new Cell(0,17,"|"), new Cell(1,17, " "),new Cell(2,17, "|") };
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
            Task playerControllerThread = new(Controller);
            playerControllerThread.Start();
            Task animationThread = new(Animate);
            animationThread.Start();
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

                if (keypress.KeyChar == 'a' && Model.FirstOrDefault()!.X != 0)
                {
                    foreach (Cell cell in Model)
                    {
                        cell.X -= 1;
                    }
                }

                if (keypress.KeyChar == 'd' && Model.FirstOrDefault()!.X != _gameWorld.X - 3)
                {
                    foreach (Cell cell in Model)
                    {
                        cell.X += 1;
                    }
                }

                //if (keypress.KeyChar == 'w' && Model.FirstOrDefault()!.Y != 0)
                //{
                //    foreach (Cell cell in Model)
                //    {
                //        cell.Y -= 1;
                //    }
                //}


                //if (keypress.KeyChar == 's' && Model.FirstOrDefault()!.Y != _gameWorld.Y - 3)
                //{
                //    foreach (Cell cell in Model)
                //    {
                //        cell.Y += 1;
                //    }
                //}

                if (keypress.KeyChar == ' ')
                    currentCell = _gameWorld.GetCell(Model.FirstOrDefault()!);
            }
        }
        /// <summary>
        /// Анимация
        /// </summary>
        private void Animate()
        {
            while (true)
            {
                foreach(Cell cell in Model)
                {
                    if(cell.Contents == "|")
                        cell.Contents = ">";
                    else if (cell.Contents == ">")
                        cell.Contents = "|";
                    
                }
               

                Thread.Sleep(1000);
            }
        }
    }
}