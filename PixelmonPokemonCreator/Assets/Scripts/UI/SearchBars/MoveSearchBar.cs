using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoveSearchBar : MonoBehaviour
{
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
		yield return new WaitForSeconds(0.1f);
		dropdown.Hide();
	}
}
