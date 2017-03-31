#Usar iron python para arrancar el servidor sockets
import socket
import pandas as pd
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
	        		data = conn.recv(1024)
	        		#Llamar a getResult
	        		text = data.decode("utf-8")
	        		retorno = self.getResult(text.split(','))
	        		
	        		retorno = str(( int(retorno[0][0]),int(retorno[0][1]),int(retorno[0][2])))
	        		conn.send(retorno.encode("utf-8"))
	        		#print(str(self.getResult(text.split(','))))
	        		#print(data.decode("utf-8")) #Importante el Decode 


    def getResult(self,data):
    	return self._p.predict([data])
        #print(data)

class Predictor:

	def __init__(self,filename):
		self._loaded_model = self.load_model(filename)

	def load_model(self,filename):
		return pickle.load(open(filename, 'rb'))

	def predict(self,string):
		return self._loaded_model.predict(string)


if __name__ == "__main__":
	#p = Predictor("randomForest.sav")
	#n = p.predict([[86171331,2.26,2.10,4.493064,2.047016,6.213881,999,999,0.52,9.29,5,-0.80,7.79,3,-0.72,8.24,5,-2.19,7.78,3,999,999,0,999,999,0]])
	#print(n)
	#print(n[0][0],n[0][1],n[0][2])

	s = ServerSocket('localhost',8888)