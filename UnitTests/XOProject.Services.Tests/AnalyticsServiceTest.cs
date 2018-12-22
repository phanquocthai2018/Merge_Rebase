using System.Collections.Generic;
using System.Text;
using System;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using XOProject.Repository.Domain;
using XOProject.Repository.Exchange;
using XOProject.Services.Exchange;
using XOProject.Services.Tests.Helpers;

namespace XOProject.Services.Tests
{
    public class AnalyticsServiceTest
    {        

        private readonly Mock<IShareRepository> _shareRepositoryMock = new Mock<IShareRepository>();

        private readonly AnalyticsService _analyticsService;

        public AnalyticsServiceTest()
        {
            _analyticsService = new AnalyticsService(_shareRepositoryMock.Object);

        }

        [TearDown]
        public void Cleanup()
        {
            _shareRepositoryMock.Reset();
        }

        [Test]
        public async Task DailySummaryAsync()
        {
            // Arrange
            ArrangeRates();

            // Act
            var result = await _analyticsService.GetDailyAsync("CBI", new DateTime(2018, 08, 17, 5, 10, 15));

            
            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(300.0M, result.Open);
            Assert.AreEqual(400.0M, result.Close);
            Assert.AreEqual(400.0M, result.High);
            Assert.AreEqual(300.0M, result.Low);
        }
        [Test]
        public async Task WeeklySummaryAsync()
        {
            // Arrange
            ArrangeRates();

            // Act
            var result = await _analyticsService.GetWeeklyAsync("CBI", 2018, 33);


            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(320.0M, result.Open);
            Assert.AreEqual(400.0M, result.Close);
            Assert.AreEqual(400.0M, result.High);
            Assert.AreEqual(300.0M, result.Low);
        }
        [Test]
        public async Task MonthlySummaryAsync()
        {
            // Arrange
            ArrangeRates();

            // Act
            var result = await _analyticsService.GetMonthlyAsync("CBI", 2018, 8);


            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(320.0M, result.Open);
            Assert.AreEqual(400.0M, result.Close);
            Assert.AreEqual(400.0M, result.High);
            Assert.AreEqual(300.0M, result.Low);
        }
        private void ArrangeRates()
        {
            var rates = new[]
            {
                new HourlyShareRate
                {
                    Id = 1,
                    Symbol = "CBI",
                    Rate = 310.0M,
                    TimeStamp = new DateTime(2017, 08, 17, 5, 0, 0)
                },
                new HourlyShareRate
                {
                    Id = 2,
                    Symbol = "CBI",
                    Rate = 320.0M,
                    TimeStamp = new DateTime(2018, 08, 16, 5, 0, 0)
                },
                new HourlyShareRate
                {
                    Id = 3,
                    Symbol = "REL",
                    Rate = 300.0M,
                    TimeStamp = new DateTime(2018, 08, 17, 5, 0, 0)
                },
                new HourlyShareRate
                {
                    Id = 4,
                    Symbol = "CBI",
                    Rate = 300.0M,
                    TimeStamp = new DateTime(2018, 08, 17, 5, 0, 0)
                },
                new HourlyShareRate
                {
                    Id = 5,
                    Symbol = "CBI",
                    Rate = 400.0M,
                    TimeStamp = new DateTime(2018, 08, 17, 6, 0, 0)
                },
                new HourlyShareRate
                {
                    Id = 6,
                    Symbol = "IBM",
                    Rate = 350.0M,
                    TimeStamp = new DateTime(2018, 08, 17, 5, 0, 0)
                },
                new HourlyShareRate
                {
                    Id = 7,
                    Symbol = "CBI",
                    Rate = 390.0M,
                    TimeStamp = new DateTime(2018, 09, 1, 5, 0, 0)
                },
                new HourlyShareRate
                {
                    Id = 8,
                    Symbol = "CBI",
                    Rate = 500.0M,
                    TimeStamp = new DateTime(2018, 09, 2, 6, 0, 0)
                },
                 new HourlyShareRate
                {
                    Id = 9,
                    Symbol = "CBI",
                    Rate = 600.0M,
                    TimeStamp = new DateTime(2018, 09, 3, 5, 0, 0)
                },
                new HourlyShareRate
                {
                    Id = 10,
                    Symbol = "CBI",
                    Rate = 450.0M,
                    TimeStamp = new DateTime(2018, 09, 4, 6, 0, 0)
                },
            };
            _shareRepositoryMock
                .Setup(mock => mock.Query())
                .Returns(new AsyncQueryResult<HourlyShareRate>(rates));
        }
    }
}
