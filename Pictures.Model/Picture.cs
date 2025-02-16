namespace Pictures.Model
{
    /// <summary>
    /// Klasa zawierające dane na temat obrazka.
    /// </summary>
    public class Picture
    {
        private const string defaultImage = @"https://cf.geekdo-images.com/pR7dcr6bs5TIOHOhKIoe_A__itemrep/img/wW9nb34NGo1UnJg0E8vbjKCKCnE=/fit-in/246x300/filters:strip_icc()/pic4946885.jpg";
        
        public int ID { get; set; } = -1;
        public string WWW { get; set; } = defaultImage;
        public string Tags { get; set; } = string.Empty;
        public Uri Uri => new Uri(WWW);
    }
}
