using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileController : Fighter
{
    public GameObject player;
    public DynamicJoystick joystick;
    public float speed;
    protected RaycastHit2D hit;
    private Vector3 originalSize;
    protected BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        speed = 0.75f;
        originalSize = transform.localScale;
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // reset move delta
        //joystick = new D(joystick.Horizontal * speed, joystick.Vertical * speed); 

        if (joystick.Vertical > 0)
        {
            player.transform.Translate(Vector2.up * Time.deltaTime * speed);
        }
        else if (joystick.Vertical < 0)
        {
            player.transform.Translate(Vector2.down * Time.deltaTime * speed);
        }

        if (joystick.Horizontal > 0)
        {
            player.transform.Translate(Vector2.right * Time.deltaTime * speed);
        }
        else if (joystick.Horizontal < 0)
        {
            player.transform.Translate(Vector2.left * Time.deltaTime * speed);
        }

        //swipe sprite direction, when going right or left
        if (joystick.Horizontal > 0)
            transform.localScale = originalSize;
        else if (joystick.Horizontal < 0)
            transform.localScale = new Vector3(originalSize.x * -1, originalSize.y, originalSize.z);

        // Reduce push force every frame, based off recovery speed
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);

        //make sure can move in this direction, by casting a nox there first, if the box return null we're free to move
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, joystick.Vertical), Mathf.Abs(joystick.Vertical * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            //make this thing move
            transform.Translate(0, joystick.Vertical * Time.deltaTime, 0);
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(joystick.Horizontal, 0), Mathf.Abs(joystick.Horizontal * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            //make this thing move
            transform.Translate(joystick.Horizontal * Time.deltaTime, 0, 0);
        }
    }
}
