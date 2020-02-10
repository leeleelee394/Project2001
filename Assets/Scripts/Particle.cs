using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public GameObject smog;
    GameObject[] smogs = new GameObject[4];
    float[] angle = { 45f, 135f, 225f, 315f};

    float totalTime = 1f;
    float currentTime = 0f;

    //복제품들의 위치정보나 충돌처리를 위해서는 복제품의 Component에 접근을 해야 되는데 
    //이것을 위해 사용하는 것이 Instantiate(오브젝트) as GameObject라던지 as Transform 등이다.

    void Start()
    {
        for (int i = 0; i < 4; ++i)
        {
            smogs[i] = GameObject.Instantiate(smog) as GameObject;
            smogs[i].transform.parent = gameObject.transform;
            smogs[i].transform.localScale = new Vector3(1,1,1);
            smogs[i].transform.localPosition = new Vector3(0, 0, 0);
        }

    }

    void Update()
    {
        if(currentTime<totalTime)
        {
            currentTime += Time.deltaTime;            
            smogs[0].GetComponent<RectTransform>().position += new Vector3(0.01f, 0.01f, 0);
            smogs[1].GetComponent<RectTransform>().position += new Vector3(0.01f, -0.01f, 0);
            smogs[2].GetComponent<RectTransform>().position += new Vector3(-0.01f, -0.01f, 0);
            smogs[3].GetComponent<RectTransform>().position += new Vector3(-0.01f, 0.01f, 0);
        }


    }
}
