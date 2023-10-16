using ConsoleGame.GameObjects;
using System.ComponentModel.Design;

namespace ConsoleGame.GameEngine
{
    /// <summary>
    /// Игравой мир
    /// </summary>
    internal class GameWorld
    {
        #region Поля
        /// <summary>
        /// Ширина мира
        /// </summary>
        public int X = 50;
        /// <summary>
        /// Высота мира
        /// </summary>
        public int Y = 20;

        /// <summary>
        /// Двумерный массив 
        /// </summary>
        private Cell[,] _board = new Cell[0,0];
        private List<GameEntity> _entities = new List<GameEntity>();
        //TODO посмотреть на замену null значением по умолчанию
        private ConsoleUI? _ui = null;

        /// <summary>
        /// Блокировка доступа к _board (стены GameWorld)
        /// </summary>
        private readonly object _boardLock = new object();
        /// <summary>
        /// Блокировка доступа к _entities (стены в игре)
        /// </summary>
        private readonly object _entitiesLock = new object();

        int playerStomachX = 0;
        int playerStomachY = 0;
        int enemyHeadX = 0;
        int enemyHeadY = 0;
        public bool isRestart = false;
        public bool gameOver = false;
        public bool gameWon = false;
        #endregion

        #region Конструктор
        /// <summary>
        /// Standard Конструктор
        /// </summary>
        public GameWorld()
        {
            _board = new Cell[X, Y];
            InitiateGameBoard();
        }

        /// <summary>
        /// Конструктор с размером мира
        /// </summary>
        public GameWorld(int x, int y)
        {
            X = x; y = Y;
            _board = new Cell[X, Y];
            InitiateGameBoard();
        }
        #endregion

        #region Публичные методы
        /// <summary>
        /// Start GameWorld
        /// </summary>
        public void Start()
        {
            lock (_entitiesLock)
            {
                foreach (GameEntity entity in _entities)
                {
                    entity.Start();
                }
            }
        }
        Player _entitiPlayer = null;
        Enemy _entitiEnemy = null;
        Boolet _entitiBoolet = null;
        GameOver _gameOver = new GameOver();
        GameWon _gameWon = new GameWon();

        /// <summary>
        /// Обнавление GameWorld каждый frame
        /// и логика игры
        /// </summary>
        public void Update()
        {

            lock (_boardLock)
            {
                lock (_entitiesLock)
                {
                    if (!isRestart) {
                        InitiateGameBoard();
                        if (_entities.Count == 0)
                            return;
                        if (gameOver) //если игрок погиб
                        {
                            foreach (var entity in _entities)
                            {
                                if (entity.GetType().Name == "Player")
                                {
                                    _entitiPlayer = (Player)entity;
                                }
                            }
                            // если игрук есть то убираем
                            var playerIndex = _entities.FindIndex(x => x == _entitiPlayer);
                            if (_entitiPlayer != null && playerIndex != -1)
                                _entities.Remove(_entitiPlayer);

                            var gameOverIndex = _entities.FindIndex(x => x == _gameOver);
                            if (gameOverIndex == -1)
                            {
                                _entities.Add(_gameOver);
                            }
                           
                            gameOver = false;
                            

                        }
                        if (gameWon) // если игрок победил
                        {
                            foreach (var entity in _entities)
                            {
                                if (entity.GetType().Name == "Enemy")
                                {
                                    _entitiEnemy = (Enemy)entity;
                                }
                                if (entity.GetType().Name == "Boolet")
                                {
                                    _entitiBoolet = (Boolet)entity;
                                }
                            }
                            var enemyIndex = _entities.FindIndex(x => x == _entitiEnemy);
                            if (_entitiEnemy != null && enemyIndex != -1)
                                _entities.Remove(_entitiEnemy);

                            var booletIndex = _entities.FindIndex(x => x == _entitiBoolet);
                            if (_entitiEnemy != null && booletIndex != -1)
                                _entities.Remove(_entitiBoolet);
                           

                            var gameWonIndex = _entities.FindIndex(x => x == _gameWon);
                            if (gameWonIndex == -1)
                            {
                                _entities.Add(_gameWon);
                            }
                            
                            gameWon = false;
                            
                        }

                        foreach (var entity in _entities)
                        {
                            foreach (Cell cell in entity.GetCells().ToList())
                            {
                                if(cell == null) continue;
                                //дополнительнвая проверка,чтобы не выйти за рамки
                                if (cell.X > X || cell.Y > Y || cell.X < 0 || cell.Y < 0)
                                    continue;
                                _board[cell.X, cell.Y] = cell;
                            }
                            //Получаем координаты живота игрока
                            if (entity.GetType().Name == "Player")
                            {
                                foreach (Cell cell in entity.GetCells().ToList())
                                {
                                    if (cell == null) continue;
                                    if (cell.Contents == "O")
                                    {
                                        playerStomachX = cell.X;
                                        playerStomachY = cell.Y;
                                    }
                                }
                            }
                            //Получаем координаты головы врага
                            if (entity.GetType().Name == "Enemy")
                            {
                                foreach (Cell cell in entity.GetCells().ToList())
                                {
                                    if (cell.Contents == "(")
                                    {
                                        enemyHeadX = cell.X;
                                        enemyHeadY = cell.Y;
                                    }
                                }
                            }
                            //Если попал враг
                            if (entity.GetType().Name == "Boolet")
                            {
                                foreach (Cell cell in entity.GetCells().ToList())
                                {
                                    if (cell.Y == playerStomachY && cell.X == playerStomachX)
                                    {
                                        gameOver = true;
                                    }
                                }
                            }
                            //Если попал игрок
                            if (entity.GetType().Name == "Player")
                            {
                                foreach (Cell cell in (entity as Player).GetGranateCells())
                                {

                                    if  (cell.Y == enemyHeadY && cell.X == enemyHeadX)
                                    {
                                        gameWon = true;
                                    }
                                }
                            }
                        } 
                    }
                    else
                    {
                        //если был рестарт добавляем объекты обратно в список entities
                        if (_entitiPlayer != null)
                        {
                            var playerIndex = _entities.FindIndex(x => x == _entitiPlayer);
                            if (playerIndex == -1)
                            {
                                _entities.Add(_entitiPlayer);
                            }
                        }

                        if (_entitiEnemy != null)
                        {
                            var enemyIndex = _entities.FindIndex(x => x == _entitiEnemy);
                            if (enemyIndex == -1)
                            {
                                _entities.Add(_entitiEnemy);
                                _entitiEnemy.Start();
                            }
                        }

                        if (_entitiBoolet != null)
                        {
                            var booletIndex = _entities.FindIndex(x => x == _entitiBoolet);
                            if (booletIndex == -1)
                            {
                                _entities.Add(_entitiBoolet);
                                _entitiBoolet.Start();
                            }
                        }
                        var gameOverIndex = _entities.FindIndex(x => x == _gameOver);
                        if (gameOverIndex != -1)
                        {
                            _entities.Remove(_gameOver);
                        }
                        
                        var gameWonIndex = _entities.FindIndex(x => x == _gameWon);
                        if (gameWonIndex != -1)
                        {
                            _entities.Remove(_gameWon);
                        }       
                        
                        isRestart = false;
                    }
                }
                 

                if (_ui != null)
                    _ui.Update();
            }
        }

