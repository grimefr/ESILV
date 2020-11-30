using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using static System.Math;

namespace Projet_TraitementImage
{
    class MyImage
    {
        //Constante utilisées de la classe MyImage
        static int limiteHeader = 14;
        static int limiteHeaderInfo = 54;

        static byte bitFormatFichier = 0;
        static byte bitTailleImage = 2;
        static byte bitTailleOffSet = 10;
        static byte bitLargeur = 18;
        static byte bitHauteur = 22;
        static byte bitProfondeurCouleur = 28;
        static byte bitTailleImageRAW = 34;

        //Attributs et propriétés de la classe

        string TypeDeFichier;
        public string GetTypeDeFichier
        {
            get { return this.TypeDeFichier; }
        }
        int TailleFichier;
        public int GetTailleFichier
        {
            get { return this.TailleFichier; }
        }
        int TailleOffset;
        public int GetTailleOffset
        {
            get { return this.TailleOffset; }
        }
        int TailleHeaderInfo;
        public int GetTailleHeaderInfo
        {
            get { return this.TailleHeaderInfo; }
        }
        int Largeur;
        public int GetLargeur
        {
            get { return this.Largeur; }
        }
        int Hauteur;
        public int GetHauteur
        {
            get { return this.Hauteur; }
        }
        int NbBits;
        public int GetNbBits
        {
            get { return this.NbBits; }
        }
        int TailleImage;
        public int GetTailleImage
        {
            get { return this.TailleImage; }
        }
        Pixel[,] Image;
        public Pixel[,] GetImage
        {
            get { return this.Image; }
        }

        /// <summary>
        /// Constructeur qui lit un fichier BMP et qui le transforme en instance de la classe MyImage.
        /// </summary>
        /// <param name="NomDuFichier">nom du fichier à la terminaison BMP</param>
        public MyImage(string NomDuFichier)
        {
            byte[] myfile = File.ReadAllBytes(NomDuFichier);

            //Initialisation du type de fichier
            byte[] nom = new byte[2] { myfile[0], myfile[1] };
            char[] asciiChar = Encoding.ASCII.GetChars(nom);
            this.TypeDeFichier = "" + asciiChar[0] + asciiChar[1];

            //Initialisation de la taille de l'image
            this.TailleFichier = Convertir_Endian_To_Int(TabDansTab(myfile, bitTailleImage, (byte)(bitTailleImage + 3)));

            //Initialisation de la taille du OffSet
            this.TailleOffset = Convertir_Endian_To_Int(TabDansTab(myfile, bitTailleOffSet, (byte)(bitTailleOffSet + 3)));

            //Initialisation de la taille du Header de l'info
            this.TailleHeaderInfo = Convertir_Endian_To_Int(TabDansTab(myfile, (byte)limiteHeader, (byte)(limiteHeader + 3)));

            //Initialisation de la largeur de l'image
            this.Largeur = Convertir_Endian_To_Int(TabDansTab(myfile, bitLargeur, (byte)(bitLargeur + 3)));

            //Initialisation de la hauteur de l'image
            this.Hauteur = Convertir_Endian_To_Int(TabDansTab(myfile, bitHauteur, (byte)(bitHauteur + 3)));

            //Initialisation du nb de bits (profondeur des couleurs)
            this.NbBits = Convertir_Endian_To_Int(TabDansTab(myfile, bitProfondeurCouleur, (byte)(bitProfondeurCouleur + 1)));

            //initialisation de la taille de l'image
            this.TailleImage = Convertir_Endian_To_Int(TabDansTab(myfile, (byte)bitTailleImageRAW, (byte)(bitTailleImageRAW + 3)));
            //initialisation matrice pixel
            Pixel[,] matPix = new Pixel[Hauteur, Largeur];

            int x = 0;
            int y = 0;

            for (int i = limiteHeaderInfo; i < (myfile.Length - 2) && (x < Hauteur); i += 3)
            {
                if (y > Largeur - 1)
                {
                    y = 0;
                    x++;
                }

                matPix[x, y] = new Pixel(myfile[i], myfile[i + 1], myfile[i + 2]);
                y++;
            }
            this.Image = matPix;
        }
        /// <summary>
        /// Constructeur qui crée une image blanche en prenant en entrée une hauteur et une largeur
        /// </summary>
        /// <param name="Hauteur">Hauteur de l'image</param>
        /// <param name="Largeur">Largeur de l'image</param>
        public MyImage(int Hauteur, int Largeur)
        {
            this.TypeDeFichier = "BM";
            this.Largeur = Largeur;
            this.Hauteur = Hauteur;
            this.TailleOffset = 54;
            this.TailleHeaderInfo = 40;
            this.NbBits = 24;
            this.TailleImage = Largeur * Hauteur;
            Pixel[,] Image1 = new Pixel[Hauteur, Largeur];
            //on initialise une image toute blanche
            for (int i = 0; i < this.Hauteur; i++)
            {
                for (int j = 0; j < this.Largeur; j++)
                {
                    Image1[i, j] = new Pixel(255, 255, 255);
                }
            }
            this.TailleFichier = this.TailleOffset + (Largeur * Hauteur) * 3;
            this.Image = Image1;
        }
        /// <summary>
        /// Constructeur qui prend en paramètre une image et qui crée une autre image identique
        /// </summary>
        /// <param name="ImageADulpiquer"></param>
        public MyImage(MyImage ImageADulpiquer)
        {
            this.TypeDeFichier = ImageADulpiquer.GetTypeDeFichier;
            this.TailleFichier = ImageADulpiquer.GetTailleFichier;
            this.TailleOffset = ImageADulpiquer.GetTailleOffset;
            this.TailleHeaderInfo = ImageADulpiquer.GetTailleHeaderInfo;
            this.Largeur = ImageADulpiquer.GetLargeur;
            this.Hauteur = ImageADulpiquer.GetHauteur;
            this.NbBits = ImageADulpiquer.GetNbBits;
            this.TailleImage = ImageADulpiquer.GetTailleImage;
            this.Image = ImageADulpiquer.GetImage;
        }

