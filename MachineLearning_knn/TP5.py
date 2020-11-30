# -*- coding: utf-8 -*-
"""
Created on Sat Apr 12 18:05:33 2020

@author: François GRIME et Thomas HARDOUIN
"""

# Question 1 : Hormis k, quels autres paramêtres ou modifications pourrait-on
#              apporter afin de modifier le comportement de l'algorithme?

#              On pourrait rajouter une valeur max pour le nombre de point 
#              d'apprentissage afin de ne pas être en surapprentissage


# Question 2 : On peut choisir un autre k, ou faire un vote (classe majoritaire) pondéré par la distance

def predictionclass(point):
    i = 0
    # Initialisation d'un tableau de la norme de chaque vecteur
    tab_dist = []
    class_choisits = []
    for index in res:
        if np.linalg.norm(point - index) != 0:
            tab_dist.append(np.linalg.norm(point - index))
            if np.linalg.norm(point - index) <= k:
                class_choisits.append(df.iloc[i][4])
            i += 1
    for index in class_choisits:
        c1 = 0
        c2 = 0
        c3 = 0
        if index == class1:
            c1 = +1
        if index == class2:
            c2 = +1
        if index == class3:
            c3 = +1
        maximum = max(c1, c2, c3)
        if maximum == c1:
            return class1
        if maximum == c2:
            return class2
        else:
            return class3


def efficacite():
    i = 0
    correct = 0
    for point in res:
        a = np.array(point)
        predictionclass(a)
        if predictionclass(a) == df.iloc[i][4]:
            correct += 1
        i += 1
    taux_correct = str(correct / (i+1) * 100)
    return taux_correct + '%'


if __name__ == '__main__' :
      
    import pandas as pd
    import numpy as np
    
    # initialiser k
    k= float(input("Choisissez le nombre de k voisins que vous voulez : ")) 
    
    # types de class
    class1 = 'Iris-setosa'
    class2 = 'Iris-versicolor'
    class3 = 'Iris-virginica'
    
    df = pd.read_csv("iris.csv", sep=';')
    
    # Initialisation d'un tableau de vecteurs pour chaque point 
    res = []
    i = 0
    for row in df.iterrows():
        res.append([df.loc[i]['sepal length'], df.loc[i]['sepal width'],
                    df.loc[i]['petal length'], df.loc[i]['petal width']])
        i += 1
    print("Efficacité de la prédiction : ", efficacite())
    print("Matrice de confusion :")
    
    
