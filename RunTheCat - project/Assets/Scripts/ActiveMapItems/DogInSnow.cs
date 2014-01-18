using UnityEngine;
using System.Collections;

public class DogInSnow : MonoBehaviour 
{
	private void OnTriggerEnter2D(Collider2D other)
	{
        if (other.transform.gameObject.layer == 0)
        {
            if (other.gameObject.tag == "Player")
            {
                Player.instance.GameOver();
            }
        }
		
	}
}
