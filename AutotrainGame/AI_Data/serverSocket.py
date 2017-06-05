import socket
import predictorMLP as p
import instanciaMLP as inst
from queue import Queue
from sklearn.ensemble import RandomForestClassifier

class ServerSocket:

    def __init__ (self,host,port,clf="model.sav"):
    	self._HOST = 'localhost'
    	self._PORT = 8888         # Arbitrary non-privileged port
    	#self._p = p.Predictor("randomForest.sav")
    	self._p = p.classifier(clf)
    	self._instancia = inst.instancia()
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
	        			arrayAtributos = data.decode("utf-8").split(',')
	        			retorno = self.getResult(arrayAtributos)
	        			retorno = str(retorno)
	        			conn.send(retorno.encode("utf-8"))
	        		except Exception as e:
	        			print("Desconexión: ",str(e))
	        			break

    def getResult(self,data):
    	self._instancia.encolar(data)
    	return self._p.predict(self._instancia.get())

if __name__ == "__main__":
	s = ServerSocket('localhost',8888)