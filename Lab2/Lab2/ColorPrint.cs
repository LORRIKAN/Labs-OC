using System;

namespace Lab2
{
    static class ColorPrint
    {
        public static void PrintWithColor(ConsoleColor color, string message)
        {
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ResetColor();
        }
        public static void ErrorPrint(string errorMessage, string askMessage)
        {
            PrintWithColor(ConsoleColor.Red, errorMessage);
            PrintWithColor(ConsoleColor.Yellow, askMessage);
        }
    }
}