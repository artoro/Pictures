using Pictures.Data;
using Pictures.Model;

namespace PicturesTests
{
    public class ShuffleTests
    {
        [SetUp]
        public void Setup()
        {
            Picture[] pictures =
            [
                new Picture { WWW = @"https://www.istockphoto.com/resources/images/PhotoFTLP/1154103408.jpg" },
                new Picture { WWW = @"https://media.istockphoto.com/id/1130904226/pl/zdj%C4%99cie/vintage-dzban-wazon-z-owocami-na-ciemnym-tle.jpg?s=612x612&w=0&k=20&c=OkAymV7aRu3L5DR1ZPe9dMs33fSQ_FfwBoFhytYhIpI=" },
                new Picture { WWW = @"https://media.istockphoto.com/id/1033392516/pl/zdj%C4%99cie/humbak-ciel%C4%99-relaks-na-powierzchni.jpg?s=612x612&w=0&k=20&c=BDeAbaHb2OzGHwquVxHRSmZaHKenLZiMspHyLh-3PZg=" },
                new Picture { WWW = @"https://www.istockphoto.com/resources/images/PhotoFTLP/1040315976.jpg" },
                new Picture { WWW = @"https://media.istockphoto.com/id/1126924502/pl/zdj%C4%99cie/kobieta-podr%C3%B3%C5%BCuj%C4%85ca-samolotem-i-s%C5%82uchaj%C4%85c-muzyki.jpg?s=612x612&w=0&k=20&c=SjaPgb1QuGHHysIw3Wfw9NIqRiJEh8kZS3Css1vJUR0=" },
                new Picture { WWW = @"https://media.istockphoto.com/id/516921682/pl/zdj%C4%99cie/westminster-most-w-zach%C3%B3d-s%C5%82o%C5%84ca-londyn-wielka-brytania.jpg?s=612x612&w=0&k=20&c=u6Yz_gdIoXP62bolIFrXpalq_dc9v1b317J7nvbtRbM=" },
                new Picture { WWW = @"https://media.istockphoto.com/id/75939350/pl/zdj%C4%99cie/dziewczyna-picie-mleka.jpg?s=612x612&w=0&k=20&c=dqeqc0SKgQsw1z6nrU9of0Y2DI8oPzVBVhAX5nl9WoU=" },
            ];
            PicturesSerializer.SerializePictures(pictures);
        }

        [Test]
        public void SameSeedTests()
        {
            var seed = new GameSeed("12345");
            var shuffle1 = new Shuffle(seed);
            var shuffle2 = new Shuffle("12345");
            Assert.That(shuffle2.Seed.Code, Is.EqualTo(shuffle1.Seed.Code));
            Assert.That(shuffle2.Seed.Value, Is.EqualTo(shuffle1.Seed.Value));
            Assert.That(shuffle2.Next(100000), Is.EqualTo(shuffle1.Next(100000)));
            shuffle1.Next(100000);
            Assert.That(shuffle2.Next(100000), Is.Not.EqualTo(shuffle1.Next(100000)));
            shuffle1.Seed = new GameSeed("ABC123");
            shuffle2.Seed = new GameSeed("ABC123");
            Assert.That(shuffle2.Next(100000), Is.EqualTo(shuffle1.Next(100000)));
        }
    }
}