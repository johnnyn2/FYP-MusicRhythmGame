using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
    private Animator anim;
    private bool finished = false;
    // private int counter = 0;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // if(counter%100==0)
        //     Debug.Log(Player.transform.position.z);
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Smash Attack") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f){
            finished = true;
            anim.speed = 0f;
        }

        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Die") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.75f){
            finished = true;
            anim.speed = 0f;
        }
        // counter++;
        if(gameObject.transform.position.z <= Player.transform.position.z && !anim.GetBool("isDead") && !anim.GetBool("isAttack")){
            anim.SetBool("isAttack", true);
        }
        if(gameObject.transform.position.z <= Player.transform.position.z-6.0f) {
            GameObject healthBar = GameObject.FindGameObjectWithTag("HealthBarBg");
            if (!anim.GetBool("isDead")) {
                healthBar.GetComponent<HealthBar>().OnTakeDamage(10);
            }
            GameObject soundManager = GameObject.Find("SoundManager");
            List<SpectralFluxInfo> peakOfPeakSamples = soundManager.GetComponent<SoundManager>().peakOfPeakSamples;
            MinionManager minionManager = GameObject.Find("MinionManager").GetComponent<MinionManager>();
            if (minionManager.minionPtr<peakOfPeakSamples.Count) {
                minionManager.SpawnMinion(peakOfPeakSamples[minionManager.minionPtr].time);
                minionManager.minionPtr++;
            }
            Destroy(gameObject);
        }
    }

    // void OnBecameInvisible(){
    //     if(finished)
    //         Destroy(gameObject);
    // }
}
