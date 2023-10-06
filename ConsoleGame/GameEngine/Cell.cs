namespace ConsoleGame.GameEngine
{
    /// <summary>
    /// Одна ячейка в игравом мире
    /// </summary>
    internal class Cell
    {
        #region Свойства 
        /// <summary>
        /// Координаты ячейки по оси x и у в массиве фрейма
        /// </summary>
        public int X { get; set; }
        public int Y { get; set; }

        /// <summary>
        /// Содержание каждой ячейки
        /// </summary>
        public string Contents { get; set; } = " ";

        /// <summary>
        /// Может ли персонаж двигаться
        /// </summary>
        public bool IsWalkable;
        #endregion

        #region Ctors
        /// <summary>
        /// Конструктор
        /// </summary>
        public Cell() { }

        /// <summary>
        /// Ctor for Cell
        /// </summary>
        /// <param name="x">ячейка по оси x</param>
        /// <param name="y">ячейка по оси y</param>
        /// <param name="contents"> " " по умолчанию</param>
        public Cell(int x, int y, string contents = " ")
        {
            X = x;
            Y = y;
            Contents = contents;
            IsWalkable = true;
        }
        #endregion
    }
}