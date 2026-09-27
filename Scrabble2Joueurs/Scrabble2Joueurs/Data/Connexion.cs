using MySql.Data.MySqlClient;

namespace Scrabble2Joueurs
{
    internal class Connexion
    {
        /// <summary>
        /// Méthode qui retourne une connexion à la base de données scrabble
        /// </summary>
        /// <returns>objet MySqlConnection</returns>
        public static MySqlConnection GetConnexion()
        {
            string chaineConnex = "server=localhost; user id=fuchs; password=VOTRE_MOT_DE_PASSE; database=scrabble";
            MySqlConnection connexion = new MySqlConnection(chaineConnex);
            return connexion;
        }
    }
}
