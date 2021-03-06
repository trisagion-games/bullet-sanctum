﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : Interactible {
    public static int lastCheckpoint = 5; // TODO set to initial spawnpoint
    public int checkpointID; // Set to be the same as the room ID

    private static string currentCheckpoint = "";
    private static Vector3 spawnPosition;
    protected override void Activate () {
        GameObject.FindObjectOfType<PlayerBounty> ().BankBounty ();
        GameObject.FindObjectOfType<Player>().TakeDamage(-Player.maxHealth);
        AudioHelper.PlaySound("checkpoint");
        lastCheckpoint = checkpointID;
        currentCheckpoint = SceneManager.GetActiveScene ().name;
        spawnPosition = transform.position;
    }

    public static string getCurrentCheckpoint () {
        return currentCheckpoint;
    }

    public static Vector3 getSpawnPosition () {
        return spawnPosition;
    }

}