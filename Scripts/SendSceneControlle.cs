using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; 

public class SendSceneControlle : MonoBehaviour {

    public static void GameStart()
    {
        SceneManager.LoadScene("Main");
    }

    public static void TitleReturn()
    {
        SceneManager.LoadScene("Title");
    }
}
