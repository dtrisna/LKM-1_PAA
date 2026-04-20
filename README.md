- Nama : Dina Trisnawati
- NIM: 242410102010
- Kelas: PAA A  

---
Deskripsi
- Klinik REST API adalah aplikasi berbasis ASP.NET Core Web API yang digunakan untuk mengelola data Pasien, Dokter, dan Jadwal Periksa.  
API ini mendukung operasi CRUD (Create, Read, Update, Delete) serta menerapkan relasi antar tabel menggunakan foreign key.

---

Teknologi yang Digunakan
- ASP.NET Core Web API  
- PostgreSQL  
- Swagger  

---

Struktur Database
Pasien
- id (Primary Key)  
- nama  
- alamat  
- tanggal_lahir  
- created_at  
- updated_at  

Dokter
- id (Primary Key)  
- nama  
- spesialis  
- created_at  
- updated_at  

JadwalPeriksa
- id (Primary Key)  
- pasien_id (Foreign Key)  
- dokter_id (Foreign Key)  
- tanggal  
- created_at  
- updated_at  

---

Cara Menjalankan Project
1. Clone repository
   ```bash
   git clone https://github.com/dtrisna/LKM-1_PAA.git
   cd LKM-1_PAA
2. Import database
   - Buka PostgreSQL
   - Jalankan file database.sql
3. Konfigurasi koneksi database
   - Buka file appsettings.json
   - Sesuaikan connection string
      ```bash
     Host=localhost;Username=postgres;Password=[Password];Database=klinikdb
4. Jalankan project
   - Buka di Visual Studio 2022
   - Klik Run
5. Akses swagger
    ```bash
     https://localhost:xxxx/swagger

Endpoint API
Pasien
| Method | Endpoint | Deskripsi |
| --- | --- | --- |
| GET | /api/pasien | Mengambil seluruh data pasien |
| GET | /api/pasien/{id} | Mengambil data pasien berdasarkan ID |
| POST | /api/pasien | Menambahkan data pasien |
| PUT | /api/pasien/{id} | Memperbarui data pasien |
| DELETE | /api/pasien/{id} | Menghapus data pasien |

Dokter
| Method | Endpoint | Deskripsi |
| --- | --- | --- |
| GET | /api/dokter | Mengambil seluruh data dokter |
| GET | /api/dokter/{id} | Mengambil data dokter berdasarkan ID |
| POST | /api/dokter | Menambahkan data dokter |
| PUT | /api/dokter/{id} | Memperbarui data dokter |
| DELETE | /api/dokter/{id} | Menghapus data dokter |

Jadwal Periksa 
| Method | Endpoint | Deskripsi |
| --- | --- | --- |
| GET | /api/jadwalperiksa | Mengambil seluruh jadwal periksa |
| GET | /api/jadwalperiksa/{id} | Mengambil detail jadwal periksa |
| POST | /api/jadwalperiksa | Menambahkan jadwal periksa baru |
| PUT | /api/jadwalperiksa/{id} | Memperbarui jadwal periksa |
| DELETE | /api/jadwalperiksa/{id} | Menghapus jadwal periksa |

Fitur Utama
- Implementasi RESTful API
- Operasi CRUD pada seluruh entitas
- Relasi antar tabel menggunakan foreign key
- Dokumentasi dan pengujian API menggunakan Swagger

Contoh request (post pasien)

    {
    "nama": "Dina",
    "alamat": "Jl. Merdeka No.10",
    "tanggal_lahir": "1998-09-14"
    }

Contoh request (post pasien)

    {
    "status": "success",
    "data": [
      {
        "id": 1,
        "nama": "Dina",
        "alamat": "Jl. Merdeka No.10",
        "tanggal_lahir": "1998-09-14"
      }
    ]
  }

Link Presentasi
  ```bash
  https://youtu.be/tUCu-sr_zAE

