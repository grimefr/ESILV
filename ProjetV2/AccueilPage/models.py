from django.db import models
from django import forms
from django.forms import ModelForm
from django.core.exceptions import ValidationError
from django.db.models.constraints import *

# Create your models here.
class Foyer(models.Model):
    identifiant=models.CharField(max_length=50, primary_key=True)
    password=models.CharField(max_length=20)
    nombreDePersonne=models.PositiveIntegerField()
    numeroDeRue=models.PositiveIntegerField()
    nomDeRue=models.CharField(max_length=100)
    codePostal=models.PositiveIntegerField()
    ville=models.CharField(max_length=50)

    def clean(self):
        if(len(self.password)<8):
            raise ValidationError('Votre mot de passe est trop court.')
        if(self.nombreDePersonne>20):
            raise ValidationError('Saississez un nombre de personne valide.')
        for char in self.nomDeRue:
            if char.isdecimal():
                raise ValidationError('Saississez un nom de rue valide.')
                break
        if(len(str(self.codePostal))!=5):
            raise ValidationError('Saississez un code postal valide.')
        for char in self.ville:
            if char.isdecimal():
                raise ValidationError('Saississez un nom de ville valide.')
                break

    class Meta:
        constraints =[UniqueConstraint(fields=['numeroDeRue', 'nomDeRue'], name='adresse_unique')]

class Citoyen(models.Model):
    SEX_CHOICES = [('F', 'Femme',),('H', 'Homme',)]
    nom=models.CharField(max_length=50)
    prenom=models.CharField(max_length=50)
    sexe=models.CharField(max_length=1,choices=SEX_CHOICES,default='H')
    age=models.PositiveIntegerField()
    telephone=models.CharField(max_length=10,primary_key=True )
    email=models.EmailField(max_length=254, blank=True)
    identifiant=models.ForeignKey(Foyer,on_delete=models.CASCADE)
    def clean(self):
        for char in self.nom:
            if char.isdecimal():
                raise ValidationError('Saississez un nom valide.')
                break
        for char in self.prenom:
            if char.isdecimal():
                raise ValidationError('Saississez un prenom valide.')
                break
        if self.age>110:
            raise ValidationError('Saississez un age valide.')
        if len(self.telephone)!=10 and self.telephone.isdecimal()==False:
            raise ValidationError('Saississez un numéro de téléphone valide.')


class ConnexionForm(forms.Form):
    identifiant=forms.CharField(max_length=50, label="Identifiant")
    password=forms.CharField(max_length=20,label="Mot de Passe",widget=forms.PasswordInput)

class RegisterForm(ModelForm):
    class Meta:
        model=Foyer
        fields="__all__"
        labels={
        'identifiant': "Identifiant du compte :",
        'password' : "Mot de Passe :",
        'nombreDePersonne':"Combien de personnes dans le foyer?",
        'numeroDeRue':"Numero de rue du foyer :",
        'nomDeRue' : "Nom de la rue :",
        'codePostal':"Code Postal :",
        'ville': "Ville :",
        }

class RegisterPersonForm(ModelForm):
    class Meta:
        model=Citoyen
        fields=('nom','prenom','sexe','age','email','telephone')
        labels={
        'nom':'Nom :',
        'prenom':'Prenom :',
        'sexe':"Sexe :",
        'age': "Age :",
        'telephone':"Numero de teléphone :",
        'email' : "Adresse Email :"}
        widgets={
        'email':forms.EmailInput,
        }
