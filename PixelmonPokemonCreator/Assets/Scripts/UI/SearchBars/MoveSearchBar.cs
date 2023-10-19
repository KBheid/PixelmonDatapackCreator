using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class MoveSearchBar : MonoBehaviour
{
	public UnityEvent ItemSelected;

	public TMPro.TMP_InputField inputField;
	public PerformantLargeDropdown dropdown;

	private bool fixInputFieldPosition = false;

	public void SearchSelected()
	{
		if (inputField.interactable)
		{
			dropdown.Show();
			SearchUpdated();
		}
	}

	public void SearchDeselected()
	{
		StartCoroutine(nameof(DelayDropdownClose));
	}

	public void SearchUpdated()
	{
		if (!inputField.interactable)
			return;

		string searchString = inputField.text.ToLower();

		var valid = MovesManager.instance.moves.Where(move => move.attackName.ToLower().Contains(searchString));
		var validList = new List<Move>(valid);

		var options = validList.ConvertAll(x => x.attackName);

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
		if (!inputField.interactable)
			return;

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
		yield return new WaitForSeconds(0.1f);
		dropdown.Hide();
	}
}
