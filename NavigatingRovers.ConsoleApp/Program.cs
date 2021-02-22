using NavigatingRovers.ConsoleApp.Helper;
using System;
using System.Collections.Generic;

namespace NavigatingRovers.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome To Rover Home Screen! Please, Enter Your Order.");

            var commandList = new List<string>();
            bool isValidCharacter = true;
            var commandHelper = new CommandHelper();

            while (isValidCharacter)
            {
                var cmd = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(cmd))
                {
                    isValidCharacter = false;
                }
                else
                {
                    commandList.Add(cmd);
                }
            }
            try
            {
                var expectedOutput = commandHelper.Command(commandList.ToArray());
                Console.WriteLine("Expected Output: \n");
                foreach (var item in expectedOutput)
                {
                    Console.WriteLine($"{item}\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Command exeption. \n Details: {ex.Message}");
            }
        }
    }
}
