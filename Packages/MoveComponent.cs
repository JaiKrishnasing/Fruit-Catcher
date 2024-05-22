using UnityEngine;

// Dit script is gekoppeld aan de objecten 

public class MoveComponent : MonoBehaviour
{
    public float speed = 1;

    void Update()
    {
        // Als het spel niet actief is of gepauzeerd is dan het object niet bewegen 
        if (!LogicManager.instance.isGameActive || LogicManager.instance.isGamePaused) return;

        // Snelheid updaten op basis van de huidige snelheid in Logic Manager 
        speed = LogicManager.instance.GetNewSpeed();

        // Beweeg het object naar beneden met de snelheid
        transform.position += Vector3.down * speed * Time.deltaTime;
    }
}
