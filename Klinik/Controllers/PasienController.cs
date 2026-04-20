using Microsoft.AspNetCore.Mvc;
using Klinik.Models;
using Klinik.Helpers;
using System.Data;

namespace Klinik.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasienController : ControllerBase
    {
        private readonly sqlHelper _db;

        public PasienController(IConfiguration config)
        {
            _db = new sqlHelper(config.GetConnectionString("WebApiDatabase"));
        }

        [HttpGet]
        public IActionResult GetPasien()
        {
            var dt = _db.ExecuteQuery("SELECT * FROM Pasien");
            var list = dt.AsEnumerable().Select(row => new Pasien
            {
                Id = row.Field<int>("Id"),
                Nama = row.Field<string>("Nama"),
                TanggalLahir = row.Field<DateTime>("TanggalLahir"),
                CreatedAt = row.Field<DateTime>("CreatedAt"),
                UpdatedAt = row.Field<DateTime>("UpdatedAt")
            }).ToList();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult GetPasienById(int id)
        {
            string query = "SELECT * FROM Pasien WHERE Id=@id";
            var parameters = new Dictionary<string, object> { { "@id", id } };
            var dt = _db.ExecuteQuery(query, parameters);

            if (dt.Rows.Count == 0) return NotFound(new { message = "Pasien tidak ditemukan" });

            var pasien = new Pasien
            {
                Id = dt.Rows[0].Field<int>("Id"),
                Nama = dt.Rows[0].Field<string>("Nama"),
                TanggalLahir = dt.Rows[0].Field<DateTime>("TanggalLahir"),
                CreatedAt = dt.Rows[0].Field<DateTime>("CreatedAt"),
                UpdatedAt = dt.Rows[0].Field<DateTime>("UpdatedAt")
            };

            return Ok(pasien);
        }

        [HttpPost]
        public IActionResult CreatePasien([FromBody] Pasien pasien)
        {
            string query = "INSERT INTO Pasien (Nama, TanggalLahir) VALUES (@nama, @tgl)";
            var parameters = new Dictionary<string, object> {
                { "@nama", pasien.Nama },
                { "@tgl", pasien.TanggalLahir }
            };
            _db.ExecuteNonQuery(query, parameters);
            return Ok(new { message = "Pasien berhasil ditambahkan" });
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePasien(int id, [FromBody] Pasien pasien)
        {
            string query = "UPDATE Pasien SET Nama=@nama, TanggalLahir=@tgl WHERE Id=@id";
            var parameters = new Dictionary<string, object> {
                { "@nama", pasien.Nama },
                { "@tgl", pasien.TanggalLahir },
                { "@id", id }
            };
            int rows = _db.ExecuteNonQuery(query, parameters);
            return rows > 0 ? Ok(new { message = "Pasien berhasil diupdate" }) : NotFound(new { message = "Pasien tidak ditemukan" });
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePasien(int id)
        {
            string query = "DELETE FROM Pasien WHERE Id=@id";
            var parameters = new Dictionary<string, object> { { "@id", id } };
            int rows = _db.ExecuteNonQuery(query, parameters);
            return rows > 0 ? Ok(new { message = "Pasien berhasil dihapus" }) : NotFound(new { message = "Pasien tidak ditemukan" });
        }
    }
}
