namespace SnakeConsolegame.GameObjects
{
    using System;
    using System.Collections.Generic;

    public class Wall
    {
        private const char wallSymbol = '\u2588';
        private int playerPoints;
        private int playerLevel;

        public Wall(int leftX, int topY)
        {
            this.LeftX = leftX;
            this.TopY = topY;
            this.playerPoints = 0;
            this.playerLevel = 1;
            this.InitializeWallBorders();
        }

        public int LeftX { get; private set; }

        public int TopY { get; private set; }

        private void InitializeWallBorders()
        {
            this.DrawHorizontalLine(0);
            this.DrawHorizontalLine(this.TopY);
            this.DrawVerticalLine(0);
            this.DrawVerticalLine(this.LeftX);
        }

        private void DrawHorizontalLine(int topY)
        {
            for (int leftX = 0; leftX <= this.LeftX; leftX++)
            {
                this.Draw(leftX, topY);
            }
        }

        private void DrawVerticalLine(int leftX)
        {
            for (int topY = 0; topY <= this.TopY; topY++)
            {
                this.Draw(leftX, topY);
            }
        }

        private void Draw(int leftX, int topY)
        {
            Console.SetCursorPosition(leftX, topY);
            Console.Write(wallSymbol);
        }

        public bool IsPointOfWall(Point point)
        {
            return point.TopY == 0 || point.LeftX == 0 ||
                   point.LeftX == this.LeftX || point.TopY == this.TopY;
        }
        public void AddPoints(int points)
        {
            this.playerPoints += points;
            int previousLevel = this.playerLevel;
            this.playerLevel = this.playerPoints / 10 + 1;
            if (this.playerLevel > previousLevel) 
            {
                PlayerInfo();
            }
            else
            {
                Console.SetCursorPosition(this.LeftX + 3, 0);
                Console.Write($"Player points: {this.playerPoints}"); 
            }
        }

        public void PlayerInfo()
        {
            Console.SetCursorPosition(this.LeftX + 3, 0);
            Console.Write($"Player points: {this.playerPoints}");
            Console.SetCursorPosition(this.LeftX + 3, 1);
            Console.Write($"Player level: {this.playerLevel}");
            Console.SetCursorPosition(this.LeftX + 3, 3);
            Console.Write("Would you like to continue? y/n");
        }

        public bool GetContinueDecision()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            return keyInfo.Key == ConsoleKey.Y;
        }
    }
}
