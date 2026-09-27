using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Scrabble2Joueurs
{
    internal class PartieDao
    {
        /// <summary>
        /// Méthode qui interroge la table Partie de la base de données scrabble et retourne une liste de Partie
        /// </summary>
        /// <returns>Liste d'objets Partie</returns>
        public static List<Partie> GetLesParties()
        {
            // déclaration et instanciation d'une liste de Partie
            List<Partie> lesParties = new List<Partie>();

            // récupération de la connexion et ouverture
            MySqlConnection connexion = Connexion.GetConnexion();
            connexion.Open();

            // instanciation d'un objet MySqlCommand pour exécuter la requête SQL
            MySqlCommand cmdSelect = new MySqlCommand();
            cmdSelect.CommandText = "SELECT date, joueur_1, score_1, joueur_2, score_2 FROM Partie";
            cmdSelect.Connection = connexion;
            MySqlDataReader dr = cmdSelect.ExecuteReader();

            // lecture des lignes retournées par la requête
            while (dr.Read())
            {
                DateTime date = DateTime.Parse(dr.GetValue(0).ToString());
                string joueur1 = dr.GetValue(1).ToString();
                int score1 = int.Parse(dr.GetValue(2).ToString());
                string joueur2 = dr.GetValue(3).ToString();
                int score2 = int.Parse(dr.GetValue(4).ToString());

                Partie unePartie = new Partie(date, joueur1, score1, joueur2, score2);
                lesParties.Add(unePartie);
            }
            dr.Close();
            connexion.Close();
            return lesParties;
        }

        /// <summary>
        /// ajoute une partie dans la table Partie
        /// </summary>
        /// <param name="unePartie">partie à enregistrer</param>
        public static void AjouterPartie(Partie unePartie)
        {
            // récupération de la connexion et ouverture
            MySqlConnection connexion = Connexion.GetConnexion();
            connexion.Open();

            // instanciation d'un objet MySqlCommand pour exécuter la requête SQL
            MySqlCommand cmdInsert = new MySqlCommand();
            cmdInsert.CommandText = "INSERT INTO Partie (date, joueur_1, score_1, joueur_2, score_2) "
                                  + "VALUES (@date, @joueur1, @score1, @joueur2, @score2)";
            cmdInsert.Connection = connexion;

            // on remplace chaque @paramètre par sa valeur
            cmdInsert.Parameters.AddWithValue("@date", unePartie.Date);
            cmdInsert.Parameters.AddWithValue("@joueur1", unePartie.Joueur1);
            cmdInsert.Parameters.AddWithValue("@score1", unePartie.Score1);
            cmdInsert.Parameters.AddWithValue("@joueur2", unePartie.Joueur2);
            cmdInsert.Parameters.AddWithValue("@score2", unePartie.Score2);

            // exécution de la requête INSERT
            cmdInsert.ExecuteNonQuery();

            // fermeture de la connexion à la base de données
            connexion.Close();
        }
    }
}
