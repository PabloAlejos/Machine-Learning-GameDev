import pickle


class classifier():
    def __init__(self,pipeline="model.sav"):
        self.pipe = pickle.load(open(pipeline, 'rb'))
    
    def predict(self,instancia):
        return self._procesar(self.pipe.predict(instancia)[0])
    
    def _procesar(self,data):
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


#Test
