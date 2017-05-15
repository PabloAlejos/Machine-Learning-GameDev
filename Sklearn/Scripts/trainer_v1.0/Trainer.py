
# coding: utf-8

# ### Entrenamiento clase

# In[2]:

from sklearn.multioutput import MultiOutputClassifier
from sklearn.ensemble import RandomForestClassifier
from sklearn.datasets import make_classification
from sklearn import preprocessing
from sklearn.utils import shuffle
from sklearn.multiclass import OneVsRestClassifier
#from sklearn.svm import LinearSVC
from sklearn.tree import DecisionTreeClassifier
import numpy as np
import pandas as pd
import pickle
import itertools
from collections import Counter


class trainer:
    def __init__ (self):
        self.df = pd.DataFrame()
        self.train_data = pd.DataFrame()
        self.target_data = pd.DataFrame()
        self.randomforest = RandomForestClassifier(n_estimators=100, random_state=1)
        self.forest = DecisionTreeClassifier()
    
    #Carga el fichero con el nombre indicado y nombra las columnas
    def load_file(self,fileName):
        self.df = pd.read_csv(fileName, sep=',', header=None)
        self.df = self.df
        self.df.columns = ['timeStamp', 'Px', 'Py', 'heat', 'Exp1','Eyp1','Exp2','Eyp2', 'Ex1', 'Ey1', 'Eh1', 'Ex2', 'Ey2', 'Eh2', 'Ex3', 'Ey3', 'Eh3', 'Ex4', 'Ey4','Eh4','Ex5', 'Ey5','Eh5','Ex6', 'Ey6','Eh6','se0','se1','se2','se3','se4','se5','se6','se7',"VKey","HKey","Shooting"]
        self.df[['Eh1','Eh2','Eh3','Eh4','Eh5','Eh6']] = self.df[['Eh1','Eh2','Eh3','Eh4','Eh5','Eh6']].astype(float) 
    
    #procesa las columnas correspondientes al trainda
    def set_train_data(self,columns):
        print("setting train_data")
        t = self.df[columns].copy()
        print("\t enemies info" )
        enemiesInfo = t[['Ex1', 'Ey1', 'Ex2', 'Ey2', 'Ex3', 'Ey3','Ex4', 'Ey4','Ex5', 'Ey5','Ex6', 'Ey6','Eh1','Eh2','Eh3','Eh4','Eh5','Eh6']].copy()
        enemiesInfo = enemiesInfo.apply(self.histo2DRow, axis = 1)
        print("\t power up info" )
        powerUpInfo = t[['Exp1','Eyp1','Exp2','Eyp2']].copy()
        powerUpInfo = powerUpInfo.apply(self.histo2DRow,axis = 1)
        print("\t other info" )
        otherInfo = t.drop(['Ex1', 'Ey1', 'Ex2', 'Ey2', 'Ex3', 'Ey3','Ex4', 'Ey4','Ex5', 'Ey5','Ex6', 'Ey6','Eh1','Eh2','Eh3','Eh4','Eh5','Eh6','Exp1','Eyp1','Exp2','Eyp2'], axis=1)
        powerUpInfo = self.rename_trainData(powerUpInfo)
        enemiesInfo = self.rename_trainData(enemiesInfo)
        self.train_data = pd.concat([otherInfo,powerUpInfo,enemiesInfo],axis = 1)
        
        
        #Concatenar instancias
        print("concatenar instancias")
        df = pd.concat([self.train_data,self.target_data],axis = 1)
        df = self.concatenar_estados(df)
        df = self.balance_data(df)
        self.train_data = df
        self.target_data = self.train_data[['VKey', 'HKey', 'Shooting']].copy()
        self.train_data = self.train_data.drop(['timeStamp','timeStamp-1','timeStamp-2','VKey', 'HKey', 'Shooting'],axis=1)
        print(len(self.train_data.ix[10]))
        
    #salva el dataframe con la info de train_data
    def save_data(self,fileName):
        print("saving data")
        
    
    #carga el dataframe con la info de train_data. Esto es para no procesar todo el dataframe cada vez.
    def load_data(self,fileName):
        print("loading data")
        
    
    def set_target_data(self,columns):
        print("Setting target_data")
        self.target_data = self.df[columns].copy()
        self.target_data["VKey"] = self.target_data["VKey"].map(self.transformaEje)
        self.target_data["HKey"] = self.target_data["HKey"].map(self.transformaEje)
        self.target_data["Shooting"] = self.target_data["Shooting"].map(self.transformaDisparo)
    
    def histo2DRow(self,row):
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

    def concatenar_estados(self,dataframe):
        dfAnterior = dataframe.shift(1)
        dfAnterior.rename(columns=lambda x: str(x)+"-1", inplace=True)
        dfAnterior2 = dataframe.shift(2)
        dfAnterior2.rename(columns=lambda x: str(x)+"-2", inplace=True)
        df = pd.concat([dfAnterior2,dfAnterior,dataframe],axis=1)
        df = df.fillna(999)
        return df
        
    def rename_trainData(self,DataFrame):
        oldNames = list(range(len(DataFrame.columns)))
        newNames = ["("+str(round(x,2))+ ","+ str(round(y,2)) +")" for x, y in itertools.product(np.linspace(0,10,7),np.linspace(-2,2,7))]
        print("Renombrado")
        DataFrame = DataFrame.rename(columns=dict(zip(oldNames,newNames)))
        return DataFrame
    
    
    def balance_data(self,dataframe):
        temp_00to25 = pd.DataFrame()
        temp_25to50 = pd.DataFrame()
        temp_50to75 = pd.DataFrame()
        temp_75to100 = pd.DataFrame()

        
        temp_00to25 = dataframe[dataframe['heat'].between(0, 25, inclusive=True)]
        temp_25to50 = dataframe[dataframe['heat'].between(26, 50, inclusive=True)]
        temp_50to75 = dataframe[dataframe['heat'].between(51, 75, inclusive=True)]
        temp_75to100 = dataframe[dataframe['heat'].between(76, 100, inclusive=True)]
        
        min_temp = min(len(temp_00to25),len(temp_25to50),len(temp_50to75),len(temp_75to100))
        
        balanced_temp = pd.concat([temp_00to25[:min_temp],temp_25to50[:min_temp],temp_50to75[:min_temp],temp_75to100[:min_temp]])
        
        
        print(len(temp_00to25),len(temp_25to50),len(temp_50to75),len(temp_75to100))
        print(balanced_temp.shape)
        

        #balanced_shoot = balanced_temp[balanced_temp['Shooting'] == 1]
        #balanced_no_shoot = balanced_temp[balanced_temp['Shooting'] == 0]
        #print(balanced_shoot.shape,balanced_no_shoot.shape)
        #min_shoot = min(len(balanced_shoot),len(balanced_no_shoot))
        #balanced_result = pd.concat([balanced_shoot[:min_shoot],balanced_no_shoot[:min_shoot]])
        return balanced_temp.sort_values(['timeStamp'],ascending=1) 
    
    def train(self):
        self.forest = self.randomforest.fit(self.train_data, self.target_data.astype(str))
        #self.forest = self.forest.fit(self.train_data, self.target_data.astype(str))
        
        
    def save_forest(self,fileName):
        pickle.dump(self.forest, open(fileName, 'wb'))
    
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
        return str(self.df)



t = trainer()
t.load_file('gameStates.csv')
t.set_target_data(["VKey","HKey","Shooting"])
t.set_train_data(['timeStamp','Px', 'Py', 'heat', 'Exp1','Eyp1','Exp2','Eyp2', 'Ex1', 'Ey1', 'Eh1', 'Ex2', 'Ey2', 'Eh2', 'Ex3', 'Ey3', 'Eh3', 'Ex4', 'Ey4','Eh4','Ex5', 'Ey5','Eh5','Ex6','Ey6','Eh6','se0','se1','se2','se3','se4','se5','se6','se7'])
t.train()

t.save_forest("randomForest.sav")
print("ok")

t.target_data.head(20)

