using UnityEngine;
using UnityEngine.UI;

public class PlayerPrefsManager : MonoBehaviour
{
    public Text highscoreUI;
    public Text currencyIU;

    private void Start()
    {
        SetPlayerPrefs();
    }

    // De UI updaten op basis van de opgeslagen highscore en currency
    public void SetPlayerPrefs()
    {
        int highscore = PlayerPrefs.GetInt("highscore");
        int currency = PlayerPrefs.GetInt("currency");

        highscoreUI.text = highscore.ToString();
        currencyIU.text = "$" + currency.ToString();
    }
}
