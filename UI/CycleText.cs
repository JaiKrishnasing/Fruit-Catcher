using UnityEngine;
using UnityEngine.UI;

public class CycleText : MonoBehaviour
{
    public Text facts;
    public string[] funFacts;  // Array om fun facts op te slaan
    public float changeInterval = 5f;  // Tijdsinterval (na hoeveel seconden verandert het funfact)

    private int currentFactIndex = 0; // Huidige index van feit
    private float timer;

    void Start()
    {
        timer = changeInterval;

        // Het eerste feit weergeven
        if (funFacts.Length > 0)
        {
            facts.text = funFacts[currentFactIndex];
        }
    }

    void Update()
    {
        timer -= Time.deltaTime;

        // Als de timer 0 heeft bereikt naar het volgende feit gaan en de UI updaten
        if (timer <= 0f)
        {
            timer = changeInterval;

            currentFactIndex = (currentFactIndex + 1) % funFacts.Length;

            facts.text = funFacts[currentFactIndex];
        }
    }
}
