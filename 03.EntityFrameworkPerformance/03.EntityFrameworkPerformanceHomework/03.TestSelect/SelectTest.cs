using System;
using System.Diagnostics;
using System.Linq;
using Ads.Entities;

class SelectTest
{
    static void Main()
    {
        var context = new AdsEntities();
        var sw = new Stopwatch();
        Console.WriteLine(context.Ads.Any());

        sw.Start();
        var ads = context.Ads.ToList();
        Console.WriteLine(sw.ElapsedMilliseconds);

        sw.Restart();

        var adsTitle = context.Ads.Select(a => a.Title).ToList();

        Console.WriteLine(sw.ElapsedMilliseconds);

        //foreach (var ad in ads)
        //{
        //    Console.WriteLine(ad.Title);
        //}

        //foreach (var title in adsTitle)
        //{
        //    Console.WriteLine(title);
        //}


        // TEST RESULTS:
        //+---------------+-------+-------+-------+-------+-------+-------+-------+-------+-------+--------+---------+
        //|               | Run 1 | Run 2 | Run 3 | Run 4 | Run 5 | Run 6 | Run 7 | Run 8 | Run 9 | Run 10 | Average |
        //+---------------+-------+-------+-------+-------+-------+-------+-------+-------+-------+--------+---------+
        //| Non-optimized |  120  |  120  |  120  |  120  |  123  |  123  |  120  |  123  |  124  |  122   |  122ms  |
        //+---------------+-------+-------+-------+-------+-------+-------+-------+-------+-------+--------+---------+
        //| Optimized     |   10  |   10  |   9   |  10   |   9   |   9   |   9   |   9   |   12  |   9    |   10ms  |
        //+---------------+-------+-------+-------+-------+-------+-------+-------+-------+-------+--------+---------+
        // Takeaway: Nearly 12 times faster for such a simple query (queries are cached)
    }
}