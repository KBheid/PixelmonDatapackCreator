using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvoConditionListItem : MonoBehaviour
{
	[SerializeField] TMPro.TMP_Text conditionText;

	public EvolutionConditionListPanel editor;
	
	public Condition condition;

	void UpdateContent()
	{
		conditionText.text = condition.evoConditionType;
	}

	public void SetCondition(Condition c)
	{
		condition = c;
		UpdateContent();
	}

	public void Select()
	{
		editor.SelectItem(this);
	}

}
