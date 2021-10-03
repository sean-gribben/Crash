using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPopulator : MonoBehaviour
{

    private void OnEnable() { MarketController.onMarketTick += shopTick; }
    private void OnDisable() { MarketController.onMarketTick -= shopTick; }

    public AudioSource newStock;

    public float stockManipulationAmount = 0.5f;

    void endGame() {
        Application.Quit();
    }

    void payMobBack() {
        NotificationController.instance.ShowPopup("Congratulations!", "You won!", popupTypes.ok, endGame);
    }

    int stocks = 2;
    void addStock() {
        MarketController.instance.AddStock(Mathf.Max(5f, 100f - (stocks)*15), 150f - stocks*15);
        if (stocks < 7) shop.AddShopItem(new ShopItem(10000 * stocks, "Add a more volatile stock to trade with", addStock));
        newStock.Play();
        stocks++;
    }



    int startingQuantity;
    void upgradeBS() {
        QuantityController.instance.maxQuantity *= 2;

        int newMoney = QuantityController.instance.maxQuantity/startingQuantity * 10000;

        shop.AddShopItem(new ShopItem(newMoney, "Increase max buy/sell quantity by 2x", upgradeBS));
    }

    void upgradeNews() {
        switch(NewsController.instance.newsLevel){
            case 0:
                shop.AddShopItem(new ShopItem(30000, "Pay an inside man to give you market updates", upgradeNews));
                break;
        }

        NewsController.instance.newsLevel++;
    }


    void upgradeAdBlocker() {
        NotificationController.instance.ShowPopup(new popupCall("Buy our even BETTER ad blocker!", "Buy our even BETTER ad blocker for a more streamlined experience. Also we'll stop selling your information to the highest bider", popupTypes.ok));
        switch (NewsController.instance.adBlockerLevel) {
            case 0:
                shop.AddShopItem(new ShopItem(50000, "Get access to the ultimate ad blocker", upgradeAdBlocker));
                break;
        }

        NewsController.instance.adBlockerLevel++;
    }

    void buyStockViewer() {
        MarketController.instance.boughtStockPriceViewer = true;
    }


    void crashMarket() {
        CrashController.instance.CauseCrash();
    }

    void boostFirstStock() {
        float newval = MarketController.instance.stonks[0].currValue * 1.6f;
        MarketController.instance.stonks[0].currValue = newval;
        MarketController.instance.stonks[0].values.Add(newval);
    }

    void boostSecondStock() {
        float newval = MarketController.instance.stonks[1].currValue * 1.6f;
        MarketController.instance.stonks[1].currValue = newval;
        MarketController.instance.stonks[1].values.Add(newval);
    }

    void boostThirdStock() {
        float newval = MarketController.instance.stonks[2].currValue * 1.6f;
        MarketController.instance.stonks[2].currValue = newval;
        MarketController.instance.stonks[2].values.Add(newval);
    }


    ShopController shop;

    public void initItems() {
        // Initial Shop items
        shop = ShopController.instance;

        shop.AddShopItem(new ShopItem(1000000, "Pay back the mob and avoid getting shanked", payMobBack));

        startingQuantity = QuantityController.instance.maxQuantity;
        shop.AddShopItem(new ShopItem(10000, "Increase max buy/sell quantity by 2x", upgradeBS));

    }

    public void sellAdBlocker() {
        shop.AddShopItem(new ShopItem(15000, "Get access to an ad blocker", upgradeAdBlocker));
    }

    int ticks = 0;
    void shopTick(string p) {
        ticks++;

        if (ticks == 24) {
            shop.AddShopItem(new ShopItem(10000, "Add a more volatile stock to trade with", addStock));
        }

        if (ticks == 120) {
            shop.AddShopItem(new ShopItem(5000, "Fix the calc error and see your stocks value", buyStockViewer));
        }

        if(ticks == 264) {
            shop.AddShopItem(new ShopItem(10000, "Get access to a better news outlet", upgradeNews));
        }

        if(ticks == 744) {
            shop.AddShopItem(new ShopItem(25000, "Crash the market with your mob connections", crashMarket));
        }

        if(ticks == 504) {
            shop.AddShopItem(new ShopItem(50000, "Manipulate stock " + MarketController.instance.stonks[0].code + " to increase value", boostFirstStock));
        }

        if (ticks == 1008) {
            if (MarketController.instance.stonks.Count < 2) return;
            shop.AddShopItem(new ShopItem(50000, "Manipulate stock " + MarketController.instance.stonks[1].code + " to increase value", boostSecondStock));
        }

        if (ticks == 2016) {
            if (MarketController.instance.stonks.Count < 3) return;
            shop.AddShopItem(new ShopItem(50000, "Manipulate stock " + MarketController.instance.stonks[2].code + " to increase value", boostThirdStock));
        }
    }
}
