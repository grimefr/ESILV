using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.ObjectModel;
using Renci.SshNet.Messages;


namespace Projet_BDI
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class Page_Accueil : Window
    {
        Compte compte_utilise = new Compte("localhost", "localhost");
        public Page_Accueil()
        {
           
            InitializeComponent();
        }

        private void S_inscrire(object Connexion, RoutedEventArgs e)
        {
            Inscription w3 = new Inscription();
            w3.Show();
        }

        private void Se_Connecter(object Connexion, RoutedEventArgs e)
        {
            
            if (Compte.logcompte(Compte.Verifcommande(identifiant.Text), Compte.Verifcommande(motdepasse.Password)))
            {
                Compte test = new Compte(identifiant.Text, motdepasse.Password);
                compte_utilise = test;


                compte_utilise.Identifiant = identifiant.Text;
                Continuer(test);

            }
            else
            {

                MessageBox.Show("L'identifiant et/ou mot de passe inccorect, veuillez recommencer");

            }
        }

        private void Continuer(Compte cpt)
        {


            MessageBox.Show("Bienvenue sur Cooking " + cpt.nom.Trim(',',' ').ToUpper() + " !");
            MenuPrincipal w2 = new MenuPrincipal(cpt);
            w2.Show();
            this.Close();
        }
        private void ModeEssaie(object sender, RoutedEventArgs e)
        {

            Compte essaie = new Compte();
            Continuer(essaie);
            
        }

        private void TestMode(object sender, RoutedEventArgs e)
        {
            Fenetre_test w2 = new Fenetre_test();
            w2.Show();   

        }

        private void Gestionnaire_Click(object sender, RoutedEventArgs e)
        {
            Gestionnaire w9 = new Gestionnaire();
            w9.Show();
            this.Close();
        }
    }
}
