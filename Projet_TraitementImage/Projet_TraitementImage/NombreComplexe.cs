using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_TraitementImage
{
    class NombreComplexe
    {
        //attributs
        double reel;
        double im;

        public NombreComplexe(double reel, double im)
        {
            this.reel = reel;
            this.im = im;
        }



        public double ModuleComplexe()
        {
            return Math.Sqrt((reel * reel) + (im * im));
        }

        public void carre() //(a+ib)^2 = (a^2-b^2)+ i(2ab)
        {
            double var = (2 * reel * im);
            reel = (reel * reel - im * im);
            im = var;
            //on calcule la partie imaginaire et réelle d'un complexe
        }


        public void Somme(NombreComplexe z)
        {
            reel = reel + z.reel;
            im = im + z.im;
        }
    }
}
