using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GigantamaxEditor : MonoBehaviour
{
    [SerializeField] Toggle canGigantamaxToggle;
    [SerializeField] Toggle canHaveFactorToggle;
    [SerializeField] TMPro.TMP_InputField formInput;
    [SerializeField] TMPro.TMP_InputField moveInput;

    Form selectedForm;

    private bool updating = false;

    void UpdateValues()
    {
        updating = true;

        SetInputActive(selectedForm != null);
        if (selectedForm == null)
            return;

        canGigantamaxToggle.isOn = selectedForm.gigantamax.canGigantamax;
        canHaveFactorToggle.isOn = selectedForm.gigantamax.canHaveFactor;

        if (selectedForm.gigantamax.form != null)
            formInput.text = selectedForm.gigantamax.form;
        else
            formInput.text = "";
            
        if (selectedForm.gigantamax.move != null)
            moveInput.text = selectedForm.gigantamax.move;
        else
            moveInput.text = "";
        
        updating = false;
    }

    void SetInputActive(bool active)
	{
        canGigantamaxToggle.interactable = active;
        canHaveFactorToggle.interactable = active;
        formInput.interactable = active;
        moveInput.interactable = active;
    }

    public void UpdateGigantamax()
	{
        if (selectedForm == null || updating)
            return;

        selectedForm.gigantamax = new Gigantamax();
        selectedForm.gigantamax.canGigantamax = canGigantamaxToggle.isOn;
        selectedForm.gigantamax.canHaveFactor = canHaveFactorToggle.isOn;

        if (formInput.text != "")
            selectedForm.gigantamax.form = formInput.text;

        if (moveInput.text != "")
            selectedForm.gigantamax.move = moveInput.text;
	}

    public void SetForm(Form f)
    {
        selectedForm = f;
        UpdateValues();
    }
}
