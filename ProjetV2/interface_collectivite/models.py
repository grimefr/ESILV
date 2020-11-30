from django.db import models
from django import forms
from django.forms import ModelForm
from django.core.exceptions import ValidationError
from django.db.models.constraints import *

# Create your models here.

class Collectivite(models.Model):
    password=models.CharField(max_length=20)    
    identifiant=models.CharField(max_length=20)
    codePostal=models.PositiveIntegerField()
    ville=models.CharField(max_length=50)

    def clean(self):
        if(len(self.password)<8):
            raise ValidationError('Votre mot de passe est trop court.')       
        
        if(len(str(self.codePostal))!=5):
            raise ValidationError('Saississez un code postal valide.')
        for char in self.ville:
            if char.isdecimal():
                raise ValidationError('Saississez un nom de ville valide.')
                break

class ConnexionForm(forms.Form):
    identifiant=forms.CharField(max_length=50, label="Identifiant")
    password=forms.CharField(max_length=20,label="Mot de Passe",widget=forms.PasswordInput)
