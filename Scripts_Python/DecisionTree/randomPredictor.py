from random import randint
import instancia as i

class classifier():

	# Este clasificador es meramente experimental, no llama a ninguna función
	# "predict", simplemente genera unos valores que simulan una predicción real
	# a diferencia de que estos valores serán completamente aleatorios.
    def __init__(self):
        self.pred = []
    
    def predict(self,instancia):
        self._vaciar()
        self.pred.append(randint(-1, 1))
        self.pred.append(randint(-1, 1))
        self.pred.append(randint(0,1))
        return self.pred
    
    # deja la predicción vacía para poder crear una nueva.
    def _vaciar(self):
        self.pred = []


#Test
s = "168184233,0.00,0.00,0,999,999,999,999,-0.80,9.33,2,999.00,999.00,0,999.00,999.00,0,999.00,999.00,0,999.00,999.00,0,999.00,999.00,0,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,0,None,None,False".split(',')
inst = i.instancia()
c = classifier()
inst.encolar(s)
print(c.predict(inst.get()))

