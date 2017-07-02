# SpaceShooterAI

## Resumen
El proyecto tiene como propósito la creación de un videojuego y,posteriormente, el desarrollo de una serie de agentes inteligentes que seancapaces de jugar al mismo. Mediante el análisis y explotación de técnicasde aprendizaje máquina y la utilización de la minería de datos, seremoscapaces de dotar al agente de la capacidad de descubrir las reglas deljuego a partir de un conjunto de datos y deberá ser capaz de jugar porimitación a partir de lo aprendido. Para el desarrollo del videojuego seutiliza Unity3D, un entorno de desarrollo de Videojuegos. Los agentesinteligentes se implementan en Python, un lenguaje de programaciónque, a día de hoy, está entre los tres más usados mundialmente.

## Abstract
The purpose of this project is the creation of a video game, and thenfollows the development of a series of intelligent agents that are able tointeract with the same. The agents will be able to discover the rules ofthe games by using Machine Learning and Data Mining techniques on adataset obtained from a human player, and hence it will be able to learnby imitation. The development of the game is done with Unity3D, a vi-deo game framework. The intelligent agents are implemented in Python,a programming language that, to date, is among the three most usedworldwide.


## Objetivos
* El Objetivo del proyecto es diseñar y desarrollar un videojuego de tipo arcade. Para la implementación del juego que sirve como base para este proyecto es necesario planear las mecánicas de juego, además de diseñar los modelos del jugador, los enemigos y otros elementos gráficos. Esta tarea también incluye el diseño de la interfaz, banda sonora y efectos de sonido. 
    
* Diseñar e implenentar un sistema inteligente que posteriormente será utilizado para interactuar con el videojuego creado. Este proceso de divide en:  
   
    * Establecer una comunicación entre el juego y el modelo del sistema inteligente. Dado que el juego y el script que hace uso del modelo serán implementados en diferente lenguaje de programación, se debe establecer un mecanismo de intercambio de datos para la comunicación.  
       
    * Diseñar el conjunto de datos que posteriormente servirá para el entrenamiento del modelo. Al ser el punto de partida el aprendizaje por imitación, necesitaremos un gran volumen de datos de los cuales seleccionaremos todos o solo los mas representativos para el entrenamiento.  
       
    * Implementar, haciendo uso de las librerías como scikit-learn(minería de datos), DEAP(algoritmos evolutivos) y Pandas y NumPy(procesamiento de datos a bajo nivel) el simtema inteligente que jugará al videojuego.



![alt text](https://github.com/PabloAlejos/Machine-Learning-GameDev/blob/master/Docs/img/UnityIfaz.PNG)
