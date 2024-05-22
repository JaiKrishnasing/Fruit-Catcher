using UnityEngine;

public class Magnet : MonoBehaviour
{
    private float magnetStrength = 0.525f;

    private void Update()
    {
        // Controleren of de speler een magneet heeft
        if (LogicManager.instance.playerHasMagnet)
        {
            ApplyMagnetEffect();
        }
    }

    private void ApplyMagnetEffect()
    {
        // Alle objecten vinden met de tag 'object' 
        GameObject[] objectsToAttract = GameObject.FindGameObjectsWithTag("object");

        foreach (GameObject obj in objectsToAttract)
        {
            // Het object naar de spelerpositie trekken met behulp van lineaire interpolatie
            obj.transform.position = Vector3.Lerp(obj.transform.position, transform.position, magnetStrength * Time.deltaTime);
        }
    }
}
