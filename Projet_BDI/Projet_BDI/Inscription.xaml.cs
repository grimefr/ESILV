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
using System.Windows.Shapes;

namespace Projet_BDI
{
    /// <summary>
    /// Logique d'interaction pour Inscription.xaml
    /// </summary>
    public partial class Inscription : Window
    {
        public Inscription()
        {
            InitializeComponent();
        }
        public static bool compteexist(string id) // FONCTION POUR SAVOIR SI L ID EST BIEN DANS LA DATABASE via ID
        {
            bool presence = false;
            string mot = "select IDclient from client where IDclient like '" + Compte.Verifcommande( id )+ "';";

            string prescemot = Compte.getdata(mot);

            id = id + ","; //on ajoute un ", " car le sql nous renvoie des infos de ce type ce qui rend impossible la comapraison sans cet ajout sur le mot de base


            if (prescemot == id) { presence = true; }
            return presence;
        }

        /// <summary>
        /// fonction pour créer un compte dans la bdd
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="prenom"></param>
        /// <param name="id"></param>
        /// <param name="mdp"></param>
        static void creacompte(string nom, string prenom, string id, string mdp)
        {

            string requetecrea = "INSERT INTO `cooking`.`client` (`IDclient`,`Nom`,`prenom`,`MDPcompte`,`soldeclient`) VALUES('" + Compte.Verifcommande(id) + "','" + Compte.Verifcommande(nom) + "','" + Compte.Verifcommande(prenom) + "','" + Compte.Verifcommande(mdp) + "','0');";

            Compte.lirecommande(requetecrea);

        }

        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!compteexist(Identifiant.Text))
            {
                if (MotDePasse.Text == MotDePasseVerif.Text)
                {
                    creacompte(Nom.Text, Prenom.Text, Identifiant.Text, MotDePasse.Text);
                    MessageBox.Show("Félicitation, votre compte viens d'être créer.\n\nNous allons vous rediriger vers la page d'accueil afin que vous puissiez vous connecter.");
                    this.Close();
                }
                else
                    MessageBox.Show("Les mots de passes entré ne correspondent pas, veuillez réessayer.");
            }
            else
            {
                MessageBox.Show("Cettes identifiant est déjà pris, veuillez en choisir un autre.");
            }
        }
    }
}
