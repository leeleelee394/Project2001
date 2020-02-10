using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBlock : MonoBehaviour
{
    bool mTreeDestroy;
    private bool mIsLeft;
    public bool IsLeft { get { return mIsLeft; } set { mIsLeft = value; } }

    private bool mIsMoveStart;
    public bool IsMoveStart { get { return mIsMoveStart; } set { mIsMoveStart = value; } }

    private bool mIsDestroy;


    private void Start()
    {
        mIsMoveStart = false;
        mIsDestroy = false;
    }

    private void Update()
    {
        if(mIsMoveStart)
        {
            if (IsLeft)
            {
                gameObject.transform.localPosition += new Vector3(40f, 40f, 1f);
            }
            else
            {
                gameObject.transform.localPosition += new Vector3(-40f, 40f, 1f);
            }
        }

        if(mIsDestroy)
        {
            Vector3 targetScreenPos = Camera.main.WorldToScreenPoint(gameObject.transform.localPosition);
            if (targetScreenPos.x < Screen.width || targetScreenPos.x > 0 ||
                targetScreenPos.y < Screen.height || targetScreenPos.y > 0)
            {
                Destroy(gameObject);
            }
        }
    }

    //나무토막이 잘리는 순간 발생하는 애니메이션
    public void ToughAniPlayEnd()
    {
        mIsDestroy = true;
    }
}
