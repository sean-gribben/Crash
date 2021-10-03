using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MarketController : MonoBehaviour
{
    private static MarketController market;
    public static MarketController instance { get { return market; } }
    void OnEnable() { market = this; }
    void OnDisable() { market = null; }

    public GameObject tutorial;
    public AudioSource contSound;


    public float minVolatility = 1f;
    public float maxVolatility = 6f;

    public float minDph = -0.03f;
    public float maxDph = 0.07f;

    public float minStockPrice;


    public float secondsPerHour;
    float sphTimer;

    public GameObject stock;
    public List<StockBehaviour> stonks = new List<StockBehaviour>();

    public System.Random rand = new System.Random();

    private string[] tickers = { "PISS", "DUMP", "MUC", "SUSY", "BALZ", "IMP", "GME", "RKT", "BB", 
        "NOK", "AMC", "SPCE", "LIT", "NUT", "JAZZ", "POG", "KEK", "UWU", "GARY", "LGBT", "MOON", "ATSM", "E",
        "LMAO"};

    private RectTransform rectTrans;
    private float stockAddLocator = 182f;

    public static event System.Action<string> onMarketTick;
    public static event System.Action onMarketTickLate;

    public bool paused = true;

    void Start()
    {
        tickers = tickers.OrderBy(x => rand.Next()).ToArray();

        rectTrans = GetComponent<RectTransform>();
        sphTimer = secondsPerHour;

        // Add an initial stock
        AddStock(100f, 150f);
/*        AddStock();
        AddStock();
        AddStock();
        AddStock();
        AddStock();
        AddStock();
        AddStock();*/


    }


    public void AddStock(float minStartVal, float maxStartVal) {
        GameObject newStock = Instantiate(stock);

        StockBehaviour stockBehav = newStock.GetComponent<StockBehaviour>();
        stockBehav.rand = rand;
        stockBehav.values.Add(RandomUtility.NextFloat(rand, minStartVal, maxStartVal));
        stockBehav.volatility = RandomUtility.NextFloat(rand, minVolatility, maxVolatility);
        stockBehav.trueDph = RandomUtility.NextFloat(rand, minDph, maxDph);
        stockBehav.code = tickers[stonks.Count];
        stockBehav.minStockPrice = minStockPrice;

        stockBehav.UpdateSocks("Init");


        stonks.Add(stockBehav);
        newStock.transform.SetParent(transform);
        RectTransform newStockRect = newStock.GetComponent<RectTransform>();

        newStockRect.localScale = new Vector3(1, 1, 1);
        newStockRect.anchoredPosition = new Vector2(0, stockAddLocator);
        stockAddLocator -= 52f;
    }

    public void UpdateStocks(float newdph) {
        foreach(StockBehaviour stck in stonks) {
            stck.dph = newdph;
        }
    }

    public void ResetStocks() {
        foreach (StockBehaviour stck in stonks) {
            stck.dph = stck.trueDph;
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) {
            paused = !paused;
            tutorial.SetActive(paused);
        }


        if (paused) {
            return;
        }
        sphTimer -= Time.deltaTime;
        if(sphTimer <= 0){
            sphTimer = secondsPerHour;

            onMarketTick("");
            onMarketTickLate();
        }
    }
}
