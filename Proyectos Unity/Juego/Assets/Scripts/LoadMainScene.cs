using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainScene : MonoBehaviour {

    //Esta clase es para colocar los objetos que deben persistir en las escenas
    //En este objeto se van a añadir los objetos que no queramos que se destruyan al cargar de nuevo la escena
    //como el contador de partidas consecutivas

	// Use this for initialization
	void Start() {
        SceneManager.LoadScene(1);
    }
	
}
