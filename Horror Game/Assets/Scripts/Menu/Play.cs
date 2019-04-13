using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Play : MonoBehaviour
{
    public void FirstPerson()
    {
        Debug.Log("FirstPerson");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ThirdPerson()
    {
        Debug.Log("ThirdPerson");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void EndGame()
    {
        Debug.Log("Has Quit");
        Application.Quit();
    }
}
