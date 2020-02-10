using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip WoodChop;
    public AudioClip Click;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = null;
        audioSource.volume = 0.1f;
        audioSource.loop = false;
        audioSource.mute = false;
        audioSource.playOnAwake = true;   //활성화시재생, 비활성화는 Play() 통해서 재생
        audioSource.priority = 1;         //씬내오디오소스중 현재 오디오소스 우선순위 정함 0(최우선)~256, 128:기본값    

    }


    public void soundManager(string audioName)
    {        
        if (audioName == "WoodChop")
        {
            audioSource.clip = WoodChop;
            audioSource.Play();
        }
        if (audioName == "Click")
        {
            audioSource.clip = Click;
            audioSource.Play();
        }
    }




}
