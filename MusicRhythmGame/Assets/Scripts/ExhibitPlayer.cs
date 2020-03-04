using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExhibitPlayer : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,60*Time.deltaTime,0);
    }
}
