import pickle
from sklearn.datasets import make_blobs
from sklearn.pipeline import Pipeline
from sklearn.preprocessing import StandardScaler
from sklearn.neural_network import MLPClassifier

class classifier():
    def __init__(self,pipeline="pipe.sav"):
        self.pipe = pickle.load(open(pipeline, 'rb'))
    
    def predict(self,instancia):
        return self.procesar(self.pipe.predict(instancia)[0])
    
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

    def size(self):
        if isinstance(self.pipe,Pipeline):
            return [coef.size for coef in self.pipe.named_steps['clf'].coefs_]
        elif isinstance(self.pipe,MLPClassifier):
            return [coef.size for coef in self.pipe.coefs_]

    def steps(self):
        stp = []
        for s in self.pipe.named_steps:
            stp.append(s)
        return stp

    def __type__(self):
        return type(self.pipe)

#Test unitarios
c = classifier()
print(c.size()[1])
