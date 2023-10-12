using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressionEditor : MonoBehaviour
{
    [SerializeField] TMPro.TMP_InputField timidInput;
    [SerializeField] TMPro.TMP_InputField passiveInput;
    [SerializeField] TMPro.TMP_InputField aggressiveInput;

    Form selectedForm;

    void UpdateValues()
    {
        SetInputActiveState(selectedForm != null);
        if (selectedForm == null)
            return;

        timidInput.text         = selectedForm.aggression.timid.ToString();
        passiveInput.text       = selectedForm.aggression.passive.ToString();
        aggressiveInput.text    = selectedForm.aggression.aggressive.ToString();
    }

    public void UpdateFormAggression()
	{
        selectedForm.aggression = new Aggression();
        selectedForm.aggression.timid = timidInput.text.ToIntegerOrNegativeOne();
        selectedForm.aggression.passive = passiveInput.text.ToIntegerOrNegativeOne();
        selectedForm.aggression.aggressive = aggressiveInput.text.ToIntegerOrNegativeOne();
	}

    void SetInputActiveState(bool active)
	{
        timidInput.interactable = active;
        passiveInput.interactable = active;
        aggressiveInput.interactable = active;
    }

    public void SetForm(Form f)
    {
        selectedForm = f;
        UpdateValues();
    }
}