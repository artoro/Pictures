using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Pictures.Model;

namespace Pictures.Data
{
    public static class PicturesSerializer
    {
        public static string LocalDatabaseFilePath { get; set; } = "Data\\PicturesDictionary.xml";

        public static void SerializePictures(Picture[] pictures)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Picture[]));
            if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(LocalDatabaseFilePath))) System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(LocalDatabaseFilePath));
            using (TextWriter writer = new StreamWriter(LocalDatabaseFilePath))
            {
                serializer.Serialize(writer, pictures);
            }
        }

        public static Picture[] DeserializePictures() => DeserializeXmlPictures(LocalDatabaseFilePath);
        public static Picture[] DeserializePictures(string file)
        {
            if (file == null || !File.Exists(file)) throw new FileNotFoundException();
            if (file.EndsWith(".xml")) return DeserializeXmlPictures(file);
            if (file.EndsWith(".csv")) return DeserializeCsvPictures(file);
            throw new FileFormatException();
        }

        private static Picture[] DeserializeCsvPictures(string csvFile)
        {
            Picture[] pictures = null;
            try
            {
                using (TextReader reader = new StreamReader(csvFile))
                {
                    var headers = reader.ReadLine()?.Split(",");
                    int indexID = Array.FindIndex(headers, (header) => header.Contains("ID"));
                    int indexWWW = Array.FindIndex(headers, (header) => header.Contains("WWW"));
                    int indexTags = Array.FindIndex(headers, (header) => header.Contains("Tags"));
                    int MaxColumn = Math.Max(Math.Max(indexID, indexWWW), indexTags);
                    if (headers == null || indexID < 0 || indexWWW < 0 || indexTags < 0 || indexID == indexWWW || indexID == indexTags || indexWWW == indexTags) return pictures;

                    var picturesBuffer = new Queue<Picture>();
                    var columns = reader.ReadLine()?.Split(",");
                    while (columns != null && MaxColumn < columns.Length)
                    {
                        int valueID = -1;
                        Int32.TryParse(columns[indexID], out valueID);
                        picturesBuffer.Enqueue(new Picture() { ID = valueID, WWW = columns[indexWWW], Tags = columns[indexTags] });
                        columns = reader.ReadLine()?.Split(",");
                    }

                    pictures = picturesBuffer.ToArray();
                }
            }
            catch { }
            return pictures;
        }

        private static Picture[] DeserializeXmlPictures(string xmlFile)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Picture[]));
            Picture[] pictures = null;
            try
            {
                using (TextReader reader = new StreamReader(xmlFile))
                    pictures = (Picture[])serializer.Deserialize(reader);
            }
            catch { }
            if (pictures == null || pictures.Length == 0) pictures = DefaultPictures();
            return pictures;
        }

        private static Picture[] DefaultPictures()
        {
            Picture[] pictures = new Picture[]
            {
                new Picture { ID = 1, WWW = @"https://media.istockphoto.com/id/1130904226/pl/zdj%C4%99cie/vintage-dzban-wazon-z-owocami-na-ciemnym-tle.jpg?s=612x612&w=0&k=20&c=OkAymV7aRu3L5DR1ZPe9dMs33fSQ_FfwBoFhytYhIpI=" },
                new Picture { ID = 2, WWW = @"https://media.istockphoto.com/id/1033392516/pl/zdj%C4%99cie/humbak-ciel%C4%99-relaks-na-powierzchni.jpg?s=612x612&w=0&k=20&c=BDeAbaHb2OzGHwquVxHRSmZaHKenLZiMspHyLh-3PZg=" },
                new Picture { ID = 3, WWW = @"https://www.istockphoto.com/resources/images/PhotoFTLP/1040315976.jpg" },
                new Picture { ID = 4, WWW = @"https://media.istockphoto.com/id/1126924502/pl/zdj%C4%99cie/kobieta-podr%C3%B3%C5%BCuj%C4%85ca-samolotem-i-s%C5%82uchaj%C4%85c-muzyki.jpg?s=612x612&w=0&k=20&c=SjaPgb1QuGHHysIw3Wfw9NIqRiJEh8kZS3Css1vJUR0=" },
                new Picture { ID = 5, WWW = @"https://media.istockphoto.com/id/516921682/pl/zdj%C4%99cie/westminster-most-w-zach%C3%B3d-s%C5%82o%C5%84ca-londyn-wielka-brytania.jpg?s=612x612&w=0&k=20&c=u6Yz_gdIoXP62bolIFrXpalq_dc9v1b317J7nvbtRbM=" },
                new Picture { ID = 6, WWW = @"https://media.istockphoto.com/id/75939350/pl/zdj%C4%99cie/dziewczyna-picie-mleka.jpg?s=612x612&w=0&k=20&c=dqeqc0SKgQsw1z6nrU9of0Y2DI8oPzVBVhAX5nl9WoU=" },
                new Picture { ID = 7, WWW = @"https://www.istockphoto.com/resources/images/PhotoFTLP/1154103408.jpg" },
            };

            PicturesSerializer.SerializePictures(pictures);

            return PicturesSerializer.DeserializePictures();
        }
    }
}
