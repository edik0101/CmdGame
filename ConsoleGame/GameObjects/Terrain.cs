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
            Model = new List<Cell>() { new Cell(0,0,"*")};
            for(int i = 0; i < _gameWorld.X; i++)
            {
                Model.Add(new Cell(i,18,"="));

            }
        }
    }
}