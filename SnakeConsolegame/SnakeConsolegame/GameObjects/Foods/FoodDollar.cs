namespace SnakeConsolegame.GameObjects.Foods
{
    using System.Collections.Generic;
    using System.Linq;

    public class FoodDollar : Food
    {
        private const char foodSymbol = '$';
        private const int foodPoints = 2;
        private const int speedUp = 3;

        public FoodDollar(Wall wall)
            : base(wall, foodSymbol, foodPoints)
        {
        }

        public override void SetRandomPosition(Queue<Point> snakeElements)
        {
            this.LeftX = this.random.Next(2, this.wall.LeftX - 2);
            this.TopY = this.random.Next(2, this.wall.TopY - 2);

            bool isPointOfSnake = snakeElements.Any(x => x.TopY == this.TopY && x.LeftX == this.LeftX);

            while (isPointOfSnake)
            {
                this.LeftX = this.random.Next(2, this.wall.LeftX - 2);
                this.TopY = this.random.Next(2, this.wall.TopY - 2);

                isPointOfSnake = snakeElements.Any(x => x.TopY == this.TopY && x.LeftX == this.LeftX);
            }

            this.Draw(foodSymbol);
        }

        public override bool IsFoodPoint(Point snake)
        {
            return this.LeftX == snake.LeftX && this.TopY == this.TopY;
        }

        public int SpeedUp => speedUp;
    }
}
