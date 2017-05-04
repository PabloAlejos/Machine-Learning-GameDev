import pandas as pd
from queue import Queue
import numpy as np

class instancia:

	def __init__(self):
        #values 				        
		self.df0 = pd.DataFrame([111]*85)
		self.df1 = pd.DataFrame([222]*85)
		self.df2 = pd.DataFrame([333]*85)

	def encolar(self, estado):
		self.desencolar()
		self.df2 = self.preprocesar(estado)
        
	def desencolar(self):
		self.df0 = self.df1.copy()
		self.df1 = self.df2.copy()

	def get(self):
		df = pd.concat([self.df0, self.df1, self.df2],axis=0)
		return(df.values.T[0][:255])

	def preprocesar(self,estado):
		target = estado[33:]
		estado = list(map(float,estado[:34]))
		xedges = np.linspace(-2,2,7)
		yedges = np.linspace(0,10,7)
		
		retorno = estado[1:12]

		pupx = [estado[12],estado[14]]
		pupy = [estado[13],estado[15]]
		pupH, xedges, yedges = np.histogram2d(pupx, pupy, bins=(xedges, yedges))
		pupH = pupH.T  # Let each row list bins with common y range
		histogramaPlano = (pupH.reshape(-1)).tolist()
		[retorno.append(i) for i in histogramaPlano]

		x = [estado[16],estado[19],estado[22],estado[25],estado[28],estado[31]]
		y = [estado[17],estado[20],estado[23],estado[26],estado[29],estado[32]]
		w = [estado[18],estado[21],estado[24],estado[27],estado[30],estado[33]]
		
		H, xedges, yedges = np.histogram2d(x, y, bins=(xedges, yedges), weights=w)
		H = H.T  # Let each row list bins with common y range
		histogramaPlano = (H.reshape(-1)).tolist()
		[retorno.append(i) for i in histogramaPlano]
		retorno.append(self.transformaEje(target[0]))
		retorno.append(self.transformaEje(target[1]))
		retorno.append(self.transformaDisparo(target[2]))
		return pd.DataFrame(retorno).astype(float)

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
	i = instancia();
	print(i.get())
	i.encolar("122202737,-1.09,1.79,4,999,999,999,999,2.28,7.96,2,-1.61,6.90,2,-1.10,5.47,2,999,999,0,999,999,0,999,999,0,0,0,0,0,0,0,0,3,1,-1,True".split(','))
	i.encolar("122202738,-1.09,1.79,4,999,999,999,999,2.28,7.96,2,-1.61,6.90,2,-1.10,5.47,2,999,999,0,999,999,0,999,999,0,0,0,0,0,0,0,0,2,1,0,False".split(','))
	i.encolar("122202739,-1.09,1.79,4,999,999,999,999,2.28,7.96,2,-1.61,6.90,2,-1.10,5.47,2,999,999,0,999,999,0,999,999,0,0,0,0,0,0,0,0,1,1,1,True".split(','))

	print(i.get())
	n = 0
	for z in i.get():
		n += 1
	print(n)
