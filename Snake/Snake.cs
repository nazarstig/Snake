using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;
namespace Snake
{
    
    class Snake
    {
        //const
        //symbols
        private const char BorderSymbol = '#';
        private const char PointSymbol = '*';
        private const char BodySymbol = '@';
        private int CoordinateHead = 110;
        
        public enum Direction { U, D, L, R };
        //sizes
        private bool _victory = false;
        private bool _taskCompleted = false;
        private bool _gameOver = false;
        private bool _isEating = false; 
        private const int FieldSize = 2000;
        private const int RowLength = 100;
        private const int PointsAmount = 20;
       
        private char[] _field = new char[FieldSize];
        private List<int> _body = new List<int>();
        private List<int> _pointsCoordinates  = new List<int>();

        private Direction _direction = new Direction();

        public Snake()
        {
            FieldInitialize();
        }

        private void PointsInitialize()
        {
            int temp;
            
          
            for(int i = 0; i < PointsAmount; ++i)
            {
                Random random = new Random();
                temp = random.Next(FieldSize);
                while (_field[temp] != ' ')
                    temp = random.Next(FieldSize);

                _pointsCoordinates.Add(temp);
                
            }
        }


        private void BodyMove()
        {
            switch (_isEating)
            {
                case false:
                    _body.Add(CoordinateHead); 
                    _body.Remove(_body.FirstOrDefault());
                    break;
                case true: _body.Add(CoordinateHead);
                    break;
                default: break;


            }
        }

        private void BodyDraw()
        {
         
            HeadMove();
            
            BodyMove();
            foreach(int point in _body)
            _field[point] = BodySymbol;
        }

        private void PointsDraw()
        {
           
            foreach (int point in _pointsCoordinates)
                _field[point] = PointSymbol;
            
                
        }
        private void FieldOutput()
        {
            BuildField();
            for (int i = 0; i < FieldSize; ++i)
            {
                Console.Write(_field[i]);
                if(((i + 1) % (RowLength)) == 0 )
                Console.WriteLine();
            }
        }
        private void ConsoleUpdate()
        {
            
            
                FieldOutput();
               
                //foreach (int i in _body)
                //    Console.WriteLine(i);
                //Console.WriteLine(_gameOver);
                Thread.Sleep(1000);
              
                Console.Clear();
            
        }

        private void BorderDraw()
        {
            for (int i = 0; i < RowLength; ++i)
            {
                _field[i] = BorderSymbol;
                _field[FieldSize - RowLength + i] = BorderSymbol;
            }
            for (int i = RowLength - 1; i < FieldSize - RowLength; ++i)
            {
                _field[i] = ' ';
                if ((i + 1) % (RowLength) == 0)
                {
                    _field[i] = BorderSymbol;
                    _field[i + 1] = BorderSymbol;
                    ++i;
                }

            }

        }




        private void FieldInitialize()
        {
            BorderDraw();
            PointsInitialize();
            _direction = Direction.R;
            _body = new List<int>{109};
            
        }

        private char Kbhit()
        {
            char key = new char();
           
            
                if (Console.KeyAvailable)
                {
                    key =(char) Console.ReadKey().KeyChar;
                    
                }
                return key;
            
        }


        private void ChangeDirection()
        {
            char direction = Kbhit();
            switch(direction)
            {
                case 'w': if (_direction != Direction.D) _direction = Direction.U; break;
                case 's': if (_direction != Direction.U) _direction = Direction.D; break;
                case 'a': if (_direction != Direction.R) _direction = Direction.L; break;
                case 'd': if (_direction != Direction.L) _direction = Direction.R; break;
                default: break;

            }

        }


        private void HeadMove()
        {
            ChangeDirection();
            switch(_direction)
            {
                case Direction.R: CoordinateHead++; break;
                case Direction.L: CoordinateHead--; break;
                case Direction.U: CoordinateHead -= RowLength; break;
                case Direction.D: CoordinateHead += RowLength; break;
                default: break;
            }
            HeadCheck();
        }

        private void HeadCheck()
        {
            switch(_field[CoordinateHead])
            {
                case ' ': _isEating = false;  break;
                case BorderSymbol: _gameOver = true; break;
                case PointSymbol: _isEating = true; _pointsCoordinates.Remove(CoordinateHead); break;
                default: break;

            }


        }


        private void EmptyField()
        {
            for (int i = 0; i < FieldSize; ++i)
                _field[i] = ' ';
                
        }

        private void BuildField()
        {
            EmptyField();
            BorderDraw();
            PointsDraw();
            BodyDraw();
        }

        private void GameOver()
        {
            if (_victory)
                VictoryAction();
            else
                FailAction();
            
        }


        private void FailAction()
        {
            Console.WriteLine("GAME OVER");
        }

        private void VictoryAction()
        {
            Console.WriteLine("CONGRATULATIONS! YOU WON !!!");
        }

        private void GameEnd()
        {
            
            _gameOver = true;
            _victory = true;
        }

        public void StartGame()
        {
            Game();
        }

        private void Game()
        {
            while (!_gameOver)
                GameContinue();

            GameOver();
        }

        


        private void GameContinue()
        {
            if (CheckIfTaskCompleted())
            {
                TaskCompletedAction();
            }
            else
                ConsoleUpdate();

        }

        private bool CheckIfTaskCompleted()
        {
            return TaskCompletedCondition();
        }

        private bool TaskCompletedCondition()
        {
            if (_pointsCoordinates.Count == 0)
                return true;

            else return false;
        }


        private void TaskCompletedAction()
        {
            GameEnd();
        }


    }
}
