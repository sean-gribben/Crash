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

        totalMoneyText.text = money.ToString();
        liquidMoneyText.text = money.ToString();
    }


    public bool UpdateLiquid(float update) {
        if(money + update < 0) {
            return false;
        }
        money += update;
        liquidMoneyText.text = money.ToString();
        float newAssetMoney = (float.Parse(assetMoneyText.text) - update);
        if(newAssetMoney < 0.03f) {
            newAssetMoney = 0f;
        }

        assetMoneyText.text = newAssetMoney.ToString();
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
        assetMoneyText.text = assets.ToString();
        totalMoneyText.text = (money + assets).ToString();
    }
}
