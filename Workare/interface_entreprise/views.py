from django.core.exceptions import ObjectDoesNotExist
from django.shortcuts import render,redirect
from .models import *
from django.http import HttpResponseRedirect
from django.contrib import messages
from django.template import Context

from datetime  import datetime
# Create your views here.

