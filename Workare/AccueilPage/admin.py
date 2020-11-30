from django.contrib import admin
from .models import Handicap, Membre, Entreprise, OffreEmploie
# Register your models here.

admin.site.register(Handicap)
admin.site.register(Membre)
admin.site.register(Entreprise)
admin.site.register(OffreEmploie)
