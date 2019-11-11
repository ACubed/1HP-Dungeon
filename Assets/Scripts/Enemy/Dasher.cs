using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dasher : Enemy
{
    [Header("Components")]
	[SerializeField] private Rigidbody2D rb2d; //the characters actual body

    [Header("Dasher variables")]
    [SerializeField] private float speed; //how fast am i
    [SerializeField] private float dashForce; //how dash am i
    
    // Start is called before the first frame update
    void Start(){
        rb2d = GetComponent<Rigidbody2D>();
        hitpoints = 15;
        speed = 5f;
        attackDamage = 100000000f;
        dashForce = 10f;
    } //end of state

    // Update is called once per frame
    void Update(){
        if(activated){
            if(hitpoints <= 0){
                Die();
            } else {
                MoveToPlayer();
            } //end of if-else
        }
    } //end of update

    /**************************************************************
     * Die
     *
     * I am dead
     **************************************************************/    
    void Die(){
        Destroy(gameObject);
    } //end of die
    
    /**************************************************************
     * moveToPlayer
     *
     * Moves the enemy to the player
     **************************************************************/    
    void MoveToPlayer(){ 
        //calculate the direction that the player is in
        Vector3 direction = player.transform.position - transform.position;
        Vector2 dir = direction.normalized; //the actual direction values (normalized between -1 , 1)
        //calculate the angle between the player and enemy
        float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;
        //rotate our rigidbody
        rb2d.rotation = angle;
        if(Vector3.Distance(player.transform.position,transform.position) > 10){
            //move the rb2d
            rb2d.MovePosition((Vector2)transform.position + (dir*speed*Time.deltaTime));
        } else {
            //if we are close, stop moving and charge a dash.
            rb2d.AddForceAtPosition(dir*dashForce, (Vector2)transform.position, ForceMode2D.Impulse);
        } //do a dash

    } //find and pursue the player!
}

