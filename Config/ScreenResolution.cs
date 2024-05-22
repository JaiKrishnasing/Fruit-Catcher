using UnityEngine;

public class ScreenResolution : MonoBehaviour
{
    public float targetAspectRatio = 16f / 9f; //1920x1080 -> 1,7777778 aspect ratio = 16/9

    void Start()
    {
        AdjustScreenResolution();
    }

    void AdjustScreenResolution()
    {
        // Bereken de gewenste breedte op basis van de huidige schermhoogte en de beeldverhouding
        int desiredWidth = Mathf.RoundToInt(Screen.height * targetAspectRatio);

        // Bepaal de breedte: als de gewenste breedte minder is dan of gelijk is aan de huidige schermbreedte, gebruik deze dan; gebruik anders de huidige schermbreedte
        int width = (desiredWidth <= Screen.width) ? desiredWidth : Screen.width;

        // Bereken de hoogte op basis van de breedte en de beeldverhouding
        int height = Mathf.RoundToInt(width / targetAspectRatio);

        // Stel de schermresolutie in op height en width, fullScreen mode is aan
        Screen.SetResolution(width, height, true);
    }
}
