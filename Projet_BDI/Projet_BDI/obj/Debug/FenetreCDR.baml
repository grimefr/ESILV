   M S B A M L   `   `   `  ����E  AProjet_BDI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Projet_BDI
Projet_BDI  5   	       Projet_BDI.FenetreCDR   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:Projet_BDI     5   	     � Title$  
FenetreCDR��5   	   $��659.6��6   $��1022.8��6+   .��5	      � $��0,0,3.6,-1.2q�6   .+�5
   
   �� -   5        $��liste_aliment��5
   /   $��56,183,811.4,203.8q�6   $��Rockwell Nova Cond Light)�6�   ��6D   �� *	aliment+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=� �� View 5      �� 5      .P�5      �� $��	IDproduit��6%    �� Width$	 150��68    �� DisplayMemberBinding 6D   �� *	IDproduit+$ TwoWay=�5      5      �� 5   
   $
��Left=�6   $��	64,57,0,0q�6/   $
��Wrap=�6B   $_��YSuivez les différentes étapes pour créer votre recette, vous allez voir c'est facile !��6V   $	��Top=�6�   $��Rockwell Nova Cond Light)�6�   $��24(�6�   $	��648��6   �� 5   
   $
��Left=�6   $	��122��6+   $��
796,10,0,0q�68   $	��Top=�6L   $	��140��6d   $��logo.jpg��6p   �� 5   
   $
��Left=�6   $��
56,158,0,0q�6/   $
��Wrap=�6C   $ ��1 - Ajoutez les aliments !��6W   $	��Top=�6y   $��Rockwell Nova Cond Light)�6�   �� 5   
   -   5        $��Ajouter ��5   )   $
��Left=�6<   $��
92,442,0,0q�6W   $	��Top=�6k   $��75��6�   $��Rockwell Nova Cond Light)�6�   �� 5   
   -   5        $��SupprimerRecette��5      $��	Supprimer��6)   $
��Left=�6=   $��707,442,0,0q�6X   $	��Top=�6m   $��94��6�   $��Rockwell Nova Cond Light)�6�   �� 5   
   -   5        $��
NomRecette��5      $
��Left=�6$   $��23��6?   $��274,209,0,0q�6K    �� TextWrapping$
 Wrap=�6`   $�� ��6t   $	��Top=�6|   $��90��6�   �� 5   
   -   5        $��DescriptionProduit��5      $
��Left=�6-   $��56��6H   $��237,303,0,0q�6T   $
 Wrap=�6i   $�� ��6}   $	��Top=�6�   $	��313��6�   �� 5   
   -   5        $��Créer la recette��5   &   $
��Left=�6A   $��529,384,0,0q�6\   $	��Top=�6q   $	��115��6�   $��43��6�   $��Rockwell Nova Cond Light)�6�   �� 5   
   -   5         ��Name$ liste_aliment_recette��5   /   $��707,183,65.4,203.8q�6   $��Rockwell Nova Cond Light)�6�   ��6N   �� *alimentrecette+$ TwoWay=�$ PropertyChanged=� 5      �� 5      .P�5      �� $	��Nom��6%   $	 130��62    6>   �� *	IDproduit+$ TwoWay=��� 5      $��	Quantité��6%   $ 60��67    6B   �� *
Quantite+$ TwoWay=�5!      5$      �� 5%   
   $
��Left=�6   $��305,158,0,0q�6/   $
��Wrap=�6D   $9��32 - Complétez les informations lié à ta rectte !��6X   $	��Top=�6�    J� RenderTransformOrigin$ 1.203,1.975&�6�   $��Rockwell Nova Cond Light)�6�   �� 5&   
   $
��Left=�6   $��707,158,0,0q�6/   $
��Wrap=�6D   $-��'3 - Ajustez les ingrédient si besoin !��6X   $	��Top=�6�   $��Rockwell Nova Cond Light)�6�   �� 5'   
   -   5        $��AjouterRecetteFinal��5'   3   $��Ajouter ��6O   $
��Left=�6b   $��853,451,0,0q�6}   $	��Top=�6�   $��94��6�   $��Rockwell Nova Cond Light)�6�   �� 5(   
   $
��Left=�6   $
��Wrap=�6/   $-��'4 - Valide ta rectte ! et c'est finit !��6C   $	��Top=�6r   $��237,398,0,0q�6�   $��Rockwell Nova Cond Light)�6�   �� 5)   
   $
��Left=�6   $��237,210,0,0q�6/   $
��Wrap=�6D   $��Nom :��6X   $	��Top=�6e   $��Rockwell Nova Cond Light)�6}   �� 5*   
   $
��Left=�6   $��237,282,0,0q�6/   $
��Wrap=�6D   $*��$Description en 256 caractères max :��6X   $	��Top=�6�   $��Rockwell Nova Cond Light)�6�   �� 5+   
   $
��Left=�6   $��237,250,0,0q�6/   $
��Wrap=�6D   $5��/Prix de vente au client : (entre 10 et 40 cook)��6X   $	��Top=�6�   $	��282��6�   $��Rockwell Nova Cond Light)�6�   �� 5,   
   -	   5        $��	PrixVente��5,      $
��Left=�6%   $��23��6@   $��564,250,0,0q�6L   $
 Wrap=�6a   $�� ��6u   $	��Top=�6}   $��80��6�   $ 
0.34,0.643&�6�   �� 5-   
   -
   5        $��Information_client��5-      $
��Left=�60   $��	24,10,0,0q�6K   $
��Wrap=�6^   $.��(Information utilisateur, dont solde cook��6r   $	��Top=�6�   $	��353��6�   $��14(�6�   $��Rockwell Nova Cond Light)�6�   �� 5.   
   -   5        $��
RetourMenu��5.      $��Retour Menu principal��6#   $
��Left=�6C   $��695,531,0,0q�6^   $	��Top=�6s   $	��240��6�   $��64��6�   $��Rockwell Nova Cond Light)�6�   $��20(�6�   �� 5/   
   $
��Left=�6   $��
44,521,0,0q�6/   $
��Wrap=�6C   $j��dUne fois votre recette crée, vous pouvez également retourner au menu principale en cliquant sur  :��6W   $	��Top=�6�   $��Rockwell Nova Cond Light)�6�   $��24(�6�   $	��651��6  $��74��6  �� 50   
   -   5        $��
typedeplat��50      $
��Left=�6$   $��23��6?   $��564,212,0,0q�6K   $
 Wrap=�6`   $�� ��6t   $	��Top=�6|   $��80��6�   �� 51   
   $
��Left=�6   $��476,214,0,0q�6/   $
��Wrap=�6D   $��Type de plat :��6X   $	��Top=�6n   $��Rockwell Nova Cond Light)�6�   �� 52   
   -   5        $��Vider recette��52   &   $
��Left=�6>   $��707,466,0,0q�6Y   $	��Top=�6n   $��94��6�   $��Rockwell Nova Cond Light)�6�   �� 53   
   -   5        $��Recette crées��53   &   $
��Left=�6>   $��529,432,0,0q�6Y   $	��Top=�6n   $	��115��6�   $��38��6�   $��Rockwell Nova Cond Light)�6�   �� 54   
   $
��Left=�6   $��237,444,0,0q�6/   $
��Wrap=�6D   $1��+5 - Visualisez toutes vos recettes créees!��6X   $	��Top=�6�   $	��248��6�   $��Rockwell Nova Cond Light)�6�   56      57      