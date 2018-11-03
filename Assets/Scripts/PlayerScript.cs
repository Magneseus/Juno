using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerScript : MonoBehaviour
{

    public int playerId = 0; // The Rewired player id of this character

    public float moveSpeed = 3.0f;

    private Player player; // The Rewired Player
    private Vector2 moveVector;
	private Rigidbody2D rigidbody;
    private bool fire;

    void Awake()
	{
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        player = ReInput.players.GetPlayer(playerId);
		rigidbody = GetComponent<Rigidbody2D>();
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
    }

    private void ProcessInput()
	{
        // Process movement
        if(moveVector.x != 0.0f || moveVector.y != 0.0f)
		{
			rigidbody.drag = 0f;
            rigidbody.velocity = moveVector * moveSpeed * Time.deltaTime;
        }
		else
		{
			this.rigidbody.drag = 100000000f;
		}
    }
}