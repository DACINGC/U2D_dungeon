using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SkillToolTip : UI_ToolTip
{
    [SerializeField] private Text skillNameText;
    [SerializeField] private Text skillDescriptionText;
    [SerializeField] private Text skillColstText;

    public void ShowSkillToolTip(UI_SkillSlot skill)
    {
        gameObject.SetActive(true);
        AdjustPosition();
        skillNameText.text = skill.skillName;
        skillDescriptionText.text = skill.skillDescription;
        skillColstText.text = "Cost:" + skill.price.ToString();
    }

    public void HideSkillToolTip()
    {
        gameObject.SetActive(false);
    }
}
