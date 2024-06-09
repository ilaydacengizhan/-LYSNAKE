namespace SnakeConsolegame.GameObjects
{
    using Foods;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Snake
    {
        private const char snakeSymbol = '\u25CF';

        private Queue<Point> snakeElements;
        private Food currentFood;
        private Wall wall;

        private int nextLeftX;
        private int nextTopY;

        public Food LastFood { get; private set; }

        public Snake(Wall wall)
        {
            this.wall = wall;
            this.snakeElements = new Queue<Point>();
            this.CreateSnake();
            this.CreateFood();
        }

        private void CreateSnake()
        {
            for (int leftX = 1; leftX <= 6; leftX++)
            {
                this.snakeElements.Enqueue(new Point(leftX, 2));
            }
        }

        private void CreateFood()
        {
            int foodType = new Random().Next(0, 3);
            switch (foodType)
            {
                case 0:
                    this.currentFood = new FoodHash(this.wall);
                    break;
                case 1:
                    this.currentFood = new FoodDollar(this.wall);
                    break;
                case 2:
                    this.currentFood = new FoodAsterisk(this.wall);
                    break;
            }

            this.currentFood.SetRandomPosition(snakeElements);
        }

        public bool IsMoving(Point direction)
        {
            Point currentSnakeHead = this.snakeElements.Last();

            this.GetNextPoint(direction, currentSnakeHead);

            bool isPointOfSnake = this.snakeElements
                .Any(x => x.LeftX == this.nextLeftX && x.TopY == this.nextTopY);

            if (isPointOfSnake)
            {
                return false;
            }

            Point snakeNewHead = new Point(this.nextLeftX, this.nextTopY);

            if (wall.IsPointOfWall(snakeNewHead))
            {
                return false;
            }

            this.snakeElements.Enqueue(snakeNewHead);
            snakeNewHead.Draw(snakeSymbol);

            if (this.currentFood.IsFoodPoint(snakeNewHead))
            {
                this.Eat(direction, currentSnakeHead);
                this.CreateFood(); 
            }

            Point snakeTail = this.snakeElements.Dequeue();
            snakeTail.Draw(' ');

            return true;
        }

        private void Eat(Point direction, Point currentSnakeHead)
        {
            int length = this.currentFood.FoodPoints;

            for (int i = 0; i < length; i++)
            {
                this.snakeElements.Enqueue(new Point(this.nextLeftX, this.nextTopY));
                this.GetNextPoint(direction, currentSnakeHead);
            }

            if (this.currentFood is FoodAsterisk)
            {
                this.wall.AddPoints(((FoodAsterisk)this.currentFood).ExtraPoints);
            }
            else
            {
                this.wall.AddPoints(this.currentFood.FoodPoints);
            }

            this.LastFood = this.currentFood;
        }

        private void GetNextPoint(Point direction, Point snakeHead)
        {
            this.nextLeftX = snakeHead.LeftX + direction.LeftX;
            this.nextTopY = snakeHead.TopY + direction.TopY;
        }
    }
}
