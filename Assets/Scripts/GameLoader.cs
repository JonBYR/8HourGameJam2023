using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        Debug.Log("!! In Dev Mode - Quit Application");
        EditorApplication.isPlaying = false;

#elif UNITY_WEBGL            
        Application.OpenURL("https://lncn.ac/rarcade");

#else
        Application.Quit();
#endif
    }
}
