from sklearn.multioutput import MultiOutputClassifier
from sklearn.ensemble import RandomForestClassifier
from sklearn.datasets import make_classification
from sklearn import preprocessing
from sklearn.utils import shuffle
import pandas as pd
import pickle

#X, y1 = make_classification(n_samples=10, n_features=100, n_informative=30, n_classes=3, random_state=1)
df = pd.read_csv('gameStates.csv', sep=',',header=None)

df.columns = ['timeStamp','Px', 'Py','heat','Pup1x' ,'Pup1y','Pup2x','Pup2y', 'E1x', 'E1y', 'E1h', 'E2x', 'E2y','E2h','E3x', 'E3y','E3h','E4x', 'E4y','E4h','E5x', 'E5y','E5h','E6x', 'E6y','E6h',"VKey","HKey","Shooting"]
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

print(target_data[:50])

forest = RandomForestClassifier(n_estimators=100, random_state=1)

forest = forest.fit(train_data, target_data.astype(str))

filename = 'randomForest.sav'
pickle.dump(forest, open(filename, 'wb'))