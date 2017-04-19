#Usar iron python para arrancar el servidor sockets
import socket
#import pandas as pd
import numpy as np
import pickle
from sklearn.ensemble import RandomForestClassifier

class ServerSocket:

    def __init__ (self,host,port):
    	self._HOST = 'localhost'
    	self._PORT = 8888              # Arbitrary non-privileged port
    	self._p = Predictor("randomForest.sav")
    	with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
	        
	        s.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1) #esto sirve para reiniciar el socket sin cerrarlo
	        s.bind((self._HOST, self._PORT))
	        print ('Socket bind complete')
	        s.listen(10)
	        print ('Socket now listening')
	        #Establece la conexión
	        conn, addr = s.accept()
	        with conn:
	        	conn.send(b"Conetado con exito")
	        	print('Connected by', addr)
	        	while True:
	        		try:
	        			data = conn.recv(1024)
	        			#Llamar a getResult
	        			text = data.decode("utf-8")
	        			retorno = self.getResult(text.split(','))
	        			retorno = str(( int(retorno[0][0]),int(retorno[0][1]),int(retorno[0][2])))
	        			conn.send(retorno.encode("utf-8"))
	        		except Exception as e:
	        			print("Desconexión: ",str(e))
	        			break


    def getResult(self,data):
    	return self._p.predict(self.preprocesar(data))

    def preprocesar(self,estado):
    	print(estado)
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
    	print(retorno)
    	return retorno

class Predictor:

	def __init__(self,filename):
		self._loaded_model = self.load_model(filename)

	def load_model(self,filename):
		return pickle.load(open(filename, 'rb'))

	def predict(self,string):
		return self._loaded_model.predict(string)


if __name__ == "__main__":
	s = ServerSocket('localhost',8888)