#Usar iron python para arrancar el servidor sockets
import socket
import pandas as pd
import pickle
from sklearn.ensemble import RandomForestClassifier

class ServerSocket:

    def __init__ (self,host,port):
    	self._HOST = 'localhost'
    	self._PORT = 8888              # Arbitrary non-privileged port
    	self._p = Predictor("Prueba.sav")
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
	        		if not data: break
	        		#Llamar a getResult
	        		text = data.decode("utf-8")
	        		print(text)
	        		retorno = (str(self.getResult(text.split(','))))
	        		conn.send(retorno.encode("utf-8"))
	        		#print(data.decode("utf-8")) #Importante el Decode 


    def getResult(self,data):
    	return self._p.predict(data)
        #print(Entrenamiento.predecir(data))

class Trainer:

	def __init__(self,data_file):

		self._dataFile = pd.read_csv(data_file, sep=',',header=None)
		self._dataFile.columns = ['Px', 'Py', 'E1x', 'E1y', 'E2x', 'E2y', 'E3x', 'E3y', 'E4x', 'E4y', "Class"]

	def train(self):
		self._train_data = self._dataFile.drop(["Class"], axis=1)
		# solo queremos los datos
		self._train_data = self._train_data.values
		self._target_data = self._dataFile["Class"].values
		self._forest = RandomForestClassifier(n_estimators=100)
		self._forestDefinitivo = self._forest.fit(self._train_data, self._target_data)

	def saveFile(self, filename):
		pickle.dump(self._forestDefinitivo, open(filename, 'wb'))


class Predictor:

	def __init__(self,filename):
		self._loaded_model = self.load_model(filename)

	def load_model(self,filename):
		return pickle.load(open(filename, 'rb'))

	def predict(self,string):
		return self._loaded_model.predict([string])


if __name__ == "__main__":
    s = ServerSocket('localhost',8888)
    print("Bye")