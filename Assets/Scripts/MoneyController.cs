using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyController : MonoBehaviour
{

    private static MoneyController bank;
    public static MoneyController instace { get { return bank; } }
    void OnEnable() { bank = this; MarketController.onMarketTickLate += UpdateTotalMoney; }
    void OnDisable() { bank = null; MarketController.onMarketTickLate -= UpdateTotalMoney; }

    public float money = 20000f;

    public Sprite uparrow;
    public Sprite downarrow;

    Text totalMoneyText;
    Text assetMoneyText;
    Text liquidMoneyText;

    Image arrowImage;


    private void Start() {
        totalMoneyText = transform.Find("totalmon").GetComponent<Text>();
        assetMoneyText = transform.Find("assetmon").GetComponent<Text>();
        liquidMoneyText = transform.Find("liquidmon").GetComponent<Text>();

        arrowImage = transform.Find("Arrow").GetComponent<Image>();

        totalMoneyText.text = money.ToString("N0");
        liquidMoneyText.text = money.ToString("N0");
    }

    public bool UpdateLiquid(float update) {
        return UpdateLiquid(update, true);
    }
   public bool UpdateLiquid(float update, bool addToAssets) {
        if(money + update < 0) {
            return false;
        }
        money += update;
        liquidMoneyText.text = money.ToString("N0");

        if (addToAssets) {
            float newAssetMoney = (float.Parse(assetMoneyText.text) - update);
            if (newAssetMoney < 0.03f) {
                newAssetMoney = 0f;
            }

            assetMoneyText.text = newAssetMoney.ToString("N0");
        }

        
        return true;
    }

    public void UpdateTotalMoney() {
        float assets = 0f;
        foreach(StockBehaviour stock in MarketController.instance.stonks) {
            assets += stock.currValue * stock.stocks;
        }
        if(assets > float.Parse(assetMoneyText.text)) {
            arrowImage.sprite = uparrow;
        } else {
            arrowImage.sprite = downarrow;
        }
        assetMoneyText.text = assets.ToString("N0");
        totalMoneyText.text = (money + assets).ToString("N0");
    }
}
