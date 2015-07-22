using System;
using System.Diagnostics;
using System.Linq;
using Ads.Entities;

class TestToList
{
    static void Main()
    {
        var context = new AdsEntities();
        context.Database.ExecuteSqlCommand("CHECKPOINT; DBCC DROPCLEANBUFFERS;");

        var sw = new Stopwatch();
        Console.WriteLine(context.Ads.Any());

        sw.Start();
        // Messy query
        var ads = context.Ads
            .ToList()
            .Where(a => a.AdStatus.Status == "Published")
            .Select(a => new
            {
                Title = a.Title,
                Category = a.Category,
                Town = a.Town,
                Date = a.Date
            })
            .ToList()
            .OrderBy(a => a.Date);

        Console.WriteLine("Millisecond with a messy query: " + sw.ElapsedMilliseconds + "ms");
        
        sw.Restart();
        var adsImproved = context.Ads
           .Where(a => a.AdStatus.Status == "Published")
           .Select(a => new
           {
               Title = a.Title,
               Category = a.Category,
               Town = a.Town,
               Date = a.Date
           })
           .OrderBy(a => a.Date)
           .ToList();

        Console.WriteLine("Millisecond with a proper query: " + sw.ElapsedMilliseconds + "ms");


        // TEST RESULTS:
        //+---------------+-------+-------+-------+-------+-------+-------+-------+-------+-------+--------+---------+
        //|               | Run 1 | Run 2 | Run 3 | Run 4 | Run 5 | Run 6 | Run 7 | Run 8 | Run 9 | Run 10 | Average |
        //+---------------+-------+-------+-------+-------+-------+-------+-------+-------+-------+--------+---------+
        //| Non-optimized |  237  |  245  |  243  |  247  |  256  |  237  |  236  |  266  |  236  |  237   |  244ms  |
        //+---------------+-------+-------+-------+-------+-------+-------+-------+-------+-------+--------+---------+
        //| Optimized     |  123  |  122  |  125  |  128  |  121  |  123  |  123  |  121  |  122  |  123   |  123ms  |
        //+---------------+-------+-------+-------+-------+-------+-------+-------+-------+-------+--------+---------+
        // Improvement - Almost 2 (1.98) times faster.

    }
}
