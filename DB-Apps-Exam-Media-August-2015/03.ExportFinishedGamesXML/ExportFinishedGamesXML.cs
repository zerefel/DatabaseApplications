namespace _03.ExportFinishedGamesXML
{
    using System.Linq;
    using System.Xml.Linq;
    using _01.EntityFrameworkMappings;

    class ExportFinishedGamesXML
    {
        static void Main()
        {
            var context = new DiabloEntities();

            var finishedGames = context.Games
                .Where(g => g.IsFinished)
                .OrderBy(g => g.Name)
                .ThenBy(g => g.Duration)
                .Select(g => new
                {
                    Name = g.Name,
                    Duration = g.Duration,
                    Users = g.UsersGames.Select(ug => new
                    {
                        Username = ug.User.Username,
                        IPAddress = ug.User.IpAddress
                    })
                });


            var doc = new XDocument();
            var rootElem = new XElement("games");

            foreach (var finishedGame in finishedGames)
            {
                if (finishedGame.Name != null)
                {
                    var gameElem = new XElement("game", new XAttribute("name", finishedGame.Name));

                    if (finishedGame.Duration != null)
                    {
                        gameElem.Add(new XAttribute("duration", finishedGame.Duration));
                    }

                    var usersElem = new XElement("users");

                    foreach (var user in finishedGame.Users)
                    {
                        var userElem = new XElement("user", new XAttribute("username", user.Username), new XAttribute("ip-address", user.IPAddress));
                        usersElem.Add(userElem);
                    }

                    gameElem.Add(usersElem);
                    rootElem.Add(gameElem);
                }
            }

            doc.Add(rootElem);
            doc.Save("../../finished-games.xml");
        }
    }
}