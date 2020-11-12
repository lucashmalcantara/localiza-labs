using System;

namespace LocalizaLabs.ConsoleApp.Helpers
{
    public class ConsoleHelper
    {
        public static void WriteHeader()
        {
            Console.WriteLine();
            Console.WriteLine("=========================================================");
            Console.WriteLine(" Olá! Seja bem vindo ao Momento mão na massa - Localiza");
            Console.WriteLine(" Por: Lucas Alcântara");
            Console.WriteLine("=========================================================");
        }

        public static void WriteResultHeader()
        {
            Console.WriteLine();
            Console.WriteLine("=========================================================");
            Console.WriteLine(" RESULTADO");
            Console.WriteLine("=========================================================");
        }

        public static void WriteProcessingHeader()
        {
            Console.WriteLine();
            Console.WriteLine("=========================================================");
            Console.WriteLine(" PROCESSANDO REQUISIÇÃO");
            Console.WriteLine("=========================================================");
        }

        public static void WriteLineWithTime(string message)
        {
            Console.WriteLine($"> [{DateTime.Now:dd/MM/yyyy HH:mm:ss.ffff}] {message}");
        }
    }
}
