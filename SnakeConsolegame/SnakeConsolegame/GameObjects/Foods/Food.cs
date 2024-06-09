namespace SnakeConsolegame.GameObjects.Foods
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class Food : Point
    {
        protected Wall wall;
        protected char foodSymbol;
        protected Random random;

        public Food(Wall wall, char foodSymbol, int points)
            : base(wall.LeftX, wall.TopY)
        {
            this.wall = wall;
            this.foodSymbol = foodSymbol;
            this.FoodPoints = points;
            this.random = new Random();
        }

        public int FoodPoints { get; private set; }

        public abstract void SetRandomPosition(Queue<Point> snakeElements);

        public abstract bool IsFoodPoint(Point snake);
    }
}
