using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAndAttackPlayer : Enemy
{
    // Start is called before the first frame update
    [Header("Components")]
	[SerializeField] private Rigidbody2D rb2d; //the characters actual body
    //[SerializeField] private GameObject player; //WHO AM I ATTACKING? 

    [Header("Variables")]
	[SerializeField] private float speed; //the characters actual body
    [SerializeField] private int pointsToGive = 10; // the points for killing me

    void Start(){
        rb2d = GetComponent<Rigidbody2D>();
        hitpoints = 3;
        attackDamage = 1f;
        speed = 4f;
    } //end of enemy

    // Update is called once per frame
    void Update(){
        if(hitpoints <= 0){
            Die();
        } else {
            MoveToPlayer();
        } //end of if-else
    } //end of Update



    /**************************************************************
     * moveToPlayer
     *
     * Moves the enemy to the player
     **************************************************************/    
    void MoveToPlayer(){ 
        //calculate the direction that the player is in
        Vector3 direction = player.transform.position - transform.position;
        //calculate the angle between the player and enemy
        float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;
        //rotate our rigidbody
        rb2d.rotation = angle;
        Vector2 dir = direction.normalized;
        rb2d.MovePosition((Vector2)transform.position + (dir*speed*Time.deltaTime));
    } //find and pursue the player!

    /**************************************************************
     * OnCED2D
     *
     * Tells us when the enemy collides with something
     **************************************************************/     
     private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<Player>().TakeDamage((int)attackDamage); //deal damage to the player
            Destroy(gameObject); // ##### FIX THIS ###### Delete to avoid bug
        } //end of if
    } //end of oncollisionenter
}