        /// <summary>
        /// Fonction appliquée à une image et qui crée un fichier bmp
        /// </summary>
        /// <param name="Namefile">Nom du fichier créé</param>
        public void From_Image_To_File(string Namefile)
        {

            byte[] file = new byte[TailleFichier];
            int compteur = limiteHeaderInfo;

            for (int i = 0; i < Hauteur; i++)
            {
                for (int j = 0; j < Largeur; j++)
                {
                    file[compteur] = Image[i, j].B;
                    file[compteur + 1] = Image[i, j].G;
                    file[compteur + 2] = Image[i, j].R;
                    compteur += 3;
                }
            }

            //initialisation du Header et HeaderInfo
            for (int i = 0; i < limiteHeaderInfo; i++)
            {
                file[i] = 0;
                if (i == 26)
                    file[i] = 1;
            }

            //TypeDeFichier
            char[] tabChar = new char[2];
            tabChar[0] = this.TypeDeFichier[0];
            tabChar[1] = this.TypeDeFichier[1];
            TabDansTab(file, Encoding.ASCII.GetBytes(tabChar), bitFormatFichier);

            //Taille du HeaderInfo
            TabDansTab(file, Convertir_Int_To_Endian(this.TailleHeaderInfo, 4), (byte)limiteHeader);

            //TailleFichier
            TabDansTab(file, Convertir_Int_To_Endian(this.TailleFichier, 4), bitTailleImage);

            //TailleOffset
            TabDansTab(file, Convertir_Int_To_Endian(this.TailleOffset, 4), bitTailleOffSet);

            //Largeur
            TabDansTab(file, Convertir_Int_To_Endian(this.Largeur, 4), bitLargeur);

            //Hauteur
            TabDansTab(file, Convertir_Int_To_Endian(this.Hauteur, 4), bitHauteur);

            //NbBits
            TabDansTab(file, Convertir_Int_To_Endian(this.NbBits, 2), bitProfondeurCouleur);

            //taille image en raw
            TabDansTab(file, Convertir_Int_To_Endian(this.TailleImage, 4), bitTailleImageRAW);

            //On crée le fichier associé au tableau de byte
            File.WriteAllBytes(Namefile, file);
        }

        /// <summary>
        /// Méthode qui retourne un tableau extrait d'un autre tableau)
        /// </summary>
        /// <param name="tab">tableau à extraire</param>
        /// <param name="indexDébut">Index dans le grand tableau pour l'extraction de debut d'extraction</param>
        /// <param name="indexFin">Index dans le grand tableau pour l'extraction de fin d'extraction</param>
        /// <returns></returns>
        public byte[] TabDansTab(byte[] tab, byte indexDébut, byte indexFin)
        {
            byte[] rep = new byte[indexFin - indexDébut + 1];

            for (int i = 0; i < indexFin - indexDébut + 1; i++)
            {
                rep[i] = tab[indexDébut + i];
            }

            return rep;
        }
        /// <summary>
        /// Fonction qui modifie un tableau en y ajoutant un autre tableau
        /// </summary>
        /// <param name="tabAModifier">Tableau au modifier</param>
        /// <param name="tabAAjouter">Tableau à ajouter</param>
        /// <param name="indexDebut">Index à partir duquel on ajoute le tableau</param>
        public void TabDansTab(byte[] tabAModifier, byte[] tabAAjouter, byte indexDebut)
        {
            for (int i = 0; i < tabAAjouter.Length; i++)
            {
                tabAModifier[i + indexDebut] = tabAAjouter[i];
            }
        }

