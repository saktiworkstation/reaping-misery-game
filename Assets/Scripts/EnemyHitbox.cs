using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : Collidable
{
    // Damage
    public int damage = 1;
    public float pushForce = 5;

    protected override void OnCollide(Collider2D coll)
    {
        //if(coll.tag == "Fighter" && coll.name == "Player")
        //{
        //    // Create a new damage object, before sending it on the player
        //    Damage dmg = new Damage
        //    {
        //        damageAmount = damage,
        //        origin = transform.position,
        //        pushForce = pushForce,
        //    };

        //    coll.SendMessage("ReciveDamage", dmg);
        //}

        if (coll.tag == "Fighter" && (coll.name == "Player" || coll.name == "Pet(Clone)"))  // semua Fighter (Player & Pet)
        {
            Damage dmg = new Damage
            {
                origin = transform.position,
                damageAmount = damage,
                pushForce = pushForce
            };
            coll.SendMessage("ReciveDamage", dmg);
        }
    }
}
