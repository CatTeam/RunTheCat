using UnityEngine;
using System.Collections;

public class Terrain : MonoBehaviour
{
    public const float MIN_SPEED_VALUE = 1;
    public static Terrain instance;
    public float minimumSpeed = 2;
    public float normalSpeed = 5;

    private float actualSpeed;
    private float previousSpeed;

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        transform.Translate(0, actualSpeed * Time.deltaTime, 0);
    }

    public static void SetSpeed(float speed)
    {
        if (speed >= MIN_SPEED_VALUE)
            instance.actualSpeed = speed;
    }

    public static void ChangeAndSaveSpeed(float speed)
    {
        if (speed >= MIN_SPEED_VALUE)
        {
            instance.previousSpeed = instance.normalSpeed;
            instance.normalSpeed = speed;
        }
    }

    public static void RestoreSpeed()
    {
        instance.normalSpeed = instance.previousSpeed;
    }

}
