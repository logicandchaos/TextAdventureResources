using System;

namespace TextAdventureLibrary
{
    public class Menu
    {
        string title;
        public string[] options;
        int columns;

        public Menu(string p_title, string p_options, int p_columns = 1)
        {
            title = p_title;
            options = p_options.Split(',');
            if (p_columns > 3)
                p_columns = 3;
            columns = p_columns;
        }

        /*public void Print(bool p_clearScreen = true)
        {
            if (p_clearScreen)
                Console.Clear();

            int maxLength = 0;
            if (columns == 2)
                maxLength = 50;
            if (columns == 3)
                maxLength = 36;

            if (title.CompareTo("") != 0)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(title);
            }
            Console.ForegroundColor = ConsoleColor.Gray;

            for (int i = 0; i < options.Length * columns; i += columns)
            {
                if (columns > 1 && i == options.Length)
                    break;
                string s = i + ": " + options[i];
                Console.Write(s);
                if (columns > 1)
                {
                    for (int o = 0; o < maxLength - s.Length; o++)
                    {
                        Console.Write(" ");
                    }
                    if (i == options.Length - 1)
                        break;
                    s = i + 1 + ": " + options[i + 1];
                    Console.Write(s);
                    if (columns > 2)
                    {
                        for (int o = 0; o < maxLength - s.Length; o++)
                        {
                            Console.Write(" ");
                        }
                        if (i == options.Length - 1)
                            break;
                        s = i + 2 + ": " + options[i + 2];
                        Console.Write(s);
                    }
                }
                Console.WriteLine();
            }
        }

        public void Print(Point p_position)
        {
            Console.Clear();

            int maxLength = 0;
            if (columns == 2)
                maxLength = 50;
            if (columns == 3)
                maxLength = 36;

            if (title.CompareTo("") != 0)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(title);
            }
            Console.ForegroundColor = ConsoleColor.Gray;

            for (int i = 0; i < options.Length * columns; i += columns)
            {
                if (columns > 1 && i == options.Length)
                    break;

                string s = i + ": " + options[i];
                if (Program.world.map.landmarks.ContainsKey(options[i]))
                {
                    s += " "
                    + (Program.world.map.landmarks[options[i]].Location.DistanceTo(p_position)
                    * Program.world.map.scale).ToString("N2")
                    + " miles" + ",";
                }
                Console.Write(s);
                if (columns > 1)
                {
                    for (int o = 0; o < maxLength - s.Length; o++)
                    {
                        Console.Write(" ");
                    }
                    if (i == options.Length - 1)
                        break;
                    s = i + 1 + ": " + options[i + 1];
                    if (Program.world.map.landmarks.ContainsKey(options[i + 1]))
                    {
                        s += " "
                        + (Program.world.map.landmarks[options[i + 1]].Location.DistanceTo(p_position)
                        * Program.world.map.scale).ToString("N2")
                        + " miles" + ",";
                    }
                    Console.Write(s);
                    if (columns > 2)
                    {
                        for (int o = 0; o < maxLength - s.Length; o++)
                        {
                            Console.Write(" ");
                        }
                        if (i == options.Length - 1)
                            break;
                        s = i + 2 + ": " + options[i + 2];
                        if (Program.world.map.landmarks.ContainsKey(options[i + 2]))
                        {
                            s += " "
                            + (Program.world.map.landmarks[options[i + 2]].Location.DistanceTo(p_position)
                            * Program.world.map.scale).ToString("N2")
                            + " miles" + ",";
                        }
                        Console.Write(s);
                    }
                }
                Console.WriteLine();
            }
        }*/

        public string GetOption(int p_option)
        {
            return options[p_option];
        }

        public int SelectOption()
        {
            int selection = -1;
            bool isValid = false;

            while (!isValid || selection > options.Length - 1 || selection < 0)
            {
                string input = Console.ReadLine();
                isValid = Int32.TryParse(input, out selection);

                //errors
                if (!isValid)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error! Input a number between 0 and " + (options.Length - 1));
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                if (selection > options.Length - 1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error! Number too large!");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                if (selection < 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error! Number too small! Enter a non negative number.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }

            //Console.WriteLine(options[selection]);
            return selection;
        }
    }
}
