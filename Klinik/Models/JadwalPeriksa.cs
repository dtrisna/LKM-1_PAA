namespace Klinik.Models  {
    public class JadwalPeriksa
    {
        public int Id { get; set; }
        public int PasienId { get; set; }
        public int DokterId { get; set; }
        public DateTime TanggalPeriksa { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}