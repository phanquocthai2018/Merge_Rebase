using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XOProject.Repository;
using XOProject.Repository.Domain;
using XOProject.Repository.Exchange;
using XOProject.Services.Domain;

namespace XOProject.Services.Exchange
{
    public interface IAnalyticsService: IGenericService<HourlyShareRate>
    {
        Task<AnalyticsPrice> GetDailyAsync(string symbol, DateTime day);
        Task<AnalyticsPrice> GetWeeklyAsync(string symbol, int year, int week);
        Task<AnalyticsPrice> GetMonthlyAsync(string symbol, int year, int month);

        //Thai       
        List<DateTime> YearWeekDaysToDateTime(int year, int week);
        //IShareRepository _shareControlRepository { get; set; }

    }
}
