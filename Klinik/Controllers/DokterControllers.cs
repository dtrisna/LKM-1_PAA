using Microsoft.AspNetCore.Mvc;
using Klinik.Models;
using Klinik.Helpers;
using System.Data;

namespace Klinik.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DokterController : ControllerBase
    {
        private readonly sqlHelper _db;

        public DokterController(IConfiguration config)
        {
            _db = new sqlHelper(config.GetConnectionString("WebApiDatabase"));
        }

        [HttpGet]
        public IActionResult GetDokter()
        {
            var dt = _db.ExecuteQuery("SELECT * FROM Dokter");
            var list = dt.AsEnumerable().Select(row => new Dokter
            {
                Id = row.Field<int>("Id"),
                Nama = row.Field<string>("Nama"),
                Spesialis = row.Field<string>("Spesialis"),
                CreatedAt = row.Field<DateTime>("CreatedAt"),
                UpdatedAt = row.Field<DateTime>("UpdatedAt")
            }).ToList();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult GetDokterById(int id)
        {
            string query = "SELECT * FROM Dokter WHERE Id=@id";
            var parameters = new Dictionary<string, object> { { "@id", id } };
            var dt = _db.ExecuteQuery(query, parameters);

            if (dt.Rows.Count == 0) return NotFound(new { message = "Dokter tidak ditemukan" });

            var dokter = new Dokter
            {
                Id = dt.Rows[0].Field<int>("Id"),
                Nama = dt.Rows[0].Field<string>("Nama"),
                Spesialis = dt.Rows[0].Field<string>("Spesialis"),
                CreatedAt = dt.Rows[0].Field<DateTime>("CreatedAt"),
                UpdatedAt = dt.Rows[0].Field<DateTime>("UpdatedAt")
            };
            return Ok(dokter);
        }

        [HttpPost]
        public IActionResult CreateDokter([FromBody] Dokter dokter)
        {
            string query = "INSERT INTO Dokter (Nama, Spesialis) VALUES (@nama, @spesialis)";
            var parameters = new Dictionary<string, object> {
                { "@nama", dokter.Nama },
                { "@spesialis", dokter.Spesialis }
            };
            _db.ExecuteNonQuery(query, parameters);
            return Ok(new { message = "Dokter berhasil ditambahkan" });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateDokter(int id, [FromBody] Dokter dokter)
        {
            string query = "UPDATE Dokter SET Nama=@nama, Spesialis=@spesialis WHERE Id=@id";
            var parameters = new Dictionary<string, object> {
                { "@nama", dokter.Nama },
                { "@spesialis", dokter.Spesialis },
                { "@id", id }
            };
            int rows = _db.ExecuteNonQuery(query, parameters);
            return rows > 0 ? Ok(new { message = "Dokter berhasil diupdate" }) : NotFound(new { message = "Dokter tidak ditemukan" });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDokter(int id)
        {
            string query = "DELETE FROM Dokter WHERE Id=@id";
            var parameters = new Dictionary<string, object> { { "@id", id } };
            int rows = _db.ExecuteNonQuery(query, parameters);
            return rows > 0 ? Ok(new { message = "Dokter berhasil dihapus" }) : NotFound(new { message = "Dokter tidak ditemukan" });
        }
    }
}
