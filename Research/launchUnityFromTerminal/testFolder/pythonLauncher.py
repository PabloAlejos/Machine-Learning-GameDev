import os
import pandas as pd
import time
import numpy as np


#os.system("..\\test.exe")
scores = pd.read_csv("myData.csv", sep=',', header=None)
print(np.mean(scores.values))
