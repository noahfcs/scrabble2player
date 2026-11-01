using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;

namespace Scrabble2Joueurs
{
    /// <summary>
    /// Classe d'accès aux données du dictionnaire.
    /// Elle interroge l'API REST APIDictionnaire pour récupérer les mots autorisés.
    /// </summary>
    internal class DictionnaireService
    {
        /// <summary>
        /// Récupère, via l'API, la liste des mots du dictionnaire
        /// comportant au maximum 7 lettres.
        /// </summary>
        /// <returns>Liste des mots autorisés (au plus 7 lettres)</returns>
        public static List<string> GetLesMots()
        {
            // liste qui contiendra les mots récupérés
            List<string> lesMots = new List<string>();

            // URL de la route de l'API : tous les mots de 1 à 7 lettres
            string url = "http://localhost:8000/api/mots?min=1&max=7";

            // création de la requête HTTP GET
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            try
            {
                // exécution de la requête et récupération de la réponse
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    // lecture du flux JSON renvoyé par l'API
                    string responseString =
                        new StreamReader(response.GetResponseStream()).ReadToEnd();

                    // analyse du JSON : { "total": ..., "mots": [ "AA", "ABC", ... ] }
                    JsonDocument doc = JsonDocument.Parse(responseString);
                    JsonElement root = doc.RootElement;
                    JsonElement motsElement = root.GetProperty("mots");

                    // parcours du tableau "mots" et ajout de chaque mot dans la liste
                    foreach (JsonElement unMot in motsElement.EnumerateArray())
                    {
                        lesMots.Add(unMot.GetString());
                    }
                }

                response.Close();
            }
            catch (Exception ex)
            {
                // en cas d'erreur (API non lancée par exemple), on prévient l'utilisateur
                System.Windows.MessageBox.Show("Erreur lors du chargement du dictionnaire : " + ex.Message);
            }

            return lesMots;
        }
    }
}
