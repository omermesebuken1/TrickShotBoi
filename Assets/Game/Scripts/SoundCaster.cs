using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCaster : MonoBehaviour
{
    [SerializeField] private AudioClip Count123;
    [SerializeField] private AudioClip CountEnd;
    [SerializeField] private AudioClip GameEnd;

    private float timer;
    private float endTime;

    private bool startTimer;

    private void Start() {
        
        
    }

    private void Update() {
        
        if(startTimer == true)
        {
            timer += Time.deltaTime;

        }
        if(timer > endTime)
        {
            Destroy(this.gameObject);
        }

    }

    public void CastSoundEndDie(string sound)
    {
        if(sound == "Count123")
        {
            GetComponent<AudioSource>().PlayOneShot(Count123);
            endTime = 1;
            startTimer = true;
        }

        if(sound == "CountEnd")
        {
            GetComponent<AudioSource>().PlayOneShot(CountEnd);
            endTime = 1;
            startTimer = true;
        }

        if(sound == "GameEnd")
        {
            GetComponent<AudioSource>().PlayOneShot(GameEnd);
            endTime = 3.5f;
            startTimer = true;
        }

    }



}
