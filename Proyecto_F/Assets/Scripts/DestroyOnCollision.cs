using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collison)
    {
        if (collison.collider.CompareTag("Car"))
        {
            Destroy(collison.gameObject);
        }



    }

    //private void OnTriggerEnter(Collision other)
    //{
    //    if (other.collider.CompareTag("Car"))
    //    {
    //        Destroy(other.gameObject);
    //    }
    //}
}
