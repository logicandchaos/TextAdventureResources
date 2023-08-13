using System;
using System.Threading;

namespace TextAdventureLibrary
{
    public class CsConsole : Terminal
    {
        ConsoleColor ColorToCsConsoleColor(Color color)
        {
            switch (color)
            {
                case Color.Black:
                    return ConsoleColor.Black;
                case Color.DarkRed:
                    return ConsoleColor.DarkRed;
                case Color.DarkGreen:
                    return ConsoleColor.DarkGreen;
                case Color.DarkYellow:
                    return ConsoleColor.Gray;
                case Color.DarkBlue:
                    return ConsoleColor.Gray;
                case Color.DarkMagenta:
                    return ConsoleColor.Gray;
                case Color.DarkCyan:
                    return ConsoleColor.Gray;
                case Color.Gray:
                    return ConsoleColor.Gray;
                case Color.DarkGray:
                    return ConsoleColor.Gray;
                case Color.Red:
                    return ConsoleColor.Gray;
                case Color.Green:
                    return ConsoleColor.Gray;
                case Color.Yellow:
                    return ConsoleColor.Gray;
                case Color.Blue:
                    return ConsoleColor.Gray;
                case Color.Magenta:
                    return ConsoleColor.Gray;
                case Color.Cyan:
                    return ConsoleColor.Gray;
                case Color.White:
                    return ConsoleColor.Gray;
                default:
                    return ConsoleColor.Gray;
            }
        }

        public override void SetColor(Color foreground, Color background)
        {
            Console.ForegroundColor = ColorToCsConsoleColor(foreground);
            Console.BackgroundColor = ColorToCsConsoleColor(foreground);
        }

        public override void ResetColor()
        {
            Console.ResetColor();
        }

