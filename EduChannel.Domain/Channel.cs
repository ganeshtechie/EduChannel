namespace EduChannel.Domain
{
    public class Channel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Author Author { get; set; }
        public int AuthorId { get; set; }

    }
}
