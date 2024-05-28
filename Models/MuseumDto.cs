namespace MuseumIstanbul.Models
{
    public class MuseumDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public MuseumType Type { get; set; }

        public string Location { get; set; }
        public string Description { get; set; }
        public TimeSpan OpeningTime { get; set; }
        public TimeSpan ClosingTime { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int? Price { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime RegisterDate { get; set; }
        public List<CommentDto>? Comments { get; set; }
        public string PhotoUrl { get; set; } // Fotoğraf URL'si

    }
}
