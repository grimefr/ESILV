# -*- coding: utf-8 -*-

import copy
from math import inf as infinity
import numpy as np

ai =1
humain = 2
grilleDim = 6




CRED = '\33[31m'
CEND = '\033[0m'
CBLUE   = '\33[34m'
grilleIA = [  [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
            [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0], 
            [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0], 
            [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0], 
            [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0], 
            [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0]]

grilleCout = [[3, 4, 5, 7, 7, 7, 7, 7, 7, 5, 4, 3],
            [4, 6, 6, 8, 10, 10, 10, 10, 10, 8, 6, 4], 
            [5, 8, 11, 13, 13, 13, 13, 13, 13, 11, 8, 5], 
            [5, 8, 11, 13, 13, 13, 13, 13, 13, 11, 8, 5], 
            [4, 6, 8, 10, 10, 10, 10, 10, 10, 8, 6, 4], 
            [3, 4, 5, 7, 7, 7, 7, 7, 7, 5, 4, 3]]

position = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0]
def monjeu():

    
    meilleurScore = -1000000
    positionGagnante = 3

    # recherche de position gagnante a n-1
    for i in range(12):
        if grilleIA[0][i] == 0:
 
            remplirGrilleADV(humain,i)
            a = copy.deepcopy(grilleIA)
            score = checkWin(a)
            retirerPionADV(humain,i)

            if checkWin(a)==2:

                
                positionGagnante = i
                remplirGrilleADV(ai,positionGagnante)
                return positionGagnante
    for i in range(12):
        if grilleIA[0][i] == 0:
 
            remplirGrilleADV(ai,i)
            a = copy.deepcopy(grilleIA)
            score = checkWin(a)
            retirerPionADV(ai,i)

            if checkWin(a)==1:

                
                positionGagnante = i
                remplirGrilleADV(ai,positionGagnante)
                return positionGagnante
    #utilisation du minmax pour maximiser les chances de gagner
    for i in range(12):
        if grilleIA[0][i] == 0:
 
            remplirGrilleADV(ai,i)
            a = copy.deepcopy(grilleIA)
            score = minmax(a,47,-infinity,infinity,True,ai)
            retirerPionADV(ai,i)

            if score[0]>meilleurScore:
                meilleurScore = score[0]
                positionGagnante = i

    remplirGrilleADV(ai,positionGagnante)

    return positionGagnante



def appliqueJeuAdv(jeu):
    print("jeu de l'adversair est ", jeu)

def minmax(a,profondeur,alpha,beta,isMax,currentPlayer):
    """
    Fonction permettant de minimiser les chances de gagner pour l'adversaire et maximiser les chances de gagner pour L'IA

    Algorithme MinMax
    """

    scores = {1:1, 2:-1,'Z':5,False:3} # Z est egalité
    resultat = checkWin(a)
    test = scores[resultat]
    
    score = fonctionCout(a)


    if(test == -1 or profondeur == 0):

        
        return [fonctionCout(a),profondeur]
    if(isMax):
        bestScore = -infinity
        for i in range(12):
        
            if(a[0][i]==0):

                remplirGrilleA(ai,i,a)

                score = minmax(a,profondeur-1,alpha,beta,False,humain)

                retirerPionA(ai,i,a)
                bestScore = max(score[0],bestScore)
                alpha = max(alpha,score[0])
                if beta <= alpha:
                    break
        return [bestScore,profondeur]
    else:
        bestScore = infinity
        for i in range(12):
        
            if(a[0][i]==0):
                
                remplirGrilleA(humain,i,a)

                score = minmax(a,profondeur-1,alpha,beta,True,ai)
                retirerPionA(humain,i,a)
                bestScore = min(score[0],bestScore)
                beta = min(beta,score[0])
                if beta <= alpha:
                    break
        return [bestScore,profondeur]  


