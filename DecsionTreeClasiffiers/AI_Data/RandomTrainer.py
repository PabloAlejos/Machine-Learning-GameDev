from sklearn.multioutput import MultiOutputClassifier
from sklearn.ensemble import RandomForestClassifier
from sklearn.datasets import make_classification
from sklearn.tree import DecisionTreeClassifier
from sklearn import preprocessing
from sklearn.utils import shuffle
from sklearn.multiclass import OneVsRestClassifier
from sklearn.svm import LinearSVC
import numpy as np
import pandas as pd
import pickle


class trainer:

    def __init__ (self):
        self.df = pd.DataFrame()

    #Carga el fichero con el nombre indicado y nombra las columnas
    def load_file(self,fileName):
        self.df = pd.read_csv(fileName, sep=',', header=None)
        #self.df = self.df[100:1100]
        self.df.columns = ['timeStamp','Px', 'Py', 'heat', 'Exp1','Eyp1','Exp2','Eyp2', 'Ex1', 'Ey1', 'Eh1', 'Ex2', 'Ey2', 'Eh2', 'Ex3', 'Ey3', 'Eh3', 'Ex4', 'Ey4','Eh4','Ex5', 'Ey5','Eh5','Ex6', 'Ey6','Eh6','ray1','ray2','ray3','ray4','ray5','ray6','ray7','ray8','ray9','ray10','ray11','ray12','ray13','ray14','ray15','ray16','ray17','ray18','ray19','ray20','ray21','ray22','ray23','ray24','ray25','ray26','ray27','score',"VKey","HKey","Shooting"]
        self.df[['Eh1','Eh2','Eh3','Eh4','Eh5','Eh6']] = self.df[['Eh1','Eh2','Eh3','Eh4','Eh5','Eh6']].astype(float) 
    
    #procesa las columnas correspondientes al traindata
    def set_train_data(self):
        retorno = self.df[['Px', 'Py', 'heat']].copy()
        rays = self.df[['ray1','ray2','ray3','ray4','ray5','ray6','ray7','ray8','ray9','ray10','ray11','ray12','ray13','ray14','ray15','ray16','ray17','ray18','ray19','ray20','ray21','ray22','ray23','ray24','ray25','ray26','ray27']].copy()
        powerUpInfo = self.df[['Exp1','Eyp1','Exp2','Eyp2']].copy()
        trainData = pd.concat([retorno,powerUpInfo,rays],axis=1)
        trainData = trainData.fillna(999)
        
        targetData = pd.DataFrame(columns=["VKey","HKey","Shooting"])
        targetData["VKey"] = self.df['VKey'].apply(lambda valor : self.transformaEje(valor))
        targetData["HKey"] = self.df['HKey'].apply(lambda valor : self.transformaEje(valor))
        targetData["Shooting"] = self.df['Shooting'].apply(lambda valor: self.transformaDisparo(valor))
        targetData = targetData.fillna(999)
        targetData = shuffle(targetData)
        temp = pd.concat([trainData,targetData],axis=1)
        temp = shuffle(temp)
        temp = self.concatenar_estados(temp)

        self.target_data = temp[["VKey","HKey","Shooting"]].copy()
        self.train_data = temp.drop(targetData,axis=1).copy()



    def concatenar_estados(self,dataframe):
        print("concatenando estados")
        dfAnterior = dataframe.shift(1)
        dfAnterior.rename(columns=lambda x: str(x)+"-1", inplace=True)
        dfAnterior2 = dataframe.shift(2)
        dfAnterior2.rename(columns=lambda x: str(x)+"-2", inplace=True)
        dfAnterior3 = dataframe.shift(3)
        dfAnterior3.rename(columns=lambda x: str(x)+"-3", inplace=True)
        df = pd.concat([dfAnterior3,dfAnterior2,dfAnterior,dataframe],axis=1)
        df = df.fillna(999)
        return df

    def save_train_data(self,fileName):
        np.save(fileName,self.train_data)

    def histo2DRow(self,row):
        x = row.filter(regex=('Ex'))
        y = row.filter(regex=('Ey'))
        w = row.filter(regex=('Eh'))
        xedges = np.linspace(-2,2,5)
        yedges = np.linspace(0,10,5)
        if len(w >0):
            H, xedges, yedges = np.histogram2d(x, y, bins=(xedges, yedges), weights=w)
        else:
            H, xedges, yedges = np.histogram2d(x, y, bins=(xedges, yedges))
        H = H.T  # Let each row list bins with common y range.
        return pd.Series(H.reshape(-1))

    def transformaEje(self,value):
        if value == "UpArrow" or value == "RightArrow":
            return 1
        elif value == "DownArrow" or value == "LeftArrow":
            return -1
        else:
            return 0 

    def transformaDisparo(self,value):
        if value:
            return 1
        else:
            return 0


    def __str__(self):
        return str(self.df.head(2))

if __name__ == "__main__":
    t = trainer()
    t.load_file('gameStates.csv')
    t.set_train_data()
    forest = DecisionTreeClassifier()
    #forest = RandomForestClassifier(n_estimators = 100,max_depth = 10)
    forest = forest.fit(t.train_data, t.target_data.astype(str))
    pickle.dump(forest, open('..\\randomclf.sav', 'wb'))