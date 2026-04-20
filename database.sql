-- Tabel Pasien
CREATE TABLE Pasien (
    Id SERIAL PRIMARY KEY,
    Nama VARCHAR(100) NOT NULL,
    TanggalLahir DATE,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE OR REPLACE FUNCTION update_updatedat_column()
RETURNS TRIGGER AS $$
BEGIN
  NEW.UpdatedAt = CURRENT_TIMESTAMP;
  RETURN NEW;
END;
$$ language plpgsql;

CREATE TRIGGER set_updatedat_pasien
BEFORE UPDATE ON Pasien
FOR EACH ROW
EXECUTE FUNCTION update_updatedat_column();

-- Tabel Dokter
CREATE TABLE Dokter (
    Id SERIAL PRIMARY KEY,
    Nama VARCHAR(100) NOT NULL,
    Spesialis VARCHAR(100),
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TRIGGER set_updatedat_dokter
BEFORE UPDATE ON Dokter
FOR EACH ROW
EXECUTE FUNCTION update_updatedat_column();

-- Index hanya di kolom Spesialis
CREATE INDEX idx_dokter_spesialis ON Dokter(Spesialis);

-- Tabel JadwalPeriksa
CREATE TABLE JadwalPeriksa (
    Id SERIAL PRIMARY KEY,
    PasienId INT NOT NULL,
    DokterId INT NOT NULL,
    TanggalPeriksa TIMESTAMP NOT NULL,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_pasien FOREIGN KEY (PasienId) REFERENCES Pasien(Id) ON DELETE CASCADE,
    CONSTRAINT fk_dokter FOREIGN KEY (DokterId) REFERENCES Dokter(Id) ON DELETE CASCADE
);

CREATE TRIGGER set_updatedat_jadwalperiksa
BEFORE UPDATE ON JadwalPeriksa
FOR EACH ROW
EXECUTE FUNCTION update_updatedat_column();

-- Sample data Pasien
INSERT INTO Pasien (Nama, TanggalLahir) VALUES
('Budi Santoso', '1990-05-12'),
('Siti Aminah', '1985-03-20'),
('Andi Wijaya', '2000-11-01'),
('Dina Kartika', '1995-07-15'),
('Rudi Hartono', '1988-09-09');

-- Sample data Dokter
INSERT INTO Dokter (Nama, Spesialis) VALUES
('Dr. Siti Aminah', 'Penyakit Dalam'),
('Dr. Budi Prasetyo', 'Anak'),
('Dr. Rina Marlina', 'Gigi'),
('Dr. Andi Saputra', 'Bedah'),
('Dr. Kartika Dewi', 'Kulit');

-- Sample data JadwalPeriksa
INSERT INTO JadwalPeriksa (PasienId, DokterId, TanggalPeriksa) VALUES
(1, 1, '2026-04-20 09:00:00'),
(2, 2, '2026-04-21 10:30:00'),
(3, 3, '2026-04-22 14:00:00'),
(4, 4, '2026-04-23 08:00:00'),
(5, 5, '2026-04-24 11:15:00');