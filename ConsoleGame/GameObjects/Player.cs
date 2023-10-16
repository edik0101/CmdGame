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
            return Model == null ? new List<Cell>() { new Cell(0, 0, " ") } : Model;
        }
        /// <summary>
        /// Возвращаем ячейку гранаты Cells
        /// </summary>
        /// <returns></returns>
        public List<Cell> GetGranateCells()
        {
            return Model.Where<Cell>(x => x.Contents == "0").ToList();
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
        protected void Controller()
        {

            ConsoleKeyInfo keypress;

            while (true)
            {
                keypress = Console.ReadKey(true);

                if (keypress.KeyChar == 'a' && Model.FirstOrDefault()!.X != 2)
                {
                    foreach (Cell cell in Model)
                    {
                        if (cell.Contents != "0")
                            cell.X -= 1;
                    }
                }

                if (keypress.KeyChar == 'd' && Model.FirstOrDefault()!.X != _gameWorld.X - 28)
                {
                    foreach (Cell cell in Model)
                    {
                        if (cell.Contents != "0")
                            cell.X += 1;
                    }
                }

                if (keypress.KeyChar == ' ')
                    Jump();
                if (keypress.KeyChar == 'e')
                {
                    Task r = new Task(SrowGranate);
                    r.Start();
                }
                if (keypress.KeyChar == 'r')
                {
                    _gameWorld.isRestart = true;
                }
            }
        }

        /// <summary>
        /// Прыжок
        /// </summary>
        private void Jump()
        {

            foreach (Cell cell in Model)
            {
                if (cell.Contents != "0")
                {
                    cell.Y -= 2;
                    if (cell.Contents == "|")
                        cell.Contents = ">";
                }
            }

            Thread.Sleep(1200);
            foreach (Cell cell in Model)
            {
                if (cell.Contents != "0")
                {
                    cell.Y += 2;
                    if (cell.Contents == ">")
                        cell.Contents = "|";
                }
            }
        }

        private void SrowGranate()
        {
            bool isTop = false;

            int granatePower = 5;

            List<GameEntity> gameEntitys = _gameWorld.GetEntitys();
            foreach (var entity in gameEntitys)
            {
                if (entity.GetType().Name == "Power")
                {
                    foreach (var en in entity.GetCells())
                    {
                        if (en.Contents == "O")
                            granatePower = en.Y;
                    }

                };
            }
            Cell granate = new Cell(X + 2, 16, "0");
            Model.Add(granate);
            while (granate.Y < _gameWorld.Y - 3)
            {

                if (!isTop)
                {
                    if (granate.Y > granatePower - 2)
                    {
                        granate.Y -= 1;
                        if (granate.X < _gameWorld.X - 1)
                            granate.X += 1;
                    }
                    else
                    {
                        isTop = true;
                    }
                }
                else
                {
                    if (granate.Y < _gameWorld.Y - 3)
                    {
                        granate.Y += 1;
                        if (granate.X < _gameWorld.X - 1)
                            granate.X += 1;
                        if (granate.Y == 16)
                        {
                            Model.Remove(granate);
                        }
                    }
                }
                Thread.Sleep(250);
            }         
            Model.Remove(granate);
        }
    }
}