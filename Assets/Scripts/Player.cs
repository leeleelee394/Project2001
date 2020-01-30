using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    //public GameObject player;
    enum PlayerPosition { left, right }
    PlayerPosition nowPosition;

    //플레이어 클래스에서 이동하면 제거되는 나무->매니저
    //요청을 보내는 역할이 플레이어 클래스 역할
    //이동된 플레이어의 위치값->플레이어


    //RemoveTree()


    private void Start()
    {
        Debug.Log(gameObject.transform);
        nowPosition = PlayerPosition.right;
    }

    public void LeftOnPointerDown()
    {
        if (gameObject.transform.localPosition.x > 0)
        {
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x* -1,
            gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
            Debug.Log("좌" + gameObject.transform.localPosition.x);
            nowPosition = PlayerPosition.left;
            gameObject.transform.localScale = new Vector3(-gameObject.transform.localScale.x,
                gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        }        
    }

    public void RightOnPointerDown()
    {
        if (gameObject.transform.localPosition.x < 0)
        {
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x * -1,
            gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
            Debug.Log("우" + gameObject.transform.localPosition.x);
            nowPosition = PlayerPosition.right;
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * -1,
                gameObject.transform.localScale.y, gameObject.transform.localScale.z);            
        }
    }

}
