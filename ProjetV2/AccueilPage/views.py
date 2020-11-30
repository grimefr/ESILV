from django.core.exceptions import ObjectDoesNotExist
from django.shortcuts import render,redirect
from .models import *
from django.http import HttpResponseRedirect
from django.contrib import messages
from django.template import Context
# Create your views here.

def connection(request):
    if request.method == 'POST':
        form = ConnexionForm(request.POST)
        if form.is_valid():
            identifiant = form.cleaned_data['identifiant']
            password=form.cleaned_data['password']
            try:
                user=Foyer.objects.get(identifiant=identifiant,password=password)
            except ObjectDoesNotExist:
                user=None
                form = ConnexionForm()
                p_error="Mot de passe ou identifiant incorrect ! "
            if(user is not None):
                request.session['identification']=True
                request.session['identifiant']=identifiant
                request.session['panier']=[]
                return redirect('mainPage/')
    else:
        form = ConnexionForm()
    return render(request, 'connexion.html',locals())


def register(request):

    if request.method=='POST':

        if 'ValidezFoyer' in request.POST:
            form=RegisterForm(request.POST)
            formPersonne=RegisterPersonForm()
            boutonValidationFoyer=True
            if form.is_valid():
                boutonValidationPersonne=True
                boutonValidationFoyer=False
                message="Ajouter les habitants de votre foyer."
                request.session['personnes'] = form.cleaned_data['nombreDePersonne']
                request.session['identifiant']=form.cleaned_data['identifiant']
                form.save()

        else:
            formPersonne=RegisterPersonForm(request.POST)
            habitants=request.session['personnes']
            identifiant=request.session['identifiant']
            boutonValidationPersonne=True
            boutonValidationFoyer=False
            if formPersonne.is_valid():
                nom=formPersonne.cleaned_data['nom']
                prenom=formPersonne.cleaned_data['prenom']
                sexe=formPersonne.cleaned_data['sexe']
                age=formPersonne.cleaned_data['age']
                telephone=formPersonne.cleaned_data['telephone']
                email=formPersonne.cleaned_data['email']
                identifiant=request.session['identifiant']
                new_citoyen=Citoyen(nom,prenom,sexe,age,telephone,email,identifiant)
                new_citoyen.save()
                formPersonne=RegisterPersonForm()
            compteur=Citoyen.objects.filter(identifiant=identifiant).count()
            restant=habitants-compteur
            message="Ajouter les membres du foyer. Il vous reste "+ str (restant)+" membres à ajouter."
            if compteur==habitants:
                return redirect('/')

    else:
        boutonValidationPersonne=False
        boutonValidationFoyer=True
        form=RegisterForm()
        formPersonne=RegisterPersonForm()

    return render(request, 'register.html',locals())

def deco(request):
    request.session.flush()
    return HttpResponseRedirect('/')
