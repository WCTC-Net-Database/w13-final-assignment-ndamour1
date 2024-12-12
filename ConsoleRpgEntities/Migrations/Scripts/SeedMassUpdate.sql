-- Update Players
UPDATE Players
SET Race = 'Human', Class = 'Fighter', Health = 17, Modifier = 10, RoomId = 1
WHERE Id = 1;

-- Update Inventory
INSERT INTO Inventory (PlayerId)
VALUES (1);

UPDATE Items
SET InventoryId = 1
WHERE Id = 1;

-- Update Rooms
UPDATE Rooms
SET PlayerId = 1
WHERE Id = 1;

-- Add Goblins
SET IDENTITY_INSERT Monsters ON;
INSERT INTO Monsters (Id, Name, Race, Health, AggressionLevel, Sneakiness)
VALUES
	(2, 'Pounk', 'Goblin', 10, 15, 5),
	(3, 'Bis', 'Goblin', 14, 15, 2),
	(4, 'Marka', 'Goblin', 13, 7, 20),
	(5, 'Gorkou', 'Goblin', 15, 17, 1),
	(6, 'Floffo', 'Goblin', 12, 9, 12),
	(7, 'Moorch', 'Goblin', 17, 13, 8),
	(8, 'Krovno', 'Goblin', 11, 14, 17),
	(9, 'Zurk', 'Goblin', 20, 20, 0),
	(10, 'Puglak', 'Goblin', 16, 5, 7),
	(11, 'Ozroth', 'Rakshasa', 80, 75, 20),
	(12, 'Ju Kwang', 'Kumiho', 90, 110, 35);
SET IDENTITY_INSERT Monsters OFF;

-- Add Orroks
SET IDENTITY_INSERT Monsters ON;
INSERT INTO Monsters (Id, Name, Race, Health, AggressionLevel, Waaagh)
VALUES
	(13, 'Facetrashah', 'Orrok', 70, 60, 10),
	(14, 'Edkrakah', 'Orrok', 77, 71, 16),
	(15, 'Doomchoppah', 'Orrok', 74, 64, 24),
	(16, 'Balgrug', 'Orrok', 78, 68, 28),
	(17, 'Urkrakh', 'Orrok', 80, 70, 30);
SET IDENTITY_INSERT Monsters OFF;

-- Add Other Monsters
SET IDENTITY_INSERT Monsters ON;
INSERT INTO Monsters (Id, Name, Race, Health, AggressionLevel)
VALUES
	(18, 'Algrozan', 'Fallen', 200, 120),
	(19, 'Nozcar', 'Balrog', 175, 125),
	(20, 'Bren Medursk', 'Ghoul', 100, 100),
	(21, 'Naphos Macham', 'Ghoul', 100, 100),
	(22, 'Gerof Midz', 'Ghoul', 100, 100),
	(23, 'Bolg', 'Uruk', 25, 50),
	(24, 'Ashgarn', 'Uruk', 21, 48),
	(25, 'Tuhorn', 'Uruk', 24, 41),
	(26, 'Grisha', 'Uruk', 28, 44),
	(27, 'Shag', 'Uruk', 26, 45),
	(28, 'Snafu', 'Uruk', 27, 47),
	(29, 'Pushkrimp', 'Uruk', 20, 40),
	(30, 'Ur-Edin', 'Troll', 64, 54),
	(31, 'Az-Bror', 'Troll', 60, 57),
	(32, 'Ar-Lisu', 'Troll', 66, 52),
	(33, 'Az-Laar', 'Troll', 68, 51),
	(34, 'Zoruk', 'Ogre', 65, 53),
	(35, 'Nuderg', 'Ogre', 63, 59),
	(36, 'Bomikor', 'Ogre', 70, 60),
	(37, 'Amemaru', 'Oni', 35, 90);
SET IDENTITY_INSERT Monsters OFF;