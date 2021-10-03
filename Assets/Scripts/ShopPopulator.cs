using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPopulator : MonoBehaviour
{

    void endGame() {
        Application.Quit();
    }

    void payMobBack() {
        NotificationController.instance.ShowPopup("Congratulations!", "You won!", popupTypes.ok, endGame);
    }

    int stocks = 2;
    void addStock() {
        MarketController.instance.AddStock(Mathf.Max(5f, 100f - (stocks)*15), 150f - stocks*15);
        if (stocks < 8) shop.AddShopItem(new ShopItem(5000 * stocks, "Add a more volatile stock to trade with", addStock));
        stocks++;
    }



    int startingQuantity;
    void upgradeBS() {
        QuantityController.instance.maxQuantity *= 2;

        int newMoney = QuantityController.instance.maxQuantity/startingQuantity * 2500;

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


    void upgradeAddBlocker() {
        switch (NewsController.instance.addBlockerLevel) {
            case 0:
                shop.AddShopItem(new ShopItem(20000, "Get access to a better add blocker", upgradeAddBlocker));
                break;
        }

        NewsController.instance.addBlockerLevel++;
    }

    ShopController shop;

    public void initItems() {
        // Initial Shop items
        shop = ShopController.instance;

        shop.AddShopItem(new ShopItem(1000000, "Pay back the mob and avoid getting shanked", payMobBack));

        shop.AddShopItem(new ShopItem(5000, "Add a more volatile stock to trade with", addStock));

        startingQuantity = QuantityController.instance.maxQuantity;
        shop.AddShopItem(new ShopItem(2500, "Increase max buy/sell quantity by 2x", upgradeBS));

        shop.AddShopItem(new ShopItem(10000, "Get access to a better news outlet", upgradeNews));

        shop.AddShopItem(new ShopItem(5000, "Get access to a better add blocker", upgradeAddBlocker));

    }
}
