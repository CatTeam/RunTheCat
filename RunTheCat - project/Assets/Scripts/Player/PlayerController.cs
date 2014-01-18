using UnityEngine;
using System.Collections;
using Assets.Scripts.Player;
using System.Threading;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public float moveSpeed = 10f;
    public float moveForce = 200f;
    public string mapLayerName = "Map";
    public PlayerState playerState { get; set; }

    private Transform groundCheck;
    private Vector3 spawnPoint;

    // Use this for initialization
    void Start()
    {
        instance = this;
        GetInitialReferences();
        Debug.Log("Start! groundpos: " + groundCheck.position.ToString());
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
        playerState.isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer(mapLayerName));
        if (!playerState.isGrounded)
        {
            var translation = -Time.deltaTime * moveSpeed;
            transform.Translate(0, translation, 0);
        }
        Move();
    }

    public void Move()
    {
        Vector3 dir = Vector3.zero;
        dir.x = Input.acceleration.x;
        dir.z = Input.acceleration.z;

        transform.Translate(dir * Time.deltaTime * moveSpeed);

        //rigidbody2D.AddForce(dir * Time.deltaTime * moveForce);
    }

    public void Dash(float translation)
    {
        transform.Translate(0, translation, 0);
    }

    public void RestartPosition()
    {
        transform.position = spawnPoint;
    }

}
