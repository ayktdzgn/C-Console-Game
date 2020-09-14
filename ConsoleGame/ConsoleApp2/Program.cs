using System;
using System.Timers;

namespace ConsoleApp2
{
    class Program
    {
        static string[,] gameArea;
        static int playerX;
        static int selectedDifficult;
        static int creatingBallCount;
        static int score;
        static int time;
        static bool hit;

        static void Main(string[] args)
        {
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 1000;
            aTimer.Enabled = true;

            Console.WriteLine("Play with 'A' and 'D' \n Rules: Don't hit balls");
            Console.WriteLine("Select Hardness:\n For easy presss 1...\n For medium press 2...\n For hard press 3...");
            selectedDifficult = Console.Read();
            ChoosingDifficult(selectedDifficult);

            GameLoop();
        }

        static void GameLoop()
        {
            int pastTime = 0;
            playerX = gameArea.GetUpperBound(1) / 2;
            while (!hit)
            { 
                if (time > pastTime)
                {
                    pastTime = time;
                    CreatingBalls();
                    FallingBalls();
                }
                PlayerPosition();
            }
            Console.Clear();
            Console.WriteLine("-----Game Over------");
            Console.WriteLine("Score : " + score);
            Console.ReadKey();
        }

        static void CheckForHit(int playerX , int ballX)
        {
            if (playerX == ballX)
            {
                hit = true;
            }
        }

        static void PlayerPosition()
        {
            int bound0 = gameArea.GetUpperBound(0);
            if ((Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.A))
            {
                if (!(playerX-1 < 0))
                {
                    playerX -= 1;
                }
            }
            if ((Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.D))
            {
                if (!(playerX + 1 > bound0))
                {
                    playerX += 1;
                }
            }
        }

        static void FallingBalls()
        {
            int bound0 = gameArea.GetUpperBound(0);
            int bound1 = gameArea.GetUpperBound(1);

            for (int i = bound0-1; i >= 0; i--)
            {
                for (int j = 0; j < bound1; j++)
                {
                    if (gameArea[i, j] =="o")
                    {
                        gameArea[i + 1, j] = "o";
                        gameArea[i, j] = "-";
                    } 
                }
            }
        }
        static void CreatingBalls()
        {
            Random random = new Random();
            for (int i = 0; i < creatingBallCount; i++)
            {
                int num = random.Next(gameArea.GetUpperBound(0));
                if (gameArea[0, num] != "o")
                {
                    gameArea[0, num] = "o";
                }
            }
            WriteArea();
        }

        static void WriteArea()
        {
            Console.Clear();
            int bound0 = gameArea.GetUpperBound(0);
            int bound1 = gameArea.GetUpperBound(1);

            for (int i = 0; i <= bound0; i++)
            {
                for (int j = 0; j <= bound1; j++)
                {
                    if (gameArea[i, j] != "o")
                    {
                        gameArea[i, j] = "-";
                    }
                    if (i == bound0 && j == playerX && gameArea[i, j] != "o")
                    {
                        gameArea[i, playerX] = "P";
                    }
                    if (i == bound0 && gameArea[i, j] == "o")
                    {
                        CheckForHit(playerX, j);
                        score++;
                        gameArea[i, j] = "-";
                    }
                    Console.Write(gameArea[i, j]);
                }
                Console.WriteLine("");
            }
        }

        static void ChoosingDifficult(int select)
        {
            switch (select)
            {
                case 49:
                    gameArea = new string[20,20];
                    creatingBallCount = 2;
                    break;
                case 50:
                    gameArea = new string[20, 20];
                    creatingBallCount = 3;
                    break;
                case 51:
                    gameArea = new string[15, 15];
                    creatingBallCount = 4;
                    break;
                default:
                    break;
            }
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            time++;
        }
    }
}
