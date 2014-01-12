using UnityEngine;
using System.Collections;
using Assets.Scripts.Player;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public float moveSpeed = 10f;
    public float moveForce = 200f;
    public PlayerState playerState { get; set; }

    private Transform groundCheck;
    private Vector3 spawnPoint;

    // Use this for initialization
    void Start()
    {
        Debug.Log("Start!");
        instance = this;
        GetInitialReferences();
        spawnPoint = transform.position;
        playerState = new PlayerState();
        playerState.isFacingRight = true;
    }

    private void GetInitialReferences()
    {
        groundCheck = transform.FindChild("groundCheck");

    }

    // Update is called once per frame
    void Update()
    {
        playerState.isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Default"));

        if (Input.GetKey("a"))
        {
            Debug.Log("A");
            Move(false);
        }
        else if (Input.GetKey("d"))
        {
            Move(true);
        }

    }

    void Flip()
    {
        playerState.isFacingRight = !playerState.isFacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void Move(bool moveRight)
    {

        if ((moveRight && !playerState.isFacingRight) || (!moveRight && playerState.isFacingRight))
            Flip();
        var translation = Time.deltaTime * moveSpeed * (moveRight ? 1 : -1);
        transform.Translate(translation, 0, 0);

    }

    public void RestartPosition()
    {
        transform.position = spawnPoint;
    }

}
