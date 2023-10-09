using ConsoleGame.GameEngine;

namespace ConsoleGame.GameObjects
{
    /// <summary>
    /// Окружение, задний фон
    /// </summary>
    internal class GameOver : GameEntity
    {
        //само окружения
        public List<Cell> Model = new() { new Cell(42, 4, "O"),
                new Cell(43, 4, "V"),
                new Cell(44, 4, "E"),
                new Cell(45, 4, "R")
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