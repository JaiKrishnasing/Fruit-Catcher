using System.Collections.Generic;
using UnityEngine;

public class ChangeQuality : MonoBehaviour
{
    private Dictionary<string, int> map;

#region Singelton

    public static ChangeQuality instance;

    private void Awake()
    {
        instance = this;
    }

#endregion

    private void Start()
    {
        // Woordenboek gebruiken om de kwaliteitslevels te bewaren
        map = new Dictionary<string, int>
        {
            { "low", 0 },
            { "medium", 1 },
            { "high", 2 },
            { "ultra", 3 }
        };
    }

    // De kwaliteitsinstelling van het spel te wijzigen op basis van de slider waarde
    public void ChangeQualityGame(int sliderValue)
    {
        string qualityLevel = GetQualityLevelString(sliderValue);

        if (qualityLevel == null) return;

        QualitySettings.SetQualityLevel(map[qualityLevel]);
    }

    // De UI updated zodat de juiste kwaliteitswaarde wordt weergegeven 
    public string ChangeQualityUI(int sliderValue)
    {
        return GetQualityLevelString(sliderValue);
    }

    // Hulpfunctie om het kwaliteitsniveau string te krijgen op basis van de slider waarde
    private string GetQualityLevelString(int sliderValue)
    {
        foreach(var pair in map)
        {
            if (pair.Value == sliderValue)
            {
                return pair.Key;
            }
        }

        return null;
    }

}
