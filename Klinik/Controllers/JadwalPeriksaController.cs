using Microsoft.AspNetCore.Mvc;
using Klinik.Models;
using Klinik.Helpers;
using System.Data;

namespace Klinik.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JadwalPeriksaController : ControllerBase
    {
        private readonly sqlHelper _db;

        public JadwalPeriksaController(IConfiguration config)
        {
            _db = new sqlHelper(config.GetConnectionString("WebApiDatabase"));
        }

        // GET semua jadwal
        [HttpGet]
        public IActionResult GetJadwal()
        {
            string query = @"
                SELECT jp.Id, p.Nama AS NamaPasien, d.Nama AS NamaDokter, d.Spesialis,
                       jp.TanggalPeriksa, jp.CreatedAt, jp.UpdatedAt
                FROM JadwalPeriksa jp
                JOIN Pasien p ON jp.PasienId = p.Id
                JOIN Dokter d ON jp.DokterId = d.Id";
            var dt = _db.ExecuteQuery(query);
            var list = dt.AsEnumerable().Select(row => new {
                Id = row.Field<int>("Id"),
                NamaPasien = row.Field<string>("NamaPasien"),
                NamaDokter = row.Field<string>("NamaDokter"),
                Spesialis = row.Field<string>("Spesialis"),
                TanggalPeriksa = row.Field<DateTime>("TanggalPeriksa"),
                CreatedAt = row.Field<DateTime>("CreatedAt"),
                UpdatedAt = row.Field<DateTime>("UpdatedAt")
            }).ToList();
            return Ok(list);
        }

        // GET jadwal by Id
        [HttpGet("{id}")]
        public IActionResult GetJadwalById(int id)
        {
            string query = @"
                SELECT jp.Id, p.Nama AS NamaPasien, d.Nama AS NamaDokter, d.Spesialis,
                       jp.TanggalPeriksa, jp.CreatedAt, jp.UpdatedAt
                FROM JadwalPeriksa jp
                JOIN Pasien p ON jp.PasienId = p.Id
                JOIN Dokter d ON jp.DokterId = d.Id
                WHERE jp.Id=@id";
            var parameters = new Dictionary<string, object> { { "@id", id } };
            var dt = _db.ExecuteQuery(query, parameters);

            if (dt.Rows.Count == 0) return NotFound(new { message = "Jadwal periksa tidak ditemukan" });

            var jadwal = new
            {
                Id = dt.Rows[0].Field<int>("Id"),
                NamaPasien = dt.Rows[0].Field<string>("NamaPasien"),
                NamaDokter = dt.Rows[0].Field<string>("NamaDokter"),
                Spesialis = dt.Rows[0].Field<string>("Spesialis"),
                TanggalPeriksa = dt.Rows[0].Field<DateTime>("TanggalPeriksa"),
                CreatedAt = dt.Rows[0].Field<DateTime>("CreatedAt"),
                UpdatedAt = dt.Rows[0].Field<DateTime>("UpdatedAt")
            };

            return Ok(jadwal);
        }

        // POST jadwal baru
        [HttpPost]
        public IActionResult CreateJadwal([FromBody] JadwalPeriksa jadwal)
        {
            string query = "INSERT INTO JadwalPeriksa (PasienId, DokterId, TanggalPeriksa) VALUES (@pasienId, @dokterId, @tgl)";
            var parameters = new Dictionary<string, object> {
                { "@pasienId", jadwal.PasienId },
                { "@dokterId", jadwal.DokterId },
                { "@tgl", jadwal.TanggalPeriksa }
            };
            _db.ExecuteNonQuery(query, parameters);
            return Ok(new { message = "Jadwal periksa berhasil ditambahkan" });
        }

        // PUT update jadwal
        [HttpPut("{id}")]
        public IActionResult UpdateJadwal(int id, [FromBody] JadwalPeriksa jadwal)
        {
            string query = "UPDATE JadwalPeriksa SET PasienId=@pasienId, DokterId=@dokterId, TanggalPeriksa=@tgl WHERE Id=@id";
            var parameters = new Dictionary<string, object> {
                { "@pasienId", jadwal.PasienId },
                { "@dokterId", jadwal.DokterId },
                { "@tgl", jadwal.TanggalPeriksa },
                { "@id", id }
            };
            int rows = _db.ExecuteNonQuery(query, parameters);
            return rows > 0 ? Ok(new { message = "Jadwal periksa berhasil diupdate" }) : NotFound(new { message = "Jadwal periksa tidak ditemukan" });
        }

        // DELETE jadwal
        [HttpDelete("{id}")]
        public IActionResult DeleteJadwal(int id)
        {
            string query = "DELETE FROM JadwalPeriksa WHERE Id=@id";
            var parameters = new Dictionary<string, object> { { "@id", id } };
            int rows = _db.ExecuteNonQuery(query, parameters);
            return rows > 0 ? Ok(new { message = "Jadwal periksa berhasil dihapus" }) : NotFound(new { message = "Jadwal periksa tidak ditemukan" });
        }
    }
}
