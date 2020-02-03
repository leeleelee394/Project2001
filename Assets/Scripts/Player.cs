using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


//플레이어 클래스에서 이동하면 제거되는 나무->매니저
//요청을 보내는 역할이 플레이어 클래스 역할
//이동된 플레이어의 위치값->플레이어

public class Player : MonoBehaviour
{    
    private bool mIsLeft;
    private bool mIsTouch;
    private bool mIsDead;        
    public Animator animator;

    //프로퍼티
    public bool IsLeft { get { return mIsLeft; } }
    public bool IsTouch { get { return mIsTouch; } set { mIsTouch = value; } }
    public bool IsDead { get { return mIsDead; } set { mIsDead = value; } }

    private void Start()
    {
        Debug.Log(gameObject.transform);
        mIsTouch = false;
        mIsLeft = false;
        mIsDead = false;
    }

    private void Update()
    {
        if(IsDead) animator.SetTrigger("Dead");
    }

    public void LeftOnPointerDown()
    {
        animator.SetTrigger("Action");
        mIsTouch = true;

        if (!mIsLeft)
        {
            mIsLeft = true;            
            Move();            
        }
    }

    public void RightOnPointerDown()
    {
        animator.SetTrigger("Action");
        mIsTouch = true;

        if (mIsLeft)
        {
            mIsLeft = false;
            Move();
        }
    }

    //플레이어 이동값
    public void Move()
    {   
        gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x * -1,
            gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * -1,
            gameObject.transform.localScale.y, gameObject.transform.localScale.z);
    }
}
