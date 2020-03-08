using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public void ToGame() {
        SceneManager.LoadScene("songmenu");
    }

    public void ToStore() {
        SceneManager.LoadScene("Shop");
    }
    public void Quit() {
        Application.Quit();
    }
}
