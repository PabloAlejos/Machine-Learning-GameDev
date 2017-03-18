# Como es más fácil trabajar con atributos (quitarlos, ponerlos etc)
# vamos a usar pandas.
import pandas as pd
import pickle
from sklearn.ensemble import RandomForestClassifier


df=pd.read_csv('testEstado.csv', sep=',',header=None)
print(df.values[:10])
df.columns = ['Px', 'Py', 'E1x', 'E1y','E2x', 'E2y','E3x', 'E3y','E4x', 'E4y',"Class"]

train_data = df.drop(["Class"], axis=1)
# solo queremos los datos
train_data = train_data.values
target_data = df["Class"].values


forest = RandomForestClassifier(n_estimators=100)

forestDefinitivo = forest.fit(train_data, target_data)
#hace la predicción
#prediccion = forest.predict([2.20,-4.00,2.85,-0.57,2.82,1.49,0.93,3.55,2.49,5.60])
filename = 'randomForest.sav'
pickle.dump(forestDefinitivo, open(filename, 'wb'))