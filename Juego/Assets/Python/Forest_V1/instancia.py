import pandas as pd
from queue import Queue
import time
import numpy as np

class instancia:

	def __init__(self):
        #values 				        
		self.df0 = pd.DataFrame([999, 999, 999, 999,999,999,999, 999, 999, 999, 999, 999, 999, 999, 999, 999, 999, 999,999,999, 999,999,999])
		self.df1 = pd.DataFrame([999, 999, 999, 999,999,999,999, 999, 999, 999, 999, 999, 999, 999, 999, 999, 999, 999,999,999, 999,999,999])
		self.df2 = pd.DataFrame([999, 999, 999, 999,999,999,999, 999, 999, 999, 999, 999, 999, 999, 999, 999, 999, 999,999,999, 999,999,999])

	def encolar(self, estado):
		self.desencolar()
		self.df2 = pd.DataFrame(self.preprocesar(estado))
        
	def desencolar(self):
		self.df0 = self.df1.copy()
		self.df1 = self.df2.copy()

	def get(self):
		df = pd.concat([self.df0,self.df1,self.df2],axis=0)
		return(df.values.T[0])

	def preprocesar(self,estado):
		estado = list(map(float,estado))
		retorno = estado[1:8]
		x = [estado[8],estado[11],estado[14],estado[17],estado[20],estado[23]]
		y = [estado[9],estado[12],estado[15],estado[18],estado[21],estado[24]]
		w = [estado[10],estado[13],estado[16],estado[19],estado[22],estado[25]]
		xedges = np.linspace(-2,2,5)
		yedges = np.linspace(0,10,5)
		H, xedges, yedges = np.histogram2d(x, y, bins=(xedges, yedges), weights=w)
		H = H.T  # Let each row list bins with common y range
		histogramaPlano = (H.reshape(-1)).tolist()
		[retorno.append(i) for i in histogramaPlano]
		print(len(retorno))
		return pd.DataFrame(retorno)


if __name__ == "__main__":
	i = instancia()
	print(len(i.get()))
	t1 = time.time()
	data = ['1111', '2999', '999', '999', '999','999','999','999', '999', '999', '999', '999', '999', '999', '999', '999', '999', '999', '999','999','999', '999','999','999', '999','999']
	i.encolar(data)
	data = ['2222', '3999', '999', '999', '999','999','999','999', '999', '999', '999', '999', '999', '999', '999', '999', '999', '999', '999','999','999', '999','999','999', '999','999']
	i.encolar(data)
	data = ['3333', '4999', '999', '999', '999','999','999','999', '999', '999', '999', '999', '999', '999', '999', '999', '999', '999', '999','999','999', '999','999','999', '999','999']
	i.encolar(data)
	t2 = time.time()
	print(t2-t1)
	print(i.get())
