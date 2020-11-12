using LocalizaLabs.ConsoleApp.Helpers;
using LocalizaLabs.Domain.Entities;
using LocalizaLabs.Domain.Interfaces.Services;
using LocalizaLabs.Domain.Services.Services;
using System;
using System.Threading;

namespace LocalizaLabs.ConsoleApp
{
    class Program
    {
        private static INumberProcessingService _numberProcessingService;

        static void Main(string[] args)
        {
            try
            {
                Initialize();

                ConsoleHelper.WriteHeader();
                long number = ReadNumber();

                ConsoleHelper.WriteProcessingHeader();
                Guid numberProcessingId = RequestNumberProcessing(number);
                TrackResult(numberProcessingId);
                NumberProcessingResult result = GetResult(numberProcessingId);
                ShowResult(result);
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteLineWithTime($"Houve um erro no processamento. Detalhes: {ex.Message}");
            }
            finally
            {
                Console.ReadKey();
            }
        }

        private static void Initialize()
        {
            _numberProcessingService = new NumberProcessingService();
        }

        private static void ShowResult(NumberProcessingResult result)
        {
            ConsoleHelper.WriteResultHeader();
            Console.WriteLine($"Número: {result.Number}");
            Console.WriteLine($"Início do processamento: {result.Start}");
            Console.WriteLine($"Fim do processamento: {result.End}");
            Console.WriteLine($"Divisores: {string.Join(", ", result.Dividers)}");
            Console.WriteLine($"Divisores primos: {string.Join(", ", result.PrimeNumbers)}");
        }

        private static NumberProcessingResult GetResult(Guid numberProcessingId)
        {
            ConsoleHelper.WriteLineWithTime($"Obtendo resultado da solicitação de ID {numberProcessingId}");
            return _numberProcessingService.GetResult(numberProcessingId);
        }

        private static void TrackResult(Guid numberProcessingId)
        {
            ConsoleHelper.WriteLineWithTime($"Acompanhando solicitação de ID {numberProcessingId}");

            bool isReady;
            const int defaultWaitTime = 2000;

            do
            {
                isReady = _numberProcessingService.CheckIsReady(numberProcessingId);

                if(isReady)
                    ConsoleHelper.WriteLineWithTime($"A solicitação de ID {numberProcessingId} foi processada com sucesso!");
                else
                {
                    ConsoleHelper.WriteLineWithTime($"A solicitação de ID {numberProcessingId} ainda está sendo processada.");
                    Thread.Sleep(defaultWaitTime);
                }
                    

            } while (!isReady);
        }

        private static Guid RequestNumberProcessing(long number)
        {
            ConsoleHelper.WriteLineWithTime($"Solicitando processamento do número {number}");
            return _numberProcessingService.RequestProcess(number);
        }

        private static long ReadNumber()
        {
            long? number = null;

            do
            {
                Console.WriteLine("Digite um número: ");

                try
                {
                    number = long.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("[Atenção] Número inválido! Tente novamente.");
                }
            } while (!number.HasValue);

            return number.Value;
        }
    }
}
