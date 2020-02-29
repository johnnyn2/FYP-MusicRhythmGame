using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SongMenu : MonoBehaviour
{
    public GameObject loadingScreen;
    public GameObject songMenuScrollView;
    public GameObject chooseModeContainer;
    public GameObject beatModeBtn;
    public GameObject onsetModeBtn;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetSong(string song) {
        PlayerPrefs.SetString("selectedSong", song);
        songMenuScrollView.SetActive(false);
        chooseModeContainer.SetActive(true);
        beatModeBtn.SetActive(true);
        onsetModeBtn.SetActive(true);
    }

    public void ToGame(string mode) {
        PlayerPrefs.SetString("mode", mode);
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
