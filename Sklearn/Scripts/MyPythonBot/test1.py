import numpy as np
import pickle
from sklearn.metrics import accuracy_score
import matplotlib as plt
from sklearn.neural_network import MLPClassifier
import pandas as pd
from sklearn.preprocessing import StandardScaler  



#df = pickle.load(open("traindata", 'rb'))
scaler = StandardScaler() 
df = pd.read_csv("gameStates.csv", sep=',', header=None)
df.columns = ['timeStamp','Px', 'Py', 'heat', 'Exp1','Eyp1','Exp2','Eyp2', 'Ex1', 'Ey1', 'Eh1', 'Ex2', 'Ey2', 'Eh2', 'Ex3', 'Ey3', 'Eh3', 'Ex4', 'Ey4','Eh4','Ex5', 'Ey5','Eh5','Ex6', 'Ey6','Eh6','ray1','ray2','ray3','ray4','ray5','ray6','ray7','ray8','ray9','ray10','ray11','ray12','ray13','ray14','ray15','ray16','ray17','ray18','ray19','ray20','ray21','ray22','ray23','ray24','ray25','ray26','ray27','score',"VKey","HKey","Shooting"]


def multioutput2multilabel(row):
	values = pd.Series([(row["VKey"] == "UpArrow"),(row["VKey"] == "DownArrow"),(row["HKey"] == "LeftArrow"),(row["HKey"] == "RightArrow"),row["Shooting"]])
	return pd.concat([row[:len(row)-3], values.astype(float)])



df.drop("timeStamp",axis=1,inplace=True)
df = df.apply(multioutput2multilabel,axis=1)
#df.columns = df.columns + ["Up","Down","left","Right","Shooting"] 
print(df.head(3))
df.fillna(0,inplace = True)
df.isnull().values.any()
print(df.isnull().values.any())
print(df.describe())
pickle.dump(df, open("trainData", 'wb'))


train = df.drop(["score", "VKey", "HKey", "Shooting"],axis=1).values
labels = df[["VKey","HKey","Shooting"]].values
scaler.fit(train)
train = scaler.transform(train)


clf = MLPClassifier(solver='lbfgs', alpha=1e-5, hidden_layer_sizes=(15,), random_state=1)
clf.fit(train, labels)

#shapes = [coef.shape for coef in clf.coefs_] # solo voy a ajustar los pesos
#sizes =[coef.size for coef in clf.coefs_]

#print(shapes,sizes)

"""
shapes = [coef.shape for coef in model.coefs_] # solo voy a ajustar los pesos
sizes =[coef.size for coef in model.coefs_]


def gen2Coefs(gen,sizes,shapes):
    coefs = []
    splits = np.split(gen, [sizes[0]])
    for i in range(len(splits)):
        coefs.append(splits[i].reshape(shapes[i]))
    return coefs

def coefs2gen(coefs,sizes,shapes):
    return np.concatenate((coefs[0].flatten(),coefs[1].flatten()))

# Test para probar las funciones anteriores

gen = np.arange(sum(sizes))
coefs = gen2Coefs(gen,sizes,shapes)
print(coefs[0])
print(coefs[1])
coefs2gen(coefs,sizes,shapes)
"""