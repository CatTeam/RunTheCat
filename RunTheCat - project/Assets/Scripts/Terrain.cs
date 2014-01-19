using UnityEngine;
using System.Collections;

public class Terrain : MonoBehaviour {

    public static Terrain instance;
    public float minimumSpeed=2;
    public float normalSpeed=4;

    private float actualSpeed;
	
	void Start () {
        instance = this;
	}
	
	
	void Update () {
        transform.Translate(0, actualSpeed * Time.deltaTime,0);
	}

    public static void SetSpeed(float speed)
    {
        instance.actualSpeed = speed;
    }
}
