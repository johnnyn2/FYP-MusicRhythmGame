using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    
    public GameObject Warrior; 
    // Update is called once per frame
    void Update()
    {
       if (Input.GetButtonDown("First Lane"))
       {
           Debug.Log("First Lane");
       }
       if (Input.GetButtonDown("Second Lane"))
       {
           Debug.Log("Second Lane");
       }
       if (Input.GetButtonDown("Third Lane"))
       {
           Debug.Log("Third Lane");
       }
       if (Input.GetButtonDown("Forth Lane"))
       {
           Debug.Log("Forth Lane");
       }
    }
}
