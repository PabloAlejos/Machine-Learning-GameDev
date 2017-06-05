import pandas as pd
from itertools import product

class instancia:

    def __init__(self,columns = ['timeStamp','Px', 'Py', 'heat', 'EnemyRay1','EnemyRay2','EnemyRay3','EnemyRay4', 'EnemyRay5', 'EnemyRay6', 'EnemyRay7', 'EnemyRay8', 'EnemyRay9', 'EnemyRay10', 'EnemyRay11', 'EnemyRay12', 'EnemyRay13', 'EnemyRay14', 'EnemyRay15','EnemyRay16','EnemyRay17', 'EnemyRay18','EnemyRay19','EnemyRay20', 'EnemyRay21','EnemyRay22','EnemyRay23','EnemyRay24','EnemyRay25','EnemyRay26','EnemyRay27','ray1','ray2','ray3','ray4','ray5','ray6','ray7','ray8','ray9','ray10','ray11','ray12','ray13','ray14','ray15','ray16','ray17','ray18','ray19','ray20','ray21','ray22','ray23','ray24','ray25','ray26','ray27','score',"VKey","HKey","Shooting"]):

        #values           
        self.columns = columns       
        n = 62
        self.df0 = pd.Series([000]*n)
        self.df1 = pd.Series([000]*n)
        self.df2 = pd.Series([000]*n)
        self.df3 = pd.Series([000]*n)


    def encolar(self, estado):
        self.desencolar()
        self.df3 = self.preprocesar(estado)
        
    def desencolar(self):
        self.df0 = self.df1.copy()
        self.df1 = self.df2.copy()
        self.df2 = self.df3.copy()

    def get(self):
        df = pd.concat([self.df0, self.df1, self.df2, self.df3.drop(["VKey","HKey","Shooting"])], axis=0)
        return df

    def preprocesar(self,estado):
        df = pd.Series(estado,index=self.columns)
        
        retorno = df.drop(["VKey","HKey","Shooting"]).copy()

        target_data = pd.Series(index=["VKey","HKey","Shooting"])
        target_data["VKey"] = self.transformaEje(df["VKey"])
        target_data["HKey"] = self.transformaEje(df["HKey"])
        target_data["Shooting"] = self.transformaEje(df["Shooting"])
        retorno = pd.concat([retorno,target_data],axis=0)
        
        return retorno

    def transformaEje(self,value):
        if value == "UpArrow" or value == "RightArrow":
            return 1
        elif value == "DownArrow" or value == "LeftArrow":
            return -1
        else:
            return 0

    def transformaDisparo(self,value):
        if value:
            return 1
        else:
            return 0


i = instancia()
i.encolar(("147111651,1.55,4.06,2.443611,4.11,4.08,999.00,3.79,999.00,3.89,999.00,4.78,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,4.61,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,15,UpArrow,RightArrow,True").split(","))
i.encolar(("147111651,1.55,4.06,2.443611,4.11,4.08,999.00,3.79,999.00,3.89,999.00,4.78,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,4.61,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,15,UpArrow,RightArrow,True").split(","))
i.encolar(("147111651,1.55,4.06,2.443611,4.11,4.08,999.00,3.79,999.00,3.89,999.00,4.78,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,4.61,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,15,UpArrow,RightArrow,True").split(","))
i.encolar(("147111651,1.55,4.06,2.443611,4.11,4.08,999.00,3.79,999.00,3.89,999.00,4.78,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,4.61,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,999.00,15,UpArrow,RightArrow,True").split(","))
print(len(i.get()))