using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStatsEditor : MonoBehaviour
{
    [SerializeField] TMPro.TMP_InputField hpInput;
    [SerializeField] TMPro.TMP_InputField attackInput;
    [SerializeField] TMPro.TMP_InputField spAttackInput;
    [SerializeField] TMPro.TMP_InputField defenseInput;
    [SerializeField] TMPro.TMP_InputField spDefenseInput;
    [SerializeField] TMPro.TMP_InputField speedInput;

    Form selectedForm;
    
    void UpdateValues()
	{
        SetInputActiveState(selectedForm != null);
        if (selectedForm == null)
            return;

        hpInput.text        = selectedForm.battleStats.hp.ToString();
        attackInput.text    = selectedForm.battleStats.attack.ToString();
        spAttackInput.text  = selectedForm.battleStats.specialAttack.ToString();
        defenseInput.text   = selectedForm.battleStats.defense.ToString();
        spDefenseInput.text = selectedForm.battleStats.specialDefense.ToString();
        speedInput.text     = selectedForm.battleStats.speed.ToString();
	}

    void SetInputActiveState(bool active)
	{
        hpInput.interactable = active;
        attackInput.interactable = active;
        spAttackInput.interactable = active;
        defenseInput.interactable = active;
        spDefenseInput.interactable = active;
        speedInput.interactable = active;
    }

    public void UpdateFormBattleStats()
	{
        if (selectedForm == null)
            return;

        selectedForm.battleStats = new Battlestats();

        selectedForm.battleStats.hp             = hpInput.text.ToIntegerOrNegativeOne();
        selectedForm.battleStats.attack         = attackInput.text.ToIntegerOrNegativeOne();
        selectedForm.battleStats.specialAttack  = spAttackInput.text.ToIntegerOrNegativeOne();
        selectedForm.battleStats.defense        = defenseInput.text.ToIntegerOrNegativeOne();
        selectedForm.battleStats.specialDefense = spDefenseInput.text.ToIntegerOrNegativeOne();
        selectedForm.battleStats.speed          = speedInput.text.ToIntegerOrNegativeOne();
	}

    public void SetForm(Form f)
	{
        selectedForm = f;
        UpdateValues();
    }
}
