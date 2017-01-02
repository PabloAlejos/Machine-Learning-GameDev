using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {

    public int sceneIndex;

	public void LoadByIndex()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
