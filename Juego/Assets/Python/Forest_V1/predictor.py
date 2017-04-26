import pickle

class Predictor:

	def __init__(self,filename):
		self._loaded_model = self.load_model(filename)

	def load_model(self,filename):
		return pickle.load(open(filename, 'rb'))

	def predict(self,string):
		return self._loaded_model.predict(string)