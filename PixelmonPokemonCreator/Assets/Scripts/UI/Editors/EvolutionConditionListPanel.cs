using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvolutionConditionListPanel : MonoBehaviour
{

    [SerializeField] EvolutionAttributeEditor editor;
    [SerializeField] Transform conditionListHolder;
    [SerializeField] EvoConditionListItem listItemPrefab;
    [SerializeField] EvolutionConditionEditor conditionEditor;
    [SerializeField] Button editButton;

    Evolution selectedEvolution;

    EvoConditionListItem selectedItem;
    Condition selectedCondition;

    public void AddCondition()
	{
        if (selectedEvolution == null)
            return;

        Condition newCondition = new Condition();
        newCondition.evoConditionType = "time";
        newCondition.time = "DAY";

        EvoConditionListItem item = Instantiate(listItemPrefab, conditionListHolder);
        item.SetCondition(newCondition);
        item.editor = this;

        List<Condition> conditions = new List<Condition>(selectedEvolution.conditions);
        conditions.Add(newCondition);

        selectedEvolution.conditions = conditions.ToArray();

        selectedItem = item;
        selectedCondition = newCondition;

        Edit();
	}

    public void RemoveCondition()
	{
        if (selectedEvolution == null)
            return;

        List<Condition> conditions = new List<Condition>(selectedEvolution.conditions);
        conditions.Remove(selectedCondition);
        selectedEvolution.conditions = conditions.ToArray();

        Destroy(selectedItem.gameObject);
	}

	public void SetEvolution(Evolution evo)
	{
        selectedEvolution = evo;

        SelectCondition(null);

        ClearConditions();

        if (evo != null)
            PopulateConditions();
	}

    public void SelectCondition(Condition c)
	{
        selectedCondition = c;

        editButton.interactable = selectedCondition != null;
    }

    internal void SelectItem(EvoConditionListItem evoConditionListItem)
    {
        selectedItem = evoConditionListItem;

        SelectCondition(evoConditionListItem.condition);
    }

    public void Edit()
	{
        conditionEditor.SetCondition(selectedCondition);
        conditionEditor.gameObject.SetActive(true);
	}

    void ClearConditions()
	{
        foreach (Transform t in conditionListHolder)
        {
            Destroy(t.gameObject);
        }
    }

    void PopulateConditions()
	{
        if (selectedEvolution.conditions == null)
            return;

        foreach (Condition c in selectedEvolution.conditions)
        {
            EvoConditionListItem item = Instantiate(listItemPrefab, conditionListHolder);
            item.SetCondition(c);
            item.editor = this;
        }
    }

    private void OnEnable()
    {
        ClearConditions();
        EvolutionEditor.OnEvolutionChanged += SetEvolution;
    }

    private void OnDisable()
    {
        EvolutionEditor.OnEvolutionChanged -= SetEvolution;
    }

	internal void OverwriteCondition(Condition condition, Condition newCondition)
	{
        for (int i = 0; i < selectedEvolution.conditions.Length; i++)
        {
            if (selectedEvolution.conditions[i] == condition)
            {
                selectedEvolution.conditions[i] = newCondition;
                SelectCondition(newCondition);
                selectedItem.SetCondition(newCondition);
                return;
            }
        }
        print("Likely error: could not find condition to overwrite.");
	}
}
