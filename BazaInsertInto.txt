INSERT into Pregledi (Mesec)
VALUES ('Januar'),('Februar'),('Mart'),('April'),('Maj'),('Jun'),('Jul'),('Avgust'),('Septembar'),('Oktobar'),('Novembar'),('Decembar'); 


INSERT into TipoviStruke (Tip)
VALUES ('Doktor'),('Volonter'),('Pomocni radnik');

INSERT into VrsteZivotinja (Vrsta)
VALUES ('pas'),('macka'),('patuljasti zec'), ('morsko prase'), ('papagaj'); 

INSERT into Zivotinje (BrojKartona, ImeZivotinje, ImeVlasnika, BrojTelefonaVlasnika, VrstaZivotinjeID)
VALUES (10, 'Reks', 'Hristijan', '0601548748', 1),
(15, 'Ben', 'Marko', '0654895896', 2),
(20, 'Badi', 'Jelena','0648959521', 3),
(25, 'Don', 'Mila', '0652145848', 4),
(30, 'Lili', 'Dusan', '0618457688', 5),
(12, 'Miki', 'Nemanja', '0614588284', 5),
(22, 'Mica', 'Vukan', '0692368398', 3),
(40, 'Tomi', 'Miljan', '0609584866', 5);


INSERT into Veterinari (Ime, Prezime, BrojTelefona, TipStrukeVeterinaraID)
VALUES ('Milica', 'Petrovic', '0601524868', 1),
('Marija', 'Djordjevic', '0654895896', 2); 


INSERT into ZivotinjeVeterinari (Lek, PregledID, ZivotinjaID, VeterinarID)
VALUES ('Vakcina protiv zaraznih bolesti', 3, 1, 1),
('Meloksikam', 4, 1, 1),
('Aurizon kapi za uši', 5, 3, 1),
('Mervue CardioCare', 10, 6, 2),
('Meloxoral', 11, 2, 2),
('Bio-Lapis', 12, 7, 2),
('Trixie povrće', 2, 4, 1),
('Versele Laga Oropharma', 6, 5, 1),
('Canural kapi za uši', 7, 3, 1),
('Engemycin sprej za rane', 4, 3, 2),
('ORGANISSIME - Anti Parasite Lotion', 9, 7, 2),
('YUUP - Tea Tree & Neem Oil', 1, 2, 1),
('Espumisana', 4, 3, 2),
('STEPAŠKA šampon', 5, 4, 2),
('Pantex Pulmomectine', 8, 5, 2),
('Diafarm Vitamin C', 1, 4, 2),
('Vitakraft', 12, 7, 1),
('Mervue ', 8, 8, 1),
('Trixie voće', 4, 4, 1),
('Simparica', 9, 1, 1);