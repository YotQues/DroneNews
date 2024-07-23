namespace DroneNews.Services.NewsApi
{
    public class EverythingRequest
    {

        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 100;

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        public string[] Sources { get; set; } = Array.Empty<string>();

        public string[] Domains { get; set; } = Array.Empty<string>();
        public void Deconstruct(out int page, out int pageSize, out DateTime? to, out DateTime? from)
        {
            page = Page;
            pageSize = PageSize;
            to = To;
            from = From;
        }
    }
}
