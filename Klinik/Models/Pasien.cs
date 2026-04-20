
namespace Klinik.Models
{
    public class Pasien
    {
        public int Id { get; set; }
        public string Nama { get; set; }
        public DateTime TanggalLahir { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}