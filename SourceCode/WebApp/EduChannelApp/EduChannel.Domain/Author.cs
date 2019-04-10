using System.Collections.Generic;

namespace EduChannel.Domain
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Channel> Channels { get; set; }
    }
}
