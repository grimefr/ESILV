from django.core.exceptions import ObjectDoesNotExist
from django.shortcuts import render,redirect
from .models import *
from django.http import HttpResponseRedirect
from django.contrib import messages
from django.template import Context
# Create your views here.

def connexion_entreprise(request):
    if request.method == 'POST':
        form = ConnexionForm(request.POST)
        if form.is_valid():
            identifiant = form.cleaned_data['identifiant']
            password=form.cleaned_data['password']
            try:
                user=Entreprise.objects.get(identifiant=identifiant,password=password)
            except ObjectDoesNotExist:
                user=None
                form = ConnexionForm()
                p_error="Mot de passe ou identifiant incorrect ! "
            if(user is not None):
                request.session['identification']=True
                request.session['identifiant']=identifiant
                return redirect('/')
    else:
        form = ConnexionForm()
    return render(request, 'connexion_entreprise.html',locals())


def connexion_membre(request):
    if request.method == 'POST':
        form = ConnexionForm(request.POST)
        if form.is_valid():
            identifiant = form.cleaned_data['identifiant']
            password=form.cleaned_data['password']
            try:
                user=Membre.objects.get(identifiant=identifiant,password=password)
            except ObjectDoesNotExist:
                user=None
                form = ConnexionForm()
                p_error="Mot de passe ou identifiant incorrect ! "
            if(user is not None):
                request.session['identification']=True
                request.session['identifiant']=identifiant
                return redirect('/')
    else:
        form = ConnexionForm()
    return render(request, 'connexion_membre.html',locals())



def register_membre(request):

    if request.method=='POST':
    
        if 'Validez' in request.POST:
            form=RegisterMembreForm(request.POST)
            formHandicap=RegisterHandicapForm(request.POST)
            boutonValidationHandicap=True
            if formHandicap.is_valid():
                nom=formHandicap['nom']
                typehandicap =formHandicap['typehandicap']
                desc=formHandicap['desc']                
                new_Handicap=Handicap(nom,typehandicap,desc)
                new_Handicap.save()
                formHandicap=RegisterHandicapForm()  
                boutonValidationHandicap=False
                boutonValidation=True
                formHandicap.save()

        else:
            form=RegisterMembreForm(request.POST)
            identifiant=request.session['identifiant']
            boutonValidationHandicap=True
            boutonValidation=False
            if form.is_valid():
                identifiant=form.cleaned_data['identifiant']
                password=form.cleaned_data['password']
                nom=form.cleaned_data['nom']
                prenom=form.cleaned_data['prenom']
                telephone=form.cleaned_data['telephone']
                email=form.cleaned_data['email']
                dateDeNaissance=form.cleaned_data['dateDeNaissance']  
                adresse=form.cleaned_data['adresse']
                codePostal=form.cleaned_data['codePostal']
                ville=form.cleaned_data['ville']
                handicap = models.Handicap.filter(nom=nom)
                carethandicap=form.cleaned_data['carethandicap']
                sexe=form.cleaned_data['sexe']
                centresInterets =form.cleaned_data['centresInterets']
                etude=form.cleaned_data['etude']
                nom_diplome=form.cleaned_data['nom_diplome']
                competence = form.cleaned_data['competence']
                descriptif_membre= form.cleaned_data['descriptif_membre']
                experience = form.cleaned_data['experience']           

                new_Membre=Membre(identifiant,sexe,nom,prenom,telephone,email,dateDeNaissance,adresse,codePostal,ville,handicap,cartehandicap,sexe,centresInterets,etude,nom_diplome,competence,descriptif_membre,experience)
                new_Membre.save()
                form=RegisterMembreForm()  
                boutonValidationHandicap=True
                #boutonValidation=True
                form.save() 
                return redirect('/connexion_membre/')

    else:
        boutonValidationHandicap=False
        boutonValidation=False
        form=RegisterMembreForm(request.POST)
        formHandicap=RegisterHandicapForm(request.POST)

    return render(request, 'register_membre.html',locals())

def deco(request):
    request.session.flush()
    return HttpResponseRedirect('/')




def register_entreprise(request):
    
    if request.method=='POST':

        if 'Validez' in request.POST:
            form=RegisterEntrepriseForm(request.POST)
            if form.is_valid():
                nom_entreprise = form.cleaned_data['nom_entreprise']  
                code_naf=form.cleaned_data['code_naf']  
                numero_siret=form.cleaned_data['numero_siret']  
                adresse_siege_sociale=form.cleaned_data['adresse_siege_sociale']  
                email=form.cleaned_data['email']  
                ville = form.cleaned_data['ville']    
                identifiant = form.cleaned_data['identifiant']
                password=form.cleaned_data['password']
                form.save()
                return redirect('/connexion_entreprise/')                
                boutonValidation=True            
    else:        
        boutonValidation=False
        form=RegisterEntrepriseForm()

    return render(request, 'register_entreprise.html',locals())


def accueil(request):
    return render(request, 'Accueil.html',locals())
