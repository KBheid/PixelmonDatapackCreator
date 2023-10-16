using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorListItem : MonoBehaviour
{
    public ErrorPanel parent;
    
    [SerializeField] TMPro.TMP_Text errorTitleText;
    [SerializeField] TMPro.TMP_Text errorBodyText;

    ValidationError error;

    public void SetValidationError(ValidationError error)
    {
        errorTitleText.text = error.errorType;
        errorBodyText.text = error.errorMessage;

        this.error = error;
    }

    public void SelectButton()
    {
        parent.ShowError(error);
    }
}
