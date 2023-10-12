using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoveSearchBar : MonoBehaviour
{
	public TMPro.TMP_InputField inputField;
	public TMPro.TMP_Dropdown dropdown;

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

		var valid = MovesManager.instance.moves.Where(move => move.attackName.ToLower().Contains(searchString));
		var validList = new List<Move>(valid);

		var options = validList.ConvertAll(x => new TMPro.TMP_Dropdown.OptionData(x.attackName, null));
		
		dropdown.ClearOptions();
		dropdown.AddOptions(options);

		RefreshOptions();
		inputField.ActivateInputField();
		fixInputFieldPosition = true;

	}

	public void OptionSelected()
	{
		inputField.text = dropdown.options[dropdown.value].text;
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
