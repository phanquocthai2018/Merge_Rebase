using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using XOProject.Api.Model.Analytics;
using XOProject.Services.Domain;
using XOProject.Services.Exchange;

using Microsoft.AspNetCore.Mvc;

namespace XOProject.Api.Controller
{
    [Route("api")]
    public class AnalyticsController : ControllerBase
    {
        private readonly IAnalyticsService _analyticsService;

        public AnalyticsController(IAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }

        [HttpGet("daily/{symbol}/{year}/{month}/{day}")]
        public async Task<IActionResult> Daily([FromRoute] string symbol, [FromRoute] int year, [FromRoute] int month, [FromRoute] int day)
        {
            // TODO: Add implementation for the daily summary
            DateTime date = new DateTime(year, month, day);

            var result = new DailyModel()
            {
                Symbol = symbol,
                Day = new DateTime(),
                Price = Map(new AnalyticsPrice())
            };
            // Get daily summary
            AnalyticsPrice _price = await _analyticsService.GetDailyAsync(symbol, date);
            // assign to result
            result.Day = date;
            result.Price = Map(_price);
            return Ok(result);
        }

        [HttpGet("weekly/{symbol}/{year}/{week}")]
        public async Task<IActionResult> Weekly([FromRoute] string symbol, [FromRoute] int year, [FromRoute] int week)
        {
            // TODO: Add implementation for the weekly summary

            var result = new WeeklyModel()
            {
                Symbol = symbol,
                Year = year,
                Week = week,
                Price = Map(new AnalyticsPrice())
            };
            // Get weekly summary
            AnalyticsPrice _price = await _analyticsService.GetWeeklyAsync(symbol, year, week);
            result.Price = Map(_price);

            return Ok(result);
        }

        [HttpGet("monthly/{symbol}/{year}/{month}")]
        public async Task<IActionResult> Monthly([FromRoute] string symbol, [FromRoute] int year, [FromRoute] int month)
        {
            // TODO: Add implementation for the monthly summary
            var result = new MonthlyModel()
            {
                Symbol = symbol,
                Year = year,
                Month = month,
                Price = Map(new AnalyticsPrice())
            };
            // Get monthly summary
            AnalyticsPrice _price = await _analyticsService.GetWeeklyAsync(symbol, year, month);
            result.Price = Map(_price);

            return Ok(result);
        }

        private PriceModel Map(AnalyticsPrice price)
        {
            return new PriceModel()
            {
                Open = price.Open,
                Close = price.Close,
                High = price.High,
                Low = price.Low
            };
        }
    }
}