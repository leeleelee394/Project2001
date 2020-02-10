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

    public GameObject Play;                 //ui 실플레이화면
    public GameObject GameOver;             //ui게임오버
    public GameObject InfoLRButton;         //ui최초시작시 탭위치알려주는 화면

    public GameObject originalTree;         //나무토막프리팹
    public GameObject treePosition;         //트리위치...

    public Sprite[] Textures;               //트리스킨1

    public GameObject Level;                //ui레벨
    float levelCheckTime;                   //ui레벨확인후사라질 시간
    public Text Score;                      //ui현재스코어
    public Text BestScore;                  //ui최고스코어
    private int scoreNumber;                //ui스코어 변수

    public Image TimeBar;                   //ui타임바
    private float currentTime;               //ui타임바용 변수
    private float totalTime;               //ui타임바용 변수

    public Player player;                  //플레이어정보

    enum TreeTexture { type1, type2, type3 }//트리스킨enum
    enum Bough { nothing, left, right }     //위치enum

    private float firstTreePivot;             //나무의 첫번재 위치 값을 따로 저장해둠
    private bool IsBough;

    List<GameObject> blocks;               //나무토막생성
    List<GameObject> tempBlock;            //삭제용 임시 나무토막
    float[] blocksHeight;

    public BGSoundManager bgSound;
    public EffectSoundManager effectSound;

    private bool dieAccess;
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
                tree.GetComponent<Image>().sprite = Textures[0];
                break;
            case TreeTexture.type2:
                tree.GetComponent<Image>().sprite = Textures[1];
                break;
            case TreeTexture.type3:
                tree.GetComponent<Image>().sprite = Textures[2];
                break;
        }

        IsBough = !IsBough;
    }

    //==============================================================================
    private void Awake()
    {
        blocks = new List<GameObject>();
        tempBlock = new List<GameObject>();
        firstTreePivot = originalTree.GetComponent<RectTransform>().rect.height * originalTree.transform.localScale.y;
        blocksHeight = new float[10];

        IsBough = false;
        scoreNumber = 0;
        levelCheckTime = 0f;
        Level.SetActive(false);
        totalTime = 10f;
        currentTime = 5f;
        TimeBar.fillAmount = currentTime / totalTime;
        dieAccess = false;

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

        //레벨확인후 1초후 끄기
        if (Level.activeSelf)
        {
            levelCheckTime += Time.deltaTime;
            if (levelCheckTime > 1f)
            {
                Level.SetActive(false);
                levelCheckTime = 0f;
            }
        }

        //타임바 계산
        currentTime -= Time.deltaTime;
        TimeBar.fillAmount = currentTime / totalTime;

        //타임오버
        if (currentTime <= 0f && !dieAccess)
        {
            playerDie();           
        }
    }
    

    public void Touch()
    {
        player.IsTouch = false;

        if ((player.IsLeft && blocks[1].transform.Find("left").gameObject.activeSelf) ||
            (!player.IsLeft && blocks[1].transform.Find("right").gameObject.activeSelf))
        {
            playerDie();

            //베스트스코어 갱신
            if (scoreNumber > Int32.Parse(BestScore.text)) BestScore.text = scoreNumber.ToString();

        }
        else
        {
            //스코어 점수
            Score.text = (++scoreNumber).ToString();

            //레벨 점수
            if (scoreNumber % 10 == 0)
            {
                Level.SetActive(true);
                Level.GetComponent<Text>().text = "Level " + (scoreNumber / 10);
            }

            //타임 추가
            currentTime += 0.3f;
        }

        effectSound.soundManager("WoodChop");

        blocks[0].GetComponent<Animator>().SetTrigger("Fall");      //토막애니메이션
        blocks[0].GetComponent<TreeBlock>().IsLeft = player.IsLeft;   //날아가는방향전달
        blocks[0].GetComponent<TreeBlock>().IsMoveStart = true;       //날아가는함수 bool 전달

        tempBlock.Add(blocks[0]);                                   //임시값에 이동            
        blocks.Remove(blocks[0]);                                   //List에서 삭제

        //한칸씩 위치 아래로 이동
        for (int i = 0; i < blocks.Count; ++i)
        {
            blocks[i].transform.localPosition = new Vector3(0, blocksHeight[i], 0);
        }
    }


    //플레이어 죽음
    void playerDie()
    {
        dieAccess = true;
        player.IsDead = true;
        bgSound.soundManager("Die");
    }
}

