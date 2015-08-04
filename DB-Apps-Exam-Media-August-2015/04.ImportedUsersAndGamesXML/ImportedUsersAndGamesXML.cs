using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using _01.EntityFrameworkMappings;

namespace _04.ImportedUsersAndGamesXML
{
    class ImportedUsersAndGamesXML
    {
        static void Main()
        {
            var context = new DiabloEntities();

            var xml = new XmlDocument();
            xml.Load("../../users-and-games.xml");
            var rootNode = xml.DocumentElement;

            foreach (XmlNode user in rootNode.ChildNodes)
            {
                var username = user.Attributes["username"].Value;
                var ipAddress = user.Attributes["ip-address"].Value;
                DateTime registrationDate = DateTime.Parse(user.Attributes["registration-date"].Value);
                bool isDeleted = false;
                string lastName = null;
                string firstName = null;
                string email = null;

                if (user.Attributes["first-name"] != null)
                {
                    firstName = user.Attributes["first-name"].Value;
                }
                if (user.Attributes["last-name"] != null)
                {
                    lastName = user.Attributes["last-name"].Value;
                }
                if (user.Attributes["email"] != null)
                {
                    email = user.Attributes["email"].Value;
                }
                if (int.Parse(user.Attributes["is-deleted"].Value) == 1)
                {
                    isDeleted = true;
                }

                if (context.Users.Any(u => u.Username == username))
                {
                    Console.WriteLine("User {0} already exists", username);
                    continue;
                }

                var userGames = rootNode.SelectSingleNode("user/games");


                // Loop through all games for the given user
                foreach (XmlNode game in userGames)
                {
                    var gameName = game["game-name"].InnerText;
                    var character = game["character"];

                    var characterName = character.Attributes["name"].Value;
                    var characterCash = decimal.Parse(character.Attributes["cash"].Value);
                    var characterLevel = int.Parse(character.Attributes["level"].Value);
                    var joinedOn = DateTime.Parse(game["joined-on"].InnerText);
                    Console.WriteLine(joinedOn);

                    if (game["joined-on"] != null && user.Attributes["registration-date"] != null)
                    {
                        var userGame = new UsersGame()
                        {
                            Cash = characterCash,
                            Character = context.Characters.FirstOrDefault(c => c.Name == characterName),
                            Game = context.Games.FirstOrDefault(g => g.Name == gameName),
                            JoinedOn = joinedOn,
                            Level = characterLevel,
                            User = new User()
                            {
                                FirstName = firstName,
                                LastName = lastName,
                                Email = email,
                                Username = username,
                                IpAddress = ipAddress,
                                RegistrationDate = registrationDate,
                                IsDeleted = isDeleted
                            }
                        };

                        context.UsersGames.Add(userGame);

                        Console.WriteLine("Successfully added user {0}", username);
                        Console.WriteLine("User {0} successfully added to game {1}", username, gameName);
                    }
                }

                context.SaveChanges();
            }
        }
    }
}