        /// <summary>
        /// Methode qui convertit une séquence d'octets en entier
        /// </summary>
        /// <param name="test">tableau de byte</param>
        /// <returns></returns>
        public int Convertir_Endian_To_Int(byte[] test)
        {
            int rep = 0;
            for (int i = 0; i < test.Length; i++)
            {
                rep += test[i] * (int)Math.Pow(256, i);
            }
            return rep;
        }
        /// <summary>
        /// Méthode qui convertit un entier en une séquence d'octets
        /// </summary>
        /// <param name="val">entier</param>
        /// <param name="tailletab">taille du tableau de byte</param>
        /// <returns></returns>
        public byte[] Convertir_Int_To_Endian(int val, int tailletab)
        {
            byte[] tab = new byte[tailletab];
            for (int i = 0; i < tailletab; i++)
            {
                tab[i] = (byte)(val >> i * 8);
            }
            return tab;
        }
        /// <summary>
        /// Méthode qui retourne les propriétés de l'Offset d'une image
        /// </summary>
        /// <returns></returns>
        public string toStringOffSet()
        {
            return "TypeDeFichier : " + this.TypeDeFichier + "\nTailleFichier : " + this.TailleFichier + "\nTailleOffset : " + this.TailleOffset + "\nLargeur : " + this.Largeur + "\nHauteur : " + this.Hauteur + "\nNbBits : " + this.NbBits;
        }
        /// <summary>
        /// Méthode qui retourne la composition des pixels d'une image
        /// </summary>
        /// <returns></returns>
        public string toStringImage()
        {
            string image = "L'image est constitué des pixel suivant en BGR :\n\n ";

            for (int i = 0; i < this.Hauteur - 1; i++)
            {
                for (int j = 0; j < this.Largeur - 1; j++)
                {
                    image = image + this.Image[i, j].toString();
                }
                image += " \n\n";
            }
            return image;
        }
        /// <summary>
        /// Applique une modification des bytes en gris
        /// </summary>
        public void NuanceDeGris()
        {
            for (int i = 0; i < this.Image.GetLength(0); i++)
            {
                for (int j = 0; j < this.Image.GetLength(1); j++)
                {
                    this.Image[i, j].Gris();
                }
            }
        }
        /// <summary>
        /// Fonction qui permet de choisir la nuance à appliquer à une photo
        /// </summary>

