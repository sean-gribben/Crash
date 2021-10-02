using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuantityController : MonoBehaviour
{
    private static QuantityController quan;
    public static QuantityController instance { get { return quan; } }
    void OnEnable() { quan = this; }
    void OnDisable() { quan = null; }

    public int stockQuantity = 1;

    public int maxQuantity = 2;

    Button plusButton;
    Button minusButton;

    Text quantityText;

    public AudioSource ButtonSound;
    public AudioSource ErrorSound;

    private void Start() {
        plusButton = transform.Find("Increase").GetComponent<Button>();
        minusButton = transform.Find("Decrease").GetComponent<Button>();
        quantityText = transform.Find("Quantity").GetComponent<Text>();

        plusButton.onClick.AddListener(IncreaseQuantity);
        minusButton.onClick.AddListener(DecreaseQuantity);
    }

    void IncreaseQuantity() {
        if(stockQuantity != maxQuantity) {
            ButtonSound.Play();
        }

        stockQuantity = Mathf.Min(maxQuantity, stockQuantity * 2);
        quantityText.text = stockQuantity.ToString();
    }

    void DecreaseQuantity() {
        if (stockQuantity != 1) {
            ButtonSound.Play();
        }
        stockQuantity = Mathf.Max(1, stockQuantity / 2);
        quantityText.text = stockQuantity.ToString();
    }

}
