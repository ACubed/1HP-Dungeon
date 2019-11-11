using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedShooter : Enemy
{
    // Start is called before the first frame update
    [Header("Components")]
	[SerializeField] private Rigidbody2D rb2d; //the characters actual body
    [SerializeField] private Weapon weapon; //WHAT AM I ATTACKING WITH?


    [Header("Variables")]
	[SerializeField] private float speed; //the characters actual body
    [SerializeField] private bool canShoot; //can i shoot?

    void Start(){
        rb2d = GetComponent<Rigidbody2D>();
        hitpoints = 3;
        attackDamage = 1f;
        speed = 4f;
        canShoot = true;
    } //end of enemy

    // Update is called once per frame
    void Update(){
        base.Update();
        if(activated){
            if(hitpoints <= 0){
                Die();
            } else {
                MoveToPlayer();
            } //end of if-else
        }
    } //end of Update


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
        angle -= 90;
        rb2d.rotation = angle;
    
        //if in range to shoot, shoot, else move to the player
        if(Vector3.Distance(player.transform.position,transform.position) > 7.5){
            //move the rb2d
            rb2d.MovePosition((Vector2)transform.position + (dir*speed*Time.deltaTime));
        } else {
            //SHOOT HIM
            if(canShoot) //if you can shoot, shoot!
                StartCoroutine(EnemyShoot());
        } //do a dash
    } //find and pursue the player!


    /**************************************************************
     * EnemyShoot
     *
     * Shoots and waits until it can shoot again
     **************************************************************/    
    IEnumerator EnemyShoot(){
        //shoot and disable shoot for 3*fireDelay
        weapon.EnemyShoot();
        canShoot = false;
        yield return new WaitForSeconds(5*weapon.fireDelay);
        //enable shoot
        canShoot = true;
    } //end of coroutine
    
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
}
