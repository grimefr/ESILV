   M S B A M L   `   `   `  ����E  AProjet_BDI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Projet_BDI
Projet_BDI  5   	       Projet_BDI.MenuPrincipal   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:Projet_BDI     5   	     � Title$  MenuPrincipal��5   	   $��628.12��6   $��940.792��6/   .��5	      � $��0,0,2.4,1.8q�6    J� RenderTransformOrigin$ 0.497,0.532&�6    .+�5
   
   �� -   5         ��Name$ Cooking_logo��5
      $
��Left=�6&   $	��120��6A   $��794,10,-30,0q�6N   $	��Top=�6d   $	��168��6|   $��logo.jpg��6�   �� 5   
   -   5        $��liste_panier��5   S   $��611,219,52,213.6q�6   $��Rockwell Nova Cond Light)�6-   ��6g   �� *panier+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=� �� View 5      �� 5      .P�5      �� $	��Nom��6%    �� Width$	 180��62    �� DisplayMemberBinding 6>   �� *NomR+$ TwoWay=��� 5      $
��Prix��6%   $ 50��63    6>   �� *Prix+$ TwoWay=�5      5      �� 5   
   $
��Left=�6   $��611,198,0,0q�6/   $
��Wrap=�6D   $��Votre pannier :��6X   $	��Top=�6o   $��Rockwell Nova Cond Light)�6�   �� 5   
   -   5        $��	commander��5      $��	Commander��64   $
��Left=�6H   $	��Top=�6c   $��90��6{   $��611,397,0,0q�6�   $��Rockwell Nova Cond Light)�6�   �� 5   
   $
��Left=�6   $��725,395,0,0q�6/   $
��Wrap=�6D   $��Prix total :��6X   $	��Top=�6l   $��Rockwell Nova Cond Light)�6�   �� 5   
   -   5        $��
plats_list��5   S   $��35,219,335,213.6q�6   $��Rockwell Nova Cond Light)�6-   ��6e   �� *plats+$ TwoWay=�$ PropertyChanged=� 5      �� 5      .P�5      �� $	��Nom��6%   $	 100��62    6>   �� *NomR+$ TwoWay=��� 5      $
��Type��6%   $	 100��63    6?   �� *Type+$ TwoWay=��� 5      $
��Prix��6%   $ 50��63    6>   �� *Prix+$ TwoWay=��� 5       $��Renommé��6%   $	 100��66    6B   �� *	Renomme+$ TwoWay=��� 5!      $��
Descriptif��6%   $	 230��69    6E   �� *
Descriptif+$ TwoWay=�5#      5&      �� 5'   
   $
��Left=�6   $��
35,198,0,0q�6/   $
��Wrap=�6C   $��Plats disponibles :��6W   $	��Top=�6r   $	��126��6�   $��Rockwell Nova Cond Light)�6�   �� 5(   
   -   5        $��Ajouter_panier��5(      $��Ajouter au panier��6>   $
��Left=�6Z   $��
35,393,0,0q�6u   $	��Top=�6�   $	��126��6�   $��43��6�   $��Rockwell Nova Cond Light)�6�   �� 5)   
   -   5        $��Supprimer du panier��5)      $
��Left=�6/   $��449,393,0,0q�6J   $	��Top=�6_   $	��148��6w   $��43��6�   $��Rockwell Nova Cond Light)�6�   �� 5*   
   -   5        $��prixdupanier��5*      $
��Left=�6)   $��810,395,0,0q�6D   $
��Wrap=�6Y   $
��prix��6m   $	��Top=�6y   $��40��6�   $��Rockwell Nova Cond Light)�6�   �� 5,   
   $
��Left=�6   $��	35,60,0,0q�6/   $
��Wrap=�6B   $U��OComment commander un de nos plats ? Rien de plus simple, suivez ces 3 étapes. ��6V   $	��Top=�6�   $��24(�6�   $	��732��6�   $��Rockwell Nova Cond Light)�6�   $��70��6  �� 5-   
   $
��Left=�6   $��
78,157,0,0q�6/   $
��Wrap=�6C   $	��Top=�6W   $��Rockwell Nova Cond Light)�6o   �� 6�   $�1 - Selectionner ��6�   �� 6�   $	�le ��6�   �� 6�   $�plat ��6�   6�   �� 5.   
   $
��Left=�6   $��339,157,0,0q�6/   $
��Wrap=�6D   $	��Top=�6X   $��Rockwell Nova Cond Light)�6p   �� 6�   $�2 ��6�   �� 6�   $	�- A��6�   �� 6�   $�jouter��6�   �� 6�   $	� le��6�   �� 6�   $�
 au panier��6�   6�   �� 5/   
   $
��Left=�6   $��611,157,0,0q�6/   $
��Wrap=�6D   $	��Top=�6X   $	��250��6p   $��Rockwell Nova Cond Light)�6|   �� 6�   $�3 - Commander��6�   �� 6�   $� ��6�   �� 6�   $�tout ce que vous voulez !��6�   6�   �� 50   
   -   5        $��
Idconnecte��50      $
��Left=�6'   $��	10,10,0,0q�6B   $
��Wrap=�6U   $
��id :��6i   $	��Top=�6u   $��Rockwell Nova Cond Light)�6�   �� 51   
   -	   5        $��
solde_cook��51      $
��Left=�6'   $��720,423,0,0q�6B   $
��Wrap=�6W   $��	pointcook��6k   $	��Top=�6|   $	��160��6�   $��Rockwell Nova Cond Light)�6�   �� 52   
   -
   5        $��Rechargement en cook��52      $
��Left=�60   $��611,456,0,0q�6K   $	��Top=�6`   $	��156��6x   $��23��6�   $��Rockwell Nova Cond Light)�6�   �� 53   
   -   5        $��Vider le panier��53      $
��Left=�6+   $��799,460,0,0q�6F   $	��Top=�6[   $��97��6s   $��Rockwell Nova Cond Light)�6�   �� 54   
   $
��Left=�6   $��611,423,0,0q�6/   $
��Wrap=�6D   $��Solde cook :��6X   $	��Top=�6l   $	��109��6�   $��Rockwell Nova Cond Light)�6�   �� 55   
   $
��Left=�6   $��
49,526,0,0q�6/   $
��Wrap=�6C   $P��JVous pouvez également créer des recettes et être rémunéré pour ça !��6W   $	��Top=�6�   $��18(�6�   $��Rockwell Nova Cond Light)�6�   $	��781��6�   �� 56   
   -   5        $��Créateur de recette��56   '   $
��Left=�6E   $��704,519,0,0q�6`   $	��Top=�6u   $	��157��6�   $��42��6�   $��14(�6�   $��Rockwell Nova Cond Light)�6�   �� 57   
   -   5        $��Retour accueil��57   &   $
��Left=�6?   $��
35,441,0,0q�6Z   $	��Top=�6n   $	��126��6�   $��Rockwell Nova Cond Light)�6�   $��38��6�   5:      5;      