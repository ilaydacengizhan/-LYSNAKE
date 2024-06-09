namespace SnakeConsolegame.Core
{
    using Enums;
    using GameObjects;
    using SnakeConsolegame.GameObjects.Foods;
    using System;
    using System.Threading;

    public class Engine
    {
        private Point[] pointsOfDirection;
        private Direction direction;
        private Snake snake;
        private Wall wall;
        private double baseSleepTime;
        private double currentSleepTime;
        private const double maxSpeed = 50;
        private const double minSpeed = 150;
        private const double speedIncrement = 2;
        private Timer resetSpeedTimer;

        public Engine(Snake snake, Wall wall)
        {
            this.snake = snake;
            this.wall = wall;
            this.pointsOfDirection = new Point[4];
            this.direction = Direction.Right;
            this.baseSleepTime = 100;
            this.currentSleepTime = baseSleepTime;
            this.resetSpeedTimer = new Timer(ResetSpeed, null, Timeout.Infinite, Timeout.Infinite);
        }

        public void Run()
        {
            this.CreateDirections();

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    this.GetNextDirection();
                }

                bool isMoving = this.snake.IsMoving(this.pointsOfDirection[(int)direction]);

                if (!isMoving)
                {
                    AskUserForRestart();
                }

                AdjustSleepTimeBasedOnFood();

                Thread.Sleep((int)currentSleepTime);
            }
        }

        private void AdjustSleepTimeBasedOnFood()
        {
            if (snake.LastFood != null)
            {
                if (snake.LastFood is FoodDollar)
                {
                    currentSleepTime = Math.Max(currentSleepTime - ((FoodDollar)snake.LastFood).SpeedUp, maxSpeed);
                    resetSpeedTimer.Change(Timeout.Infinite, Timeout.Infinite);
                }
                else if (snake.LastFood is FoodHash)
                {
                    currentSleepTime = Math.Min(currentSleepTime + speedIncrement, minSpeed);
                    resetSpeedTimer.Change(10000, Timeout.Infinite);
                }

                currentSleepTime = Math.Max(currentSleepTime, maxSpeed);
            }
        }

        private void ResetSpeed(object state)
        {
            currentSleepTime = baseSleepTime;
        }

        private void AskUserForRestart()
        {
            int leftX = this.wall.LeftX + 2;
            int topY = 3;

            Console.SetCursorPosition(leftX, topY);
            Console.Write("Would you like to continue? y/n");

            string input = Console.ReadLine();

            if (input == "y")
            {
                Console.Clear();
                StartUp.Main();
            }
            else
            {
                Environment.Exit(0);
            }
        }

        private void CreateDirections()
        {
            this.pointsOfDirection[0] = new Point(1, 0);
            this.pointsOfDirection[1] = new Point(-1, 0);
            this.pointsOfDirection[2] = new Point(0, 1);
            this.pointsOfDirection[3] = new Point(0, -1);
        }

        private void GetNextDirection()
        {
            ConsoleKeyInfo userInput = Console.ReadKey();

            if (userInput.Key == ConsoleKey.LeftArrow)
            {
                if (direction != Direction.Right)
                {
                    direction = Direction.Left;
                }
            }
            else if (userInput.Key == ConsoleKey.RightArrow)
            {
                if (direction != Direction.Left)
                {
                    direction = Direction.Right;
                }
            }
            else if (userInput.Key == ConsoleKey.UpArrow)
            {
                if (direction != Direction.Down)
                {
                    direction = Direction.Up;
                }
            }
            else if (userInput.Key == ConsoleKey.DownArrow)
            {
                if (direction != Direction.Up)
                {
                    direction = Direction.Down;
                }
            }
        }
    }
}
