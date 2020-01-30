using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{   
    public GameObject Option;
    public GameObject BottomButton;
    public GameObject Title;
    public GameObject TimeBar;
    public GameObject Play;
    public GameObject GameOver;
    public GameObject InfoLRButton;

    public GameObject originalTree;
    public GameObject treePosition;
        
    public Sprite Texture1;
    public Sprite Texture2;
    public Sprite Texture3;

    public Text Level;
    public Text Score;
    public Text OverScore;
    public Text BestScore;
    

    enum treeTexture { type1, type2, type3 }

    enum bough { nothing, left, right }


    static T RandomEnumValue<T>()
    {
        var v = Enum.GetValues(typeof(T));
        //Debug.Log()
        return (T)v.GetValue(UnityEngine.Random.Range(0, v.Length));
    }


    //나뭇가지좌우,스프라이트랜덤받기
    void boughCreate(GameObject tree, bool type)
    {
        var value = type? RandomEnumValue<bough>() : bough.nothing; //나무가지 랜덤값       
        var texture = RandomEnumValue<treeTexture>();               //나무 스프라이트 랜덤값
        GameObject left = tree.transform.GetChild(0).gameObject;
        GameObject right = tree.transform.GetChild(1).gameObject;

        switch (value)
        {            
            case bough.nothing:
                left.SetActive(false);
                right.SetActive(false);
                break;
            case bough.left:
                left.SetActive(true);
                right.SetActive(false);
                break;
            case bough.right:
                left.SetActive(false);
                right.SetActive(true);
                break;
        }
        
        switch (texture)
        {
            case treeTexture.type1:
                tree.GetComponent<Image>().sprite = Texture1;
                break;
            case treeTexture.type2:
                tree.GetComponent<Image>().sprite = Texture2;
                break;
            case treeTexture.type3:
                tree.GetComponent<Image>().sprite = Texture3;
                break;
        }
    }

    public void RemoveTree()
    {

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



    private void Awake()
    {
        //uiStart();

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
        













        //GameObject[] name = GameObject.FindGameObjectsWithTag("TREE");
        //for(int i=0;i<name.Length;++i)// GameObject block in name)
        //{
        //    Debug.Log("블럭 위치" + i+"========="+name[i].transform.position);
        //}
        /*
        while(trees.Count<10)
        {

            //좌우값 하나 있는거,
            //없는거하나가 한세트로 받아야함
            break;
        }
        */
        //if(개수가 10개보다 적다면 추가로 생성한다. 생성시 랜덤값 두개와 위치값, 위치값은 전 오브젝트-y 240)

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
}
