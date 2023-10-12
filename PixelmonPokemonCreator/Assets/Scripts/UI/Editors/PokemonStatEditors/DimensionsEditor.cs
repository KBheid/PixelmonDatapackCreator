using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionsEditor : MonoBehaviour
{
    [SerializeField] TMPro.TMP_InputField heightInput;
    [SerializeField] TMPro.TMP_InputField widthInput;
    [SerializeField] TMPro.TMP_InputField lengthInput;
    [SerializeField] TMPro.TMP_InputField eyeHeightInput;
    [SerializeField] TMPro.TMP_InputField hoverHeightInput;

    Form selectedForm;

    void UpdateContent()
    {
        SetInputActiveState(selectedForm != null);
        if (selectedForm == null)
            return;

        heightInput.text = selectedForm.dimensions.height.ToString();
        widthInput.text = selectedForm.dimensions.width.ToString();
        lengthInput.text = selectedForm.dimensions.length.ToString();
        eyeHeightInput.text = selectedForm.dimensions.eyeHeight.ToString();
        hoverHeightInput.text = selectedForm.dimensions.hoverHeight.ToString();
    }

    void SetInputActiveState(bool active)
	{
        heightInput.interactable = active;
        widthInput.interactable = active;
        lengthInput.interactable = active;
        eyeHeightInput.interactable = active;
        hoverHeightInput.interactable = active;
    }

    public void UpdateFormDimensions()
	{
        if (selectedForm == null)
            return;

        selectedForm.dimensions = new Dimensions();
        selectedForm.dimensions.height      = heightInput.text.ToFloatOrNegativeOne();
        selectedForm.dimensions.width       = widthInput.text.ToFloatOrNegativeOne();
        selectedForm.dimensions.length      = lengthInput.text.ToFloatOrNegativeOne();
        selectedForm.dimensions.eyeHeight   = eyeHeightInput.text.ToFloatOrNegativeOne();
        selectedForm.dimensions.hoverHeight = hoverHeightInput.text.ToFloatOrNegativeOne();
	}

    public void SetForm(Form f)
    {
        selectedForm = f;
        UpdateContent();
    }

}