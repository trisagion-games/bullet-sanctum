using UnityEngine;
using System;

public abstract class Enemy : LivingEntity {
    public enum EnemyType { Melee, Ranged, Swarm };
    public EnemyType enemyType;
    public int DMG;
    protected bool playerFound;
    protected bool facingRight;
    protected Player player;

    override protected void Start() {
        facingRight = transform.localScale.x > 0;
        player = GameObject.FindObjectOfType<Player>();
        OnSpawn();
    }

    // Runs on Update(). 
    public abstract void MovePattern();

    public virtual void PlayerLookout() {
        if (!playerFound) {
            // Code to see if player is visible by enemy.
            // playerFound = true;
        }
    }

    public void PlayerFound(bool state) {
        playerFound = state;
    }

    void Update() {
        // if... AI Stuff & MinMax trees
        PlayerLookout();
        MovePattern();
        if (playerFound) {
            if ((player.transform.position.x < transform.position.x && facingRight) ||
                (player.transform.position.x > transform.position.x && !facingRight)) {
                Flip();
            }
        }
    }

    // takeDamage is inherited from LivingEntity

    public void Flip() {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        facingRight = transform.localScale.x > 0;
    }
}