        /// <summary>
        /// Fonction qui modifie une image en rotation peu importe le degré
        /// </summary>
        /// <param name="degre">entier degré</param>
        public void Rotation(int degre)
        {
            degre = degre % 360;
            double rad = PI / 180 * degre;

            //Redéfinition des Hauteur et Largeur, de multiple de 4 car sinon le fichier ne sera pas lu.
            int HauteurNouvelleMatrice = (int)(this.Hauteur * Abs(Cos(rad)) + this.Largeur * Abs(Cos(PI / 2 - rad)));
            while (HauteurNouvelleMatrice % 4 != 0)
            {
                HauteurNouvelleMatrice++;
            }

            int LargeurNouvelleMatrice = (int)(this.Largeur * Abs(Sin(PI / 2 - rad)) + this.Hauteur * Abs(Sin(rad)));
            while (LargeurNouvelleMatrice % 4 != 0)
            {
                LargeurNouvelleMatrice++;
            }
            Pixel[,] MatriceDePassage = new Pixel[HauteurNouvelleMatrice, LargeurNouvelleMatrice];

            int debI = 0;
            int debJ = 0;
            if (degre == 90 || degre == 180 || degre == 270)
            {
                if (degre == 90)
                {
                    for (int i = 0; i < this.Hauteur; i++)
                    {
                        for (int j = 0; j < this.Largeur; j++)
                        {
                            MatriceDePassage[this.Largeur - j - 1, i] = this.Image[i, j];
                        }
                    }

                }
                if (degre == 180)
                {
                    for (int i = 0; i < this.Hauteur; i++)
                    {
                        for (int j = 0; j < this.Largeur; j++)
                        {
                            MatriceDePassage[this.Hauteur - i - 1, this.Largeur - 1 - j] = this.Image[i, j];
                        }
                    }

                }
                if (degre == 270)
                {
                    for (int i = 0; i < this.Hauteur; i++)
                    {
                        for (int j = 0; j < this.Largeur; j++)
                        {
                            MatriceDePassage[j, this.Hauteur - 1 - i] = this.Image[i, j];
                        }
                    }


                }
            }
            else
            {
                if (degre > 0 && degre < 90)
                {
                    debI = (int)((this.Largeur - 1) * Abs((Cos(PI / 2 - rad))));
                    debJ = 0;
                }
                if (degre > 90 && degre < 180)
                {
                    debI = HauteurNouvelleMatrice - 1;
                    debJ = (int)((this.Largeur - 1) * Cos(PI - rad));
                }
                if (degre > 180 && degre < 270)
                {
                    debI = (int)((this.Hauteur - 1) * Abs(Cos(rad - PI)));
                    debJ = LargeurNouvelleMatrice - 1;
                }
                if (degre > 270)
                {
                    debI = 0;
                    debJ = (int)((this.Hauteur - 1) * Abs(Cos(rad - (3 * PI / 2))));
                }



                //on crée une image toute blanche puis pour avoir les bords blancs.


                for (int i = 0; i < this.Hauteur; i++)
                {
                    for (int j = 0; j < this.Largeur; j++)
                    {
                        MatriceDePassage[debI + (int)(Cos(rad) * i - Sin(rad) * j), debJ + (int)(Sin(rad) * i + Cos(rad) * j)] = this.Image[i, j];

                    }
                }
                
                for (int i = 0; i < HauteurNouvelleMatrice; i++)
                {
                    for (int j = 0; j < LargeurNouvelleMatrice; j++)
                    {
                        if (MatriceDePassage[i, j] == null)
                        {
                            //if ((i > (Cos(rad) / Sin(rad)) * j) && (i < (Cos(rad) / Sin(rad)) * j + (HauteurNouvelleMatrice - 1) / Cos(90 - rad)) && (i > (-Cos(90 - rad) / Sin(90 - rad)) * j) && (i < (-Cos(90 - rad) / Sin(90 - rad)) * j + (LargeurNouvelleMatrice - 1) / Cos(rad)))
                            //{
                            //    MatriceDePassage[i, j] = this.Image[(int)(Cos(rad) * i + Sin(rad) * j), (int)(-Sin(rad) * i + Cos(rad) * j)];
                            //}
                            //else
                            //{
                                MatriceDePassage[i, j] = new Pixel(255, 255, 255);
                            //}
                        }
                    }
                }
                

            }


            this.Largeur = LargeurNouvelleMatrice;
            this.Hauteur = HauteurNouvelleMatrice;
            this.Image = MatriceDePassage;
            this.TailleImage = this.Largeur * this.Hauteur * 3;
            this.TailleFichier = this.TailleImage + this.TailleOffset;
        }
        /// <summary>
        /// Cette fonction traite une image en faisant un effet miroir en l'inversant 
        /// </summary>
        public void EffetMiroir()
        {
            Pixel[,] MatricePassage = new Pixel[this.Hauteur, this.Largeur];
            for (int i = 0; i < this.Hauteur; i++)
            {
                for (int j = 0; j < this.Largeur; j++)
                {
                    MatricePassage[i, j] = this.Image[i, this.Largeur - 1 - j];
                }
            }
            this.Image = MatricePassage;
        }
        /// <summary>
        /// Rétrécit une image selon un coefficient prit en entrée, cette fonction va rétrecir l'image par rapport au coefficient en paramètre
        /// </summary>
        /// <param name="coef">coefficient de rétrécissement par lequel la largeur et la hauteur de base sera divisée</param>
        public void Retrecir(int coef)
        {

            int NouvelleLargeur = this.Largeur / coef;
            while (NouvelleLargeur % 4 != 0)
            {
                NouvelleLargeur++;
            }
            int NouvelleHauteur = this.Hauteur / coef;
            while (NouvelleHauteur % 4 != 0)
            {
                NouvelleHauteur++;
            }

            Pixel[,] mat = new Pixel[NouvelleHauteur, NouvelleLargeur];

            for (int i = 0; i < this.Hauteur / coef; i++)
            {
                for (int j = 0; j < this.Largeur / coef; j++)
                {
                    mat[i, j] = this.Image[i * coef, j * coef];
                }
            }
            for (int i = 0; i < NouvelleHauteur; i++)
            {
                for (int j = 0; j < NouvelleLargeur; j++)
                {
                    if (mat[i, j] == null)
                        mat[i, j] = new Pixel(255, 255, 255);
                }
            }



            this.Hauteur = NouvelleHauteur;
            this.Largeur = NouvelleLargeur;
            this.Image = mat;
            this.TailleImage = this.Hauteur * this.Largeur * 3;
            this.TailleFichier = this.TailleImage + this.TailleOffset;
        }
        /// <summary>
        /// Agrandit une image selon un coefficient prit en entrée, cette fonction va agrandir une image par rapport au coefficient prit en entrée
        /// </summary>
        /// <param name="coef">coefficient d'agrandissement par lequel la largeur et la hauteur de base sera multipliée</param>
        public void Agrandir(int coef)
        {

            int nouvelleHauteur = (this.Hauteur) * coef;
            //while (nouvelleHauteur % 4 != 0)
            //{
            //    nouvelleHauteur++;
            //}

            int nouvelleLargeur = (this.Largeur) * coef;
            //while (nouvelleLargeur % 4 != 0)
            //{
            //    nouvelleLargeur++;
            //}


            Pixel[,] MatFinale = new Pixel[this.Hauteur * coef, this.Largeur * coef];

            for (int i = 0; i < this.Hauteur * coef; i = i + coef)
            {
                for (int j = 0; j < this.Largeur * coef; j = j + coef)
                {
                    for (int k = 0; k < coef; k++)
                    {
                        for (int m = 0; m < coef; m++)
                        {
                            MatFinale[i + k, j + m] = this.Image[i / coef, j / coef];
                        }
                    }
                }
            }

            //il faut repasser pour combler les vides
            for (int i = 0; i < nouvelleHauteur; i++)
            {
                for (int j = 0; j < nouvelleLargeur; j++)
                {
                    if (MatFinale[i, j] == null)
                    {
                        MatFinale[i, j] = new Pixel(255, 255, 255);
                    }
                }
            }



            this.Largeur = nouvelleLargeur;
            this.Hauteur = nouvelleHauteur;
            this.TailleImage = nouvelleLargeur * nouvelleHauteur * 3;
            this.TailleFichier = this.TailleImage + TailleOffset;
            this.Image = MatFinale;
        }
        /// <summary>
        /// Fonction qui applique un filtre de convolution au choix à une image
        /// /// </summary>
        /// <param name="index">Entier qui définit le filtre de convolution à appliquer à l'image</param>
        public void Convol(int index)
        {
            //initialisation de la matrice de convolution selon l'effet demandé.
            int[,] matConv = new int[3, 3];
            int coef = 1;
            switch (index)
            {
                //lissage
                case 1:
                    matConv = new int[3, 3] { { 1, 1, 1 }, { 1, 0, 1 }, { 1, 1, 1 } };
                    coef = 9;
                    this.Image = MatriceConvolution(matConv, coef);
                    break;
                //détection de contour
                case 2:
                    matConv = new int[3, 3] { { -1, -1, -1 }, { -1, 8, -1 }, { -1, -1, -1 } };
                    coef = 9;
                    this.Image = MatriceConvolution(matConv, coef);
                    break;
                //renforcement des bords
                case 3:
                    matConv = new int[3, 3] { { 0, 0, 0 }, { -1, 1, 0 }, { 0, 0, 0 } };
                    coef = 1;
                    this.Image = MatriceConvolution(matConv, coef);
                    break;
                // flou de gauss
                case 4:
                    matConv = new int[3, 3] { { 1, 2, 1 }, { 2, 4, 2 }, { 1, 2, 1 } };
                    coef = 16;
                    this.Image = MatriceConvolution(matConv, coef);
                    break;
                //repoussage
                case 5:
                    matConv = new int[3, 3] { { -2, 1, 0 }, { -1, 1, 1 }, { 0, 1, 2 } };
                    coef = 3;
                    this.Image = MatriceConvolution(matConv, coef);

                    break;
                //contraste
                case 6:
                    matConv = new int[3, 3] { { 0, -1, 0 }, { -1, 5, -1 }, { 0, -1, 0 } };

                    this.Image = MatriceConvolution(matConv, coef);
                    break;
            }
        }

