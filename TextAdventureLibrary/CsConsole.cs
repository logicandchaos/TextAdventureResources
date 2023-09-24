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
                    return ConsoleColor.DarkYellow;
                case Color.DarkBlue:
                    return ConsoleColor.DarkBlue;
                case Color.DarkMagenta:
                    return ConsoleColor.DarkMagenta;
                case Color.DarkCyan:
                    return ConsoleColor.DarkCyan;
                case Color.Gray:
                    return ConsoleColor.Gray;
                case Color.DarkGray:
                    return ConsoleColor.DarkGray;
                case Color.Red:
                    return ConsoleColor.Red;
                case Color.Green:
                    return ConsoleColor.Green;
                case Color.Yellow:
                    return ConsoleColor.Gray;
                case Color.Blue:
                    return ConsoleColor.Blue;
                case Color.Magenta:
                    return ConsoleColor.Magenta;
                case Color.Cyan:
                    return ConsoleColor.Cyan;
                case Color.White:
                    return ConsoleColor.White;
                default:
                    return ConsoleColor.Gray;
            }
        }

        public override void SetColor(Color foreground, Color background)
        {
            Console.ForegroundColor = ColorToCsConsoleColor(foreground);
            Console.BackgroundColor = ColorToCsConsoleColor(background);
        }

        public override void ResetColor()
        {
            Console.ResetColor();
        }

        public override void SetColorToHealth(float healthPercent)
        {
            if ((int)healthPercent > 33)
                Console.ForegroundColor = ConsoleColor.Gray;
            else
                Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public override void Print(string text, bool newLine = false)
        {
            Console.Write(text);
            if (newLine)
                Console.Write("\n");
        }

        public override void Type(string text, bool newLine = false, int delay = 20)
        {
            char[] letters = text.ToCharArray();
            foreach (char c in letters)
            {
                Console.Write(c);
                //skip epicness
                if (!Console.KeyAvailable)
                {
                    Thread.Sleep(delay);
                }
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

        public override void Menu(Menu menu)
        {
            ClearScreen();
            SetColor(Color.White, Color.Black);

            /*int maxLength = 0;
            if (columns == 2)
                maxLength = 50;
            if (columns == 3)
                maxLength = 36;*/

            if (menu.title.CompareTo("") != 0)
            {
                Console.WriteLine(menu.title);
            }

            SetColor(Color.Gray, Color.Black);

            for (int i = 0; i < menu.items.Length; i++)
            {
                string s = "";
                if (i == 10)
                    s = 0 + ": " + menu.items[i].Text;
                else
                    s = (i + 1) + ": " + menu.items[i].Text;
                Console.WriteLine(s);
            }
            Console.WriteLine();

            menu.SelectOption(GetDigit(menu.items.Length));

            /*for (int i = 0; i < menu.items.Length * columns; i += columns)
            {
                if (columns > 1 && i == menu.items.Length)
                    break;
                string s = (i + 1) + ": " + menu.items[i].Text;
                Console.Write(s);
                if (columns > 1)
                {
                    for (int o = 0; o < maxLength - s.Length; o++)
                    {
                        Console.Write(" ");
                    }
                    if (i == menu.items.Length - 1)
                        break;
                    s = (i + 1) + 1 + ": " + menu.items[i + 1].Text;
                    Console.Write(s);
                    if (columns > 2)
                    {
                        for (int o = 0; o < maxLength - s.Length; o++)
                        {
                            Console.Write(" ");
                        }
                        if (i == menu.items.Length - 1)
                            break;
                        s = (i + 1) + 2 + ": " + menu.items[i + 2].Text;
                        Console.Write(s);
                    }
                }
                Console.WriteLine();
            }*/
        }

        public override void Print(Noun textGameObject)
        {

        }

        public override void PrintDescription(Noun textGameObject)
        {

        }

        public override void MoveCurser(Point point)
        {

        }

        public override void Anykey(string message = "<Press any key>")
        {
            while (Console.KeyAvailable) // Flush input queue
                Console.ReadKey();

            //sound
            //Console.Beep(500, 500);
            ConsoleColor temp = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n" + message);
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

        public override char GetChar()
        {
            int charValue;
            do
            {
                while (Console.KeyAvailable) // Flushes the input queue.
                    Console.ReadKey();

                charValue = Console.Read(); // Gets the user's response.
                Console.WriteLine(); // Breaks the line.
            }
            while (charValue != -1);

            return (char)charValue;
        }

        public override int GetDigit(int max)
        {
            int digit = -1;
            while (digit < 0 || digit > max || (max < 10 && digit == 0))
            {
                while (Console.KeyAvailable) // Flushes the input queue.
                    Console.ReadKey();

                ConsoleKeyInfo keyInfo;
                keyInfo = Console.ReadKey(true); // Gets the user's response. true-echo off
                /*Console.WriteLine("digit " + keyInfo);
                Console.WriteLine("digit.Key " + keyInfo.Key);
                Console.WriteLine("digit.KeyChar " + keyInfo.KeyChar);
                Console.WriteLine("digit.KeyChar-0 " + (keyInfo.KeyChar - '0'));//*/
                digit = keyInfo.KeyChar - '0';
                //Console.WriteLine(); // Breaks the line.
            }
            /*Console.WriteLine("digit " + digit);
            Console.WriteLine("digit.Key " + digit.Key);
            Console.WriteLine("digit.KeyChar " + digit.KeyChar);
            Console.WriteLine("digit.KeyChar-0 " + (digit.KeyChar - '0'));*/

            return digit;
        }

        public override string GetString(string message = "")
        {
            Print(message);

            while (Console.KeyAvailable) // Flush input queue
                Console.ReadKey();

            string inputString = Console.ReadLine();

            return inputString;
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

        public override void ClearScreen()
        {
            Console.Clear();
        }
    }
}
