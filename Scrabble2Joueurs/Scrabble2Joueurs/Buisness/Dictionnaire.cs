using System.Collections.Generic;

namespace Scrabble2Joueurs
{
    /// <summary>
    /// Classe métier Dictionnaire : contient la liste des mots autorisés
    /// au Scrabble et permet de vérifier si un mot en fait partie.
    /// </summary>
    public class Dictionnaire
    {
        #region attributs
        private List<string> lesMots;
        #endregion

        #region constructeur
        /// <summary>
        /// Constructeur : initialise une liste de mots vide
        /// </summary>
        public Dictionnaire()
        {
            this.lesMots = new List<string>();
        }
        #endregion

        #region méthodes
        /// <summary>
        /// Enregistre la liste des mots du dictionnaire
        /// </summary>
        /// <param name="desMots">liste des mots autorisés</param>
        public void SetLesMots(List<string> desMots)
        {
            this.lesMots = desMots;
        }

        /// <summary>
        /// Indique si un mot figure dans le dictionnaire
        /// </summary>
        /// <param name="unMot">mot proposé par un joueur</param>
        /// <returns>true si le mot existe, false sinon</returns>
        public bool ExisteMot(string unMot)
        {
            // les mots du dictionnaire sont stockés en majuscules
            return this.lesMots.Contains(unMot.ToUpper());
        }
        #endregion
    }
}
