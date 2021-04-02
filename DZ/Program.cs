using System;
using System.Collections.Generic;

namespace DZ
{
    public enum Direction
    {
        Left = 0,
        Right = 1,
        Top = 2,
        Down = 3,
        None = 4
    }

    public struct Vector2Int
    {
        public int x { get; set; }
        public int y { get; set; }

        public Vector2Int(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector2Int operator +(Vector2Int value1, Vector2Int value2)
        {
            return new Vector2Int(value1.x + value2.x, value1.y + value2.y);
        }

        public static Vector2Int operator -(Vector2Int value1, Vector2Int value2)
        {
            return new Vector2Int(value1.x - value2.x, value1.y - value2.y);
        }
    }

    /*public static class ConsoleUtility
    {
        private static int originColumn;
        private static int originRow;

        static ConsoleUtility()
        {
            originRow = Console.CursorTop;
            originColumn = Console.CursorLeft;
        }

        public static void WriteAt(string s, Vector2Int position)
        {
            WriteAt(s, position.x, position.y);
        }

        public static void WriteAt(string s, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(originColumn + x, originRow + y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }
    }
    */

    class Program
    {
        private static int originColumn;
        private static int originRow;

        static void Main(string[] args)
        {
            Console.Clear();
            Console.CursorVisible = false;
            originRow = Console.CursorTop;
            originColumn = Console.CursorLeft;
 
            AnimateSpiral(50);
        }

        public static void WriteAt(string s, Vector2Int position) 
        {
            WriteAt(s, position.x, position.y);
        }

        public static void WriteAt(string s, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(originColumn + x, originRow + y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }

        public static void AnimateSpiral(int amountNumber)
        {
            Vector2Int startPosition = new Vector2Int(15, 26);
            Vector2Int step = new Vector2Int(50, 20);
            Vector2Int deltaStep = new Vector2Int(5, 2);

            List<AnimateCharacter> animateCharacters = new List<AnimateCharacter>();
            
            for (int i = 0; i < amountNumber; i++)
            {
                animateCharacters.Add(new AnimateCharacter(new Vector2Int(15, 26), new Vector2Int(50, 20), new Vector2Int(5, 2), i * 5, 1));
            }
            
            while (animateCharacters.Count != 0)
            {
                for (int i = 0; i < animateCharacters.Count; i++)
                {
                    animateCharacters[i].Update();
                    WriteAt(animateCharacters[i].character.ToString(), animateCharacters[i].position);
                    
                    if (animateCharacters[i].IsOver)
                    {
                        animateCharacters.RemoveAt(i);
                        i--;
                    }
                }

                System.Threading.Thread.Sleep(20);
            }
        }
    }

    public class AnimateCharacter
    {
        public char character;
        public Vector2Int position;
        public Vector2Int step;
        public Vector2Int deltaStep;
        public float distance;
        public float speed;
        public float delay;
        public Direction moveDirection;

        private Vector2Int lastPoint;

        public bool IsOver => step.x == 0 || step.y == 0;

        public AnimateCharacter(Vector2Int position, Vector2Int step, Vector2Int deltaStep, float delay, float speed)
        {
            character = GetRandomLetter();
            this.position = position;
            this.step = step;
            this.deltaStep = deltaStep;
            this.speed = speed;
            this.delay = delay;
            moveDirection = Direction.Right;
            lastPoint = position;
            distance = 0f;
        }

        public void Update()
        {
            if (delay > 0)
            {
                delay--;
                return;
            }

            character = GetRandomLetter();

            switch (moveDirection)
            {
                case Direction.Right:
                    distance += speed;
                    position.x = (int) (lastPoint.x + distance);

                    if ((int) distance > step.x)
                    {
                        position.x = (int)(lastPoint.x + step.x);
                        moveDirection = Direction.Top;
                        distance = 0;
                        lastPoint = position;
                        step.x -= deltaStep.x;
                    }
                    else
                    {
                        position.x = (int)(lastPoint.x + distance);
                    }
                    break;
                case Direction.Left:
                    distance += speed;
                    position.x = (int)(lastPoint.x - distance);

                    if ((int) distance > step.x)
                    {
                        position.x = (int)(lastPoint.x - step.x);
                        moveDirection = Direction.Down;
                        distance = 0;
                        lastPoint = position;
                        step.x -= deltaStep.x;
                    }
                    else
                    {
                        position.x = (int)(lastPoint.x - distance);
                    }
                    break;
                case Direction.Top:
                    distance += speed;

                    if ((int) distance > step.y)
                    {
                        position.y = (int)(lastPoint.y - step.y);
                        moveDirection = Direction.Left;
                        distance = 0;
                        lastPoint = position;
                        step.y -= deltaStep.y;
                    }
                    else
                    {
                        position.y = (int)(lastPoint.y - distance);
                    }
                    break;
                case Direction.Down:
                    distance += speed;
                    
                    if ((int)distance > step.y)
                    {
                        position.y = (int)(lastPoint.y + step.y);
                        moveDirection = Direction.Right;
                        distance = 0;
                        lastPoint = position;
                        step.y -= deltaStep.y;
                    }
                    else
                    {
                        position.y = (int)(lastPoint.y + distance);
                    }
                    break;
                case Direction.None:
                default:
                    break;
            }
        }
        public static char GetRandomLetter()
        {
            string chars = "$%#@!*abcdefghijklmnopqrstuvwxyz1234567890?;:ABCDEFGHIJKLMNOPQRSTUVWXYZ^&";
            Random rand = new Random();
            int num = rand.Next(0, chars.Length - 1);
            return chars[num];
        }
    }        
}
