namespace Serialization.Benchmark.Models
{
    public class ImageRootObject
    {
        public Image Image { get; set; }
    }

    public class Image
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public string Title { get; set; }
        public Thumbnail Thumbnail { get; set; }
        public bool Animated { get; set; }
        public int[] IDs { get; set; }
    }

    public class Thumbnail
    {
        public string Url { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
    }

}

