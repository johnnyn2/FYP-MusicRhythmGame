using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SongMenu : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void ToGame(string song) {
        PlayerPrefs.SetString("selectedSong", song);
        SceneManager.LoadScene("game");
        // StartCoroutine(LoadAsynchronously());
    }

    IEnumerator LoadAsynchronously() {
        AsyncOperation op = SceneManager.LoadSceneAsync("game");
        loadingScreen.SetActive(true);

        while(!op.isDone) {
            float progress = Mathf.Clamp01(op.progress / .9f);
            slider.value = progress;
            yield return null;
        }
    }
}
