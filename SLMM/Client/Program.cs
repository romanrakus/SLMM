﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using SLMM.Common;

namespace SLMM.Client
{
    class Program
    {
        private static readonly HttpClient Client = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:9000/")
        };

        static void Main(string[] args)
        {
            var exit = false;
            while (!exit)
            {
                ShowHelp();
                string input = Console.ReadLine()?.ToLower();
                switch (input)
                {
                    case "tl":
                        SaveExecution(() => TurnMachine(-1));
                        break;
                    case "tr":
                        SaveExecution(() => TurnMachine(1));
                        break;
                    case "mf":
                        SaveExecution(async () =>
                       {
                           Console.WriteLine("Enter steps to move:");
                           int steps;
                           if (!int.TryParse(Console.ReadLine(), out steps))
                               Console.WriteLine("Invalid input. Number is expected.");
                           else
                               await Client.PutAsJsonAsync("location", new { moveBy = steps }).ContinueWith(PrintResult);
                       });
                        break;
                    case "ml":
                        SaveExecution(() => Client.PutAsJsonAsync("mower", new { on = true }).ContinueWith(PrintResult));
                        break;
                    case "exit":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Not supported command.");
                        break;
                }
            }
        }

        private static async void SaveExecution(Func<Task> func)
        {
            try
            {
                await func();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occur while command execution: {ex.Message}");
            }
        }

        private static async Task TurnMachine(int direction)
        {
            HttpResponseMessage response = await Client.GetAsync("Rotation");
            if (response.IsSuccessStatusCode)
            {
                var rotation = await response.Content.ReadAsAsync<Rotation>();
                await Client.PutAsJsonAsync("Rotation", new { direction = rotation + direction }).ContinueWith(PrintResult);
            }
            else
            {
                Console.WriteLine("Error occur during command execution");
            }
        }

        private static void PrintResult(Task<HttpResponseMessage> obj)
        {
            var result = obj.Result;
            Console.WriteLine(result.IsSuccessStatusCode
                ? "Operation completed successful."
                : $"Operation failed to execute. StatusCode: {result.StatusCode}");
        }

        private static void ShowHelp()
        {
            Console.WriteLine("Enter your command. Following commands expected:\r\n" +
                              "TL -> Turn Left\r\n" +
                              "TR -> Turn Right\r\n" +
                              "MF -> Move Forward\r\n" +
                              "ML -> Mow Lawn\r\n" +
                              "Exit -> end application\r\n");
        }
    }
}
