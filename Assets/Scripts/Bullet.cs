using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float maxDistance;
    public float currentDistance;
    public float damage;
    private GameObject triggeringEnemy;
    public bool isPlayerOwned;

    private void Update()
  {
       transform.Translate(Vector3.forward * Time.deltaTime * speed);
       currentDistance += 1 * Time.deltaTime;

        if (currentDistance >= maxDistance)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("bullet hit "  +  other.name);
        if(other.tag == "Enemy")
        {
            
            triggeringEnemy = other.gameObject;
            triggeringEnemy.GetComponent<Enemy>().health -= damage;
        }


            
    }





}

