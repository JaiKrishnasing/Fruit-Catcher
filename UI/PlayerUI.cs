using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Header("Menu")]
    public GameObject menuBackground;

    [Header("Settings")]
    public GameObject settingBackground;
    public Text feedbackText;
    public Slider changeQualitySlider;
    public Text FPSText;
    public Slider FPS_Slider;

    [Header("Score")]
    public Text scoreUI;

    [Header("Health")]
    public Image healthBar;

    [Header("End Game")]
    public GameObject parent_endgame;
    public Text endScore;
    public Text currencyUI;
    public Text highScoreUI;

    [Header("Sell")]
    public GameObject sell;
    public Text currencySell;
    public Text scoreSell;

    [Header("Shop")]
    public Text amountOfMoney;
    public GameObject shop;

#region Singelton
    public static PlayerUI instance;

    private void Awake()
    {
        instance = this;
    }

#endregion

    private void Start()
    {
        OnChangeValueSlider();
    }

    public void OnClickPurchaseSpeed()
    {
        if (LogicManager.instance.playerHasSpeedBoost) return;
        LogicManager.instance.HandlePurchasePlayerBoost();
    }

    public void OnClickPurchaseHelper()
    {
        if(LogicManager.instance.playerHasHelper) return;
        LogicManager.instance.HandlePurchaseHelper();
    }

    public void OnClickShopButton()
    {
        LogicManager.instance.isGamePaused = true;
        AudioManager.instance.PlayButtonSettingsMenu();
        shop.SetActive(true);
    }

    public void OnClickPurchaseGift()
    {
        if (LogicManager.instance.health >= 100) return;
        LogicManager.instance.HandlePurchaseGift();
    }

    public void OnClickPurchaseMagnet()
    {
        if (LogicManager.instance.playerHasMagnet) return;
        LogicManager.instance.HandlePurchaseMagnet();
    }

    public void ChangeUIEndGame(int score, int currency, int highScore)
    {
        parent_endgame.SetActive(true);
        currencyUI.text = "$" + currency.ToString();
        highScoreUI.text = highScore.ToString();
        endScore.text = score.ToString();
    }

    public void OnClickExchangeButton()
    {
        LogicManager.instance.isGamePaused = true;
        AudioManager.instance.PlayButtonSettingsMenu();
        sell.SetActive(true);
    }

    public void OnExitExchangeButton()
    {
        LogicManager.instance.isGamePaused = false;
        AudioManager.instance.PlayGameMusic();
        AudioManager.instance.PlayButtonSettingsMenu();
        sell.SetActive(false);
    }

    public void OnExchange()
    {
        if (LogicManager.instance.score <= 0) return;
        LogicManager.instance.Exchange();
        sell.SetActive(false);
        shop.SetActive(true);
    }

    public void SetAmountOfMoneyUI(int currency)
    {
        amountOfMoney.text = "$" + currency.ToString();
    }

    public void OnClickExitShop()
    {
        LogicManager.instance.isGamePaused = false;
        AudioManager.instance.PlayGameMusic();
        shop.SetActive(false);
        AudioManager.instance.PlayButtonSettingsMenu();
    }

    public void OnClickEndRestartButton()
    {
        parent_endgame.SetActive(false);
        AudioManager.instance.PlayButtonSettingsMenu();
        LogicManager.instance.Init();
    }

    public void OnClickSettingButton()
    {
        AudioManager.instance.PlayButtonSettingsMenu();
        AudioManager.instance.PlayButtonSettingsMenu();
        menuBackground.SetActive(true);
        LogicManager.instance.isGamePaused = true;
    } 

    public void OnClickExitButton()
    {
        AudioManager.instance.PlayButtonSettingsMenu();
        menuBackground.SetActive(false);
        LogicManager.instance.isGamePaused = false;
        AudioManager.instance.PlayGameMusic();
    }

    public void OnClickSettingExitButton()
    {
        AudioManager.instance.PlayButtonSettingsMenu();
        menuBackground.SetActive(true);
        settingBackground.SetActive(false);
    }

    public void OnClickGoToSettingButton()
    {
        AudioManager.instance.PlayButtonSettingsMenu();
        menuBackground.SetActive(false);
        settingBackground.SetActive(true);
    }

    public void OnChangeValueSlider()
    {
        int value = (int)changeQualitySlider.value; 

        ChangeQuality.instance.ChangeQualityGame(value);
        feedbackText.text = ChangeQuality.instance.ChangeQualityUI(value).ToString();
    }

    public void OnChangeValueFPSSlider()
    {
        int value = (int)FPS_Slider.value;

        LogicManager.instance.SetFrameRate(value);
        FPSText.text = "FPS:" + value.ToString();
    }

    public void UpdateScoreUI(int score)
    {
        scoreUI.text = score.ToString();
        UpdateSellText(score);
    }

    public void UpdateHealth(int health)
    {
        healthBar.fillAmount = (float) health / 100;
    }

    public void UpdateSellText(int score)
    {
        scoreSell.text = score.ToString();
        currencySell.text = "$" + LogicManager.instance.GetExchange().ToString();
    }

}
