﻿using UnityEngine;
using System.Collections;
using System;

public class BushTrap : MonoBehaviour
{
    public float trapDifficulty = 50;
    public float playerSpeed = 2;

    private bool isPlayerTrapped = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.gameObject.layer == 0)
        {
            if (other.gameObject.tag == "Player" && !isPlayerTrapped)
            {
                Terrain.ChangeAndSaveSpeed(playerSpeed);
                isPlayerTrapped = true;
                audio.Play();
                PlayerController.instance.moveEnabled = false;
                Debug.Log("player trapped" + isPlayerTrapped);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        audio.Stop();
    }

    private void Update()
    {
        if (isPlayerTrapped)
        {
            if (trapDifficulty > 0)
            {
                Vector3 currentAcceleration = Input.acceleration;
                Debug.Log(string.Format("Difficulty: {0}, input: x={1}, y={2}, z={3}",
                            trapDifficulty, currentAcceleration.x, currentAcceleration.y, currentAcceleration.z));
                trapDifficulty -= 0.3f * Math.Abs(currentAcceleration.x + currentAcceleration.y);
                
            }
            else if (trapDifficulty <= 0)
            {
                Debug.Log("player free");
                audio.Stop();
                isPlayerTrapped = false;
                this.gameObject.GetComponent<CircleCollider2D>().isTrigger = false;
                this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                Terrain.RestoreSpeed();
                PlayerController.instance.moveEnabled = true;
            }
        }
    }
}
