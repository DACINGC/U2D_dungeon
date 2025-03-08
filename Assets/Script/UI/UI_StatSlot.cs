using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_StatSlot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private UI ui;

    [SerializeField] private string statName;
    [SerializeField] private StatType statType;
    [SerializeField] private Text statValueText;
    [SerializeField] private Text statNameText;

    [TextArea]
    [SerializeField] private string description;
    private void OnValidate()
    {
        gameObject.name = "Stat-" + statName;

        if(statNameText != null)
            statNameText.text = statName;
    }

    private void Awake()
    {
        ui = GetComponentInParent<UI>();
    }
    private void Start()
    {
        UpdateStatValueUI();
    }
    public void UpdateStatValueUI()
    {
        PlayerStat playerStat = PlayerManager.instance.player.stat;

        if (playerStat != null)
        { 
            statValueText.text = playerStat.GetStat(statType).GetValue().ToString();

            if (statType == StatType.damage)
                statValueText.text = (playerStat.damage.GetValue() + playerStat.strength.GetValue()).ToString();
            else if (statType == StatType.evation)
                statValueText.text = (playerStat.evation.GetValue() + playerStat.agility.GetValue()).ToString();
            else if (statType == StatType.critChance)
                statValueText.text = (playerStat.critChance.GetValue() + playerStat.strength.GetValue()).ToString();
            else if (statType == StatType.critPower)
                statValueText.text = (playerStat.critPower.GetValue() + playerStat.strength.GetValue()).ToString();
            else if (statType == StatType.magicPower)
                statValueText.text = (playerStat.magicPower.GetValue() + playerStat.intelligence.GetValue()).ToString();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ui.statToolTip.ShowToolTip(description);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ui.statToolTip.HideToolTip();
    }
}
