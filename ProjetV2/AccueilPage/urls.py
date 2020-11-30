from django.urls import path
from . import views

urlpatterns = [
    path('', views.connection,name='Connexion'),
    path('register/',views.register,name="Créez un compte"),
    path('deco/',views.deco,name="Deconnexion")
]
