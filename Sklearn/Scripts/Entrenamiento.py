# Como es más fácil trabajar con atributos (quitarlos, ponerlos etc)
# vamos a usar pandas.
import pandas as pd
from sklearn.ensemble import RandomForestClassifier

def CargarDatos():
	df = pd.read_csv('testEstado.csv',sep=',',header=None)
	print(df.values[:10])
	# lo más fácil es generar csv sin cabecera y luego añadirsela, así
	# les podemos concatenar etc más fácilmente
	df.columns = ['Px', 'Py', 'E1x', 'E1y','E2x', 'E2y','E3x', 'E3y','E4x', 'E4y',"Class"]

def Entrenar():
	train_data = df.drop(["Class"], axis=1)
	print(train_data.head())
	# solo queremos los datos
	train_data = train_data.values
	target_data = df["Class"].values
	forest = RandomForestClassifier(n_estimators = 100)
	# entrena
	forest = forest.fit(train_data, target_data)

def predecir(datos):
	# hace la predicción
	forest = RandomForestClassifier(n_estimators = 100)
	forest = forest.fit(train_data, target_data)
	prediccion = forest.predict([datos])
	return(prediccion)

