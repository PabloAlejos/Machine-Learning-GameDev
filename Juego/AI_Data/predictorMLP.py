import pickle
from sklearn.datasets import make_blobs
from sklearn.pipeline import Pipeline
from sklearn.preprocessing import StandardScaler
from sklearn.neural_network import MLPClassifier

class classifier():
    def __init__(self,pipeline):
        self.pipe = pickle.load(open(pipeline, 'rb'))
    
    def predictMLP(self,instancia):
        return self.procesar(self.pipe.predict(instancia)[0])

    def predict(self,instancia):
        pred = self.pipe.predict(instancia)[0]
        return pred
    
    def procesar(self,data):
        H = 0
        V = 0
        if data[0] == 1:
            V = 1
        elif data[1] == 1:
            V = -1
        
        if data[2] == 1:
            H = 1
        elif data[3] == 1:
            H = -1
        
        return (V,H,data[4])