        public Pixel[,] MatriceConvolution(int[,] matriceConvolution, int coefDeDivision)
        {
            Pixel[,] matPix = new Pixel[this.Hauteur, this.Largeur];

            for (int i = 0; i < this.Hauteur; i++)
            {
                for (int j = 0; j < this.Largeur; j++)
                {
                    if (i >= (matriceConvolution.GetLength(0) / 2) && i < this.Hauteur - (matriceConvolution.GetLength(0) / 2) && j >= (matriceConvolution.GetLength(1) / 2) && j < this.Largeur - (matriceConvolution.GetLength(0) / 2))
                    {
                        int valR = 0;
                        int valG = 0;
                        int valB = 0;

                        for (int m = 0; m < matriceConvolution.GetLength(0); m++)
                        {
                            for (int k = 0; k < matriceConvolution.GetLength(1); k++)
                            {
                                valR += (this.Image[(i - 1) + k, (j - 1) + m].R * matriceConvolution[k, m]);
                                valG += (this.Image[(i - 1) + k, (j - 1) + m].G * matriceConvolution[k, m]);
                                valB += (this.Image[(i - 1) + k, (j - 1) + m].B * matriceConvolution[k, m]);
                            }
                        }
                        valB = (byte)(valB / coefDeDivision);
                        valG = (byte)(valG / coefDeDivision);
                        valR = (byte)(valR / coefDeDivision);

                        if (valB < 0)
                            valB = 0;
                        if (valR < 0)
                            valR = 0;
                        if (valG < 0)
                            valG = 0;
                        if (valB > 255)
                            valB = 255;
                        if (valG > 255)
                            valG = 255;
                        if (valR > 255)
                            valR = 255;

                        matPix[i, j] = new Pixel((byte)(valB), (byte)(valG), (byte)(valR));
                    }
                    else
                    {
                        matPix[i, j] = this.Image[i, j];
                    }
                }
            }
            return matPix;
        }
        public void NoirEtBlanc()
        {
            for (int i = 0; i < this.Hauteur; i++)
            {
                for (int j = 0; j < this.Largeur; j++)
                {
                    if (this.Image[i, j].B > 80 && this.Image[i, j].G > 80 && this.Image[i, j].R > 80)
                        this.Image[i, j].Blanc();
                    else
                        this.Image[i, j].Noir();

                }
            }


        }

        

