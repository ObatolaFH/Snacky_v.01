using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static int level = 1;
    public static int template = 0;
    public static int points = -10;
    public void PlayGame ()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene("SampleScene");;
    }

    public void QuitGame ()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
    
}
