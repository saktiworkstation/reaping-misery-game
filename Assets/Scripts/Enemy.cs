using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    // Experience
    public int xpValue = 1;

    // Logic
    public float triggerLength = 1;
    public float chaseLength = 5;
    private bool chasing;
    private bool collidingWithPlayer;
    private Transform playerTransform;
    private Vector3 startingPosition;
    public string enemyName;

    // Hitbox
    public ContactFilter2D filter;
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];

    protected override void Start()
    {
        base.Start();
        playerTransform = GameManager.instance.player.transform;
        startingPosition = transform.position;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        if(Vector3.Distance(playerTransform.position, startingPosition) < chaseLength)
        {
            if(Vector3.Distance(playerTransform.position, startingPosition) < triggerLength)
                chasing = true;

            if (chasing)
            {
                if (!collidingWithPlayer)
                {
                    UpdateMotor((playerTransform.position - transform.position).normalized);
                }
            }
            else
            {
                UpdateMotor(startingPosition - transform.position);
            }
        }
        else
        {
            UpdateMotor(startingPosition - transform.position);
            chasing = false;
        }

        // Check for overlaps
        collidingWithPlayer = false;
        boxCollider.Overlap(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;

            if(hits[i].tag == "Fighter" && hits[i].name == "Player")
            {
                collidingWithPlayer = true;
            }

            //the array is not cleanup, so we do it ourself
            hits[i] = null;
        }
    }

    protected override void Death()
    {
        Achivement.instance.upProgres(enemyName);
        GameManager.instance.GrantXp(xpValue);
        GameManager.instance.ShowText("+" + xpValue + " XP", 30, Color.magenta, transform.position, Vector3.up * 40, 1.0f);
        Destroy(gameObject);
    }
}


//s += pMisi1.ToString();
//s += pMisi2.ToString();
//s += pMisi3.ToString();
//s += pMisi4.ToString();
//s += pMisi5.ToString();
//s += misi1.ToString();
//s += misi2.ToString();
//s += misi3.ToString();
//s += misi4.ToString();
//s += misi5.ToString();
