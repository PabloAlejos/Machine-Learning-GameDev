# SpaceShooterAI

* El Objetivo del proyecto es diseñar y desarrollar un videojuego de tipo arcade. Para la implementación del juego que sirve como base para este proyecto es necesario planear las mecánicas de juego, además de diseñar los modelos del jugador, los enemigos y otros elementos gráficos. Esta tarea también incluye el diseño de la interfaz, banda sonora y efectos de sonido. 
    
* Diseñar e implenentar un sistema inteligente que posteriormente será utilizado para interactuar con el videojuego creado. Este proceso de divide en:  
   
    * Establecer una comunicación entre el juego y el modelo del sistema inteligente. Dado que el juego y el script que hace uso del modelo serán implementados en diferente lenguaje de programación, se debe establecer un mecanismo de intercambio de datos para la comunicación.  
       
    * Diseñar el conjunto de datos que posteriormente servirá para el entrenamiento del modelo. Al ser el punto de partida el aprendizaje por imitación, necesitaremos un gran volumen de datos de los cuales seleccionaremos todos o solo los mas representativos para el entrenamiento.  
       
    * Implementar, haciendo uso de las librerías como scikit-learn(minería de datos), DEAP(algoritmos evolutivos) y Pandas y NumPy(procesamiento de datos a bajo nivel) el simtema inteligente que jugará al videojuego.