        /// <summary>
        /// Fonction static qui créé une image sur laquelle apparaît une fractale from scratch 
        /// </summary>
        public static void Fractale()
        {
            MyImage ImageFract = new MyImage(1600, 1600);
            for (int i = 0; i < ImageFract.Hauteur; i++)
            {
                for (int j = 0; j < ImageFract.Largeur; j++)
                {
                    int cpt = 0;
                    NombreComplexe z = new NombreComplexe(0, 0);
                    double reel = (double)(i - (ImageFract.Hauteur / 2)) / (double)(ImageFract.Hauteur / 4);
                    double im = (double)(j - (ImageFract.Largeur / 2)) / (double)(ImageFract.Largeur / 4);
                    NombreComplexe z1 = new NombreComplexe(reel, im);
                    do
                    {
                        cpt++;
                        z.carre();
                        z.Somme(z1);
                        if (z.ModuleComplexe() > 2)
                        {
                            break;

                        }

                    } while (cpt < 1000);

                    if (cpt == 1000) ImageFract.Image[i, j] = new Pixel(0, 0, 0);
                    else ImageFract.Image[i, j] = new Pixel((byte)((cpt) % 256), (byte)((cpt) % 256), (byte)(cpt % 256));
                }
            }
            ImageFract.From_Image_To_File("Fractal.bmp");
            Process.Start("Fractal.bmp");

        }
        /// <summary>
        /// Fonction qui crée 3 histogrammes de répartition de l'intensité de 3 bytes de référence pour une image
        /// </summary>
        public void Histogramme()
        {

            MyImage histo = new MyImage(3 * 100 * 4 + 40 * 4, 255 * 4 + 40 * 2);



            byte[,] compteur = new byte[3, 256];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 256; j++)
                {
                    compteur[i, j] = 0;
                }
            }
            for (int i = 0; i < this.Hauteur; i++)
            {
                for (int j = 0; j < this.Largeur; j++)
                {
                    compteur[0, this.Image[i, j].B]++;
                    compteur[1, this.Image[i, j].G]++;
                    compteur[2, this.Image[i, j].R]++;
                }
            }


            for (int i = 0; i < 3; i++)
            {
                int departI = 0;
                Pixel Couleur = new Pixel(0, 0, 0);

                //Pixel 
                switch (i)
                {
                    case 0:
                        Couleur.Bleu();
                        break;
                    case 1:
                        Couleur.Vert();
                        departI = 100 * 4 + 40 * 2;
                        break;
                    case 2:
                        Couleur.Rouge();
                        departI = 100 * 8 + 40 * 3;
                        break;
                }

                histo.Trait(40 + departI, 40, 2, 255 * 4, Couleur);
                histo.Trait(40 + departI, 40, 100 * 4, 2, Couleur);

                for (int j = 0; j < 256 * 4; j += 8)
                {
                    histo.Trait(38 + departI, 40 + j, 5, 2, Couleur);
                }
                for (int j = 0; j < 100 * 4; j += 10)
                {
                    histo.Trait(40 + departI + j, 38, 2, 5, Couleur);
                }

                for (int j = 0; j < 128; j++)
                {
                    histo.Trait(40 + departI, 40 + j * 8, (compteur[i, j * 2] + compteur[i, (j * 2) + 1]) / 2, 2, Couleur);
                }

            }

