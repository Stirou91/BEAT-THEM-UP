using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    public int bonusCount;
    [SerializeField] GameObject bonusCountText;

    public static Inventory instance;
    TextMeshProUGUI textMeshProBonus;
    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }
    private void Start()
    {
        textMeshProBonus = bonusCountText.GetComponent<TextMeshProUGUI>();
    }
    public void AddCoints(int bonus)
    {

        bonusCount += bonus;
        textMeshProBonus.text = bonusCount.ToString("00000");

    }
}
