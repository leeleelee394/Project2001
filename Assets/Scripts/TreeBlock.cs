using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBlock : MonoBehaviour
{
    bool mTreeDestroy;
    public bool TreeDestroy { get { return mTreeDestroy; } set { mTreeDestroy = value; } }
    
    
    //나무토막이 잘리는 순간 발생하는 애니메이션
    public void ToughAniPlayEnd()
    {
        //blocks[0].GetComponent<Animation>().Stop();    //애니메이션 플레이 중단  
        mTreeDestroy = true;
    }


    // Start is called before the first frame update
    void Start()
    {
        mTreeDestroy = false;
    }

    /*
    //전체 끄기
    public void uiActiveFalse()
    {
        Option.SetActive(false);
        BottomButton.SetActive(false);
        Title.SetActive(false);
        TimeBar.SetActive(false);
        GameOver.SetActive(false);
        InfoLRButton.SetActive(false);
    }

    //최초 시작: 타이틀+옵션+하단버튼
    public void uiStart()
    {
        uiActiveFalse();
        Title.SetActive(true);
        Option.SetActive(true);
        BottomButton.SetActive(true);
    }

    //플레이: 타임바+옵션+인포LR(터치후 사라짐)
    public void uiPlay()
    {
        uiActiveFalse();
        TimeBar.SetActive(true);
        Option.SetActive(true);
        InfoLRButton.SetActive(true);
    }

    //게임오버: 옵션+하단버튼+게임오버
    public void uiGameOver()
    {
        uiActiveFalse();
        GameOver.SetActive(true);
        Option.SetActive(true);
        BottomButton.SetActive(true);
    }
    */
}
