using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int numberOfPickedUpCoins;
    public Text numberOfPickedUpCoinsText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickUpCoin(int numberOfCoins)
    {
        numberOfPickedUpCoins += numberOfCoins;
        numberOfPickedUpCoinsText.text = "Coins: " + numberOfPickedUpCoins;
    }
}
