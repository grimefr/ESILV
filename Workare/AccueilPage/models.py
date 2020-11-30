from django.db import models
from django import forms
from django.forms import ModelForm
from django.core.exceptions import ValidationError
from django.db.models.constraints import *

# Create your models here.
class Handicap(models.Model):
    nom = models.CharField(max_length=30,primary_key=True, default="Inconnue")
    TYPEHANDICAP_CHOICES = [('M', 'Handicap mental',),('P', 'Handicap Physique',),('S', 'Handicap Sensoriel',),('C', 'Handicap Cognitif',),('Y', 'Handicap psychique',),('O', 'Polyhandicap',)]
    typehandicap = models.CharField(max_length=1,choices =TYPEHANDICAP_CHOICES,default = 'O')
    desc = models.CharField(max_length=256, default="sans") 

class Membre(models.Model):
    identifiant=models.CharField(max_length=50, primary_key=True)
    password=models.CharField(max_length=20)

    nom=models.CharField(max_length = 20)
    prenom=models.CharField(max_length = 20)

    telephone=models.CharField(max_length=10)
    email=models.EmailField(max_length=254, blank=True)

    dateDeNaissance=models.DateField()    

    adresse=models.CharField(max_length=100)
    codePostal=models.PositiveIntegerField()
    ville=models.CharField(max_length=50)

    handicap = models.ForeignKey(Handicap,on_delete=models.CASCADE,default=None)    
    cartehandicap=models.BooleanField(default=False)

    SEX_CHOICES = [('F', 'Femme',),('H', 'Homme',),('N','Non Binaire')]
    sexe=models.CharField(max_length=1,choices=SEX_CHOICES,default='H')

    centresInterets = models.CharField(max_length=500,default='aucun')

    ETUDE_CHOICES =[('S', 'Sans Etude',),('B', 'Brevet des collèges',),('P', 'Formation pro',),('G', 'Bts',),('F', 'DUT',),('C', 'Bac',),('D', 'Bac + 3',),('E', 'Bac + 5',),('F', 'Bac + 8',)]
    etude=models.CharField(max_length=1,choices=ETUDE_CHOICES,default='S')
    nom_diplome=models.CharField(max_length=255,default='aucun')

    competence = models.CharField(max_length = 800,default='aucun')

    descriptif_membre= models.CharField(max_length=1000,default='aucun')

    experience = models.CharField(max_length=1000,default='aucun')

    def clean(self):
        if(len(self.password)<8):
            raise ValidationError('Votre mot de passe est trop court.')
        if(len(str(self.codePostal))!=5):
            raise ValidationError('Saississez un code postal valide.')
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





class Entreprise(models.Model):
    
    identifiant=models.CharField(max_length=20,primary_key=True)
    password=models.CharField(max_length=20)   
    nom_entreprise = models.CharField(max_length=30)
    ville=models.CharField(max_length=50)
    code_naf=models.CharField(max_length=50)
    numero_siret=models.CharField(max_length=50)
    adresse_siege_sociale=models.CharField(max_length=60)
    email=models.EmailField(max_length=254, blank=True)

    def clean(self):
        if(len(self.password)<8):
            raise ValidationError('Votre mot de passe est trop court.')   
        if(Entreprise.objects.filter(code_naf = self.code_naf).count()>=1):
            raise ValidationError('Votre entreprise possède déjà un compte.')      
        for char in self.ville:
            if char.isdecimal():
                raise ValidationError('Saississez un nom de ville valide.')
                break 



class ConnexionForm(forms.Form):
    identifiant=forms.CharField(max_length=50, label="Identifiant")
    password=forms.CharField(max_length=20,label="Mot de Passe",widget=forms.PasswordInput)


class RegisterMembreForm(ModelForm):
    class Meta:
        model=Membre
        fields=('identifiant','password','adresse', 'nom','prenom','codePostal','ville','email', 'telephone','dateDeNaissance', 'cartehandicap', 'sexe','centresInterets','etude','nom_diplome', 'competence','descriptif_membre','experience')
        labels={
        'identifiant': "Identifiant du compte :",
        'password' : "Mot de Passe :",
        'nom' : "Nom :",
        'prenom' : "Prenom :",
        'adresse' : "Adresse :",
        'codePostal':"Code Postal :",
        'ville': "Ville :",
        'email' : "Adresse Email :",
        'telephone' : "Téléphone :",
        'dateDeNaissance' : "Date de naissance :",
        'cartehandicap' : "Je jure sur l'honneur être titulaire d'une carte inclusion",   
        'sexe':"Vous êtes :",
        'centresInterets':"Centres d'intêrets :",
        'etude' : "Votre niveau d'étude :",
        'nom_diplome':"Nom de votre diplome, si vous en possédez :",
        'competence':"Vos compétences :",
        'descriptif_membre':"Décrivez vous :",
        'experience':"Vos expériences professionnelles :",      

        }
        widgets={
        'email':forms.EmailInput,}

class RegisterEntrepriseForm(ModelForm):
    class Meta:
        model=Entreprise
        fields="__all__"
        labels={
        'nom_entreprise':'Nom de l\'entreprise :',
        'identifiant':'Identifiant :',
        'password': "Mot de passe :",
        'telephone':"Numero de teléphone :",
        'email' : "Adresse Email :",
        'adresse_siege_sociale' : "Adresse du siège sociale :",
        'ville' : "Ville :",
        'code_naf' : "Code NAF :",
        'numero_siret' : "Numéros SIRET :",
        }
        
    widgets={
        'email':forms.EmailInput,
        }



class RegisterHandicapForm(ModelForm):
    class Meta:
        model=Handicap
        fields="__all__"
        labels={
        'nom': "Nom de l'handicap :",
        'typehandicap' : "Quel est le type d'handicap :",
        'desc': "Description de l'handicap :",    
        
        }



