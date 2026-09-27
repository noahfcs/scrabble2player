CREATE DATABASE scrabble COLLATE 'utf8mb4_roman_ci';
GRANT ALL PRIVILEGES ON scrabble.*TO 'fuchs_td4'@'localhost';

USE scrabble;

CREATE TABLE Partie
(
    id INT AUTO_INCREMENT,
    date DATE NOT NULL,
    score_1 INT,
    score_2 INT,
    joueur_1 VARCHAR(20),
    joueur_2 VARCHAR(20),
    CONSTRAINT pk_id PRIMARY KEY(id)
);

INSERT INTO Partie (date,score_1,score_2,joueur_1,joueur_2) VALUES ("2026-05-13",43,21,"noah","hugo");
INSERT INTO Partie (date,score_1,score_2,joueur_1,joueur_2) VALUES ("2026-05-12",46,31,"noah","titou");
INSERT INTO Partie (date,score_1,score_2,joueur_1,joueur_2) VALUES ("2026-05-11",73,71,"noah","ismael");
