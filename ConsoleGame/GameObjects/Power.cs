namespace ConsoleGame.GameEngine
{
    /// <summary>
    ///  Персонаж и Управление по умолчанию
    /// </summary>
    internal class Power : GameEntity
    {
        /// <summary>
        /// Чем мы будем управлять
        /// </summary>

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
            //=
            //O
            //=
            Model = new List<Cell> { 
            new Cell(0,8,"="), new Cell(0,9,"="),
            new Cell(0, 10, "="), new Cell(0,11, "="), 
            new Cell(0,12, "="),new Cell(0,13, "="),
            new Cell(0,14,"="), new Cell(0,15, "="),
            new Cell(0,16,"="), new Cell(0,17, "=") };
            Task animationThread = new(Animate);
            animationThread.Start();
        }

     
        /// <summary>
        /// Анимация
        /// </summary>
        private void Animate()
        {
            int i = 17;
            //если на низу
            bool isDown = true;
            while (true)
            {
                foreach(Cell cell in Model)
                {
                    cell.Contents = "="; 
                    if (cell.Y == i)
                    {
                        cell.Contents = "O";
                    }
                }

                if (i >= 17)
                {
                    i = 17;
                    isDown = false;
                }
                else if (i <= 8)
                {
                    i = 8;
                    isDown = true;
                }
                if (isDown) i++;
                if (!isDown) i--;
                Thread.Sleep(350);
            }
        }
    }
}