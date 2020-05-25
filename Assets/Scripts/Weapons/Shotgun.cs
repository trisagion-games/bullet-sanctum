using UnityEngine;
using System.Collections;

public class Shotgun : Weapon {

    // Bullet projectile, cooldown, and cooldown time is inherited from Weapon.

    protected override void Start() {   
        base.Start();
        onCooldown = false;
    }
    protected override void Update() {   
        base.Update();
        // TODO
    }

    public override void UseWeapon() {   
        if (!onCooldown) {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot() {    
        onCooldown = true;
        base.UseWeapon();
        yield return new WaitForSeconds(cooldownTime);
        onCooldown = false;
    }

}