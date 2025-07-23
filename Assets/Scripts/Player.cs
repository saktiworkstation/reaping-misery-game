using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover
{
    public static Player instance;
    private SpriteRenderer spriteRenderer;
    private bool isAlive = true;
    public FloatingJoystick joystick;

    [Header("Pet")]
    public GameObject petPrefab;
    private GameObject petInstance;
    public Vector2 petOffset = new Vector2(0.2f, 0.1f);

    private void Awake()
    {
        instance = this;
    }

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        SpawnPet();
    }

    private void SpawnPet()
    {
        if (petPrefab != null && petInstance == null)
        {
            Vector3 spawnPos = transform.position + (Vector3)petOffset;
            petInstance = Instantiate(petPrefab, spawnPos, Quaternion.identity);
            DontDestroyOnLoad(petInstance);
        }
    }

    protected override void ReciveDamage(Damage dmg)
    {
        if (!isAlive)
            return;

        base.ReciveDamage(dmg);
        GameManager.instance.OnHitpointChange();
    }
    protected override void Death()
    {
        isAlive = false;
        GameManager.instance.deathMenuAnim.SetTrigger("Show");  
    }

    private void FixedUpdate()
    {
        //float x = Input.GetAxisRaw("Horizontal");
        //float y = Input.GetAxisRaw("Vertical");
        float x = joystick.Horizontal;
        float y = joystick.Vertical;

        if(isAlive)
            UpdateMotor(new Vector3 (x, y, 0));
    }
    public void SwapSprite(int skinId)
    {
        spriteRenderer.sprite = GameManager.instance.playerSprites[skinId];
    }
    public void OnLevelUp()
    {
        maxhitpoint++;
        hitpoint = maxhitpoint;
    }
    public void SetLevel(int level)
    {
        for (int i = 0; i < level; i++)
        {
            OnLevelUp();
        }
    }
    public void Heal(int healingAmount)
    {
        if (hitpoint == maxhitpoint)
            return;

        hitpoint += healingAmount;
        if(hitpoint > maxhitpoint)
            hitpoint = maxhitpoint;
        GameManager.instance.ShowText("+" + healingAmount.ToString() + "hp", 25, Color.green, transform.position, Vector3.up * 30, 1.0f);
        GameManager.instance.OnHitpointChange();
    }
    public void Respawn()
    {
        Heal(maxhitpoint);
        isAlive = true;
        lastImmune = Time.time;
        pushDirection = Vector3.zero;
        // spawn Pet lagi
        SpawnPet();
    }
}
