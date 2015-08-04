using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

class XMLProcessingMain
{
    static void Main()
    {
        // 02. Extract Album Names

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(@"../../albums.xml");
        XmlNodeList albumNames = xmlDoc.SelectNodes("//catalog/albums/album/name");

        foreach (XmlNode album in albumNames)
        {
            Console.WriteLine("Album Name: " + album.InnerText);
        }


        // 03. Extract All Artists Alphabetically
        SortedSet<string> artistNames = new SortedSet<string>();
        XmlNodeList names = xmlDoc.SelectNodes("//catalog/albums/album/artist");

        foreach (XmlNode name in names)
        {
            artistNames.Add(name.InnerText);
        }

        foreach (var artistName in artistNames)
        {
            Console.WriteLine(artistName);
        }

        
        // 04. Extract artist and number of albums
        Dictionary<string, int> artistsAlbums = new Dictionary<string, int>();
        XmlNodeList aristAlbums = xmlDoc.SelectNodes("//catalog/albums/album");

        foreach (XmlNode artistAlbum in aristAlbums)
        {
           // var artistName = artistAlbum["artist"].InnerText;
            var artistAlbumsCount = artistAlbum["artist"].InnerText.Count();
            Console.WriteLine(artistAlbumsCount);

            //  artistsAlbums.Add(artistName, 1);
        }

        //foreach (var artistAlbum in artistsAlbums)
        //{
        //    Console.WriteLine(artistAlbum.Key + " " + artistAlbum.Value);
        //}


        // 06. Delete Albums with price bigger than 15.00
        XmlNodeList albums = xmlDoc.SelectNodes("//catalog/albums");

        foreach (XmlNode albumNodes in albums)
        {
            foreach (XmlNode album in albumNodes)
            {
                decimal albumPrice = decimal.Parse(album["price"].InnerText);
                if (albumPrice > 15.00m)
                {
                    album.ParentNode.RemoveChild(album);
                }
            }
        }

        xmlDoc.Save(@"../../cheap-albums-catalog.xml");


        // 07. Extract albums published 5 years ago or earlier
        XmlNodeList albumsPublished5YearsAgo = xmlDoc.SelectNodes("//catalog/albums/album[year<2010]");

        // 08. Extract title and prices for albums published 5 years ago using XDocument and LINQ
        XDocument doc = XDocument.Load(@"../../albums.xml");
        var oldAlbums = from a in doc.Descendants("album")
                        where int.Parse(a.Element("year").Value) < 2010
                        select new
                        {
                            title = (string)a.Element("name"),
                            price = (string)a.Element("price")
                        };


    }
}