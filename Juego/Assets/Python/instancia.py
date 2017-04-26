import pandas as pd
from queue import Queue
import time
import numpy as np

class instancia:

	def __init__(self):
        #values 				        
		self.df0 = pd.DataFrame([999]*229)
		self.df1 = pd.DataFrame([999]*229)
		self.df2 = pd.DataFrame([999]*229)

	def encolar(self, estado):
		self.desencolar()
		self.df2 = self.preprocesar(estado)
        
	def desencolar(self):
		self.df0 = self.df1.copy()
		self.df1 = self.df2.copy()

	def get(self):
		df = pd.concat([self.df0,self.df1,self.df2],axis=0)
		return(df.values.T[0][:687])

	def preprocesar(self,estado):
		target = estado[26:]
		estado = list(map(float,estado[:26]))
		xedges = np.linspace(-2,2,9)
		yedges = np.linspace(0,10,15)
		
		retorno = estado[1:4]

		pupx = [estado[4],estado[6]]
		pupy = [estado[5],estado[7]]
		pupH, xedges, yedges = np.histogram2d(pupx, pupy, bins=(xedges, yedges))
		pupH = pupH.T  # Let each row list bins with common y range
		histogramaPlano = (pupH.reshape(-1)).tolist()
		[retorno.append(i) for i in histogramaPlano]

		x = [estado[8],estado[11],estado[14],estado[17],estado[20],estado[23]]
		y = [estado[9],estado[12],estado[15],estado[18],estado[21],estado[24]]
		w = [estado[10],estado[13],estado[16],estado[19],estado[22],estado[25]]
		
		H, xedges, yedges = np.histogram2d(x, y, bins=(xedges, yedges), weights=w)
		H = H.T  # Let each row list bins with common y range
		histogramaPlano = (H.reshape(-1)).tolist()
		[retorno.append(i) for i in histogramaPlano]
		retorno.append(self.transformaEje(target[0]))
		retorno.append(self.transformaEje(target[1]))
		retorno.append(self.transformaDisparo(target[2]))
		print(len(retorno))
		return pd.DataFrame(retorno).astype(int)

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

if __name__ == "__main__":
	i = instancia()
	print(len(i.get()))
	t1 = time.time()
	data = [1102067,-1.60,0.70,27,999,999,999,999,1.45,8.39,2,2.06,8.10,3,-0.81,5.92,1,-1.49,4.03,2,999,999,0,999,999,0,'None','LeftArrow','False']
	print(len(data))
	i.encolar(data)
	data = ['2222', '3999', '999', '999', '999','999','999','999', '999', '999', '999', '999', '999', '999', '999', '999', '999', '999', '999','999','999', '999','999','999', '999','999',0,1,0]
	i.encolar(data)
	data = ['3333', '4999', '999', '999', '999','999','999','999', '999', '999', '999', '999', '999', '999', '999', '999', '999', '999', '999','999','999', '999','999','999', '999','999',0,1,1]
	i.encolar(data)
	t2 = time.time()
	print(t2-t1)
	print(len(i.get()))
