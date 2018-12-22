using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using XOProject.Repository;
using XOProject.Repository.Domain;
using XOProject.Repository.Exchange;
using XOProject.Services.Domain;

namespace XOProject.Services.Exchange
{
    public class AnalyticsService : GenericService<HourlyShareRate>, IAnalyticsService
    {
        private readonly IShareRepository _shareRepository;
        public AnalyticsService(IShareRepository shareRepository) : base(shareRepository)
        {
            _shareRepository = shareRepository;
     
        }


        public async Task<AnalyticsPrice> GetDailyAsync(string symbol, DateTime day)
        {
            // TODO: Add implementation for the daily summary
            IList<HourlyShareRate> hourlyShare = await _shareRepository
                .Query()
                .Where(x => x.Symbol.Equals(symbol)
                            && x.TimeStamp.Date == day.Date).ToListAsync();
            AnalyticsPrice result = new AnalyticsPrice
            {
                Open = hourlyShare.OrderByDescending(x => x.TimeStamp).LastOrDefault().Rate,
                High = hourlyShare.Max(x => x.Rate),
                Low = hourlyShare.Min(x => x.Rate),
                Close = hourlyShare.OrderByDescending(x => x.TimeStamp).FirstOrDefault().Rate
            };
            return result;

            throw new NotImplementedException();
        }

        public async Task<AnalyticsPrice> GetWeeklyAsync(string symbol, int year, int week)
        {
            // TODO: Add implementation for the weekly summary
            List<DateTime> dates = YearWeekDaysToDateTime(year, week);

            IList<HourlyShareRate> hourlyShares = await _shareRepository
                .Query()
                .Where(x => x.Symbol.Equals(symbol)
                            && x.TimeStamp.Date >= dates.FirstOrDefault()
                            && x.TimeStamp.Date <= dates.LastOrDefault()).ToListAsync();


            AnalyticsPrice result = new AnalyticsPrice
            {
                Open = hourlyShares.OrderByDescending(x => x.TimeStamp).LastOrDefault().Rate,
                High = hourlyShares.Max(x => x.Rate),
                Low = hourlyShares.Min(x => x.Rate),
                Close = hourlyShares.OrderByDescending(x => x.TimeStamp).FirstOrDefault().Rate
            };
            return result;

            throw new NotImplementedException();
        }

        public async Task<AnalyticsPrice> GetMonthlyAsync(string symbol, int year, int month)
        {
            // TODO: Add implementation for the monthly summary
            IList<HourlyShareRate> hourlyShare = await _shareRepository
                .Query()
                .Where(x => x.Symbol.Equals(symbol)
                            && x.TimeStamp.Month == month
                            && x.TimeStamp.Year == year).ToListAsync();

            AnalyticsPrice result = new AnalyticsPrice
            {
                Open = hourlyShare.OrderByDescending(x => x.TimeStamp).LastOrDefault().Rate,
                High = hourlyShare.Max(x => x.Rate),
                Low = hourlyShare.Min(x => x.Rate),
                Close = hourlyShare.OrderByDescending(x => x.TimeStamp).FirstOrDefault().Rate
            };
            return result;
            throw new NotImplementedException();
        }

        //Thai    
        public List<DateTime> YearWeekDaysToDateTime(int year, int week)
        {
            DateTime startOfYear = new DateTime(year, 1, 1);

            // The +7 and %7 stuff is to avoid negative numbers etc.
            int daysToFirstCorrectDay = (((int)startOfYear.DayOfWeek) + 7) % 7;

            DateTime firstdaysofweek = startOfYear.AddDays(7 * (week - 1));
            DateTime lastdaysofweek = startOfYear.AddDays(7 * week - 1);

            List<DateTime> weekdays = new List<DateTime>();
            for (int i = 0; i < 7; i++)
            {
                if (firstdaysofweek.AddDays(i).Year == year)
                    weekdays.Add(firstdaysofweek.AddDays(i));
            }

            return weekdays;
        }
    }
}