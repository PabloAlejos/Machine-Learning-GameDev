import pickle
import instancia as i

class classifier():
    def __init__(self,filename):
        self.pipe = pickle.load(open(filename, 'rb'))
    
    def predict(self,instancia):
        return self._procesar(self.pipe.predict(instancia)[0])
    
    def _procesar(self,data):
        retorno = []
        for i in (data):
            if i > 0:
                retorno.append(1)
            elif i < 0:
                retorno.append(-1)
            else:
                retorno.append(0)
        return retorno


#Test
s = "168184233,0.00,0.00,0,999,999,999,999,-0.80,9.33,2,999.00,999.00,0,999.00,999.00,0,999.00,999.00,0,999.00,999.00,0,999.00,999.00,0,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,0,None,None,False".split(',')
inst = i.instancia()
c = classifier("..\\randomForest.sav")
inst.encolar(s)
print(c.predict(inst.get()))

