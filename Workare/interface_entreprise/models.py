from django.db import models
from django import forms
from django.forms import ModelForm
from django.core.exceptions import ValidationError
from django.db.models.constraints import *
# Create your models here.

class OffreEmploie(models.Model):
    nom = models.CharField(max_length=30,primary_key=True, default="Inconnue")
    TYPEEMPLOIE_CHOICES = [('I', 'CDI',),('C', 'CDD',),('S', 'Stage',)]
    typeEmploie = models.CharField(max_length=1,choices =TYPEEMPLOIE_CHOICES,default = 'C')
    description = models.CharField(max_length=1000, default="absente") 