        /// <summary>
        /// Удаляем объекты
        /// </summary>
        public void ClearWorld()
        {
            lock (_entitiesLock)
            {
                _entities.Clear();
            }
        }
        
        /// <summary>
        /// Зарегистрируйте GameWorld в GameEntity
        /// Регистрирует все игровые сущности
        /// </summary>
        /// <param name="entity"></param>
        public void RegisterEntity(GameEntity entity)
        {
            lock (_entitiesLock)
            {
                entity._gameWorld = this;
                _entities.Add(entity);
            }
        }

        /// <summary>
        /// Зарегистрируйте GameWorld в GameEntity и запуск Start()
        /// </summary>
        /// <param name="entity"></param>
        public void RegisterAndStartEntity(GameEntity entity)
        {
            lock (_entitiesLock)
            {
                entity._gameWorld = this;
                _entities.Add(entity);
                entity.Start();
            }
        }
        /// <summary>
        /// Получить все Entitys
        /// </summary>
        /// <param name="entity"></param>
        public List<GameEntity> GetEntitys()
        {
            lock (_entitiesLock)
            {
                return _entities;
            }
        }
        /// <summary>
        /// Получить GameEntity, назначенной ячейке
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public GameEntity? GetEntity(Cell cell)
        {
            foreach (GameEntity entity in _entities)
            {
                foreach (Cell _cell in entity.GetCells())
                {
                    if (_cell.X == cell.X && _cell.Y == cell.Y)
                        return entity;
                }
            }

            return null;
        }


        /// <summary>
        /// Зарегистрируйте  отрисовка UI с GameWorld
        /// </summary>
        /// <param name="ui"></param>
        public void RegisterUI(ConsoleUI ui)
        {
            _ui = ui;
            _ui.RegisterGameWorld(this);
        }

        /// <summary>
        /// Возвращает буфер UI
        /// </summary>
        /// <returns></returns>
        public string? GetUIBuffer()
        {
            if (_ui != null)
                return _ui.GetUI();
            return null;
        }

        /// <summary>
        /// Возвращает отрисовку UI (элемент игры)
        /// </summary>
        /// <returns></returns>
        public ConsoleUI GetUI()
        {
            if (_ui != null)
                return _ui;
            return null;
        }

        #region Управление ячейками
        /// <summary>
        /// Возвращать содержимое ячейки
        /// В основном используется GraphicsManager
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>Возвращать содержимое ячейки</returns>
        public string GetCellContents(int x, int y)
        {
            string contents;
            lock (_boardLock)
            {
                contents = _board[x, y].Contents;
            }
            return contents;
        }

        /// <summary>
        /// Вернуть ячейку через ее координаты X и Y
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Cell GetCell(int x, int y)
        {
            Cell cell;
            lock (_boardLock)
            {
                cell = _board[x, y];
            }
            return cell;
        }

        /// <summary>
        /// Возврат ячейки через ссылку на ячейку
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public Cell GetCell(Cell cell)
        {
            Cell _cell;
            lock (_boardLock)
            {
                _cell = _board[cell.X, cell.Y];
            }
            return _cell;
        }
        #endregion  // Управление ячейками
        #endregion  //  Публичные методы

        #region Private 
        /// <summary>
        /// Запустите игровое поле из построенных ячеек.
        /// Строим ячейки в двумерном массиве
        /// </summary>
        private void InitiateGameBoard()
        {
            for (int x = 0; x < X; x++)
            {
                for (int y = 0; y < Y; y++)
                {
                    _board[x, y] = new Cell(x, y);
                }
            }
        }
        #endregion
    }
}