            histo.From_Image_To_File("histogramme.bmp");
            Process.Start("histogramme.bmp");
        }
        /// <summary>
        /// Créé des rectangles remplis par une couleur en entrée (pour l'histogramme)
        /// </summary>
        /// <param name="x">Coordonée en x du point de départ du trait</param>
        /// <param name="y">Coordonnée en y du point de départ du trait</param>
        /// <param name="TailleI">longueur du rectangle </param>
        /// <param name="TailleJ">largeur du rectangle</param>
        /// <param name="couleur">couleur du remplissage du rectangle</param>
        public void Trait(int x, int y, int TailleI, int TailleJ, Pixel couleur)
        {
            for (int i = 0; i < TailleI; i++)
            {
                for (int j = 0; j < TailleJ; j++)
                {
                    this.Image[x + i, y + j] = couleur;

                }

            }
        }
        /// <summary>
        /// Fonction que l'on applique à image1 qui prend en entrée image2 qui est à cacher. Cette fonction
        /// prend également en compte le fait qu'une image à cacher puisse être éventuellement plus grande
        /// que l'autre. On décale les quatre premiers bits vers la droite puis on les redécale à gauche dans 
        /// le but de balayer les 4 bits a gauche puis on les remplace par les 4 premiers bits de l'image à coder
        /// </summary>
        /// <param name="NomImageACacher">Image à cacher</param>
        public void CacherImage(string NomImageACacher)
        {
            MyImage ImageACacher = new MyImage(NomImageACacher);
            int k = 2;
            while (this.TailleImage < ImageACacher.GetTailleImage)
            {
                ImageACacher.Retrecir(k);
                k++;
            }
            for (int i = 0; i < ImageACacher.GetHauteur; i++)
            {
                for (int j = 0; j < ImageACacher.GetLargeur; j++)
                {
                    this.Image[i, j].R = (byte)(this.Image[i, j].R >> 4 << 4 | (ImageACacher.GetImage[i, j].R) >> 4);
                    this.Image[i, j].G = (byte)(this.Image[i, j].G >> 4 << 4 | (ImageACacher.GetImage[i, j].G) >> 4);
                    this.Image[i, j].B = (byte)(this.Image[i, j].B >> 4 << 4 | (ImageACacher.GetImage[i, j].B) >> 4);
                }
            }
        }
        /// <summary>
        /// Fonction qui sert à décoder une image codée dans laquelle on retrouve toutes les images codées à l'intérieur d'une image
        /// En redécalant les 4 derniers bits vers la gauche ce qui permet de voir l'image codée
        /// </summary>
        public void DecoderImage()
        {
            for (int i = 0; i < this.Hauteur; i++)
            {
                for (int j = 0; j < this.Largeur; j++)
                {
                    this.Image[i, j].R = (byte)(this.Image[i, j].R << 4);
                    this.Image[i, j].G = (byte)(this.Image[i, j].G << 4);
                    this.Image[i, j].B = (byte)(this.Image[i, j].B << 4);
                }
            }
        }


        //Créations 

        /// <summary>
        /// fonction qui modifie les bytes d'une image de manière à la rendre négative
        /// </summary>
        public void Negatif()
        {
            for (int i = 0; i < this.Hauteur; i++)
            {
                for (int j = 0; j < this.Largeur; j++)
                {
                    this.Image[i, j].R = (byte)(255 - this.Image[i, j].R);
                    this.Image[i, j].G = (byte)(255 - this.Image[i, j].G);
                    this.Image[i, j].B = (byte)(255 - this.Image[i, j].B);
                }
            }
        }
        /// <summary>
        /// Fonction qui modifie les bytes d'une image de manière à la rendre Sépia
        /// </summary>
        public void Sepia()
        {
            int nivgris = 0;
            for (int i = 0; i < this.Hauteur; i++)
            {
                for (int j = 0; j < this.Largeur; j++)
                {
                    nivgris = (this.Image[i, j].R + this.Image[i, j].G + this.Image[i, j].B) / 3;
                    this.Image[i, j].R = (byte)((nivgris + 162) / 2);
                    this.Image[i, j].G = (byte)((nivgris + 128) / 2);
                    this.Image[i, j].B = (byte)((nivgris + 101) / 2);
                    //R=162, V=128 et B=101
                }
            }
        }


        /// <summary>
        /// Fonction qui modifie une image de manière à lui donner un effet 3D
        /// </summary>
        public void RendreImage3D(int coef)
        {
            Pixel[,] Image3D = new Pixel[this.Hauteur, this.Largeur];

            for (int i = 0; i < this.Hauteur; i++)
            {
                for (int j = 0; j < this.Largeur; j++)
                {
                    Image3D[i, j] = new Pixel(Pixel.PixelNoir());
                    if (i > coef && i <= this.Hauteur - coef && j > coef && j <= this.Largeur - coef)
                    {
                        Image3D[i, j - coef].R = this.Image[i, j].R;
                        Image3D[i - coef, j].G = this.Image[i, j].G;
                        Image3D[i - coef, j - coef].B = this.Image[i, j].B;
                    }
                }
            }
            this.Image = Image3D;

        }

        /// <summary>
        /// Fonction qui modifie légèrement une image de manière à lui donner un effet bande dessinée
        /// </summary>
        public void BD()
        {
            Pixel[,] dupli = new Pixel[this.Hauteur, this.Largeur];

            for (int i = 0; i < this.Hauteur; i++)
            {
                for (int j = 0; j < this.Largeur; j++)
                {
                    dupli[i, j] = new Pixel(this.Image[i, j]);

                }
            }

            for (int i = 0; i < this.Hauteur - 1; i++)
            {
                for (int j = 0; j < this.Largeur - 1; j++)
                {

                    this.Image[i, j].B = (byte)(this.Image[i, j].B >> 4 << 4);
                    this.Image[i, j].G = (byte)(this.Image[i, j].G >> 4 << 4);
                    this.Image[i, j].R = (byte)(this.Image[i, j].R >> 4 << 4);


                    if (((this.Image[i, j].B + this.Image[i + 1, j + 1].B / 2 >= this.Image[i, j].B - 10) && ((this.Image[i, j].G + this.Image[i + 1, j + 1].G) / 2 >= this.Image[i, j].G - 10) && ((this.Image[i, j].R + this.Image[i + 1, j + 1].R) / 2 >= this.Image[i, j].R - 10))) ;
                    {
                        dupli[i + 1, j + 1].B = (byte)(this.Image[i, j].B + 1);
                        dupli[i + 1, j + 1].G = (byte)(this.Image[i, j].G + 1);
                        dupli[i + 1, j + 1].R = (byte)(this.Image[i, j].R + 1);
                    }
                }
            }

            this.Image = dupli;

        }

        /// <summary>
        /// fonction qui rogne une image à laquelle est appliquée la fonction
        /// </summary>
        /// <param name="XDeb">Marge à rogner en partant de la gauche vers la droite </param>
        /// <param name="XFin">Marge à rogner en partant de la droite vers la gauche</param>
        /// <param name="YDeb">Marge à rogner en partant du haut vers le bas</param>
        /// <param name="YFin">Marge à rogner en partant du bas vers le haut</param>
        public void Rogner(int XDeb, int XFin, int YDeb, int YFin)
        {
            int nouvelleHauteur = this.Hauteur - XDeb - XFin;
            while (nouvelleHauteur % 4 != 0) nouvelleHauteur++;
            int nouvelleLargeur = this.Largeur - YDeb - YFin;
            while (nouvelleLargeur % 4 != 0) nouvelleLargeur++;

            Pixel[,] matfinale = new Pixel[nouvelleHauteur, nouvelleLargeur];
            for (int i = 0; i < nouvelleHauteur; i++)
            {
                for (int j = 0; j < nouvelleLargeur; j++)
                {
                    matfinale[i, j] = this.Image[i + XDeb, j + YDeb];
                }
            }
            this.Largeur = nouvelleLargeur;
            this.Hauteur = nouvelleHauteur;
            this.TailleImage = nouvelleHauteur * nouvelleLargeur * 3;
            this.TailleFichier = this.TailleImage + TailleOffset;
            this.Image = matfinale;

        }

        public void Sauvegarde(MyImage imageSauvegarde)
        {
            this.TypeDeFichier = imageSauvegarde.GetTypeDeFichier;
            this.TailleFichier = imageSauvegarde.GetTailleFichier;
            this.TailleOffset = imageSauvegarde.GetTailleOffset;
            this.TailleHeaderInfo = imageSauvegarde.GetTailleHeaderInfo;
            this.Largeur = imageSauvegarde.GetLargeur;
            this.Hauteur = imageSauvegarde.GetHauteur;
            this.NbBits = imageSauvegarde.GetNbBits;
            this.TailleImage = imageSauvegarde.GetTailleImage;
            this.Image = imageSauvegarde.GetImage;
        }

        //rotation est comprise également car elle faisait intervenir beaucoup de calculs, ce qui la rendait très intéressante à faire.

        //PISTE POR L'ANNEE PROCHAINE


        /// <summary>
        /// Remplace la couleur des pixel prit en entrée
        /// </summary>
        /// <param name="marge">c'est un nombre que l'on prend </param>
        /// <param name="Aremplacer"></param>
        /// <param name="Remplassage"></param>
        public void RemplacerCouleur(int marge, Pixel Aremplacer, Pixel Remplassage)
        {

            int diffB = 0;
            int diffG = 0;
            int diffR = 0;
            for (int i = 0; i < this.Hauteur; i++)
            {
                for (int j = 0; j < this.Largeur; j++)
                {
                    if ((Aremplacer.B <= this.Image[i, j].B - marge) && (Aremplacer.G <= this.Image[i, j].G - marge) && (Aremplacer.R <= this.Image[i, j].R - marge))
                    {


                        diffB = this.Image[i, j].B - Aremplacer.B;
                        diffG = this.Image[i, j].G - Aremplacer.G;
                        diffR = this.Image[i, j].R - Aremplacer.R;

                        this.Image[i, j].B = (byte)((Remplassage.B - diffB) / 2);
                        this.Image[i, j].G = (byte)((Remplassage.G - diffG) / 2);
                        this.Image[i, j].R = (byte)((Remplassage.R - diffR) / 2);
                    }

                }
            }
        }

        /// <summary>
        /// Effectuer des changements de luminosité de certains pixels peut être très intéressant. En effet les pixels sont définis en fonction de RGB mais également en fonction d'un autre modèle : teinte, saturation et valeur(intensité lumineuse)
        /// ici éroder sert à augmenter les zones les plus éclairées
        /// Malheureusement nous n'avons découvert cette possibilité que très tardivement donc nous n'avons pas pu nous y pencher plus que le code qui suit.
        /// </summary>
        public void Eroder()
        {
            int valLum = 255;
            byte[] index = new byte[2];
            Pixel[,] MatPassage = new Pixel[this.Hauteur, this.Largeur];

            for (int i = 0; i < this.Hauteur; i++)
            {
                for (int j = 0; j < this.Largeur; j++)
                {
                    if (i == 0 || j == 0 || i == this.Hauteur - 1 || j == this.Largeur - 1)
                    {
                        MatPassage[i, j] = this.Image[i, j];
                    }
                    else
                    {
                        valLum = 255;
                        index[0] = 0;
                        index[1] = 0;
                        for (int m = 0; m < 3; m++)
                        {
                            for (int k = 0; k < 3; k++)
                            {
                                if (valLum != Min(valLum, this.Image[i - 1 + k, j - 1 + m].Luminosite()))
                                {
                                    valLum = Min(valLum, this.Image[i - 1 + k, j - 1 + m].Luminosite());
                                    index[0] = (byte)(i - 1 + k);
                                    index[1] = (byte)(j - 1 + m);
                                }
                            }
                        }
                        MatPassage[i, j] = new Pixel((byte)(this.Image[i, j].B + this.Image[index[0], index[1]].Luminosite()), (byte)(this.Image[i, j].G + this.Image[index[0], index[1]].Luminosite()), (byte)(this.Image[i, j].R + this.Image[index[0], index[1]].Luminosite()));

                    }




                }
            }
            this.Image = MatPassage;
        }
    }
}