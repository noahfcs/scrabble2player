using System.Collections.Generic;
using System.Windows;

namespace Scrabble2Joueurs
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Dictionnaire des mots autorisés, accessible depuis toute l'application
        /// </summary>
        public static Dictionnaire LeDictionnaire = new Dictionnaire();

        /// <summary>
        /// Méthode appelée au démarrage de l'application :
        /// charge la liste des mots du dictionnaire via l'API.
        /// </summary>
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // récupération des mots via l'API (logique d'accès aux données)
            List<string> mots = DictionnaireService.GetLesMots();

            // stockage des mots dans le dictionnaire métier
            LeDictionnaire.SetLesMots(mots);
        }
    }
}
