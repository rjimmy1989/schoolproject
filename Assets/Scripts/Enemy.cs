using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Transform goal;
    public NavMeshAgent playerAgent;

    public float health;
    public float timer;
    public float reTargetTime;

    void Awake()
    {
        playerAgent = GetComponent<NavMeshAgent>();
    }

    public void AssignTarget(Transform assignTarget)

    {
        goal = assignTarget;
        timer = reTargetTime;
        playerAgent.destination = goal.position;

    }


    public void Update()
    {
        if (goal != null)
        {
            Debug.Log("Enemy retargetting timer" + timer);
            timer -= reTargetTime;
            if (timer <= 0)
            {
                AssignTarget(goal);

            }
        }




        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        print("Enemy" + this.gameObject.name + " has died");
        Destroy(this.gameObject);
    }

    

}









