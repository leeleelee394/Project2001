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
    private int scoreNumber;
    public Player player;                  //플레이어정보
    
    enum TreeTexture { type1, type2, type3 }//트리스킨enum
    enum Bough { nothing, left, right }     //위치enum

    private float firstTreePivot;             //나무의 첫번재 위치 값을 따로 저장해둠
    private bool IsBough;   

    List<GameObject> blocks;               //나무토막생성
    float[] blocksHeight;
   

    //랜덤값
    static T RandomEnumValue<T>()
    {
        var v = Enum.GetValues(typeof(T));
        return (T)v.GetValue(UnityEngine.Random.Range(0, v.Length));
    }

    //나뭇가지좌우,스프라이트랜덤받기
    void boughCreate(GameObject tree)
    {
        var value = IsBough ? RandomEnumValue<Bough>() : Bough.nothing; //나무가지 랜덤값       
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

        IsBough = !IsBough;
    }

    //==============================================================================
    private void Awake()
    {
        blocks = new List<GameObject>();
        firstTreePivot = originalTree.GetComponent<RectTransform>().rect.height * originalTree.transform.localScale.y;
        blocksHeight = new float[10];

        IsBough = false;
        scoreNumber = 0;

        for (int i = 0; i < 10; ++i)
        {
            blocksHeight[i] = (i * firstTreePivot);
            GameObject block = GameObject.Instantiate(originalTree) as GameObject;

            block.transform.parent = treePosition.transform;
            block.transform.localScale = originalTree.transform.localScale;
            block.transform.localPosition = new Vector3(0, blocksHeight[i], 0);
            boughCreate(block); //true: 나무가지 있는 나무, false: 빈 나무       
            blocks.Add(block);  //list에 담음
        }
    }

    void Update()
    {
        if (player.IsTouch) Touch();
        if(blocks[0].GetComponent<TreeBlock>().TreeDestroy)
        {
            ToughAniPlayEnd();
        }

        //나무 개수가 모자르면 새로 생성
        if (blocks.Count < 10)
        {
            GameObject block = GameObject.Instantiate(originalTree) as GameObject;

            block.transform.parent = treePosition.transform;
            block.transform.localScale = originalTree.transform.localScale;
            block.transform.localPosition = new Vector3(0, blocksHeight[9], 0);
            boughCreate(block); //true: 나무가지 있는 나무, false: 빈 나무       
            blocks.Add(block);
        }
    }


    public void Touch()
    {
        player.IsTouch = false;        

        if (player.IsLeft && blocks[0].transform.Find("left").gameObject.activeSelf)
        {
            Debug.Log("게임오버:" + blocks[0].transform.GetChild(0).gameObject.activeSelf);
        }        
        else if (!player.IsLeft && blocks[0].transform.Find("right").gameObject.activeSelf)
        {
            Debug.Log("게임오버:"+blocks[0].transform.GetChild(1).gameObject.activeSelf);
        }
        else
        {
            scoreNumber++;
            Debug.Log(scoreNumber+"스코어점수");           

            //한칸씩 위치 아래로 이동
            for (int i = 0; i < blocks.Count; ++i)
            {
                blocks[i].transform.localPosition = new Vector3(0, blocksHeight[i], 0);
            }

            blocks[0].GetComponent<Animator>().SetTrigger("Fall");
        }
    }

    //나무토막이 잘리는 순간 발생하는 애니메이션
    public void ToughAniPlayEnd()
    {
        //blocks[0].GetComponent<Animation>().Stop();    //애니메이션 플레이 중단          
        Destroy(blocks[0]);                              //0번 나무토막 삭제
        blocks.Remove(blocks[0]);                        //List에서 삭제
    }
}