        public override void SetColorToHealth(float healthPercent)
        {
            //float ratio = (float)c.GetHealth() / (float)c.GetMaxHealth() * 100;
            //Console.WriteLine(ratio);
            if ((int)healthPercent > 33)
                Console.ForegroundColor = ConsoleColor.Gray;
            else
                Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public override void Print(string message, bool newLine = false)
        {
            Console.Write(message);
            if (newLine)
                Console.Write("\n");
        }

        public override void Type(string message, bool newLine = false, int delay = 20)
        {
            char[] letters = message.ToCharArray();
            foreach (char c in letters)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            if (newLine)
                Console.Write("\n");
        }

        public override void Print(Cell tile)
        {

        }

        public override void Print(Matrix matrix)
        {

        }

        public override void Print(Menu menu)
        {

        }

        public override void Print(Noun textGameObject)
        {

        }

        public override void PrintDescription(Noun textGameObject)
        {

        }

        public override void MoveCurser(Point2D point)
        {

        }

        public override void Anykey(string p_message = "<Press any key>")
        {
            while (Console.KeyAvailable) // Flush input queue
                Console.ReadKey();

            //sound
            //Console.Beep(500, 500);
            ConsoleColor temp = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n" + p_message);
            Console.ReadKey(true);//the true turns echo off
            Console.ForegroundColor = temp;
            //Console.WriteLine();
            Console.Clear();
        }

        public override bool YesNo()
        {
            ConsoleKey response;
            do
            {
                while (Console.KeyAvailable) // Flushes the input queue.
                    Console.ReadKey();

                response = Console.ReadKey().Key; // Gets the user's response.
                Console.WriteLine(); // Breaks the line.
            }
            while (response != ConsoleKey.Y && response != ConsoleKey.N);
            if (response == ConsoleKey.Y)
            {
                return true;
            }
            return false;
        }

        public override bool GraphicText(string input, Color col, bool clearScreen = true)
        {
            if (clearScreen)
                Console.Clear();
            string[] output = { "", " ", "  ", "   ", "    ", "     ", "      ", "       ", "        " };
            char[] charArray = input.ToUpper().ToCharArray();
            //if (charArray.Length > 5)//6 fits for short letters
            //  return false;
            foreach (char c in charArray)
            {
                if (c == 'A')
                {
                    output[0] += "     /\\\\\\\\\\\\\\\\\\    ";
                    output[1] += "   /\\\\\\\\\\\\\\\\\\\\\\\\\\  ";
                    output[2] += " /\\\\\\/////////\\\\\\  ";
                    output[3] += "\\/\\\\\\       \\/\\\\\\  ";
                    output[4] += "\\/\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\  ";
                    output[5] += "\\/\\\\\\/////////\\\\\\  ";
                    output[6] += "\\/\\\\\\       \\/\\\\\\  ";
                    output[7] += "\\/\\\\\\       \\/\\\\\\  ";
                    output[8] += "\\///        \\///   ";
                }
                else if (c == 'B')
                {
                    output[0] += " /\\\\\\\\\\\\\\\\\\\\\\\\\\   ";
                    output[1] += "\\/\\\\\\/////////\\\\\\ ";
                    output[2] += "\\/\\\\\\       \\/\\\\\\ ";
                    output[3] += "\\/\\\\\\\\\\\\\\\\\\\\\\\\\\\\  ";
                    output[4] += "\\/\\\\\\/////////\\\\\\ ";
                    output[5] += "\\/\\\\\\       \\/\\\\\\ ";
                    output[6] += "\\/\\\\\\       \\/\\\\\\ ";
                    output[7] += "\\/\\\\\\\\\\\\\\\\\\\\\\\\\\/  ";
                    output[8] += "\\/////////////    ";
                }
                else if (c == 'C')
                {
                    output[0] += "       /\\\\\\\\\\\\\\\\\\ ";
                    output[1] += "    /\\\\\\////////  ";
                    output[2] += "  /\\\\\\/           ";
                    output[3] += " /\\\\\\             ";
                    output[4] += "\\/\\\\\\             ";
                    output[5] += "\\//\\\\\\            ";
                    output[6] += " \\///\\\\\\          ";
                    output[7] += "   \\////\\\\\\\\\\\\\\\\\\ ";
                    output[8] += "      \\/////////  ";
                }
                else if (c == 'D')
                {
                    output[0] += " /\\\\\\\\\\\\\\\\\\\\\\\\    ";
                    output[1] += "\\/\\\\\\////////\\\\\\  ";
                    output[2] += "\\/\\\\\\      \\//\\\\\\ ";
                    output[3] += "\\/\\\\\\       \\/\\\\\\ ";
                    output[4] += "\\/\\\\\\       \\/\\\\\\ ";
                    output[5] += "\\/\\\\\\       \\/\\\\\\ ";
                    output[6] += "\\/\\\\\\       /\\\\\\  ";
                    output[7] += "\\/\\\\\\\\\\\\\\\\\\\\\\\\/   ";
                    output[8] += "\\////////////     ";
                }
                else if (c == 'E')
                {
                    output[0] += " /\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\ ";
                    output[1] += "\\/\\\\\\///////////  ";
                    output[2] += "\\/\\\\\\             ";
                    output[3] += "\\/\\\\\\\\\\\\\\\\\\\\\\     ";
                    output[4] += "\\/\\\\\\///////      ";
                    output[5] += "\\/\\\\\\             ";
                    output[6] += "\\/\\\\\\             ";
                    output[7] += "\\/\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\ ";
                    output[8] += "\\///////////////  ";
                }
                else if (c == 'F')
                {
                    output[0] += " /\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\ ";
                    output[1] += "\\/\\\\\\///////////  ";
                    output[2] += "\\/\\\\\\             ";
                    output[3] += "\\/\\\\\\\\\\\\\\\\\\\\\\     ";
                    output[4] += "\\/\\\\\\///////      ";
                    output[5] += "\\/\\\\\\             ";
                    output[6] += "\\/\\\\\\             ";
                    output[7] += "\\/\\\\\\             ";
                    output[8] += "\\///              ";
                }
                else if (c == 'G')
                {
                    output[0] += "    /\\\\\\\\\\\\\\\\\\\\\\\\ ";
                    output[1] += "  /\\\\\\//////////  ";
                    output[2] += " /\\\\\\             ";
                    output[3] += "\\/\\\\\\    /\\\\\\\\\\\\\\ ";
                    output[4] += "\\/\\\\\\   \\/////\\\\\\ ";
                    output[5] += "\\/\\\\\\       \\/\\\\\\ ";
                    output[6] += "\\/\\\\\\       \\/\\\\\\ ";
                    output[7] += "\\//\\\\\\\\\\\\\\\\\\\\\\\\/  ";
                    output[8] += " \\////////////    ";
                }
                else if (c == 'H')
                {
                    output[0] += " /\\\\\\        /\\\\\\ ";
                    output[1] += "\\/\\\\\\       \\/\\\\\\ ";
                    output[2] += "\\/\\\\\\       \\/\\\\\\ ";
                    output[3] += "\\/\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\ ";
                    output[4] += "\\/\\\\\\/////////\\\\\\ ";
                    output[5] += "\\/\\\\\\       \\/\\\\\\ ";
                    output[6] += "\\/\\\\\\       \\/\\\\\\ ";
                    output[7] += "\\/\\\\\\       \\/\\\\\\ ";
                    output[8] += "\\///        \\///  ";
                }
                else if (c == 'I')
                {
                    output[0] += "  /\\\\\\\\\\\\\\\\\\\\\\  ";
                    output[1] += " \\/////\\\\\\///   ";
                    output[2] += "     \\/\\\\\\      ";
                    output[3] += "     \\/\\\\\\      ";
                    output[4] += "     \\/\\\\\\      ";
                    output[5] += "     \\/\\\\\\      ";
                    output[6] += "     \\/\\\\\\      ";
                    output[7] += "  /\\\\\\\\\\\\\\\\\\\\\\  ";
                    output[8] += " \\///////////   ";
                }
                else if (c == 'J')
                {
                    output[0] += "     /\\\\\\\\\\\\\\\\\\\\\\ ";
                    output[1] += "    \\/////\\\\\\///  ";
                    output[2] += "        \\/\\\\\\     ";
                    output[3] += "        \\/\\\\\\     ";
                    output[4] += "        \\/\\\\\\     ";
                    output[5] += "        \\/\\\\\\     ";
                    output[6] += " /\\\\\\   \\/\\\\\\     ";
                    output[7] += "\\//\\\\\\\\\\\\\\\\\\      ";
                    output[8] += " \\/////////       ";
                }
                else if (c == 'K')
                {
                    output[0] += " /\\\\\\        /\\\\\\ ";
                    output[1] += "\\/\\\\\\     /\\\\\\//  ";
                    output[2] += "\\/\\\\\\  /\\\\\\//     ";
                    output[3] += "\\/\\\\\\\\\\\\//\\\\\\     ";
                    output[4] += "\\/\\\\\\// \\//\\\\\\    ";
                    output[5] += "\\/\\\\\\    \\//\\\\\\   ";
                    output[6] += "\\/\\\\\\     \\//\\\\\\  ";
                    output[7] += "\\/\\\\\\      \\//\\\\\\ ";
                    output[8] += "\\///        \\///  ";
                }
                else if (c == 'L')
                {
                    output[0] += " /\\\\\\             ";
                    output[1] += "\\/\\\\\\             ";
                    output[2] += "\\/\\\\\\             ";
                    output[3] += "\\/\\\\\\             ";
                    output[4] += "\\/\\\\\\             ";
                    output[5] += "\\/\\\\\\             ";
                    output[6] += "\\/\\\\\\             ";
                    output[7] += "\\/\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\ ";
                    output[8] += "\\///////////////  ";
                }
                else if (c == 'M')
                {
                    output[0] += " /\\\\\\\\            /\\\\\\\\ ";
                    output[1] += "\\/\\\\\\\\\\\\        /\\\\\\\\\\\\ ";
                    output[2] += "\\/\\\\\\//\\\\\\    /\\\\\\//\\\\\\ ";
                    output[3] += "\\/\\\\\\\\///\\\\\\/\\\\\\/ \\/\\\\\\ ";
                    output[4] += "\\/\\\\\\  \\///\\\\\\/   \\/\\\\\\ ";
                    output[5] += "\\/\\\\\\    \\///     \\/\\\\\\ ";
                    output[6] += "\\/\\\\\\             \\/\\\\\\ ";
                    output[7] += "\\/\\\\\\             \\/\\\\\\ ";
                    output[8] += "\\///              \\///  ";
                }
                else if (c == 'N')
                {
                    output[0] += " /\\\\\\\\\\     /\\\\\\ ";
                    output[1] += "\\/\\\\\\\\\\\\   \\/\\\\\\ ";
                    output[2] += "\\/\\\\\\/\\\\\\  \\/\\\\\\ ";
                    output[3] += "\\/\\\\\\//\\\\\\ \\/\\\\\\ ";
                    output[4] += "\\/\\\\\\\\//\\\\\\\\/\\\\\\ ";
                    output[5] += "\\/\\\\\\ \\//\\\\\\/\\\\\\ ";
                    output[6] += "\\/\\\\\\  \\//\\\\\\\\\\\\ ";
                    output[7] += "\\/\\\\\\   \\//\\\\\\\\\\ ";
                    output[8] += "\\///     \\/////  ";
                }
                else if (c == 'O')
                {
                    output[0] += "      /\\\\\\\\\\       ";
                    output[1] += "    /\\\\\\///\\\\\\     ";
                    output[2] += "  /\\\\\\/  \\///\\\\\\   ";
                    output[3] += " /\\\\\\      \\//\\\\\\  ";
                    output[4] += "\\/\\\\\\       \\/\\\\\\  ";
                    output[5] += "\\//\\\\\\      /\\\\\\   ";
                    output[6] += " \\///\\\\\\  /\\\\\\     ";
                    output[7] += "   \\///\\\\\\\\\\/      ";
                    output[8] += "     \\/////        ";
                }
                else if (c == 'P')
                {
                    output[0] += " /\\\\\\\\\\\\\\\\\\\\\\\\\\   ";
                    output[1] += "\\/\\\\\\/////////\\\\\\ ";
                    output[2] += "\\/\\\\\\       \\/\\\\\\ ";
                    output[3] += "\\/\\\\\\\\\\\\\\\\\\\\\\\\\\/  ";
                    output[4] += "\\/\\\\\\/////////    ";
                    output[5] += "\\/\\\\\\             ";
                    output[6] += "\\/\\\\\\             ";
                    output[7] += "\\/\\\\\\             ";
                    output[8] += "\\///              ";
                }
                else if (c == 'Q')
                {
                    output[0] += "       /\\\\\\      ";
                    output[1] += "    /\\\\\\\\/\\\\\\\\   ";
                    output[2] += "  /\\\\\\//\\////\\\\\\ ";
                    output[3] += " /\\\\\\      \\//\\\\\\";
                    output[4] += "\\/\\\\\\       /\\\\\\ ";
                    output[5] += "\\///\\\\\\\\/\\\\\\\\/   ";
                    output[6] += "  \\////\\\\\\//     ";
                    output[7] += "     \\///\\\\\\\\\\\\  ";
                    output[8] += "       \\//////   ";
                }
                else if (c == 'R')
                {
                    output[0] += "   /\\\\\\\\\\\\\\\\\\     ";
                    output[1] += " /\\\\\\///////\\\\\\   ";
                    output[2] += "\\/\\\\\\     \\/\\\\\\   ";
                    output[3] += "\\/\\\\\\\\\\\\\\\\\\\\\\/    ";
                    output[4] += "\\/\\\\\\//////\\\\\\    ";
                    output[5] += "\\/\\\\\\    \\//\\\\\\   ";
                    output[6] += "\\/\\\\\\     \\//\\\\\\  ";
                    output[7] += "\\/\\\\\\      \\//\\\\\\ ";
                    output[8] += "\\///        \\///  ";
                }
                else if (c == 'S')
                {
                    output[0] += "   /\\\\\\\\\\\\\\\\\\\\\\   ";
                    output[1] += " /\\\\\\/////////\\\\\\ ";
                    output[2] += "\\//\\\\\\      \\///  ";
                    output[3] += " \\////\\\\\\         ";
                    output[4] += "    \\////\\\\\\      ";
                    output[5] += "       \\////\\\\\\   ";
                    output[6] += " /\\\\\\      \\//\\\\\\ ";
                    output[7] += "\\///\\\\\\\\\\\\\\\\\\\\\\/  ";
                    output[8] += "  \\///////////    ";
                }
                else if (c == 'T')
                {
                    output[0] += " /\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\ ";
                    output[1] += "\\///////\\\\\\/////  ";
                    output[2] += "      \\/\\\\\\       ";
                    output[3] += "      \\/\\\\\\       ";
                    output[4] += "      \\/\\\\\\       ";
                    output[5] += "      \\/\\\\\\       ";
                    output[6] += "      \\/\\\\\\       ";
                    output[7] += "      \\/\\\\\\       ";
                    output[8] += "      \\///        ";
                }
                else if (c == 'U')
                {
                    output[0] += " /\\\\\\        /\\\\\\ ";
                    output[1] += "\\/\\\\\\       \\/\\\\\\ ";
                    output[2] += "\\/\\\\\\       \\/\\\\\\ ";
                    output[3] += "\\/\\\\\\       \\/\\\\\\ ";
                    output[4] += "\\/\\\\\\       \\/\\\\\\ ";
                    output[5] += "\\/\\\\\\       \\/\\\\\\ ";
                    output[6] += "\\//\\\\\\      /\\\\\\  ";
                    output[7] += " \\///\\\\\\\\\\\\\\\\\\/   ";
                    output[8] += "   \\/////////     ";
                }
                else if (c == 'V')
                {
                    output[0] += " /\\\\\\        /\\\\\\ ";
                    output[1] += "\\/\\\\\\       \\/\\\\\\ ";
                    output[2] += "\\//\\\\\\      /\\\\\\  ";
                    output[3] += " \\//\\\\\\    /\\\\\\   ";
                    output[4] += "  \\//\\\\\\  /\\\\\\    ";
                    output[5] += "   \\//\\\\\\/\\\\\\     ";
                    output[6] += "    \\//\\\\\\\\\\      ";
                    output[7] += "     \\//\\\\\\       ";
                    output[8] += "      \\///        ";
                }
                else if (c == 'W')
                {
                    output[0] += " /\\\\\\              /\\\\\\ ";
                    output[1] += "\\/\\\\\\             \\/\\\\\\ ";
                    output[2] += "\\/\\\\\\             \\/\\\\\\ ";
                    output[3] += "\\//\\\\\\    /\\\\\\    /\\\\\\  ";
                    output[4] += " \\//\\\\\\  /\\\\\\\\\\  /\\\\\\   ";
                    output[5] += "  \\//\\\\\\/\\\\\\/\\\\\\/\\\\\\    ";
                    output[6] += "   \\//\\\\\\\\\\\\//\\\\\\\\\\     ";
                    output[7] += "    \\//\\\\\\  \\//\\\\\\      ";
                    output[8] += "     \\///    \\///       ";
                }
                else if (c == 'X')
                {
                    output[0] += " /\\\\\\       /\\\\\\ ";
                    output[1] += "\\///\\\\\\   /\\\\\\/  ";
                    output[2] += "  \\///\\\\\\\\\\\\/    ";
                    output[3] += "    \\//\\\\\\\\      ";
                    output[4] += "     \\/\\\\\\\\      ";
                    output[5] += "     /\\\\\\\\\\\\     ";
                    output[6] += "   /\\\\\\////\\\\\\   ";
                    output[7] += " /\\\\\\/   \\///\\\\\\ ";
                    output[8] += "\\///       \\///  ";
                }
                else if (c == 'Y')
                {
                    output[0] += " /\\\\\\        /\\\\\\ ";
                    output[1] += "\\///\\\\\\    /\\\\\\/  ";
                    output[2] += "  \\///\\\\\\/\\\\\\/    ";
                    output[3] += "    \\///\\\\\\/      ";
                    output[4] += "      \\/\\\\\\       ";
                    output[5] += "      \\/\\\\\\       ";
                    output[6] += "      \\/\\\\\\       ";
                    output[7] += "      \\/\\\\\\       ";
                    output[8] += "      \\///        ";
                }
                else if (c == 'Z')
                {
                    output[0] += " /\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\ ";
                    output[1] += "\\////////////\\\\\\  ";
                    output[2] += "          /\\\\\\/   ";
                    output[3] += "        /\\\\\\/     ";
                    output[4] += "      /\\\\\\/       ";
                    output[5] += "    /\\\\\\/         ";
                    output[6] += "  /\\\\\\/           ";
                    output[7] += " /\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\ ";
                    output[8] += "\\///////////////  ";
                }

                else if (c == '!')
                {
                    output[0] += "    /\\\\\\    ";
                    output[1] += "  /\\\\\\\\\\\\\\  ";
                    output[2] += " /\\\\\\\\\\\\\\\\\\ ";
                    output[3] += "\\//\\\\\\\\\\\\\\  ";
                    output[4] += " \\//\\\\\\\\\\   ";
                    output[5] += "  \\//\\\\\\    ";
                    output[6] += "   \\///     ";
                    output[7] += "    /\\\\\\    ";
                    output[8] += "   \\///     ";
                }

                else if (c == '?')
                {
                    output[0] += @"     /\\\\\\\    ";
                    output[1] += @"  /\\\//////\\\  ";
                    output[2] += @" \///     \//\\\ ";
                    output[3] += @"            /\\\ ";
                    output[4] += @"         /\\\\/  ";
                    output[5] += @"        /\\\/    ";
                    output[6] += @"       \///      ";
                    output[7] += @"        /\\\     ";
                    output[8] += @"       \///      ";
                }
            }
            foreach (string str in output)
            {
                if (str.Length >= Console.BufferWidth)
                {
                    Console.WriteLine(input);
                    return false;
                }
            }
            ConsoleColor temp = Console.ForegroundColor;
            Console.ForegroundColor = ColorToCsConsoleColor(col);
            Console.BackgroundColor = ConsoleColor.Black;
            foreach (string str in output)
            {
                Console.WriteLine(str);
            }
            Console.ForegroundColor = temp;
            return true;
        }
    }
}
