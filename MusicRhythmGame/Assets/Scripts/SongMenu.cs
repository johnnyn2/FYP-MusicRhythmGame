using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SongMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public void ToGame(string song) {
        PlayerPrefs.SetString("selectedSong", song);
        SceneManager.LoadScene("game");
    }
}
