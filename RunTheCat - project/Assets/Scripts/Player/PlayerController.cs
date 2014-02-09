using UnityEngine;
using System.Collections;
using Assets.Scripts.Player;
using System.Threading;
using System;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public bool moveEnabled = true;
    public float iceMoveSpeedMultiplier = 2;
    public float moveSpeed = 10f;
    public float moveForce = 200f;
    public string mapLayerName = "Map";
    public float Ytarget = 0f;
    public float Ymargin = 1f;
    public float YreturnForce = 3f;
    public PlayerState playerState { get; set; }

    private Transform groundCheck;
    private Vector3 spawnPoint;

    // Use this for initialization
    void Start()
    {
        instance = this;
        GetInitialReferences();
        spawnPoint = transform.position;
        playerState = new PlayerState();
        LowPassFilter.Initialize();
    }

    private static class LowPassFilter
    {
        public static float AccelerometerUpdateInterval = 1.0f / 60.0f;
        public static float LowPassKernelWidthInSeconds = 0.5f;

        private static float LowPassFilterFactor;  // tweakable
        private static Vector3 lowPassValue = Vector3.zero;

        public static void Initialize()
        {
            LowPassFilterFactor = AccelerometerUpdateInterval / LowPassKernelWidthInSeconds;
            lowPassValue = Input.acceleration;
        }
        public static Vector3 Accelerometer()
        {
            lowPassValue = Vector3.Lerp(lowPassValue, Input.acceleration, LowPassFilterFactor);
            return lowPassValue;
        }
    }

    private void GetInitialReferences()
    {
        groundCheck = transform.FindChild("groundCheck");
    }

    // Update is called once per frame
    void Update()
    {
        if (collider2D.enabled)
        {
            playerState.isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer(mapLayerName));
            if (playerState.isGrounded && !playerState.isOnIce)
            {
                //rigidbody2D.AddForce(new Vector2(0, jumpForce));
                Terrain.SetSpeed(Terrain.instance.minimumSpeed);
                Debug.Log("Grounded");
            }
            else
            {
                Terrain.SetSpeed(Terrain.instance.normalSpeed);
            }
        }
        else
        {
            Terrain.SetSpeed(Terrain.instance.normalSpeed);
        }
        Move();

    }

    void FixedUpdate()
    {
        if (!playerState.isGrounded)
        {
            float yDistance = Ytarget - transform.position.y;
            if (Math.Abs(yDistance) > Ymargin)
            {
                rigidbody2D.velocity = new Vector2(0, yDistance * YreturnForce);
            }
        }
    }

    public void Move()
    {
        float lean = Input.acceleration.x;
        float filterLean = LowPassFilter.Accelerometer().x;
        filterLean = Mathf.Clamp(filterLean, -0.3f, 0.3f);


        Vector3 rotation = new Vector3(0, 0, filterLean * 90);
        //dir.z = Input.acceleration.z;

        if (moveEnabled)
            transform.Translate(Vector3.right * lean * Time.deltaTime * moveSpeed * (playerState.isOnIce == true? iceMoveSpeedMultiplier : 1));
        if(Time.timeScale!=0)
        {
            transform.rotation = Quaternion.identity;
            transform.Rotate(rotation);
        }

        //rigidbody2D.AddForce(dir * Time.deltaTime * moveForce);
    }

    public void VerticalTranslate(float translation)
    {
        transform.Translate(0, translation, 0);
    }

    public void RestartPosition()
    {
        transform.position = spawnPoint;
    }

}
