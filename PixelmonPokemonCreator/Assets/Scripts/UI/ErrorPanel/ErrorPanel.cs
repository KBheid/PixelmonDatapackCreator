using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorPanel : MonoBehaviour
{
    [SerializeField] StatsEditor statsEditor;
    [SerializeField] EvolutionEditor evoEditor;
    [SerializeField] EvolutionConditionEditor evoConditionEditor;
    // [SerializeField] MovesEditor movesEditor;

    [SerializeField] Transform listHolder;
    [SerializeField] ErrorListItem listItemPrefab;
    [SerializeField] TMPro.TMP_Text pageCount;

    List<List<ValidationError>> allErrors;
    List<ValidationError> currentErrors;
    private int errorListIndex = 0;

    public void SetAllErrors(List<List<ValidationError>> totalErrors)
    {
        errorListIndex = 0;
        allErrors = totalErrors;
        pageCount.text = string.Format("{0} / {1}", errorListIndex+1, allErrors.Count);

        if (totalErrors.Count > 0)
        {
            SetActiveErrors(allErrors[errorListIndex]);
            gameObject.SetActive(true);
        }
        else
            gameObject.SetActive(false);
    }

    public void SetActiveErrors(List<ValidationError> errors)
    {
        currentErrors = errors;

        // Clear List
        foreach (Transform t in listHolder)
            Destroy(t.gameObject);

        // Populate List
        foreach (ValidationError error in errors)
        {
            ErrorListItem item = Instantiate(listItemPrefab, listHolder);
            item.SetValidationError(error);
            item.parent = this;
        }
    }

    public void ShowNextError()
    {
        errorListIndex = (errorListIndex + 1 < allErrors.Count) ? errorListIndex+1 : errorListIndex;
        pageCount.text = string.Format("{0} / {1}", errorListIndex + 1, allErrors.Count);
        SetActiveErrors(allErrors[errorListIndex]);
    }

    public void ShowLastError()
    {
        errorListIndex = (errorListIndex - 1 >= 0) ? errorListIndex - 1 : errorListIndex;
        pageCount.text = string.Format("{0} / {1}", errorListIndex + 1, allErrors.Count);
        SetActiveErrors(allErrors[errorListIndex]);
    }

    public void CopyError()
    {
        string errorsToString = "";
        foreach (ValidationError error in currentErrors)
            errorsToString += error.errorMessage + "\n\n";
        if (errorsToString.Length > 0)
            GUIUtility.systemCopyBuffer = errorsToString[0..^2];
    }

    internal void ShowError(ValidationError error)
    {
        if (error.pokemon != null)
        {
            PokemonManager.instance.SelectPokemon(error.pokemon);
        }
        if (error.form != null)
        {
            evoEditor.gameObject.SetActive(false);
            statsEditor.gameObject.SetActive(true);
            PokemonManager.instance.SelectForm(error.form);
        }
        if (error.evolution != null)
        {
            statsEditor.gameObject.SetActive(false);
            evoEditor.gameObject.SetActive(true);
            evoEditor.SelectEvolution(error.evolution);
        }
        if (error.condition != null) 
        {
            evoConditionEditor.gameObject.SetActive(true);
            evoConditionEditor.SetCondition(error.condition);
        }
    }
}
