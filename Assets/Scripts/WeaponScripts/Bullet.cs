using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Bullet Script
 * Aaron
 * 
 * This script handles bullet movement and collisions. Will be instantiated from a weapon script.
 */


public class Bullet : MonoBehaviour
{

    private bool player;
    private int damage; //how much damage the bullet will do on impact
    private float bulletSpeed; //initialized later
    private float bulletLifeTime; //how long the bullet will stay alive for
    private float bulletAlive; //how long the bullet has been alive for
    //the we instantiate the values from the function so we can have all the settings in the weapon script
    //makes it a little less hacky i think

    private Rigidbody2D rigidBody; //reference to this objects rigidbody component, instantiated later


    // Start is called before the first frame update
    void Awake()
    {
        //initialize some variables
        rigidBody = GetComponent<Rigidbody2D>(); //get the attatched rigidbody
        bulletAlive = 0f; //the bullet has been alive for 0 seconds
    }

    void Update()
    {
        if (bulletAlive >= bulletLifeTime)
            Destroy(gameObject); //the bullet has lived its life
        else
            bulletAlive += Time.deltaTime;
    }

    //this function must be called after instantiation
    //instantiates variables and actually moves the bullet
    public void BulletShoot(int damage, float bulletSpeed, float bulletLifeTime)
    {
        this.damage = damage;
        this.bulletSpeed = bulletSpeed;
        this.bulletLifeTime = bulletLifeTime;
        //move the bullet
        rigidBody.velocity = transform.up * bulletSpeed;
    }

    public void SetPlayer(bool val){
        player = val;
    } //end of setPlayer

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(player){ //the bullets are coming from the player
            if(collision.gameObject.tag == "Player")
                return;
            if (collision.gameObject.tag == "Enemy") {
                collision.gameObject.GetComponent<Enemy>().TakeDamage(damage); //deal damage to the enemy :::NOT DONE:::
            }
        } else { //the bullets are coming from someone else
            if(collision.gameObject.tag == "Enemy")
                return;
            if (collision.gameObject.tag == "Player") {
                collision.gameObject.GetComponent<Player>().TakeDamage(damage); //deal damage to the player
            }
        } //end of if-else
        Destroy(gameObject); //delete this bullet
    }
}
