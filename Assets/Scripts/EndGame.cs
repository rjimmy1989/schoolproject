using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public GameObject UI;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("door detects  " + other.name);
        if (other.tag == "Player")
        {
                       
            UI.gameObject.SetActive(true);
            Destroy(other.gameObject);
        }



    }
}
