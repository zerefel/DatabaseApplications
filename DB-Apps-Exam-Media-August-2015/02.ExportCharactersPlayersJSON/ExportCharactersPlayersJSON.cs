using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using _01.EntityFrameworkMappings;
using System.IO;

namespace _02.ExportCharactersPlayersJSON
{
    class ExportCharactersPlayersJSON
    {
        static void Main()
        {
            var context = new DiabloEntities();

            var charactersPlayer = context.Characters
                .OrderBy(c => c.Name)
                .Select(c => new
                {
                    name = c.Name,
                    playedBy = c.UsersGames.Select(ug => ug.User.Username)
                });

            var json = new JavaScriptSerializer().Serialize(charactersPlayer);
            File.WriteAllText("../../characters.json", json);
        }
    }
}
