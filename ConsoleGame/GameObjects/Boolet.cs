namespace ConsoleGame.GameEngine
{
    /// <summary>
    ///  Персонаж и Управление по умолчанию
    /// </summary>
    internal class Boolet : GameEntity
    {
        /// <summary>
        /// Чем мы будем управлять
        /// </summary>
        //  -
        public Cell Model;
        /// <summary>
        /// Получения кординат нашего объекта управления по Х
        /// </summary>
        public int X { get { return Model.X; } }
        /// <summary>
        /// Получения кординат нашего объекта управления по У
        /// </summary>
        public int Y { get { return Model.Y; } }

        //Позиция старта пули
        int booletStart = 25;
        /// <summary>
        /// Возвращаем Boolet Cells
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
            List<GameEntity> gameEntitys = _gameWorld.GetEntitys();
            foreach (var entity in gameEntitys)
            {
                if (entity.GetType().Name == "Enemy")
                {
                    foreach(var en in entity.GetCells())
                    {
                        if (en.Contents == "'")
                            booletStart = en.X - 1;
                    }
                };
            }
            Model = new Cell(booletStart, 16,"-");
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
                if (Model.X <= 1)
                {
                    Model.X = booletStart;
                    Thread.Sleep(5000);
                }
                else
                {
                    Model.X--;
                }

                Thread.Sleep(250);
            }
        }
    }
}