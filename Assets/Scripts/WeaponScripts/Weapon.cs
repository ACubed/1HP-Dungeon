using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Weapon Script
 * 
 * This is the basic model on which weapons are built.
 * A bullet will be instantiated at the fire point and move forward.
 */

public enum WeaponTypes
{
    NONE, PISTOL, FAL, SNIPER
}

public class Weapon : MonoBehaviour
{

    //logic variables
 // private float lastShot = 0; //the last time a bullet was shot. Used for fire rate


    [Header("Bullet Settings")]
    [SerializeField] private int damage = 1;
    [SerializeField] private float bulletSpeed = 30f;
    [SerializeField] private float bulletLifeTime = 7.5f;
    [SerializeField] private GameObject bulletPrefab; //the bullet prefab we want to shoot should be inserted here

    [Header("Weapon Settings")]
    [SerializeField] private string weaponName = "abstract weapon";
    [SerializeField] private WeaponTypes myWeaponID = WeaponTypes.NONE;
    [SerializeField] public float fireDelay = 0.5f; //the "fire rate" or delay between the shots (in seconds)
    [SerializeField] private GameObject firePoint; //the position the bullet will be instantiated

    // Start is called before the first frame update
    void Start()
    {
        if (bulletPrefab == null)
        {
            Debug.Log("something went wrong in weapon, on awake");
            bulletPrefab = new GameObject(); //make sure this is set in the inspector
            firePoint = new GameObject();
        }
    }

    /**************************************************************
     * PlayerShoot
     *
     * Moves the enemy to the player
     **************************************************************/    
    public void PlayerShoot(){
        //fires a bullet at firePoint
        GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
        bullet.GetComponent<Bullet>().SetPlayer(true); //set true since we are the player
        bullet.GetComponent<Bullet>().BulletShoot(damage, bulletSpeed, bulletLifeTime); //shoot the bullet
    } //end of PlayerShoot

    /**************************************************************
     * enemyShoot
     *
     * Called by the enemy to shoot at the player
     **************************************************************/    
    public void EnemyShoot(){
        //fires a bullet at firePoint
        GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
        bullet.GetComponent<Bullet>().SetPlayer(false); //this is CRUCIAL for when u are enemy
        bullet.GetComponent<Bullet>().BulletShoot(damage, bulletSpeed, bulletLifeTime); //shoot the bullet
    } //end of EnemyShoot


    public int GetDamage()
    {
        return damage;
    }

    public float GetBulletSpeed()
    {
        return bulletSpeed;
    }

    public GameObject GetBullet()
    {
        return bulletPrefab;
    }


    public WeaponTypes GetWeaponID()
    {
        return myWeaponID;
    }

    public float GetBulletLifeTime()
    {
        return bulletLifeTime;
    }


    /// <summary>
    /// if you dont understand this, just stop coding
    /// </summary>
    /// <returns></returns>
    public string GetWeaponName()
    {
        return weaponName;
    } //GetWeaponName
    
} //Weapon
