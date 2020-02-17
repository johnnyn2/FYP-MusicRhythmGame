using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
    private Animator anim;
    private bool finished = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("start");
        if((anim.GetCurrentAnimatorStateInfo(0).IsName("Die") || anim.GetCurrentAnimatorStateInfo(0).IsName("Smash Attack")) && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
            Destroy(gameObject);
    }
}
