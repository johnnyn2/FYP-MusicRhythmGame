using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusContainer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        HideStatus();
    }

    public void HideStatus() {
        gameObject.SetActive(false);
    }
    public void ShowStatus() {
        gameObject.SetActive(true);
    }
}
