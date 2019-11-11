using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour{

	[Header("Components")]
	[SerializeField] private Rigidbody2D rb2d; //the characters actual body
    
	[Header("Speed Variables")]
	[SerializeField] private float velocity; //the velocity
    [SerializeField] private float speedModifier; //this allows for us to increase speed, without velocity

    [Header("Dash Variables")]
    [SerializeField] private bool isDashing; //determines if we are dashing, this can be used for invulnerable
    [SerializeField] private bool canDash; //determines if we can dash or not

    [SerializeField] private float dashLength; //the cooldown on the dash represents the amount of time the dash takes to PERFORM
    [SerializeField] private float dashCD; //the cooldown on the dash represents the amount of time the dash takes to PERFORM

    [SerializeField] private float dashTimer; //the current timer on the dash. 0 means ready to use.
    [SerializeField] private float dashForce; //how fast we get to the point of the end of the dash
    [SerializeField] private GameObject dash; //how fast we get to the point of the end of the dash

   
    /**************************************************************
     * Start
     *
     * called at the beginning of every "play"
     **************************************************************/
    void Start(){
        rb2d = GetComponent<Rigidbody2D>();
        velocity = 6.5f; 
        speedModifier = 1f;
        isDashing = false;
        canDash = true;
        dashCD = 1.6f;
        dashTimer = 0f;
        dashLength = .08f;
        dashForce = 5f;
    } //end of start

    /**************************************************************
     * Update
     *
     * called at every cycle
     **************************************************************/
    void Update(){
    //continually check for:
        //if the character needs to rotate towards the mouse
        if(Time.timeScale == 1) Rotate();
        //check for player movement
        if(!isDashing) //we cannot move if we dash 
            Move();
        //check if the player dashes
        Dash();
    } //end of Update

    /**************************************************************
     * Rotate
     *
     * Rotate the character towards the mouse based on the mouse.x and mouse y
     **************************************************************/
    void Rotate(){
        //get the mouse position
        Vector3 mousePos = Input.mousePosition; 
        //get the object position in the world
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        //get the delta X and Y. Keep z as 0 since we are 2D boys
        mousePos.z = 0; mousePos.x = mousePos.x - objectPos.x; mousePos.y = mousePos.y - objectPos.y;
        //get the angle to point to the mouse
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        //angle -= 90; //this is required for 360 movement, as opposed to 180 movement
        //rotate the character.
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    } //end of rotate

    /**************************************************************
     * Move
     *
     * This method handles movement; vertical and horizontal.
     **************************************************************/
    void Move(){
    	//the horizontal and vertical axis's(?)
    	float moveX = Input.GetAxis("Horizontal");
    	float moveY = Input.GetAxis("Vertical");
    	//set the velocity to a constant value. This prevents crazy acceleration
        // if we are not dashing, we want a consistent speed.
        rb2d.velocity = new Vector2(moveX*velocity, moveY*velocity)*speedModifier;
    	//get the movement vector. (i.e., direction.)
    	Vector2 movement = new Vector2(moveX,moveY);
    	//do the actual move
    	rb2d.AddForce(movement*velocity);
    } //end of movement

    /**************************************************************
     * Dash
     *
     * Performs a dash, or sudden increase in speed for a short moment.
     **************************************************************/
    void Dash(){
        // rb2d velocity is essentially the direction we are going in
        // so for when we want to add a force, we add it in the same direction as velocity
        if(Input.GetKeyDown(KeyCode.LeftShift) && dashTimer <= 0 && !isDashing && canDash){
            //we are performing a dash
            //a normalized vector changes all the values that are not 0 to 1. was WANT this.
            Instantiate(dash, transform.position, Quaternion.Euler(new Vector3(0f, 0f, rb2d.rotation)));
            rb2d.AddForce(rb2d.velocity*dashForce, ForceMode2D.Impulse);
            //let the world know we are dashing
            dashTimer = dashLength; //this represents how long the dash takes to perform
            isDashing = true; //we can use this for invulnerability when that happens
            StartCoroutine(DashAgain());
        } else if(dashTimer > 0) //only decrement if we need to.
            dashTimer -= Time.deltaTime;
        else if(dashTimer <= 0)
            isDashing = false;
        //end of if-else statements.
    } //end of method dash

    IEnumerator DashAgain(){
        canDash = false;
        yield return new WaitForSeconds(dashCD);
        canDash = true;
    } //end dash again
} //end of class Movement
