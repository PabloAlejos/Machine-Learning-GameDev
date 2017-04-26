from sklearn.multioutput import MultiOutputClassifier
from sklearn.ensemble import RandomForestClassifier
from sklearn.datasets import make_classification
from sklearn import preprocessing
from sklearn.utils import shuffle
from sklearn.multiclass import OneVsRestClassifier
from sklearn.svm import LinearSVC
import numpy as np
import pandas as pd
import pickle
import itertools
from collections import Counter

def histo2DRow(row):
   
    x = row.filter(regex=('Ex'))
    y = row.filter(regex=('Ey'))
    w = row.filter(regex=('Eh'))
    xedges = np.linspace(-2,2,5)
    yedges = np.linspace(0,10,6)

    H, xedges, yedges = np.histogram2d(x, y, bins=(xedges, yedges), weights=w)
    H = H.T  # Let each row list bins with common y range.
    return pd.Series(H.reshape(-1))
    
def transformaEje(value):
    if value == "UpArrow" or value == "RightArrow":
        return 1
    elif value == "DownArrow" or value == "LeftArrow":
        return -1
    else:
        return 0 

def transformaDisparo(value):
    if value:
        return 1
    else:
        return 0

def concatenar_estados(dataframe):
    dfAnterior = dataframe.shift(1)
    dfAnterior.rename(columns=lambda x: x+"-1", inplace=True)
    dfAnterior2 = dataframe.shift(2)
    dfAnterior2.rename(columns=lambda x: x+"-2", inplace=True)
    df = pd.concat([dfAnterior2,dfAnterior,dataframe],axis=1)
    df.fillna(999)
    return df

print("Inicio...")
print("Leyendo archivo")
df = pd.read_csv('gameStates.csv', sep=',',header=None) #Carga el Dataframe
#Define los nombre de las columnas para facilitar su manejo
df.columns = ['timeStamp', 'Px', 'Py', 'heat', 'Exp1','Eyp1','Exp2','Eyp2', 'Ex1', 'Ey1', 'Eh1', 'Ex2', 'Ey2', 'Eh2', 'Ex3', 'Ey3', 'Eh3', 'Ex4', 'Ey4','Eh4','Ex5', 'Ey5','Eh5','Ex6', 'Ey6','Eh6',"VKey","HKey","Shooting"]
df = df.drop(['timeStamp'],axis=1)
df[['Eh1','Eh2','Eh3','Eh4','Eh5','Eh6']] = df[['Eh1','Eh2','Eh3','Eh4','Eh5','Eh6']].astype(float) 

#Preprocesado valorando la vida
enemiesInfo = df[['Ex1', 'Ey1', 'Ex2', 'Ey2', 'Ex3', 'Ey3','Ex4', 'Ey4','Ex5', 'Ey5','Ex6', 'Ey6','Eh1','Eh2','Eh3','Eh4','Eh5','Eh6']].copy()
df = df.drop(['Ex1', 'Ey1', 'Ex2', 'Ey2', 'Ex3', 'Ey3','Ex4', 'Ey4','Ex5', 'Ey5','Ex6', 'Ey6','Eh1','Eh2','Eh3','Eh4','Eh5','Eh6'], axis =1)
target_data = df[["VKey","HKey","Shooting"]].copy()
#target_data = target_data.drop([0,1])
df = df.drop(["VKey","HKey","Shooting"],axis=1).copy()
print("Generando histogramas")
hist2D = enemiesInfo.apply(histo2DRow, axis = 1)
#hist2D = hist.apply(lambda x: histo2D(x, x.filter(regex=('Eh'))))
oldNames = list(range(23))
newNames = ["("+str(x)+ ","+ str(y) +")" for x, y in itertools.product(np.linspace(-2,2,6), np.linspace(0,10,6))]
print("Renombrado")
hist2D = hist2D.rename(columns=dict(zip(oldNames,newNames)))
train_data = pd.concat([df,hist2D],axis=1)

#train_data_Shuffle = [train_data,target_data]
#shuffle(train_data_Shuffle)
#train_data = train_data_Shuffle[0]
#target_data = train_data_Shuffle[1]
print("Concatenando estados...")
train_data = concatenar_estados(train_data)
train_data = train_data.fillna(999)
#train_data = train_data.drop([0,1])

###Entrenamiento###
#train_data = df.drop(["Shooting", "VKey", "HKey", "timeStamp"], axis =1)
#target_data =df[["VKey","HKey","Shooting"]]

print("Procesando las salidas...")
target_data["VKey"] = target_data["VKey"].map(transformaEje)
target_data["HKey"] = target_data["HKey"].map(transformaEje)
target_data["Shooting"] = target_data["Shooting"].map(transformaDisparo)
#print("Balancing...")
#balance_data(target_data)


print("Entrenamiento")
#Entrenamiento random forest
forest = RandomForestClassifier(n_estimators=100, random_state=1)

#from sklearn.tree import DecisionTreeClassifier
#forest  = DecisionTreeClassifier()
forest = forest.fit(train_data, target_data.astype(str))

filename = 'randomForest.sav'
pickle.dump(forest, open(filename, 'wb'))

"""
from sklearn.model_selection import cross_val_score
print("Vkey",cross_val_score(forest, train_data, target_data["VKey"], cv=5))
print("Hkey",cross_val_score(forest, train_data, target_data["HKey"], cv=5))
print("Shooting",cross_val_score(forest, train_data, target_data["Shooting"], cv=5))
"""
