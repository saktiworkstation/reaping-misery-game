using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTextPerson : Collectable
{
    public string message;

    private float cooldown = 3.0f;
    private float lastShout;

    protected override void Start()
    {
        base.Start();
        lastShout = -cooldown;
    }

    protected override void OnCollide(Collider2D coll)
    {
        if(Time.time - lastShout > cooldown)
        {
            lastShout = Time.time;
            GameManager.instance.ShowText(message, 25, Color.white, transform.position + new Vector3(0,0.16f,0), Vector3.zero, 3.0f);
        }
    }
}
