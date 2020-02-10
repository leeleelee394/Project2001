using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//스크립트를오브젝트에붙이는순간컴포넌트가 붙는다.
//컴포넌트 실행시 없으면 오류가 남
//[RequireComponent(typeof(AudioSource))] 

public class BGSoundManager : MonoBehaviour
{
    //플레이어
    public AudioSource audioSource;
    public AudioClip BG;
    public AudioClip Die;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = BG;
        audioSource.volume = 0.1f;          //볼륨값: 0~1
        audioSource.loop = true;
        audioSource.mute = false;
        audioSource.Play();
        audioSource.playOnAwake = true;   //활성화시재생, 비활성화는 Play() 통해서 재생
        audioSource.priority = 0;         //씬내오디오소스중 현재 오디오소스 우선순위 정함 0(최우선)~256, 128:기본값    

    }


    public void soundManager(string audioName)
    {
        if (audioName == "BG")
        {
            audioSource.clip = BG;
            audioSource.loop = true;
            audioSource.Play();
        }
        if (audioName == "Die")
        {
            audioSource.clip = Die;
            audioSource.loop = false;
            audioSource.Play();
        }
    }




}
