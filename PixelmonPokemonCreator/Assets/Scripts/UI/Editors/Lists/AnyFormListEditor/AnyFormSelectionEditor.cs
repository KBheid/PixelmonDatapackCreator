using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyFormSelectionEditor : MonoBehaviour
{
	public TMPro.TMP_InputField input;
	public Transform formListHolder;
	public AnyFormListItem formListItemPrefab;

	private List<AnyFormListItem> forms = new List<AnyFormListItem>();
	private AnyFormListItem selectedForm;

	private void Start()
	{
		foreach (Transform t in formListHolder)
		{
			AnyFormListItem afli = t.gameObject.GetComponent<AnyFormListItem>();
			afli.editor = this;
			forms.Add(afli);

			if (selectedForm == null)
				selectedForm = afli;
		}
	}

	public void SelectForm(AnyFormListItem selected)
	{
		selectedForm = selected;
	}

    public void AddForm()
	{
		if (input.text == "")
			return;

		string biomeText = input.text;
		AnyFormListItem added = Instantiate(formListItemPrefab, formListHolder);
		forms.Add(added);
		added.textItem.text = biomeText;
		added.editor = this;

		input.text = "";

		selectedForm = added;
	}

    public void RemoveForm()
	{
		if (selectedForm == null)
			return;

		int index = forms.IndexOf(selectedForm);

		forms.Remove(selectedForm);
		Destroy(selectedForm.gameObject);

		index = (index - 1 >= 0) ? index - 1 : 0;
		
		if (index+1 < forms.Count)
			selectedForm = forms[index];
	}

}
