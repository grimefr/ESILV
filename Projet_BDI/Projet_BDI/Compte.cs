using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows;


namespace Projet_BDI
{
    public class Compte : INotifyPropertyChanged
    {


        string identifiant;
        public string motdepasse;
        public string nom;
        public string prenom;
        bool connecte;
        int pointCooking;


        public string Identifiant
        {
            get { return this.identifiant; }
            set { identifiant = value; OnPropertyChanged("identifiant"); }
        }
        public bool Connecte
        {
            get { return connecte; }
            set { connecte = value; OnPropertyChanged("connecte"); }
        }
        public int PointCooking
        {
            get { return this.pointCooking; }
            set
            {

                pointCooking = value; OnPropertyChanged("PointCooking");
            }
        }

        //utilisé pour avoir un affichage en continue sur le wpf
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        //constructeurs
        public Compte()
        {
            this.identifiant = "localhost";
            this.nom = "Visiteur";
            this.motdepasse = " ";
            this.prenom = " ";
            this.pointCooking = 0;
            this.connecte = false;
        }
        public Compte(string id, string mdp)
        {
            this.identifiant = id;
            this.pointCooking = GetDataInt("select soldeclient from client where IDclient like '"+ this.identifiant + "';");
            this.nom = getdata("select Nom from client where IDclient like '" + id + "';");
            this.motdepasse = mdp;
            this.prenom = getdata("select prenom from client where IDclient like '" + id + "';");
            this.connecte = true;
        }


        //3 fonction qui servent à prendre les donné de la bdd sous forme de string (getdata) ou sous forme e'int (GetDataInt); la fonction lirecommande sert à lire lune commande dans la bdd mais ne retourne rien
        public static void lirecommande(string commande)
        {
            string rep = "";
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=azerty91;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = commande;
            MySqlDataReader reader;
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string valueAsString = reader.GetValue(i).ToString();
                    currentRowAsString += valueAsString + ", ";
                }
                rep += "|" + currentRowAsString;
            }

            connection.Close();
           
        }
        public static string getdata(string commande)
        {
            string data = "";
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=cooking;UID=root;PASSWORD=azerty91;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = commande;

            MySqlDataReader reader;
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string valueAsString = reader.GetValue(i).ToString();
                    currentRowAsString += valueAsString + ",";
                    data += currentRowAsString;
                }
                
            }
            connection.Close();
            
            return data;
        }
        public static int GetDataInt(string commande)
        {
            string test = Compte.getdata(commande).Trim(' ', ',');
            return Convert.ToInt32(test);
        }

        /// <summary>
        /// Verifie que le string en entré ne comporte pas de ', car cela engeandrait une erreur dans la bdd 
        /// </summary>
        /// <param name="commande">texte à vérifier</param>
        /// <returns>retourne le texte modifié si modification il y a eu sinon renvoie le même string</returns>
        public static string Verifcommande(string commande)
        {
            
            return commande = commande.Replace('\'','.');
        }


        /// <summary>
        /// renvoie un bool si le compte existe et que le mdp correspond au compte
        /// </summary>
        /// <param name="id">identifiant</param>
        /// <param name="mdp">mot de passe</param>
        /// <returns>un bool, à valeur </returns>
        static public bool logcompte(string id, string mdp) 
        {
            bool exi = false;
            if (Inscription.compteexist(id))
            {
                string mdpnormal = getdata("select MDPcompte from client where IDclient like '" + id + "';");
                mdp = mdp + ",";//on ajoute un ", " car le sql nous renvoie des infos de ce type ce qui rend impossible la comapraison sans cet ajout sur le mot de base
                if (mdpnormal == mdp) { exi = true; }
            }
            return exi;
        }


       


        


    }
}
