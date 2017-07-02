import pandas as pd
import numpy as np
from itertools import product

class instancia:

	# Dado que la instancia se compone de 4 instancias consecutivas en el tiempo, se
	# crea una instancia con datos "Basura" para los primeros estados
    def __init__(self,n=37,columns = ['timeStamp','Px', 'Py', 'heat', 'Exp1','Eyp1','Exp2','Eyp2', 'Ex1', 'Ey1', 'Eh1', 'Ex2', 'Ey2', 'Eh2', 'Ex3', 'Ey3', 'Eh3', 'Ex4', 'Ey4','Eh4','Ex5', 'Ey5','Eh5','Ex6', 'Ey6','Eh6','ray1','ray2','ray3','ray4','ray5','ray6','ray7','ray8','ray9','ray10','ray11','ray12','ray13','ray14','ray15','ray16','ray17','ray18','ray19','ray20','ray21','ray22','ray23','ray24','ray25','ray26','ray27','score',"VKey","HKey","Shooting"]):
        #values           
        self.columns = columns       
        self.df0 = pd.Series([000]*n)
        self.df1 = pd.Series([000]*n)
        self.df2 = pd.Series([000]*n)
        self.df3 = pd.Series([000]*n)

    # Para el concatenado de instancias he simulado una cola FIFO, de tal forma que cuando
    # llega una nueva instancia, esta se coloca al final y la más antígua (la primera) se 
    # elimina.
    def encolar(self, estado):
        self.desencolar()
        self.df3 = self.preprocesar(estado)
        
    # Elimina la primera instancia y recoloca las siguientes.
    def desencolar(self):
        self.df0 = self.df1.copy()
        self.df1 = self.df2.copy()
        self.df2 = self.df3.copy()

    # Devuelve la instancia completa sin las clases de la última.
    def get(self):
        print(len(self.df3))
        df = pd.concat([self.df0, self.df1, self.df2, self.df3.drop(["VKey","HKey","Shooting"])], axis=0)
        print(len(self.df3))
        return df

    # Cuando llega una nueva instancia se hace un preprocesado antes de encolarla. Este preprocesado
    # nos permite elegir qué etiquetas queremos utilizar en la instancia y hacer algunas modificaciones
    # en los datos para que el modelo los pueda procesar
    # Devuelve una instancia procesada
    def preprocesar(self,estado):
        df = pd.Series(estado,index=self.columns)
        
        retorno = df[['Px', 'Py', 'heat']].copy()
        rays = df[['ray1','ray2','ray3','ray4','ray5','ray6','ray7','ray8','ray9','ray10','ray11','ray12','ray13','ray14','ray15','ray16','ray17','ray18','ray19','ray20','ray21','ray22','ray23','ray24','ray25','ray26','ray27']].copy()
        
        powerUpInfo = df[['Exp1','Eyp1','Exp2','Eyp2']].copy()
        #powerUpInfo = pd.Series(self.histo2DRow(powerUpInfo))
        
        targetData = pd.Series(index=["VKey","HKey","Shooting"])
        targetData["VKey"] = self.transformaEje(df['VKey'])
        targetData["HKey"] = self.transformaEje(df['HKey'])
        targetData["Shooting"] = self.transformaEje(df['Shooting'])

        retorno = pd.concat([retorno,powerUpInfo,rays,targetData])
        
        return retorno

    # Este método (no se utiliza en la vesión final), nos permite crear los HeatMap descritos en
    # la memoria. De este manera los enemigos quedan localizados en un punto concreto del mapa
    # Devuelve unn mapa con el número de enemigos en cada sección
    def histo2DRow(self,row):
        x = row.filter(regex=('Ex'))
        y = row.filter(regex=('Ey'))

        xedges = np.linspace(-2,2,7)
        yedges = np.linspace(0,10,7)
        
        H, xedges, yedges = np.histogram2d(x, y, bins=(xedges, yedges))
        H = H.T  # Let each row list bins with common y range.
        #print(H.reshape(-1))
        return H.reshape(1,-1).T.astype(float)

    # transforma el texto de la instancia a valor numérico
    def transformaEje(self,value):
        if value == "UpArrow" or value == "RightArrow":
            return 1
        elif value == "DownArrow" or value == "LeftArrow":
            return -1
        else:
            return 0
    # transforma el texto de la instancia a valor numérico
    def transformaDisparo(self,value):
        if value:
            return 1
        else:
            return 0

#--- Test ---#
#i = instancia()
#i.encolar("130165221,0.51,1.91,4,999,999,999,999,-2.46,8.66,2,0.38,5.68,2,0.98,6.02,2,999.00,999.00,0,999.00,999.00,0,999.00,999.00,0,999.00,999.00,999.00,999.00,999.00,7.17,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,24,None,RightArrow,False".split(','))
#i.encolar("130165221,0.51,1.91,4,999,999,999,999,-2.46,8.66,2,0.38,5.68,2,0.98,6.02,2,999.00,999.00,0,999.00,999.00,0,999.00,999.00,0,999.00,999.00,999.00,999.00,999.00,7.17,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,24,None,RightArrow,False".split(','))
#i.encolar("130165221,0.51,1.91,4,999,999,999,999,-2.46,8.66,2,0.38,5.68,2,0.98,6.02,2,999.00,999.00,0,999.00,999.00,0,999.00,999.00,0,999.00,999.00,999.00,999.00,999.00,7.17,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,24,None,RightArrow,False".split(','))
#print(len(i.get()))