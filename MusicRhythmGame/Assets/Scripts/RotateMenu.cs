using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RotateMenu : MonoBehaviour
{
    public string[] songList;
    public string[] duration;
    public string[] songNickname;
    private int currSongIndex = 0;

    public TextMeshProUGUI songNameText;
    public TextMeshProUGUI songDurationText;
    public TextMeshProUGUI songNumText;

    public GameObject loadingScreen;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        songNameText.text = songList[0];
        songDurationText.text = duration[0];
        PlayerPrefs.SetString("selectedSong", songNickname[0]);
        songNumText.text = 1.ToString() + " / " + songList.Length.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Arrow(string dir) {
        if (dir == "left") {
            Debug.Log("RotateMenuLeftArrowPressed");
            currSongIndex = (currSongIndex+songList.Length-1) % songList.Length;
        } else {
            Debug.Log("RotateMenuRightArrowPressed");
            currSongIndex = (currSongIndex+1) % songList.Length;
        }
        songNameText.text = songList[currSongIndex];
        songDurationText.text = duration[currSongIndex];
        songNumText.text = (currSongIndex+1).ToString() + " / " + songList.Length.ToString();
        
        PlayerPrefs.SetString("selectedSong", songNickname[currSongIndex]);
    }

    public void Play(string mode) {
        PlayerPrefs.SetString("mode", mode);
        SceneManager.LoadScene("game");
        StartCoroutine(LoadAsynchronously());
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

    public void returnBtn(){
        SceneManager.LoadScene("Menu");
    }
}
