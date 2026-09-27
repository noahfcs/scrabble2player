using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Scrabble2Joueurs
{
    public partial class MainWindow : Window
    {
        Joueur J1;
        Joueur J2;
        int tourActuel;
        List<char> sac;
        List<char> chevaletJ1;
        List<char> chevaletJ2;
        List<int> selection;
        Button[] tuiles;
        static Random rand = new Random();

        public MainWindow()
        {
            InitializeComponent();
            EcranDepart.Visibility = Visibility.Visible;
            EcranJeu.Visibility = Visibility.Hidden;
            EcranFin.Visibility = Visibility.Collapsed;
            ChargerHistorique();
        }

        /// <summary>
        /// Méthode qui charge le DataGrid avec la liste des parties de la base de données
        /// </summary>
        private void ChargerHistorique()
        {
            // liaison (binding) du DataGrid à la liste des parties
            dgHistorique.ItemsSource = PartieDao.GetLesParties();
        }

        private List<char> CreerSac()
        {
            List<char> unSac = new List<char>();
            string lettres = "AAAAAAAAABBCCDDDEEEEEEEEEEEEEFFGGGHIIIIIIIIJKLLLLLMMNNNNNOOOOOOOPPRRRRRRSSSSSSTTTTTTUUUUUVVWXYYZ";
            foreach (char l in lettres)
                unSac.Add(l);
            return unSac;
        }

        private char Piocher()
        {
            int i = rand.Next(0, sac.Count);
            char lettre = sac[i];
            sac.RemoveAt(i);
            return lettre;
        }

        private void CompléterChevalet(List<char> chevalet)
        {
            while (chevalet.Count < 7 && sac.Count > 0)
                chevalet.Add(Piocher());
        }

        private void AfficherChevalet()
        {
            List<char> chevalet = (tourActuel == 1) ? chevaletJ1 : chevaletJ2;
            selection.Clear();

            for (int i = 0; i < 7; i++)
            {
                if (i < chevalet.Count)
                {
                    tuiles[i].Content = chevalet[i].ToString();
                    tuiles[i].Visibility = Visibility.Visible;
                    tuiles[i].Background = new SolidColorBrush(Colors.Goldenrod);
                }
                else
                {
                    tuiles[i].Visibility = Visibility.Hidden;
                }
            }
        }

        private void MettreAJourAffichage()
        {
            txtScoreJ1.Text = J1.GetTotalPoints().ToString();
            txtScoreJ2.Text = J2.GetTotalPoints().ToString();
            txtSac.Text = sac.Count + " lettres";

            string nomActuel = (tourActuel == 1) ? J1.GetNom() : J2.GetNom();
            txtTourActuel.Text = "Tour de : " + nomActuel;
            txtRackLabel.Text = "CHEVALET DE " + nomActuel.ToUpper();
        }

        private void PasserTour()
        {
            tourActuel = (tourActuel == 1) ? 2 : 1;
            AfficherChevalet();
            MettreAJourAffichage();
        }

        private void BtnCommencer_Click(object sender, RoutedEventArgs e)
        {
            if (txtNomJ1.Text == "" || txtNomJ2.Text == "")
            {
                erreur.Visibility = Visibility.Visible;
                erreur.Text = "Vous devez remplir le nom des joueurs.";
                return;
            }
            if (txtNomJ1.Text == txtNomJ2.Text)
            {
                erreur.Visibility = Visibility.Visible;
                erreur.Text = "Les noms des 2 joueurs doivent être différents.";
                return;
            }

            J1 = new Joueur(txtNomJ1.Text);
            J2 = new Joueur(txtNomJ2.Text);
            sac = CreerSac();
            chevaletJ1 = new List<char>();
            chevaletJ2 = new List<char>();
            CompléterChevalet(chevaletJ1);
            CompléterChevalet(chevaletJ2);
            tourActuel = 1;
            selection = new List<int>();
            tuiles = new Button[] { Tuile1, Tuile2, Tuile3, Tuile4, Tuile5, Tuile6, Tuile7 };

            txtNomJ1Display.Text = J1.GetNom();
            txtNomJ2Display.Text = J2.GetNom();
            lblHistoJ1.Text = "Mots de " + J1.GetNom();
            lblHistoJ2.Text = "Mots de " + J2.GetNom();
            HistoriqueJ1.Children.Clear();
            HistoriqueJ2.Children.Clear();

            AfficherChevalet();
            MettreAJourAffichage();

            erreur.Visibility = Visibility.Hidden;
            EcranDepart.Visibility = Visibility.Hidden;
            EcranJeu.Visibility = Visibility.Visible;
        }

        private void Tuile_Click(object sender, RoutedEventArgs e)
        {
            Button tuile = (Button)sender;
            int index = Array.IndexOf(tuiles, tuile);

            if (selection.Contains(index))
            {
                selection.Remove(index);
                tuile.Background = new SolidColorBrush(Colors.Goldenrod);
            }
            else
            {
                selection.Add(index);
                tuile.Background = new SolidColorBrush(Colors.Tomato);
            }
        }

        private void BtnValider_Click(object sender, RoutedEventArgs e)
        {
            if (selection.Count == 0)
                return;

            List<char> chevalet = (tourActuel == 1) ? chevaletJ1 : chevaletJ2;
            string mot = "";
            foreach (int i in selection)
                mot += chevalet[i];

            int points = Utilitaire.PointsMot(mot);

            if (tourActuel == 1)
                J1.AjouterMot(mot);
            else
                J2.AjouterMot(mot);

            List<int> indices = new List<int>(selection);
            indices.Sort();
            for (int i = indices.Count - 1; i >= 0; i--)
                chevalet.RemoveAt(indices[i]);

            CompléterChevalet(chevalet);

            TextBlock tb = new TextBlock();
            tb.Text = mot + "  (" + points + " pts)";
            tb.Margin = new Thickness(0, 2, 0, 2);

            if (tourActuel == 1)
                HistoriqueJ1.Children.Add(tb);
            else
                HistoriqueJ2.Children.Add(tb);

            if (J1.GetNbMots() + J2.GetNbMots() >= 10 || (sac.Count == 0 && chevaletJ1.Count == 0 && chevaletJ2.Count == 0))
            {
                AfficherEcranFin();
                return;
            }

            PasserTour();
        }

        private void BtnPasser_Click(object sender, RoutedEventArgs e)
        {
            PasserTour();
        }

        private void BtnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            AfficherChevalet();
        }

        private void AfficherEcranFin()
        {
            txtFinNomJ1.Text = J1.GetNom();
            txtFinScoreJ1.Text = J1.GetTotalPoints() + " pts";
            txtFinNomJ2.Text = J2.GetNom();
            txtFinScoreJ2.Text = J2.GetTotalPoints() + " pts";

            if (J1.GetTotalPoints() > J2.GetTotalPoints())
                txtGagnant.Text = J1.GetNom() + " remporte la partie !";
            else if (J2.GetTotalPoints() > J1.GetTotalPoints())
                txtGagnant.Text = J2.GetNom() + " remporte la partie !";
            else
                txtGagnant.Text = "Égalité !";

            // enregistrement de la partie dans la base de données
            Partie laPartie = new Partie(DateTime.Now, J1.GetNom(), J1.GetTotalPoints(), J2.GetNom(), J2.GetTotalPoints());
            PartieDao.AjouterPartie(laPartie);

            EcranFin.Visibility = Visibility.Visible;
        }

        private void BtnNouvellePartie_Click(object sender, RoutedEventArgs e)
        {
            EcranFin.Visibility = Visibility.Collapsed;
            EcranJeu.Visibility = Visibility.Hidden;
            txtNomJ1.Text = "Joueur 1";
            txtNomJ2.Text = "Joueur 2";
            erreur.Visibility = Visibility.Hidden;
            ChargerHistorique();
            EcranDepart.Visibility = Visibility.Visible;
        }
    }
}