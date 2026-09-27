using System.Collections.Generic;

namespace Scrabble2Joueurs
{
    /// <summary>
    /// Classe Joueur
    /// </summary>
    public class Joueur
    {
        #region attributs
        private string nom;
        private List<string> lesMots;
        private int totalPoints;
        #endregion

        #region constructeur
        /// <summary>
        /// Constructeur de la classe Joueur
        /// </summary>
        /// <param name="unNom">nom du joueur</param>
        public Joueur(string unNom)
        {
            this.nom = unNom;
            this.lesMots = new List<string>();
            this.totalPoints = 0;
        }
        #endregion

        #region méthodes
        /// <summary>
        /// Retourne le nom du joueur
        /// </summary>
        /// <returns>nom du joueur</returns>
        public string GetNom()
        {
            return this.nom;
        }

        /// <summary>
        /// Méthode qui permet d'ajouter un mot à la liste des mots du joueur
        /// et qui actualise le nombre total de points du joueur
        /// </summary>
        /// <param name="unMot">mot proposé par le joueur</param>
        public void AjouterMot(string unMot)
        {
            this.lesMots.Add(unMot);
            this.totalPoints += Utilitaire.PointsMot(unMot);
        }

        /// <summary>
        /// Retourne le nombre total de points du joueur
        /// </summary>
        /// <returns>nombre total de points du joueur</returns>
        public int GetTotalPoints()
        {
            return this.totalPoints;
        }

        /// <summary>
        /// Retourne le nombre de mots du joueur
        /// </summary>
        /// <returns>nombre de mots du joueur</returns>
        public int GetNbMots()
        {
            return this.lesMots.Count;
        }

        /// <summary>
        /// Retourne la liste des mots du joueur
        /// </summary>
        /// <returns>liste des mots du joueur</returns>
        public List<string> GetLesMots()
        {
            return this.lesMots;
        }

        /// <summary>
        /// Retourne le mot qui a rapporté le plus grand nombre de points
        /// parmi les mots proposés par le joueur
        /// </summary>
        /// <returns>mot qui a rapporté le plus grand nombre de points</returns>
        public string MeilleurMot()
        {
            if (this.lesMots.Count == 0)
                return "";

            string meilleurMot = this.lesMots[0];
            int maxPoints = Utilitaire.PointsMot(meilleurMot);

            foreach (string mot in this.lesMots)
            {
                int points = Utilitaire.PointsMot(mot);
                if (points > maxPoints)
                {
                    maxPoints = points;
                    meilleurMot = mot;
                }
            }
            return meilleurMot;
        }
        #endregion
    }
}
