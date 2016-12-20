using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

    public enum FireMode { Auto, Burst, Single};
    public FireMode fireMode;

    public Transform[] projectileSpawn;
    public Projectile projectile;
    public float msBetweenShots = 100;
    public float muzzleVelocity = 35;
    public int burstCount;

    [Header("Effects")]
    public Transform shell;
    public Transform shellEjection;
    public AudioClip shootAudio;
    MuzzleFlash muzzleflash;
    float nextShotTime;

    bool triggerReleasedSinceLastShot;
    int shotsRemainingInBurst;

     void Start()
    {
        muzzleflash = GetComponent<MuzzleFlash>();
        shotsRemainingInBurst = burstCount;
    } 

    float nextShortTime;

     void Shoot()
    {
    if (Time.time > nextShortTime)
        {
            if(fireMode == FireMode.Burst)
            {
                if (shotsRemainingInBurst == 0)
                {
                    return;
                }
                shotsRemainingInBurst--;
            } else if (fireMode == FireMode.Single)
            {
                if (!triggerReleasedSinceLastShot)
                {
                    return;
                }
            }

            for(int i =0; i< projectileSpawn.Length; i++)
            {

            
            nextShortTime = Time.time + msBetweenShots / 1000;
            Projectile newProjecttile = Instantiate(projectile, projectileSpawn[i].position, projectileSpawn[i].rotation) as Projectile;
            newProjecttile.SetSpeed(muzzleVelocity);
            }

            Instantiate(shell, shellEjection.position, shellEjection.rotation);
            muzzleflash.Activate();

            AudioManager.instance.PlaySound(shootAudio, transform.position);
        }
    }

    public void OnTriggerHold()
    {
        Shoot();
        triggerReleasedSinceLastShot = false;
    }

        public void OnTriggerRelease()
    {
        triggerReleasedSinceLastShot = true;
        shotsRemainingInBurst = burstCount;
    }
}
