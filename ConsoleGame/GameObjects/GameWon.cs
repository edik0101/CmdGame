using ConsoleGame.GameEngine;

namespace ConsoleGame.GameObjects
{
    /// <summary>
    /// Окружение, задний фон
    /// </summary>
    internal class GameWon : GameEntity
    {
        //само окружения
        public List<Cell> Model = new() { new Cell(42, 4, "W"),
                new Cell(43, 4, "O"),
                new Cell(44, 4, "N")
            };

        public override List<Cell> GetCells()
        {
            return Model;
        }

        public override void Start()
        {
            throw new NotImplementedException();
        }
    }
}