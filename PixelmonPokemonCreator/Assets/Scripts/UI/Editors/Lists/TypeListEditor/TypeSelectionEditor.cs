using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeSelectionEditor : MonoBehaviour
{
	public TMPro.TMP_Dropdown dropdown;
	public Transform typeListHolder;
	public TypeListItem typeListItemPrefab;

	private List<TypeListItem> types = new List<TypeListItem>();
	private TypeListItem selectedType;

	private void Start()
	{
		foreach (Transform t in typeListHolder)
		{
			TypeListItem tli = t.gameObject.GetComponent<TypeListItem>();
			tli.editor = this;
			types.Add(tli);

			if (selectedType == null)
				selectedType = tli;
		}
	}

	public void SelectType(TypeListItem selected)
	{
		selectedType = selected;
	}

    public void AddType()
	{
		if (dropdown.value == 0)
			return;

		string biomeText = dropdown.options[dropdown.value].text;
		TypeListItem added = Instantiate(typeListItemPrefab, typeListHolder);
		types.Add(added);
		added.textItem.text = biomeText;
		added.editor = this;

		dropdown.value = 0;

		selectedType = added;
	}

    public void RemoveType()
	{
		if (selectedType == null)
			return;

		int index = types.IndexOf(selectedType);

		types.Remove(selectedType);
		Destroy(selectedType.gameObject);

		index = (index - 1 >= 0) ? index - 1 : 0;
		
		if (index+1 < types.Count)
			selectedType = types[index];
	}

}
