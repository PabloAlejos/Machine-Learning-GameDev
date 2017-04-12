from sklearn.multioutput import MultiOutputClassifier
from sklearn.ensemble import RandomForestClassifier
from sklearn.datasets import make_classification
from sklearn import preprocessing
from sklearn.utils import shuffle
import numpy as np
import pandas as pd
import pickle
import itertools

def mapNumber(value,min1,max1,min2,max2):
	return (value - min1) / (max1 - min1) * (max2 - min2) + min2;


df = pd.read_csv('gameStates.csv', sep=',',header=None)
df.columns = ['timeStamp', 'Px', 'Py', 'heat', 'Exp1','Eyp1','Exp2','Eyp2', 'Ex1', 'Ey1', 'Eh2', 'Ex2', 'Ey2', 'Eh2', 'Ex3', 'Ey3', 'E3h', 'Ex4', 'Ey4','Eh4','Ex5', 'Ey5','Eh5','Ex6', 'Ey6','Eh6',"VKey","HKey","Shooting"]
#Preprocesado sin valorar vida restante
hist = df[['Ex1', 'Ey1', 'Ex2', 'Ey2', 'Ex3', 'Ey3','Ex4', 'Ey4','Ex5', 'Ey5','Ex6', 'Ey6']].copy()
df = df.drop(['Ex1', 'Ey1', 'Ex2', 'Ey2', 'Ex3', 'Ey3','Ex4', 'Ey4','Ex5', 'Ey5','Ex6', 'Ey6'],axis =1)

target_data =df[["VKey","HKey","Shooting"]].copy()
df = df.drop(["VKey","HKey","Shooting"],axis=1)

def histo2DRow(row):
   
    x = row.filter(regex=('Ex'))
    y = row.filter(regex=('Ey'))
    
    xedges = [-2,  -1, 0,   1,  2]
    yedges = [ 0, 2.5, 5, 7.5, 10]

    H, xedges, yedges = np.histogram2d(x, y, bins=(xedges, yedges))
    H = H.T  # Let each row list bins with common y range.
    return pd.Series(H.reshape(-1))


hist2D = hist.apply(histo2DRow, axis = 1)
oldNames = list(range(23))
newNames = ["("+str(x)+","+str(y)+")" for x,y in itertools.product(np.arange(-3,3), np.arange(0,11,2.5))]
hist2D = hist2D.rename(columns=dict(zip(oldNames,newNames)))
df = pd.concat([df,hist2D,target_data],axis=1)
print(df.ix[[10]])

###Entrenamiento###
train_data = df.drop(["Shooting", "VKey", "HKey"], axis =1)
target_data =df[["VKey","HKey","Shooting"]].copy()


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

target_data["VKey"] = target_data["VKey"].map(transformaEje)
target_data["HKey"] = target_data["HKey"].map(transformaEje)
target_data["Shooting"] = target_data["Shooting"].map(transformaDisparo)

forest = RandomForestClassifier(n_estimators=100, random_state=1)

forest = forest.fit(train_data, target_data.astype(str))
filename = 'randomForest4.sav'
pickle.dump(forest, open(filename, 'wb'))