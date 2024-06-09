namespace SnakeConsolegame
{
    using Core;
    using GameObjects;
    using System;
    using Utilities;

    public class StartUp
    {
        public static void Main()
        {
            ConsoleWindow.CustomizeConsole();

            Wall wall = new Wall(60, 20);
            Snake snake = new Snake(wall);

            wall.PlayerInfo();

            Engine engine = new Engine(snake, wall);
            engine.Run();
        }
    }
}
