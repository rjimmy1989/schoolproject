using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Player : MonoBehaviour
{

    //Variables 
    public float movementSpeed;
    public GameObject playerObj;
    public GameObject bulletSpawnPoint;
    public float waitTime;
    public GameObject Bullet;
    public float rateOfFire;
    private float timer;
    private Animator anim;
    public AudioClip pistolSound;
    public ParticleSystem muzzleFlash;
    public int health;
    public bool isPlayerOWned;
    public bool isAlive = true;
    public GameObject UI;
    public GameObject player;

    //Methods
    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();

    }








    void Update()
    {
        if(isAlive == false)
        {
            return;
        }

        //player facing mouse
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDist = 0f;

        if (playerPlane.Raycast(ray, out hitDist))
        {
            UnityEngine.Debug.Log("We hit something");
            Vector3 targetPoint = ray.GetPoint(hitDist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            targetRotation.x = 0;
            targetRotation.z = 0;
            // if(Vector3.Distance(targetPoint,transform.position))

            playerObj.transform.rotation = Quaternion.Slerp(playerObj.transform.rotation, targetRotation, 3f * Time.deltaTime);
        }


        if (Input.GetKey(KeyCode.W))
        {
            UnityEngine.Debug.Log("is walking forward");
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
            anim.SetFloat("isWalking", Input.GetAxis("Vertical"));
        }
        else

            if (Input.GetKey(KeyCode.S))
        {
            UnityEngine.Debug.Log("is walking backward");
            transform.Translate(Vector3.back * movementSpeed * Time.deltaTime);
            anim.SetFloat("isWalking", Input.GetAxis("Vertical"));

        }
        else
        {
            UnityEngine.Debug.Log("is idle");
            anim.SetFloat("isWalking", 0f);

        }


        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
            // anim.SetFloat("isWalking", Input.GetAxis("Vertical"));
        }
        else
        {

            // anim.SetFloat("isWalking", 0f);

        }


        // if (Input.GetKey(KeyCode.S))
        // {
        //    transform.Translate(Vector3.back * movementSpeed * Time.deltaTime);
        //  }


        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
        }

        //shooting
        if (Input.GetMouseButton(0))
        {
            Shoot();
            muzzleFlash.gameObject.SetActive(true);
        }
        else
        {
            muzzleFlash.gameObject.SetActive(false);
        }
    }

    public void Shoot()
     
    {
        if(timer >= rateOfFire)
        {
            Instantiate(Bullet, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation); //.GetComponent<Bullet>().isPlayerOwned = true;
            timer = 0f;

        } else
        {
            timer += Time.deltaTime; 
        }
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Player hit something!"  + collision.gameObject.tag);
        if(collision.gameObject.tag == "Enemy")
        {
            isAlive = false;
            UI.gameObject.SetActive(true);
            player.gameObject.SetActive(false);


        }


    }

    


}
