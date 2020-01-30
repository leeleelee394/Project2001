using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{   
    public GameObject Option;               //ui옵션
    public GameObject BottomButton;         //ui하단3개버튼
    public GameObject Title;                //ui시작화면
    public GameObject TimeBar;              //ui타임바
    public GameObject Play;                 //ui 실플레이화면
    public GameObject GameOver;             //ui게임오버
    public GameObject InfoLRButton;         //ui최초시작시 탭위치알려주는 화면

    public GameObject originalTree;         //나무토막프리팹
    public GameObject treePosition;         //트리위치...
        
    public Sprite Texture1;                 //트리스킨1
    public Sprite Texture2;                 //트리스킨2
    public Sprite Texture3;                 //트리스킨3//배열로 바꿀까?

    public Text Level;                      //레벨
    public Text Score;                      //현재스코어
    public Text BestScore;                  //최고스코어

    private Player player;                  //플레이어정보

    enum TreeTexture { type1, type2, type3 }//트리스킨enum
    enum Bough { nothing, left, right }     //위치enum



    //랜덤값
    static T RandomEnumValue<T>()
    {
        var v = Enum.GetValues(typeof(T));
        return (T)v.GetValue(UnityEngine.Random.Range(0, v.Length));
    }


    //나뭇가지좌우,스프라이트랜덤받기
    void boughCreate(GameObject tree, bool type)
    {
        var value = type? RandomEnumValue<Bough>() : Bough.nothing; //나무가지 랜덤값       
        var texture = RandomEnumValue<TreeTexture>();               //나무 스프라이트 랜덤값
        GameObject left = tree.transform.GetChild(0).gameObject;
        GameObject right = tree.transform.GetChild(1).gameObject;

        switch (value)
        {            
            case Bough.nothing:
                left.SetActive(false);
                right.SetActive(false);
                break;
            case Bough.left:
                left.SetActive(true);
                right.SetActive(false);
                break;
            case Bough.right:
                left.SetActive(false);
                right.SetActive(true);
                break;
        }
        
        switch (texture)
        {
            case TreeTexture.type1:
                tree.GetComponent<Image>().sprite = Texture1;
                break;
            case TreeTexture.type2:
                tree.GetComponent<Image>().sprite = Texture2;
                break;
            case TreeTexture.type3:
                tree.GetComponent<Image>().sprite = Texture3;
                break;
        }
    }

    
    private void Awake()
    {
        //player = new Player();
        //Debug.Log(player.transform);

        //나무토막생성
        Queue<GameObject> blocks = new Queue<GameObject>();

        for (int i = 0; i < 10; ++i)
        {
            GameObject block = GameObject.Instantiate(originalTree) as GameObject;
            
            block.transform.parent = treePosition.transform;
            block.transform.localScale = originalTree.transform.localScale;                     
            block.transform.localPosition = new Vector3(0, 520+(i * block.GetComponent<RectTransform>().rect.height*block.transform.localScale.y), 0);            
            boughCreate(block, i % 2 == 1 ? true : false); //true: 나무가지 있는 나무, false: 빈 나무            
        }
    }
        

    void Start()
    {
        
    }
   
    void Update()
    {      

        //좌우 터치값을 받는다
        //    좌면 캐릭터를 좌로/우면 캐릭터를 우로/(터치시 애니메이션 실행)
        //    이동할때마다 타임바에 값을 추가한다.
        //
        //
        //타임바가 0이하일 경우 게임오버
        //
        //캐릭터와 나무가지가 충돌한다면 게임오버



        //좌우 버튼으로 플레이어를 이동한다.
        //플레이어 이동시 충돌하지 않았다면 전체 블럭 값을 이동한다



    }

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


    public void RemoveTree()
    {

    }
}
