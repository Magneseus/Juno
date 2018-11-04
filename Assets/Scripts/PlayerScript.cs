using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerScript : MonoBehaviour
{

    public int playerId = 0; // The Rewired player id of this character
    public float moveSpeed = 3.0f;
    public bool isKBM = false;

    private Player player; // The Rewired Player
    private Vector2 moveVector;
    private Vector2 lookVector;
	private Rigidbody2D rb;
    public GameObject border;
    private SpriteRenderer borderSprite;
    private bool fire;

    void Awake()
	{
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        player = ReInput.players.GetPlayer(playerId);
		rb = GetComponent<Rigidbody2D>();
        borderSprite = border.GetComponent<SpriteRenderer>();
        
        switch (playerId)
        {
        case 0:
            borderSprite.color = Color.red;
            break;
        case 1:
            borderSprite.color = Color.green;
            break;
        case 2:
            borderSprite.color = Color.blue;
            break;
        case 3:
            borderSprite.color = Color.magenta;
            break;
        }
    }

    void Update ()
	{
        GetInput();
        ProcessInput();
    }

    private void GetInput()
	{
        // Get the input from the Rewired Player. All controllers that the Player owns will contribute, so it doesn't matter
        // whether the input is coming from a joystick, the keyboard, mouse, or a custom controller.

        moveVector.x = player.GetAxis("MoveHorizontal"); // get input by name or action id
        moveVector.y = player.GetAxis("MoveVertical");
        lookVector.x = player.GetAxis("LookHorizontal");
        lookVector.y = player.GetAxis("LookVertical");
        
        if (isKBM)
        {
            lookVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;//(new Vector3(Screen.width/2f, Screen.height/2f, 0));
            lookVector.Normalize();
        }
    }

    private void ProcessInput()
	{
        // Process movement
        if(moveVector.x != 0.0f || moveVector.y != 0.0f)
		{
			rb.drag = 0f;
            rb.velocity = moveVector * moveSpeed * Time.deltaTime;
        }
		else
		{
			rb.drag = 100000000f;
		}
        
        if (lookVector.SqrMagnitude() > 0)
        {
            float __z = Vector2.SignedAngle((new Vector2(-0.5f, 0.5f)), lookVector);
            border.transform.rotation = Quaternion.Euler(0, 0, __z);
        }
    }
}