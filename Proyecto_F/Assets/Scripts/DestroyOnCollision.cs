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
}
