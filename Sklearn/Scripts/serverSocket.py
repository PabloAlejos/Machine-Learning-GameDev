#Usar iron python para arrancar el servidor sockets
import socket
import Entrenamiento


class ServerSocket:

    def __init__ (self,host,port):
        self._HOST = 'localhost'
        self._PORT = 8888              # Arbitrary non-privileged port
        with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
            s.bind((self._HOST, self._PORT))
            print ('Socket bind complete')
            s.listen(10)
            print ('Socket now listening')
            conn, addr = s.accept()
            with conn:
                print('Connected by', addr)
                while True:
                    data = conn.recv(1024)
                    if not data: break
                    #Llamar a getResult
                    self.getResult(data.decode("utf-8"))
                    #print(data.decode("utf-8")) #Importante el Decode

    def getResult(self,data):
        print(Entrenamiento.predecir(data))



if __name__ == "__main__":
    s = ServerSocket('localhost',8888)
    print("b")