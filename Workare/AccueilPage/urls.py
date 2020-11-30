from django.urls import path
from . import views

urlpatterns = [
    path('', views.accueil,name='Accueil'),
    path('deco/',views.deco,name="Deconnexion"),
    path('connexion_entreprise/', views.connexion_entreprise,name='Connexion entreprise'),
    path('connexion_membre/', views.connexion_membre,name='Connexion membre'),   
    path('register_membre/',views.register_membre,name="Créer un compte"),
    path('register_entreprise/',views.register_entreprise,name="Créez un compte pour votre entreprise"),
]
