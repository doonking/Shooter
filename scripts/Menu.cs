using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public GameObject mainMenuHolder;

	public void Play()
    {
        SceneManager.LoadScene("Game");
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void MainMenu()
    {
        mainMenuHolder.SetActive(true);

    }
}
