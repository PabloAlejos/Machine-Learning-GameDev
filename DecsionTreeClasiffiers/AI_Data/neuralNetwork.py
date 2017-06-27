import pandas as pd # Librería para el uso de dataFrames
import instanciaMLP as inst # Mi tipo de instancia
import pickle # Para la serialización
#import pythonBot #Mi bot de telegram
import printProgressBar as progressBar


class agente():

    def __init__ (self):
        
        #self.loadData("gameStates")
        #print(self.df.head(5))
        #print(self.df.shape)
        self.loadRawData("..\\gameStates.csv")
        #self.saveData("gameStates")

    def loadRawData(self,fileName):
        self._instancia = inst.instancia()
        print('Loading data')
        rawData = pd.read_csv(fileName, sep=',', header=None)
        self.df = pd.DataFrame(columns=self._instancia.get_full_instance().columns)
        rows,cols = rawData.shape
        print(rows,cols)
        print("preprocesado")
        for i in range(0,10):
            self._instancia.encolar(rawData.iloc[i].tolist())
            print(self._instancia.get_full_instance())
            self.df.loc[i] = self._instancia.get_full_instance()
            #progressBar.printProgressBar(i,10)
        print(self.df.head(100))
        print(self.df.shape)
        print('Loaded')

    def saveData(self,fileName):
        self.df.to_pickle(fileName)

    def loadData(self,fileName):
        self.df = pickle.load(open(fileName, 'rb'))





#try:
#   pythonBot.send_message("Hola!", 4191538)
#except Exception as e:
#   print('Error al comunicar con Telegram ', str(e))
a = agente()