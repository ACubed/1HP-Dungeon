using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour{
    [Header("Enemy Variables")]
    [SerializeField] public int hitpoints;
    [SerializeField] public float attackDamage;
    [SerializeField] public GameObject player; //WHO AM I ATTACKING?
    [SerializeField] public bool activated; //WHO AM I ATTACKING?
    [SerializeField] private int pointsToGive = 5;

    public void Awake(){
        player = GameObject.Find("Hero"); 
        activated = false;
    }  //end of awake

    public void Update(){
        //get my transform
        if(Vector3.Distance(transform.position, player.transform.position) < 7){
            activated = true;
        } // end of if
    } //end of Update

    public void TakeDamage(int damage) {
        hitpoints -= damage; //update health. (check for death in another method)
    } //end of TakeDamage


    protected void Die()
    {
        player.GetComponent<Player>().IncrementPoints(pointsToGive);
        Destroy(gameObject);

    } //end of die
} //end of abstract class
