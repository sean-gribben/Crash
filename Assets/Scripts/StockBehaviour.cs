using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StockBehaviour : MonoBehaviour
{
    public string code;
    public List<float> values = new List<float>();
    public float currValue;
    public int stocks = 0;
    public float volatility;
    public float trueDph;
    public float dph;
    public float minStockPrice;

    public Sprite uparrow;
    public Sprite downarrow;

    public System.Random rand;

    Text codeText;
    Text valueText;
    Text stockCountText;
    Text stockValueText;

    Image arrow;

    Button buyButton;
    Button sellButton;

    private void OnEnable() { MarketController.onMarketTick += UpdateSocks;}
    private void OnDisable() { MarketController.onMarketTick -= UpdateSocks;}


    private void Awake() {
        codeText = transform.Find("Code").GetComponent<Text>();
        valueText = transform.Find("Value").GetComponent<Text>();
        stockCountText = transform.Find("StockCount").GetComponent<Text>();
        stockValueText = transform.Find("StockValue").GetComponent<Text>();

        arrow = transform.Find("Arrow").GetComponent<Image>();

        buyButton = transform.Find("Buy").GetComponent<Button>();
        sellButton = transform.Find("Sell").GetComponent<Button>();

        buyButton.onClick.AddListener(buyStocks);
        sellButton.onClick.AddListener(sellStocks);

        
    }

    private void Start() {
        dph = trueDph;
    }


    public void UpdateSocks(string p) {
        float lastVal = values[values.Count - 1];

        if (p == "Init") {
            currValue = lastVal;
            valueText.text = lastVal.ToString();
            codeText.text = code;
            stockCountText.text = stocks.ToString();
            stockValueText.text = (currValue * stocks).ToString("N0");
            return;
        }
        

        float newVal = Mathf.Clamp(lastVal + RandomUtility.RandNorm(rand, dph, volatility), 0.8f*lastVal, 1.2f*lastVal); // Make sure the increase isn't too enourmous relative to stock price
        newVal = Mathf.Max(minStockPrice, newVal); // Ensure the value of the stock stays positive

        values.Add(newVal);
        valueText.text = newVal.ToString();

        if(newVal > lastVal) {
            arrow.sprite = uparrow;
        } else {
            arrow.sprite = downarrow;
        }
        currValue = newVal;


        stockValueText.text = (currValue * stocks).ToString("N0");
    }

    void buyStocks() {
        if (!MoneyController.instance.UpdateLiquid(-currValue*QuantityController.instance.stockQuantity)) return;
        stocks += QuantityController.instance.stockQuantity;
        stockCountText.text = stocks.ToString();
        stockValueText.text = (currValue * stocks).ToString("N0");
    }

    void sellStocks() {
        if(stocks < 1) {
            return;
        }
        int takeAway = QuantityController.instance.stockQuantity;
        if (stocks < takeAway) {
            takeAway = stocks;
        }

        stocks -= takeAway;
        MoneyController.instance.UpdateLiquid(currValue * takeAway);
        stockCountText.text = stocks.ToString();
        stockValueText.text = (currValue * stocks).ToString("N0");
    }
}
