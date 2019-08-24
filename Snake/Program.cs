using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
namespace Snake
{
    class Program
    {
     
       static public int number = 22;
       

      static public void ConsoleUpdate()
        {
            while (number != 200)
            {
                Console.WriteLine(number);
                number++;
                Thread.Sleep(1000);
                Console.Clear();
            }
        }
        static void Main(string[] args)
        {
            Snake snake = new Snake();
            //  snake.ConsoleUpdate();
            snake.StartGame();
           
          
            //List<int> numbers = new List<int> { 4, 5, 1, 3 };
            //int value = numbers.First();
            // double r = 34.340;
            //numbers.Remove(numbers.LastOrDefault());
            //Console.WriteLine(numbers.LastOrDefault());
        }
    }
}
