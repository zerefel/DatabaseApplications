using System;
using System.Diagnostics;
using System.Linq;
using Ads.Entities;

class TestDataSpeed
{
    static void Main()
    {
        var context = new AdsEntities();
        var stopwatch = new Stopwatch();

        Console.WriteLine(context.Ads.Any());

        stopwatch.Start();

        var allAdsNoInclude = context.Ads.ToList();

        Console.WriteLine(stopwatch.ElapsedMilliseconds);

        //foreach (var ad in allAdsNoInclude)
        //{
        //    Console.WriteLine("Ad Title: {0}, Ad Status: {1}, Ad Category: {2}, Ad Town: {3}, Ad User: {4}",
        //        ad.Title, ad.AdStatus.Status, (ad.Category == null ? "no category" : ad.Category.Name), (ad.Town == null ? "no town" : ad.Town.Name), ad.AspNetUser.Name);
        //}

        stopwatch.Restart();

        var allAdsInclude = context.Ads
            .Include("Category")
            .Include("Town")
            .Include("AspNetUser")
            .Include("AdStatus")
            .ToList();

        Console.WriteLine(stopwatch.ElapsedMilliseconds);

        //foreach (var ad in allAdsInclude)
        //{
        //    Console.WriteLine("Ad Title: {0}, Ad Status: {1}, Ad Category: {2}, Ad Town: {3}, Ad User: {4}",
        //        ad.Title, ad.AdStatus.Status, (ad.Category == null ? "no category" : ad.Category.Name), (ad.Town == null ? "no town" : ad.Town.Name), ad.AspNetUser.Name);
        //}

        // TESTS RESULTS: 
        //+--------------------------+-----------------+-------------------+
        //|                          | No Include(...) | With Include(...) |
        //+--------------------------+-----------------+-------------------+
        //| Number of SQL Statements |        29       |         1         |
        //+--------------------------+-----------------+-------------------+
        //| Milliseconds to complete |       123       |        164        |
        //+--------------------------+-----------------+-------------------+
    }
}
