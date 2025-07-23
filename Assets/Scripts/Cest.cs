using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cest : Collectable
{
    public Sprite emptyChest;
    public int pesosAmount = 5;

    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            GameManager.instance.pesos += pesosAmount;
            GameManager.instance.ShowText("+" + pesosAmount + " peso!", 25, Color.yellow, transform.position, Vector3.up * 50, 1.5f);
        }
    }
}
