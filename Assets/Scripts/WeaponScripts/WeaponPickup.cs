using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{

    [SerializeField] private float detectRadius = 3f;
    [SerializeField] private bool playerOver = false;
    [SerializeField] private Weapon thisWeapon;
    [SerializeField] GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        thisWeapon = GetComponent<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerOver();
        if(playerOver && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("You picked up a " + thisWeapon.GetWeaponName());
            player.GetComponent<Player>().GetWeapon(thisWeapon);
            Destroy(gameObject);
        }
    }


    void CheckPlayerOver()
    {
        Collider2D []colliders = Physics2D.OverlapCircleAll(transform.position, detectRadius);
        for(int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i].gameObject.tag == "Player")
            {
                playerOver = true;
                player = colliders[i].gameObject;
                return;
            }//if
        } //for
        playerOver = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }

}
