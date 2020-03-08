using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExhibitPlayer : MonoBehaviour
{
    [SerializeField]
    private float RotationPerSec;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,RotationPerSec*Time.deltaTime,0);
    }
}
