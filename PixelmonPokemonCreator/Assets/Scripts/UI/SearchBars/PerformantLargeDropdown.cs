using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PerformantLargeDropdown : MonoBehaviour
{
	public UnityEvent<string> OptionSelectedEvent;

	[SerializeField] PerformantDropdownItem dropdownItemPrefab;
	[SerializeField] GameObject dropdownWindow;
	[SerializeField] Transform optionsHolder;

	public string selectedItem;

	List<PerformantDropdownItem> options = new List<PerformantDropdownItem>();
	int currentItemCount = 0;

	public void SetOptions(List<string> strOptions)
	{
		ReserveSpaceIfNecessary(strOptions.Count);

		// Set text and enable required options
		for (int i=0; i<strOptions.Count; i++)
		{
			options[i].textItem.text = strOptions[i];
			options[i].gameObject.SetActive(true);
		}

		// Disable options that we do not need
		for (int i=strOptions.Count; i<options.Count; i++)
		{
			options[i].gameObject.SetActive(false);
		}

		currentItemCount = strOptions.Count;
	}

	public void AddOptions(List<string> strOptions)
	{
		ReserveSpaceIfNecessary(strOptions.Count);

		for (int i=0; i<strOptions.Count; i++)
		{
			PerformantDropdownItem option = options[i + currentItemCount];
			option.textItem.text = strOptions[i];
			option.gameObject.SetActive(true);
		}
		currentItemCount += strOptions.Count;
	}

	public void ClearOptions()
	{
		if (options.Count == 0)
			return;

		foreach (PerformantDropdownItem item in options)
		{
			item.gameObject.SetActive(false);
		}
	}

	public void Show()
	{
		dropdownWindow.SetActive(true);
	}

	public void Hide()
	{
		dropdownWindow.SetActive(false);
	}

	public void Refresh()
	{
		dropdownWindow.SetActive(false);
		dropdownWindow.SetActive(true);
	}

	public void SelectOption(string option)
	{
		selectedItem = option;
		OptionSelectedEvent?.Invoke(option);
	}

	void ReserveSpaceIfNecessary(int space)
	{
		// Add items if necessary
		while (options.Count < space)
		{
			PerformantDropdownItem added = Instantiate(dropdownItemPrefab, optionsHolder);
			added.dropdownParent = this;
			options.Add(added);
			added.gameObject.SetActive(false);
		}
	}
}