def fonctionCout(grilleTest):
    """
    Fonction cout permettant d'attribuer un cout a la grille en jeu
    
    Le cout est calculé en fonction de la position de chaque pion. Pour chaque position, la valeur correspondante 
    a la position est ajoutée s'il sagit du pion IA et est retranchée s'il sagit d'un jeton appartenant a l'humain (qui est a minimiser)
    
    La grille de cout correspond au nombre de possibilitée de gagner par position.
    """
    total = 0
    for i in range(6):
        for j in range(12):
            if(grilleTest[i][j] == ai):
                total += grilleCout[i][j]
            if(grilleTest[i][j] == humain):
                total -= grilleCout[i][j]
    return total

def checkWin(s):
    """
    Fonction permettant de connaitre si le plateau comporte un gagnant et renvoi ce gagnant
    """
    test = 0
    player = 0
    for x in range (6):
        for y in range (9):
            player = s[x][y]
            for i in range(4):
                
                if(s[x][y+i] == player and s[x][y+i]!=0):
                    test +=1
                    if(test == 4):
                       
                        return player
            test = 0
    test = 0
    for x in range (3):
        for y in range (12):
            player = s[x][y]
            for i in range(4):
              
                if(s[x+i][y] == player and s[x+i][y]!=0):
                    
                    test +=1
                    if(test == 4):
                        
                        return player
            test = 0
    for x in range (3):
        for y in range (9):
            player = s[x][y]
            for i in range(4):
              
                if(s[x+i][y+i] == player and s[x+i][y+i]!=0):
                    test +=1
                    if(test == 4):
                        
                        return player
            test = 0

    for x in range (3):
        for y in range (3,12):
            player = s[x][y]
            for i in range(4):
              
                if(s[x+i][y-i] == player and s[x+i][y-i]!=0):
                    test +=1
                    if(test == 4):
                        
                        return player
            test = 0
    count= 0
    for i in range(6):
        for j in range(12):
            
            if s[x][y] == 0:
                count += 1
                
    if count == 0:
        return "Z"
    return False


#####################################
def remplirGrilleA(joueur, jeu,grilleA):
    for i in range(grilleDim-1,-1,-1):
        if(grilleA[i][jeu]==0):
            grilleA[i][jeu]=joueur
            return grilleA
            
def retirerPionA(joueur,jeu,grilleA):
    for i in range(grilleDim-1,-1,-1):
        if(grilleA[i][jeu]==0):
            grilleA[i+1][jeu]=0
            return grilleA
#####################################

def remplirGrilleADV(joueur, jeu):
    for i in range(grilleDim-1,-1,-1):
        if(grilleIA[i][jeu]==0):
            grilleIA[i][jeu]=joueur
            break
            
def retirerPionADV(joueur,jeu):
    for i in range(grilleDim-1,-1,-1):
        if(grilleIA[i][jeu]==0):
            grilleIA[i+1][jeu]=0
            break


def printGrille():
    for i in range(grilleDim):
        print("|",end=' ')
        for j in range(12):
            if(grilleIA[i][j]==1):
                print(CBLUE+'0'+CEND,end=' ')
            elif grilleIA[i][j]==2:
                print(CRED+'0'+CEND,end=' ')
            else:
                print(" ",end=' ')
            print("|",end=' ')
        print()
    print("|",end=' ')
    for i in range(12):
        print("_",end=" ")
        print("|",end=' ')
    print()
    print("|",end=' ')
    for i in range(12):
        print(i+1,end=" ")
        print("|",end=' ')
    print()



def printMatrice(s):
    for e in s:
        print(e)



#print(checkWin(plateau))



while checkWin(grilleIA) == False:
    printGrille()
    print("l'ia a joué colonne : ",monjeu()+1)
    if(checkWin(grilleIA) == True):
        break
    printGrille()
    remplirGrilleADV(humain,int(input(">> "))-1)



#remplirGrille(2,4)
#printMatrice(grille)
#retirerPion(2,4)
#printMatrice(grille)
