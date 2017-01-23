using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void ChangeScene(int scene)
    {

        SceneManager.LoadScene(scene);

    }
}
