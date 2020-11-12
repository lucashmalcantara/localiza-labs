using LocalizaLabs.Domain.Entities;
using LocalizaLabs.Domain.Interfaces.Services;
using LocalizaLabs.Domain.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading;

namespace LocalizaLabs.Domain.Services.Tests.Services
{
    [TestClass]
    public class NumberProcessingServiceTests
    {
        private INumberProcessingService _numberProcessingService;

        [TestInitialize]
        public void Initialize()
        {
            _numberProcessingService = new NumberProcessingService();
        }

        [TestMethod]
        public void TestDivisors_ResultAreEqual()
        {
            const long number = 100000;

            #region Expected object
            var expectedDivisors = new List<long> {
                1, 2, 4, 5, 8, 10, 16, 20, 25, 32, 40, 50, 80, 100,
                125, 160, 200, 250, 400, 500, 625, 800, 1000, 1250,
                2000, 2500, 3125, 4000, 5000, 6250, 10000, 12500, 20000,
                25000, 50000, 100000
            };
            #endregion

            Guid numberProcessingId = _numberProcessingService.RequestProcess(number);

            bool isReady;
            const int defaultWaitTime = 500;

            do
            {
                Thread.Sleep(defaultWaitTime);
                isReady = _numberProcessingService.CheckIsReady(numberProcessingId);
            } while (!isReady);

            NumberProcessingResult result = _numberProcessingService.GetResult(numberProcessingId);

            result.Divisors.Sort();
            expectedDivisors.Sort();

            CollectionAssert.AreEqual(expectedDivisors, result.Divisors);
        }

        [TestMethod]
        public void TestPrimeNumbers_ResultAreEqual()
        {
            const long number = 10;

            #region Expected object
            var expectedPrimeNumbers = new List<long> {
                2, 5
            };
            #endregion

            Guid numberProcessingId = _numberProcessingService.RequestProcess(number);

            bool isReady;
            const int defaultWaitTime = 500;

            do
            {
                Thread.Sleep(defaultWaitTime);
                isReady = _numberProcessingService.CheckIsReady(numberProcessingId);
            } while (!isReady);

            NumberProcessingResult result = _numberProcessingService.GetResult(numberProcessingId);

            result.PrimeNumbers.Sort();
            expectedPrimeNumbers.Sort();

            CollectionAssert.AreEqual(expectedPrimeNumbers, result.PrimeNumbers);
        }
    }
}
