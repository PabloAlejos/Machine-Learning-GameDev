
# coding: utf-8

# ### Entrenamiento clase

# In[2]:

from sklearn.multioutput import MultiOutputClassifier
from sklearn.ensemble import RandomForestClassifier
from sklearn.datasets import make_classification
from sklearn import preprocessing
from sklearn.utils import shuffle
from sklearn.multiclass import OneVsRestClassifier
from sklearn.tree import DecisionTreeClassifier
import numpy as np
import pandas as pd
import pickle
import itertools
from collections import Counter


class trainer:
    def __init__ (self):
        self._df = pd.DataFrame()
        self.train_data = pd.DataFrame()
        self.target_data = pd.DataFrame()
        self.randomforest = RandomForestClassifier(n_estimators=100, random_state = 1, max_depth = 100)
        self.forest = DecisionTreeClassifier()
    
    #Carga el fichero con el nombre indicado y nombra las columnas
    def load_file(self,fileName):
        try:
            self._df = pd.read_csv(fileName, sep=',', header=None)
            self._df.columns = ['timeStamp','Px', 'Py', 'heat', 'Exp1','Eyp1','Exp2','Eyp2', 'Ex1', 'Ey1', 'Eh1', 'Ex2', 'Ey2', 'Eh2', 'Ex3', 'Ey3', 'Eh3', 'Ex4', 'Ey4','Eh4','Ex5', 'Ey5','Eh5','Ex6', 'Ey6','Eh6','ray1','ray2','ray3','ray4','ray5','ray6','ray7','ray8','ray9','ray10','ray11','ray12','ray13','ray14','ray15','ray16','ray17','ray18','ray19','ray20','ray21','ray22','ray23','ray24','ray25','ray26','ray27','score',"VKey","HKey","Shooting"]
            self._df[['Eh1','Eh2','Eh3','Eh4','Eh5','Eh6']] = self._df[['Eh1','Eh2','Eh3','Eh4','Eh5','Eh6']].astype(float)
        except Exception as e:
            print(e)
            raise

    #procesa las columnas correspondientes al trainda
    def set_train_data(self,train_columns,target_columns):
        self._set_target_data(target_columns)
        print("setting train_data")
        self.train_data = self._df[train_columns].copy()
        #print("\t power up info" )
        #powerUpInfo = t[['Exp1','Eyp1','Exp2','Eyp2']].copy()
        #powerUpInfo = powerUpInfo.apply(self._histo2DRow,axis = 1)
        #print("\t other info" )
        #otherInfo = t.drop(['Exp1','Eyp1','Exp2','Eyp2'], axis=1)
        #powerUpInfo = self._rename_trainData(powerUpInfo)
        #self.train_data = pd.concat([otherInfo,powerUpInfo],axis = 1)

        #Concatenar instancias
        print("concatenar instancias")
        df = pd.concat([self.train_data,self.target_data],axis = 1)
        df = self._concatenar_estados(df)
        self.train_data = df
        self.target_data = self.train_data[['VKey', 'HKey', 'Shooting']].copy()
        self.train_data = self.train_data.drop(['VKey', 'HKey', 'Shooting'],axis=1)
        
    #salva el dataframe con la info de train_data
    def save_data(self,fileName):
        print("saving data")
        
    
    #carga el dataframe con la info de train_data. Esto es para no procesar todo el dataframe cada vez.
    def load_data(self,fileName):
        print("loading data")
        
    
    def _set_target_data(self,columns):
        print("Setting target_data")
        self.target_data = self._df[columns].copy()
        self.target_data["VKey"] = self.target_data["VKey"].map(self._transformaEje)
        self.target_data["HKey"] = self.target_data["HKey"].map(self._transformaEje)
        self.target_data["Shooting"] = self.target_data["Shooting"].map(self._transformaDisparo)
    
    def _histo2DRow(self,row):
        x = row.filter(regex=('Ex'))
        y = row.filter(regex=('Ey'))
        w = row.filter(regex=('Eh'))
        
        xedges = np.linspace(-2,2,7)
        yedges = np.linspace(0,10,7)
        if len(w >0):
            H, xedges, yedges = np.histogram2d(x, y, bins=(xedges, yedges), weights=w)
        else:
            H, xedges, yedges = np.histogram2d(x, y, bins=(xedges, yedges))
        H = H.T  # Let each row list bins with common y range.
        return pd.Series(H.reshape(-1))

    def _concatenar_estados(self,dataframe):
        dfAnterior = dataframe.shift(1)
        dfAnterior.rename(columns=lambda x: str(x)+"-1", inplace=True)
        dfAnterior2 = dataframe.shift(2)
        dfAnterior2.rename(columns=lambda x: str(x)+"-2", inplace=True)
        dfAnterior3 = dataframe.shift(3)
        dfAnterior3.rename(columns=lambda x: str(x)+"-3", inplace=True)
        df = pd.concat([dfAnterior3,dfAnterior2,dfAnterior,dataframe],axis=1)
        df = df.fillna(999)
        return df
        
    def _rename_trainData(self,DataFrame):
        oldNames = list(range(len(DataFrame.columns)))
        newNames = ["("+str(round(x,2))+ ","+ str(round(y,2)) +")" for x, y in itertools.product(np.linspace(0,10,7),np.linspace(-2,2,7))]
        print("Renombrado")
        DataFrame = DataFrame.rename(columns=dict(zip(oldNames,newNames)))
        return DataFrame

    def train(self):
        #self.forest = self.randomforest.fit(self.train_data, self.target_data.astype(str))
        self.forest = self.forest.fit(self.train_data, self.target_data.astype(str))
        print(self.forest.get_params())
        #print(self.forest)
        
        
    def save_forest(self,fileName):
        pickle.dump(self.forest, open("..\\"+fileName, 'wb'))
    

    def _transformaEje(self,value):
        if value == "UpArrow" or value == "RightArrow":
            return 1
        elif value == "DownArrow" or value == "LeftArrow":
            return -1
        else:
            return 0
        
    def _transformaDisparo(self,value):
        if value:
            return 1
        else:
            return 0
        
    def __str__(self):
        return str(self._df)


t = trainer()
try:
    t.load_file('gameStates.csv')
    t.set_train_data(['Px', 'Py', 'heat', 'ray1','ray2','ray3','ray4','ray5','ray6','ray7','ray8','ray9','ray10','ray11','ray12','ray13','ray14','ray15','ray16','ray17','ray18','ray19','ray20','ray21','ray22','ray23','ray24','ray25','ray26','ray27'],["VKey","HKey","Shooting"])
    t.train()
    print(len(t.train_data.ix[10]))
    t.save_forest("Forest.sav")
    print("ok")
except:
    print("Fail")
