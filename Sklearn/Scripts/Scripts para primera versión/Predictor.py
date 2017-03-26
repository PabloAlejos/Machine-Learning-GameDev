import pickle


def load_model(filename):
	return pickle.load(open(filename, 'rb'))

def predict(string):
	return loaded_model.predict([string])


loaded_model = load_model("randomForest.sav")

prueba = [
	[0.00,-4.00,-1.34,6.00,999,999,999,999,999,999],
	[-1.70,-4.00,-1.90,-0.45,0.83,1.60,1.56,3.66,2.85,5.67],
	[-2.00,-4.00,-2.35,-0.89,-1.95,1.16,2.16,3.22,-0.67,5.27]
	]

for t in prueba:
	print(predict(t))