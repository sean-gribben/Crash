using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct ShopItem {
    public int price;
    public string description;
    public System.Action itemFunc;

    public ShopItem(int price, string description, System.Action itemFunc) {
        this.price = price;
        this.description = description;
        this.itemFunc = itemFunc;
    }
}

public class ShopController : MonoBehaviour
{
    private static ShopController shop;
    public static ShopController instance { get { return shop; } }
    void OnEnable() { shop = this; }
    void OnDisable() { shop = null; }

    public Vector2 itemPlaceLocation = new Vector2(-98, 156);

    public GameObject ShopItemPrefab;

    List<ShopItemBehaviour> shopItems = new List<ShopItemBehaviour>();
    List<ShopItem> queuedItems = new List<ShopItem>();

    public AudioSource ItemBoughtSound;
    public AudioSource NewItemSound;
    public AudioSource ErrorSound;


    void pass() {

    }

    private void Start() {
        // AddShopItem(new ShopItem(400, "Testing1", pass));
    }

    public void AddShopItem(ShopItem item) {
        if(shopItems.Count >= 14) {
            queuedItems.Add(item);
            return;
        }

        GameObject newShopitem = Instantiate(ShopItemPrefab);

        ShopItemBehaviour itemBehav = newShopitem.GetComponent<ShopItemBehaviour>();

        itemBehav.id = shopItems.Count;
        itemBehav.price = item.price;
        itemBehav.description = item.description;
        itemBehav.itemFunc = item.itemFunc;

        shopItems.Add(itemBehav);
        newShopitem.transform.SetParent(transform);

        RectTransform itemTrans = newShopitem.GetComponent<RectTransform>();

        itemTrans.localScale = new Vector3(1, 1, 1);
        itemTrans.anchoredPosition = itemPlaceLocation;

        itemPlaceLocation.y -= 52;
        if(itemPlaceLocation.y <= -208) {
            itemPlaceLocation.y = 156;
            itemPlaceLocation.x += 196;
        }

        NewItemSound.Play();
    }

    public void RemoveShopItem(int id) {
        shopItems.RemoveAt(id);

        for(int i = id; i < shopItems.Count; i++) {
            shopItems[i].id--;
            RectTransform itemTrans = shopItems[i].GetComponent<RectTransform>();

            Vector2 newPos = itemTrans.anchoredPosition;
            newPos.y += 52;
            if(newPos.y > 156) {
                newPos.x -= 196;
                newPos.y = -156;
            }

            itemTrans.anchoredPosition = newPos;

        }

        itemPlaceLocation.y += 52;
        if (itemPlaceLocation.y > 156) {
            itemPlaceLocation.y = -156;
            itemPlaceLocation.x -= 196;
        }

        if(queuedItems.Count > 0) {
            AddShopItem(queuedItems[0]);
            queuedItems.RemoveAt(0);
        }
    }


}
