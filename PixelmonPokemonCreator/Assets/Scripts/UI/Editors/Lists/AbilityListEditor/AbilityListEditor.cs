using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityListEditor : MonoBehaviour
{
	[SerializeField] AbilityEditor editor;

	public TMPro.TMP_InputField input;
	public Transform abilityListHolder;
	public AbilityListItem abilityListItemPrefab;
	public List<string> currentAbilities;

	private List<AbilityListItem> abilityListItems = new List<AbilityListItem>();
	private AbilityListItem selectedAbility;

	private void Start()
	{
		foreach (Transform t in abilityListHolder)
		{
			AbilityListItem ali = t.gameObject.GetComponent<AbilityListItem>();
			ali.editor = this;
			abilityListItems.Add(ali);

			if (selectedAbility == null)
				selectedAbility = ali;
		}
	}

	public void SetAbilities(string[] newAbilities)
    {
		ClearList();

		if (newAbilities == null || newAbilities.Length == 0)
			return;

		foreach (string s in newAbilities)
			AddAbility(s);
    }

	public void SelectAbility(AbilityListItem selected)
	{
		selectedAbility = selected;
	}

    public void AddAbility()
	{
		if (input.text == "")
			return;

		string abilityName = input.text;
		AddAbility(abilityName);
		input.text = "";
	}

	void AddAbility(string abilityName)
	{
		AbilityListItem added = Instantiate(abilityListItemPrefab, abilityListHolder);
		abilityListItems.Add(added);
		added.textItem.text = abilityName;
		added.editor = this;

		selectedAbility = added;
		currentAbilities.Add(abilityName);
		
		editor.UpdateContent();
	}

    public void RemoveAbility()
	{
		if (selectedAbility == null)
			return;

		int index = abilityListItems.IndexOf(selectedAbility);
		abilityListItems.Remove(selectedAbility);
		currentAbilities.Remove(selectedAbility.textItem.text);
		Destroy(selectedAbility.gameObject);

		index = (index - 1 >= 0) ? index - 1 : 0;
		
		if (index+1 < abilityListItems.Count)
			selectedAbility = abilityListItems[index];

		editor.UpdateContent();
	}

	void ClearList()
    {
		foreach (AbilityListItem item in abilityListItems)
		{
			Destroy(item.gameObject);

		}
		abilityListItems.Clear();
		currentAbilities.Clear();
	}
}
