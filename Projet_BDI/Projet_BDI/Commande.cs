using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Projet_BDI
{
    public class Commande : INotifyPropertyChanged
    {

        string iDcommande;
        string dateC;
        int quantiteC;
        int prixC;
        string iDclient;
        string nomR;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public string IDcommande
        {
            get { return iDcommande; }
            set
            {
                iDcommande = value; OnPropertyChanged("IDcommande");
            }
        }
        public string DateC
        {
            get { return dateC; }
            set
            {
                dateC = value; OnPropertyChanged("DateC");
            }
        }
        public int PrixC
        {
            get { return prixC; }
            set
            {
                prixC = value; OnPropertyChanged("PrixC");
            }
        }
        public int QuantiteC
        {
            get { return quantiteC; }
            set
            {
                quantiteC = value; OnPropertyChanged("QuantiteC");
            }
        }
        public string IDclient
        {
            get { return iDclient; }
            set
            {
                iDclient = value; OnPropertyChanged("IDclient");
            }
        }
        public string NomR
        {
            get { return nomR; }
            set
            {
                nomR = value; OnPropertyChanged("NomR");
            }
        }

        public Commande()
        {

        }
        public Commande(string idcommande)
        {
            this.iDcommande = idcommande;
            this.dateC=Compte.getdata("select DateC from commande where IDcommande like '"+idcommande+"';").Trim(',');
            this.quantiteC= Compte.GetDataInt("select Quantitec from commande where IDcommande like '" + idcommande + "';");
            this.prixC= Compte.GetDataInt("select prixc from commande where IDcommande like '" + idcommande + "';");
            this.iDclient=Compte.getdata("select IDclient from commande where IDcommande like '" + idcommande + "';").Trim(',');
            this.nomR=Compte.getdata("select NomR from commande where IDcommande like '" + idcommande + "';").Trim(',');
        }

        /// <summary>
        /// retourne vrai si le plat a tester en entré est contenue dans une des commandes donné par la liste de commande.
        /// </summary>
        /// <param name="contenant"></param>
        /// <param name="atester"></param>
        /// <returns></returns>
        static public bool Contient(List<Commande> contenant, Plat atester)
        {
            bool rep = false;
            for (int i = 0; i < contenant.Count; i++)
            {
                if (atester.NomR == contenant[i].NomR)
                    rep = true;
            }
            return rep;
        }
        /// <summary>
        /// retourne la liste des commandes des derniers "duree" jour
        /// </summary>
        /// <param name="duree"></param>
        /// <returns></returns>
        static public List<Commande> commandeSurDuree(int duree)
        {
            List<Commande> temp = new List<Commande>();
            DateTime date = DateTime.Today;

            for (int i = 0; i < duree; i++)
            {

                string[] platCom = Compte.getdata("select IDcommande from commande where Datec like '" + date.ToString().Remove(10) + "' ;").Split(',');

                for (int j = 0; j < platCom.Length; j++)
                {
                    if (platCom[j] != "")
                    {
                        temp.Add(new Commande(platCom[j].Trim(',', ' ')));
                    }
                }
                date = date.AddDays(-1);
            }
            return temp;
        }
        /// <summary>
        /// retourne la liste des plats dans la liste des commande donné en entré 
        /// </summary>
        /// <param name="listecommande"></param>
        /// <returns></returns>
        static public List<Plat> ListPlatDansListCommande(List<Commande> listecommande)
        {
            List<Plat> listePlat = new List<Plat>();

            for(int i = 0; i< listecommande.Count; i++)
            {
                Plat plattemp = new Plat(listecommande[i].NomR, listecommande[i].QuantiteC);
                if (Plat.Contient(listePlat, plattemp))
                {
                    Plat.AjouteQuantitePlat(listePlat, plattemp) ;
                }
                else
                    listePlat.Add(plattemp);

            }

            return listePlat;
        }        
    }
}
