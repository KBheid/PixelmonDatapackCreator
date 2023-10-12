using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class FormSearchBar : MonoBehaviour
{
	public UnityEvent ItemSelected;

	public TMPro.TMP_InputField inputField;
	public TMPro.TMP_Dropdown dropdown;
	public List<Form> forms;

	private bool fixInputFieldPosition = false;

	public void SearchSelected()
	{
		dropdown.enabled = true;
		SearchUpdated();
	}

	public void SearchDeselected()
	{
		//dropdown.enabled = false;
	}

	public void SearchUpdated()
	{
		string searchString = inputField.text.ToLower();

		var valid = forms.Where(form => form.name.ToLower().Contains(searchString));
		var validList = new List<Form>(valid);

		var options = validList.ConvertAll(x => new TMPro.TMP_Dropdown.OptionData(x.name, null));
		
		dropdown.ClearOptions();
		dropdown.AddOptions(options);

		RefreshOptions();
		inputField.ActivateInputField();
		fixInputFieldPosition = true;

	}

	public void OptionSelected()
	{
		inputField.text = dropdown.options[dropdown.value].text;
		ItemSelected.Invoke();
	}

	private void RefreshOptions()
	{
		dropdown.enabled = false;
		dropdown.enabled = true;
		dropdown.Show();
	}

	private void LateUpdate()
	{
		if (fixInputFieldPosition)
		{
			inputField.MoveTextEnd(true);
			fixInputFieldPosition = false;
		}
	}
}
