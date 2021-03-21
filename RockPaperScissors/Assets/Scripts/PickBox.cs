using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickBox : MonoBehaviour
{
    public SpriteRenderer rendererr;
    public Sprite[] sprites= new Sprite[4];
    public string[] tags = new string[4];
    void Start()
    {
        int i = Random.Range(0, sprites.Length);
        rendererr.sprite = sprites[i];
        tag = tags[i];
    }
}
