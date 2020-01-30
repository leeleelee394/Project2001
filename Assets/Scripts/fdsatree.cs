using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tree : MonoBehaviour
{
    public GameObject Left;
    public GameObject Right;

    public Sprite Texture;
    public Sprite Texture1;
    public Sprite Texture2;
    public Sprite Texture3;

    enum treeTexture
    {
        type1,type2,type3
    }

    enum bough
    {
        nothing, left, right
    }

    static T RandomEnumValue<T>()
    {
        var v = Enum.GetValues(typeof(T));
        //Debug.Log(v.ToString());
        return (T) v.GetValue(new System.Random().Next(v.Length));
    }    

    void Start()
    {
        var value = RandomEnumValue<bough>();
        var texture = RandomEnumValue<treeTexture>();

        //Debug.Log(value.ToString());
        switch (value)
        {
            case bough.nothing:
                Left.SetActive(false);
                Right.SetActive(false);
                break;
            case bough.left:
                Left.SetActive(true);
                Right.SetActive(false);
                break;
            case bough.right:
                Left.SetActive(false);
                Right.SetActive(true);
                break;
        }


        switch (texture)
        {
            case treeTexture.type1:
                Texture = Texture1;
                break;
            case treeTexture.type2:
                Texture = Texture2;
                break;
            case treeTexture.type3:
                Texture = Texture3;
                break;
        }

        gameObject.GetComponent<Image>().sprite= Texture;
    }
}
