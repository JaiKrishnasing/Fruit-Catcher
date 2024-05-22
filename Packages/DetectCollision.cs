using UnityEngine;

// Controleer op collision met de grond en de speler, gekoppeld aan objecten

public class DetectCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ground"))
        {
            LogicManager.instance.TakeDamage(10);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Player"))
        {
            LogicManager.instance.UpdateScore(100);
            Destroy(gameObject);
        }

        if(gameObject.tag == "danger" && other.gameObject.CompareTag("Player"))
        {
            LogicManager.instance.EndGame();
        }


    }

}
