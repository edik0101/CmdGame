namespace ConsoleGame.GameEngine
{
    /// <summary>
    ///  Персонаж и Управление по умолчанию
    /// </summary>
    internal class Enemy : GameEntity
    {
        /// <summary>
        /// Чем мы будем управлять
        /// </summary>

        public static List<Cell> Model;
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
            return Model;
        }

        /// <summary>
        /// Вызывает метод контроллера в своем собственном потоке
        /// </summary>
        public override void Start()
        {

            /// <summary>
            /// Стартовая позиция по Х
            /// </summary>        
            int position = 27; //c 27 до 43
            Random random = new Random();
            position = random.Next(27, 39);
            //    ()
            //'-,-[]\
            //    ||
            Model = new List<Cell> {
                new Cell(position + 4,15,"("), new Cell(position + 5,15,")"),
            new Cell(position,16, "'"), new Cell(position + 1,16, "-"), new Cell(position + 2,16, ","),new Cell(position + 3,16, "-"),
            new Cell(position + 4,16, "["),new Cell(position + 5,16, "]"),new Cell(position + 6,16, "\\"),
            new Cell(position + 4,17,"|"), new Cell(position + 5,17, "|") };
            Task animationThread = new(Animate);
            animationThread.Start();
        }

        /// <summary>
        /// Анимация
        /// </summary>
        private void Animate()
        {
            //if (token.IsCancellationRequested)
            //    return;
            while (true)
            {
                foreach (Cell cell in Model)
                {
                    if (cell.Contents == "\\")
                        cell.Contents = "V";
                    else if (cell.Contents == "V")
                        cell.Contents = "\\";

                }

                Thread.Sleep(10000);
            }
        }
    }
}