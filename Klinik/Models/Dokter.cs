namespace Klinik.Models {
public class Dokter 
    {
        public int Id { get; set; }
        public string Nama { get; set; }
        public string Spesialis { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}