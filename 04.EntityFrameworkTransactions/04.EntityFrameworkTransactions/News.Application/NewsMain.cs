using System.Data.Entity.Infrastructure;

namespace News.Application
{
    using System;
    using System.Linq;
    using News.Entities;
    using Models;

    class NewsMain
    {
        static void Main()
        {

            // ConcurrencyCheck test. Open two apps at once and try to edit the same thing. 
            // The second edit will throw an exception and won't allow you to change the data.

            var context = new NewsEntities();

            while (true)
            {
                var newsList = context.News.ToList();

                Console.WriteLine("Here are the news for today: ");

                for (int i = 0; i < newsList.Count; i++)
                {
                    Console.WriteLine((i + 1) + ") " + newsList[i].Content);
                }

                Console.WriteLine("Enter the line number you wish to edit: ");

                int newsNumber = int.Parse(Console.ReadLine());
                bool exceptionCaught = false;
                News newsToEdit;

                try
                {
                    newsToEdit = newsList[newsNumber - 1];
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine("Whooops, there was no such index, please try again!");
                    continue;
                }

                Console.WriteLine("Enter the new content for the news: ");
                string newContent = Console.ReadLine();

                try
                {
                    newsToEdit.Content = newContent;
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    exceptionCaught = true;
                    Console.WriteLine("Error updating news content. It seems someone before you edited it. Please try again!");
                }

                if (!exceptionCaught)
                {
                    Console.WriteLine("News content updated successfully, exiting!");
                    Console.ReadLine();
                    break;
                }
            }
        }
    }
}