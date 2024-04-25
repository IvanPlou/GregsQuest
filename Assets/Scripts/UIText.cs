using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinCountText;
    [SerializeField] string coinsInArea = "Total coins in the Area";
    PlayerMovement player;

    void  Awake() 
    {
        player = FindObjectOfType<PlayerMovement>();        
    }
    
    void Start()
    {
        coinCountText.text = "Coins: " + player.coinCount.ToString() + " / " + coinsInArea;
    }

    void Update()
    {
        coinCountText.text = "Coins: " + player.coinCount.ToString() + " / " + coinsInArea;
    }
}
