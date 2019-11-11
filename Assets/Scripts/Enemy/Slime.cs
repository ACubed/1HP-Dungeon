using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy{
    
    [Header("Components")]
	[SerializeField] private Rigidbody2D rb2d; //the characters actual body
    [SerializeField] private GameObject slimePrefab; //WHO ARE MY CHILDREN?


    [Header("Slime variables")]
    [SerializeField] private float scale; //how big am i
    [SerializeField] private float speed; //how fast am i
    
    /**************************************************************
     * Start
     *
     * called at the beginning of every "play"
     **************************************************************/
     void Start(){
        //initialize some variables
        rb2d = GetComponent<Rigidbody2D>(); //get the attatched rigidbody
        scale = transform.localScale.x; //we assume that xyz will always be equal.
        hitpoints = (int)scale; //i.e., you have to hit it for how big it is. 5x5x5 takes 5 hits etc.
        speed = 5f*(1/scale);
        attackDamage = 1f*scale;
    } //end of awake
    
    /**************************************************************
     * Update
     *
     * called every tick
     **************************************************************/
    void Update(){
        //check if we're dead
        base.Update();
        if(activated){
            if(hitpoints <= 0){
                Split();
            } else {
                //otherwise, move!
                MoveToPlayer();
            } //end of if-else
        }
    } //end of update
    
    /**************************************************************
     * Split
     *
     * Splits the slime if we've taken too much damage for this slime to handle
     **************************************************************/    
     void Split(){
        float nextSlime = scale - 1f;
        if(scale < 1){ //delete if we are too small
            base.Die();
        } else { //else generate 2 subslimes which are one size smaller than this.
            //generate a random location for both of the new slimes
            System.Random rnd = new System.Random();
            float x = rnd.Next(-(int)scale,(int)scale), y = rnd.Next(-(int)scale,(int)scale);
            Vector3 offset = new Vector3(x,y,2);
            //create the two slimes at that random location and change their scale
            GameObject slime1 = Instantiate(slimePrefab, transform.position += offset, transform.rotation);
            slime1.transform.localScale = new Vector3(nextSlime,nextSlime,nextSlime);
            GameObject slime2 = Instantiate(slimePrefab, transform.position -= offset, transform.rotation);
            slime2.transform.localScale = new Vector3(nextSlime,nextSlime,nextSlime);
            //destroy this current slime
            Destroy(gameObject);
        } //split up
    } //end of method split

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
            
        } //end of if
    } //end of oncollisionenter
} //end of abstract class
