using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class AbilitySearchBar : MonoBehaviour
{
	public UnityEvent ItemSelected;

	public TMPro.TMP_InputField inputField;
	public PerformantLargeDropdown dropdown;

	private bool fixInputFieldPosition = false;

	public void SearchSelected()
	{
		dropdown.Show();
		SearchUpdated();
	}

	public void SearchDeselected()
	{
		StartCoroutine(nameof(DelayDropdownClose));
	}

	public void SearchUpdated()
	{
		string searchString = inputField.text.ToLower();

		var options = new List<string>(MovesManager.instance.abilities.Where(ability => ability.ToLower().Contains(searchString.ToLower())));

		dropdown.SetOptions(options);

		RefreshOptions();
		inputField.ActivateInputField();
		fixInputFieldPosition = true;

	}

	public void OptionSelected()
	{
		inputField.text = dropdown.selectedItem;
		dropdown.Hide();
		ItemSelected?.Invoke();
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


	IEnumerator DelayDropdownClose()
	{
		yield return new WaitForSeconds(0.15f);
		dropdown.Hide();
	}
}
