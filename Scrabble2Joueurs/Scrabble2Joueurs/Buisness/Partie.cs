using System;

namespace Scrabble2Joueurs
{
    /// <summary>
    /// Classe Partie
    /// </summary>
    public class Partie
    {
        #region attributs
        private DateTime date;
        private string joueur1;
        private int score1;
        private string joueur2;
        private int score2;
        #endregion

        #region proprietes
        public DateTime Date { get => date; }
        public string Joueur1 { get => joueur1; }
        public int Score1 { get => score1; }
        public string Joueur2 { get => joueur2; }
        public int Score2 { get => score2; }
        #endregion

        #region constructeur
        /// <summary>
        /// Constructeur de la classe Partie
        /// </summary>
        /// <param name="uneDate">date de la partie</param>
        /// <param name="unJoueur1">nom du joueur 1</param>
        /// <param name="unScore1">score du joueur 1</param>
        /// <param name="unJoueur2">nom du joueur 2</param>
        /// <param name="unScore2">score du joueur 2</param>
        public Partie(DateTime uneDate, string unJoueur1, int unScore1, string unJoueur2, int unScore2)
        {
            this.date = uneDate;
            this.joueur1 = unJoueur1;
            this.score1 = unScore1;
            this.joueur2 = unJoueur2;
            this.score2 = unScore2;
        }
        #endregion
    }
}
