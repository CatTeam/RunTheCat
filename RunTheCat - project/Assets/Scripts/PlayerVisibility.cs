using UnityEngine;
using System.Collections;

public class PlayerVisibility : MonoBehaviour {

    void OnBecameVisible()
    {
        Debug.Log("Appeared!");
    }

    void OnBecameInvisible()
    {
        Debug.Log("Disappeared!");
		Player.instance.isLevelFailed = true;
    }

}
