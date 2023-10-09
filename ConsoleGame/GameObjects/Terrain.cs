using ConsoleGame.GameEngine;

namespace ConsoleGame.GameObjects
{
    /// <summary>
    /// Окружение, задний фон
    /// </summary>
    internal class Terrain : GameEntity
    {
        //само окружения
        public List<Cell> Model;

        public override List<Cell> GetCells()
        {
            return Model;
        }

        public override void Start()
        {

            Model = new() { new Cell(7, 2, "_"), new Cell(8, 2, "_"), new Cell(9, 2, "_"),
            new Cell(6,3, "("), new Cell(7,3, "_"), new Cell(8,3, "_"), new Cell(9,3, "_"), new Cell(10,3, ")"),
            new Cell(5,4,"("), new Cell(6,4, "_"), new Cell(7,4, "_"), new Cell(8,4, "_"), new Cell(9,4, "_"), new Cell(10,4, "_"),new Cell(11,4, ")"),

            new Cell(23, 1, "_"), new Cell(24, 1, "_"), new Cell(25, 1, "_") ,
            new Cell(22,2, "("), new Cell(23,2, "_"), new Cell(24,2, "_"), new Cell(25,2, "_"), new Cell(26,2, ")"),
            new Cell(21,3,"("), new Cell(22,3, "_"), new Cell(23,3, "_"), new Cell(24,3, "_"), new Cell(25,3, "_"), new Cell(26,3, "_"),new Cell(27,3, ")"),

            new Cell(43, 2, "C"), new Cell(44, 2, "M"), new Cell(45, 2, "D"),
            new Cell(42,3,"G"), new Cell(43,3, "A"), new Cell(44,3, "M"), new Cell(45,3, "E"),
            new Cell(6, 5, "*"),new Cell(7, 5, " "),new Cell(8, 5, "*"),new Cell(8, 5, "*"),new Cell(9, 5, " "),new Cell(10, 5, "*"),
            new Cell(6, 6, " "),new Cell(7, 6, "*"),new Cell(8, 6, " "),new Cell(9, 6, "*"),new Cell(10, 6, " "),
            new Cell(25, 17, "|"), new Cell(26, 17, "|")
        };

            for (int i = 0; i < _gameWorld.X; i++)
            {
                Model.Add(new Cell(i, 18, "="));
                Model.Add(new Cell(i, 19, "#"));
            }


            Task animationThread = new(Animate);
            animationThread.Start();
        }

        /// <summary>
        /// Анимация
        /// </summary>
        private void Animate()
        {
            while (true)
            {
                foreach (Cell cell in Model)
                {
                    if (GetAnimatedCell(cell))
                    {
                        if (cell.Contents == "*")
                            cell.Contents = " ";
                        else cell.Contents = "*";
                    }
                }
                Thread.Sleep(2000);
            }
        }
        /// <summary>
        /// Получение Cell по условию
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private bool GetAnimatedCell(Cell cell)
        {
            if (Enumerable.Range(6,11).Contains(cell.X) && Enumerable.Range(5, 6).Contains(cell.Y))
                return true;
            else
                return false;
        }
    }
}