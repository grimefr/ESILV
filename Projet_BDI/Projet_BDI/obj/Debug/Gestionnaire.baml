   M S B A M L   `   `   `  ����E  AProjet_BDI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Projet_BDI
Projet_BDI  5   	       Projet_BDI.Gestionnaire   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:Projet_BDI     5   	     � Title$  Gestionnaire��5   	   $��681.4��6   $
��1064��6-   .��5	      � $��0,0,0,1q�6   .+�5
   
   �� -   5        $��
CDRsemaine��5
      $
��Left=�6&   $��
39,371,0,0q�6A   $
��Wrap=�6U   $��CDR de la semaine : ��6i   $	��Top=�6�   $��Rockwell Nova Cond Light)�6�   �� 5   
   -   5        $��CDRor��5      $
��Left=�6!   $��
39,411,0,0q�6<   $
��Wrap=�6P   $��
CDR d'or :��6d   $	��Top=�6v   $��Rockwell Nova Cond Light)�6�   �� 5   
   $
��Left=�6   $��153,160,0,0q�6/   $
��Wrap=�6D   $(��"Top 5 Des recettes de la semaine :��6X   $	��Top=�6�   $��Rockwell Nova Cond Light)�6�   $��14(�6�   �� 5   
   -   5        $��ClientSuppr��5      $
��Left=�6%   $��19��6@   $��673,433,0,0q�6L    �� TextWrapping$
 Wrap=�6a   $�� ��6u   $	��Top=�6}   $	��108��6�   �� 5   
   -   5        $��RecetteSuppr��5      $
��Left=�6&   $��19��6A   $��673,475,0,0q�6M   $
 Wrap=�6b   $�� ��6v   $	��Top=�6~   $	��108��6�   �� 5   
   -   5        $��SupprimerClient��5      $��Supprimer client��6F   $
��Left=�6a   $��534,434,0,0q�6|   $	��Top=�6�   $	��116��6�   $��Rockwell Nova Cond Light)�6�   �� 5   
   -   5        $��SupprimerRecette��5      $��Supprimer Recette��6I   $
��Left=�6e   $��534,475,0,0q�6�   $	��Top=�6�   $	��116��6�   $��Rockwell Nova Cond Light)�6�   �� 5   
   -   5        $��ListePlatCDRor��5      $
��Left=�6*   $��
39,445,0,0q�6E   $
��Wrap=�6Y   $1��+Liste de ses 5 plats les plus commandés : ��6m   $	��Top=�6�   $��Rockwell Nova Cond Light)�6�   �� 5   
   -   5        $��Retour��5      $��Retour menu��64   $
��Left=�6J   $��475,548,0,0q�6e   $	��Top=�6z   $	��175��6�   $��69��6�   $��Rockwell Nova Cond Light)�6�   �� 5   
   -	   5        $��recette_list��5   -   $��33,183,549.6,310q�6   $��Rockwell Nova Cond Light)�6�   ��6A   �� *Plat+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=� �� View 5      �� 5      .P�5      �� $	��Nom��6%    �� Width$	 100��62    �� DisplayMemberBinding 6>   �� *NomR+$ TwoWay=��� 5      $
��Type��6%   $	 100��63    6?   �� *Type+$ TwoWay=��� 5      $
��Prix��6%   $ 50��63    6>   �� *Prix+$ TwoWay=��� 5      $��Renommé��6%   $ 70��66    6A   �� *	Renomme+$ TwoWay=��� 5      $��
Descriptif��6%   $	 150��69    6E   �� *
Descriptif+$ TwoWay=�5      5      �� 5    
   $
��Left=�6   $	��100��6+   $��
707,32,0,0q�68   $	��Top=�6L   $	��107��6d   $��logo.jpg��6p   �� 5!   
   $
��Left=�6   $��
253,64,0,0q�6/   $
��Wrap=�6C   $*��$Bienvenue dans l'onglet Gestionnaire��6W   $	��Top=�6�   $��Rockwell Nova Cond Light)�6�   $��24(�6�   �� 5"   
   -
   5        $��OuvriFichier��5"      $��Ouvrir le fichier��6A   $
��Left=�6]   $	��Top=�6x   $	��100��6�   $��806,452,0,0q�6�   $��Rockwell Nova Cond Light)�6�    J� RenderTransformOrigin$ 
0.54,-1.72&�6�   �� 5#   
   $
��Left=�6   $��808,146,0,0q�6/   $
��Wrap=�6D   $)��#Liste des commandes de la semaine :��6X   $	��Top=�6�   $��Rockwell Nova Cond Light)�6�   $��14(�6�   �� 5$   
   -   5         ��Name$ ListeProduitRapro��5$      $
��Left=�6�   $	��193��6�   $��529,183,0,0q�6�   $	��Top=�6�   $	��252��6�   $��Rockwell Nova Cond Light)�6  ��6.   �� *	Produit+$ TwoWay=�$ PropertyChanged=� 5%      �� 5&      .P�5'      �� $��	IDproduit��6%   $	 100��68    6D   �� *	IDproduit+$ TwoWay=��� 5(      $��Stock��6%   $ 60��64    6?   �� *Stock+$ TwoWay=��� 5)      $��	Stockmini��6%   $ 85��68    6C   �� *	Stockmini+$ TwoWay=�5*      5,      �� 5-   
   $
��Left=�6   $��529,160,0,0q�6/   $
��Wrap=�6D   $.��(Liste des produit à réaprovisionner : ��6X   $	��Top=�6�   $��Rockwell Nova Cond Light)�6�   $��14(�6�   �� 5.   
   -   5        	 ��Name$	 Reapprovisionnement��5.      $��Réapprovisioner��6P   $
��Left=�6j   $��534,393,0,0q�6�   $	��Top=�6�   $	��116��6�   $��Rockwell Nova Cond Light)�6�   �� 5/   
   -   5        $ ListeCommande��5/      $
��Left=�6�   $	��264��6�   $��806,183,0,0q�6�   $	��Top=�6�   $	��191��6�   $��Rockwell Nova Cond Light)�6  ��6*   �� *RecapCommande+$ TwoWay=�$ PropertyChanged=� 50      �� 51      .P�52      �� $	��Nom��6%   $	 180��62   54      56      �� 57   
   -   5        $��MiseAjourStock��57      $��Mise à jour stock��6D   $
��Left=�6`   $��673,393,0,0q�6{   $	��Top=�6�   $	��108��6�   $��Rockwell Nova Cond Light)�6�   �� 58   
   -   5        $ ListRecetteCDROr��58      $
��Left=�6}   $	��144��6�   $��
39,473,0,0q�6�   $	��Top=�6�   $	��255��6�   $��Rockwell Nova Cond Light)�6  ��6-   �� *Plat+$ TwoWay=�$ PropertyChanged=� 59      �� 5:      .P�5;      �� $	��Nom��6%   $	 150��62    6>   �� *NomR+$ TwoWay=��� 5<      $��Commandées��6%   $	 100��69    6E   �� *	Renomme+$ TwoWay=�5=      5?      5A      5B      