using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EVYieldsEditor : MonoBehaviour
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

        hpInput.text        = selectedForm.evYields.hp.ToString();
        attackInput.text    = selectedForm.evYields.attack.ToString();
        spAttackInput.text  = selectedForm.evYields.specialAttack.ToString();
        defenseInput.text   = selectedForm.evYields.defense.ToString();
        spDefenseInput.text = selectedForm.evYields.specialDefense.ToString();
        speedInput.text     = selectedForm.evYields.speed.ToString();
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

    public void UpdateFormEVYields()
	{
        if (selectedForm == null)
            return;

        selectedForm.evYields = new Evyields();
        selectedForm.evYields.hp            = hpInput.text.ToIntegerOrNegativeOne();
        selectedForm.evYields.attack        = attackInput.text.ToIntegerOrNegativeOne();
        selectedForm.evYields.specialAttack = spAttackInput.text.ToIntegerOrNegativeOne();
        selectedForm.evYields.defense       = defenseInput.text.ToIntegerOrNegativeOne();
        selectedForm.evYields.specialDefense = spDefenseInput.text.ToIntegerOrNegativeOne();
        selectedForm.evYields.speed         = speedInput.text.ToIntegerOrNegativeOne();
	}

    public void SetForm(Form f)
    {
        selectedForm = f;
        UpdateValues();
    }

}