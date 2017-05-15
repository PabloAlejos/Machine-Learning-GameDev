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

class trainter:

	def __init__ (self):
		self.df = pd.DataFrame()
		self.train_data = pd.DataFrame()
		self.target_data = pd.DataFrame()

	#Carga el fichero con el nombre indicado y nombra las columnas
	def load_file(self,fileName):
		self.df = pd.read_csv(fileName, sep=',', header=None)
		self.df.columns = ['timeStamp', 'Px', 'Py', 'heat', 'Exp1','Eyp1','Exp2','Eyp2', 'Ex1', 'Ey1', 'Eh1', 'Ex2', 'Ey2', 'Eh2', 'Ex3', 'Ey3', 'Eh3', 'Ex4', 'Ey4','Eh4','Ex5', 'Ey5','Eh5','Ex6', 'Ey6','Eh6',"VKey","HKey","Shooting"]
		self.df[['Eh1','Eh2','Eh3','Eh4','Eh5','Eh6']] = self.df[['Eh1','Eh2','Eh3','Eh4','Eh5','Eh6']].astype(float) 
	
	#procesa las columnas correspondientes al traindata
	def set_train_data(self,columns):
		t = self.df[columns].copy()
		enemiesInfo = t[['Ex1', 'Ey1', 'Ex2', 'Ey2', 'Ex3', 'Ey3','Ex4', 'Ey4','Ex5', 'Ey5','Ex6', 'Ey6','Eh1','Eh2','Eh3','Eh4','Eh5','Eh6']].copy()
		otherInfo = enemiesInfo.drop(['Ex1', 'Ey1', 'Ex2', 'Ey2', 'Ex3', 'Ey3','Ex4', 'Ey4','Ex5', 'Ey5','Ex6', 'Ey6','Eh1','Eh2','Eh3','Eh4','Eh5','Eh6'], axis =1)
		enemiesInfo = enemiesInfo.apply(self.histo2DRow, axis = 1)
		print(pd.concat([otherInfo,enemiesInfo]).head(2))

	def save_train_data(self,fileName):
		np.save(fileName,self.train_data)

	def load_train_data(self,fileName):
		self.train_data = np.load(fileName)

	def set_target_data(self,columns):
		self.target_data = self.df[columns].copy()

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

	def renombrado(self,DataFrame,ejex,ejey):
		pass


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


	def __str__(self):

		return str(self.df.head(2))

if __name__ == "__main__":
	t = trainter()
	t.load_file('gameStates.csv')
	t.set_train_data(['Px', 'Py', 'heat', 'Exp1','Eyp1','Exp2','Eyp2', 'Ex1', 'Ey1', 'Eh1', 'Ex2', 'Ey2', 'Eh2', 'Ex3', 'Ey3', 'Eh3', 'Ex4', 'Ey4','Eh4','Ex5', 'Ey5','Eh5','Ex6', 'Ey6','Eh6'])
	t.set_target_data(["VKey","HKey","Shooting"])
	print(str(t))