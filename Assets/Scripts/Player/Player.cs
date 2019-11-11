using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{

    [Header("Player Variables")]
    [SerializeField] private int hitPoints = 1;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Weapon Variables")]
    [SerializeField] private Weapon[] weapons; //array of all possible weapons
    [SerializeField] private WeaponTypes[] inventory = { WeaponTypes.PISTOL, WeaponTypes.FAL, WeaponTypes.SNIPER };
    [SerializeField] private int currentWeapon = 0; //the current weapon equipped in the inventory
    [SerializeField] private int numWeapons = 1; //the number of weapons we have
    [SerializeField] private int maxNumWeapons = 3; //the maximum number of weapons we can have in our inventory    
    public Sprite pistolSprite;
    public Sprite falSprite;

    [Header("Debugging Weapon Variables")]
    [SerializeField] private bool canShoot; //shooting logic
    [SerializeField] private GameObject firePoint;
    [SerializeField] private GameObject firePointPis;
    [SerializeField] private GameObject firePointFal;

    [Header("Score Variables")]
    public TextMeshProUGUI text;
    [SerializeField] private int score = 0;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        canShoot = true;
    } //end of start

    void Update()
    {
        UpdateSprite();
        if (hitPoints <= 0)
            Die();
        if (Input.GetButtonDown("Fire1") && canShoot)
            StartCoroutine(Shoot());
        CycleWeapons();
    } //end of update

    void UpdateSprite(){
        Weapon myWeapon = weapons[(int)inventory[currentWeapon]]; //get the weapon we are currently using
            switch(myWeapon.GetWeaponID()){
                case WeaponTypes.PISTOL: 
                spriteRenderer.sprite = pistolSprite; 
                firePoint = firePointPis;
                break;
                case WeaponTypes.FAL:  spriteRenderer.sprite = falSprite; Debug.Log("fal boi");
                firePoint = firePointFal;
                break;
                default: Debug.Log("there is an error"); break;
            } //end of switch
    }
    /**************************************************************
     * Shoot
     *
     * shoot!
     **************************************************************/
    IEnumerator Shoot()
    {
        Weapon myWeapon = weapons[(int)inventory[currentWeapon]]; //get the weapon we are currently using
        Debug.Log(myWeapon);
        if (myWeapon != null)
        {
            GameObject bullet = Instantiate(myWeapon.GetBullet(), firePoint.transform.position, firePoint.transform.rotation);
            bullet.GetComponent<Bullet>().SetPlayer(true);

            bullet.GetComponent<Bullet>().BulletShoot(myWeapon.GetDamage(), myWeapon.GetBulletSpeed(), myWeapon.GetBulletLifeTime());
            canShoot = false;
            yield return new WaitForSeconds(myWeapon.fireDelay);
            canShoot = true;
        }
        
    } //end of coroutine


    void CycleWeapons()
    {
        if(Input.GetKeyDown(KeyCode.Tab)){
            currentWeapon = (currentWeapon + 1) % numWeapons; //cycle through the weapons we have
            //get current weapon and change sprite accordingly
        } //end of if           
    }



    /// <summary>
    /// Get a new weapon and add it to your inventory
    /// </summary>
    /// <param name="newWeapon"></param>
    public void GetWeapon(Weapon newWeapon)
    {
        if(numWeapons < maxNumWeapons)
        {
            numWeapons++;
            inventory[numWeapons-1] = newWeapon.GetWeaponID();
        } else
        {
            inventory[currentWeapon] = newWeapon.GetWeaponID(); //change the current weapon for the new weapon
        }
    }//getWeapon


    public void TakeDamage(int damage)
    {
        hitPoints -= damage;
    } //end of takedamage


    void Die()
    {
        //for now we just delete ourselves, add restart option in the future
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name, LoadSceneMode.Single);
    } //end of Die

    public void IncrementPoints(int points)
    {
        score += points;
        GameObject textObj = GameObject.Find("Score");
        text = textObj.GetComponent<TextMeshProUGUI>();
        text.text = "Score: " + score;
    }
}
