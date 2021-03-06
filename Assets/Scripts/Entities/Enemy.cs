using System.Collections;
using UnityEngine;

public abstract class Enemy : LivingEntity {
    public enum EnemyType { Melee, Ranged, Swarm }
    public EnemyType enemyType;
    public int DMG;
    public int bounty;
    protected bool playerFound;
    protected bool facingRight;
    protected Player player;
    public Rigidbody2D rb;
    public bool initialized;

    override protected void Start () {
        initialized = false;
        StartCoroutine (PlayerDetect ());
    }

    // Runs on Update(). 
    public virtual void MovePattern () {
        if (playerFound) {
            if ((player.transform.position.x < transform.position.x && facingRight) ||
                (player.transform.position.x > transform.position.x && !facingRight)) {
                Flip ();
            }
        }
    }

    public virtual void PlayerLookout () {
        if (!playerFound) {
            // Code to see if player is visible by enemy.
            // playerFound = true;
        }
    }

    public override void OnDeath () {
        GameObject.FindObjectOfType<PlayerBounty> ().collectBounty (bounty);
        BountyNum.ShowNum (transform.position, bounty);
        base.OnDeath ();
    }

    public void PlayerFound (bool state) {
        playerFound = state;
        rb.velocity = Vector2.zero;
    }

    void FixedUpdate () {
        if (initialized) {
            // if... AI Stuff & MinMax trees
            PlayerLookout ();
            MovePattern ();
        }
    }

    // takeDamage is inherited from LivingEntity

    public virtual void Flip () {
        transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        facingRight = transform.localScale.x > 0;
    }

    // Required to delay player detection so that it doesn't NullReferenceException
    protected IEnumerator PlayerDetect () {
        yield return new WaitForSeconds (1f);
        facingRight = transform.localScale.x > 0;
        player = GameObject.FindObjectOfType<Player> ();
        OnSpawn ();
        initialized = true;
    }
}