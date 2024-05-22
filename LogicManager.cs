using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicManager : MonoBehaviour
{
    public int health = 100;
    private float speedOfObjects = 1f;
    private float speedOfSpawn = 2.5f;
    private float minSpeedOfSpawn = 0.8f;
    private float maxSpeedOfObjects = 7f;
    private int playerSpeedBoostPrice = 100;
    private int playerMagnetPrice = 100;
    private int playerHelperPrice = 100;
    private int playerHealthPrice = 200;
    public float speed = 5;
    public int score = 0;
    public int highScore = 0;
    public int currency = 0;
    public bool isGameActive = true;
    public bool isGamePaused = false;
    public bool playerHasSpeedBoost = false;
    public bool playerHasMagnet = false;
    public bool playerHasHelper = false;

#region Singelton

    public static LogicManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    void Start()
    {
        LoadData();
        AudioManager.instance.PlayGameMusic();
        SetFrameRate(120);
    }

    void Update()
    {
        if(health <= 0)
        {
            EndGame();
        }

        if (isGamePaused)
        {
            AudioManager.instance.PauseGameMusic();
        }

        if (isGameActive)
        {
            speedOfSpawn -= 0.0001f;
            speedOfObjects += 0.0005f;
        }

    }

    public float GetNewSpeed()
    {
        if (speedOfObjects >= maxSpeedOfObjects)
        {
            speedOfObjects = maxSpeedOfObjects;
        }
        return speedOfObjects;
    }

    public float GetNewSpawnRate()
    {
        if(speedOfSpawn <= minSpeedOfSpawn)
        {
            speedOfSpawn = minSpeedOfSpawn;
        }

        return speedOfSpawn;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        PlayerUI.instance.UpdateHealth(health);
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        PlayerUI.instance.UpdateScoreUI(score);
        AudioManager.instance.PlayScoreSFX();
    }

    public void Init()
    {
        SceneManager.LoadScene(1);
        LoadData();
    }

    public void EndGame()
    {
        isGameActive = false;
        playerHasSpeedBoost = false;
        playerHasMagnet = false;
        playerHasHelper = false;
        HighScore();
        PlayerUI.instance.ChangeUIEndGame(score, currency, highScore);
        AudioManager.instance.PauseGameMusic();
        SaveData("highscore", highScore);
        SaveData("currency", currency);
    }

    public void DestroyObjectsOnScreen()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("object");
        GameObject[] dangers = GameObject.FindGameObjectsWithTag("danger");

        foreach (GameObject obj in objects)
        {
            Destroy(obj);
        }

        foreach (GameObject danger in dangers)
        {
            Destroy(danger);
        }
    }

    private void HighScore()
    {
        if(score >= highScore)
        {
            highScore = score;
        }
    }

    public void SaveData(string key, int data)
    {
        PlayerPrefs.SetInt(key, data);
    }

    public void LoadData()
    {
        highScore = PlayerPrefs.GetInt("highscore");
        currency = PlayerPrefs.GetInt("currency");
        PlayerUI.instance.SetAmountOfMoneyUI(currency);
    }

    public void ClearData()
    {
        PlayerPrefs.DeleteAll();
    }

    public int GetExchange()
    {
        return score / 10;
    }

    public void SetFrameRate(int value)
    {
        Application.targetFrameRate = value;
    }

    public void Exchange()
    {
        currency += GetExchange();
        score = 0;
        PlayerUI.instance.SetAmountOfMoneyUI(currency);
        PlayerUI.instance.UpdateScoreUI(score);
    }

    public void HandlePurchasePlayerBoost()
    {
        if (currency < playerSpeedBoostPrice) return;
        currency -= playerSpeedBoostPrice;
        PlayerUI.instance.SetAmountOfMoneyUI(currency);
        SaveData("currency", currency);
        playerHasSpeedBoost = true;
        StartCoroutine(DurationSpeedBoost());
    }

    public void HandlePurchaseMagnet()
    {
        if (currency < playerMagnetPrice) return;
        currency -= playerMagnetPrice;
        PlayerUI.instance.SetAmountOfMoneyUI(currency);
        playerHasMagnet = true;
        SaveData("currency", currency);
        StartCoroutine(DurationMagnet());
    }

    public void HandlePurchaseHelper()
    {
        if (currency < playerHelperPrice) return;
        currency -= playerHelperPrice;
        SaveData("currency", currency);
        PlayerUI.instance.SetAmountOfMoneyUI(currency);
        playerHasHelper = true;
    }

    public void HandlePurchaseGift()
    {
        if (currency < playerHealthPrice) return;
        currency -= playerHealthPrice;
        SaveData("currency", currency);
        health += 20;
        PlayerUI.instance.SetAmountOfMoneyUI(currency);
        PlayerUI.instance.UpdateHealth(health);
    }

    IEnumerator DurationMagnet()
    {
        yield return new WaitForSeconds(25);
        playerHasMagnet = false;
    }

    IEnumerator DurationSpeedBoost()
    {
        yield return new WaitForSeconds(45);
        playerHasSpeedBoost = false;
    }
}
