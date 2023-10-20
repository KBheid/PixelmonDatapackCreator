using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EvolutionAttributeEditor : MonoBehaviour
{
	public EvolutionEditor editor;
    Evolution selectedEvolution;

	[SerializeField] TMPro.TMP_InputField targetInput;
	[SerializeField] TMPro.TMP_InputField targetFormInput;
	[SerializeField] TMPro.TMP_Dropdown evoTypeDropdown;
	[SerializeField] TMPro.TMP_InputField levelInput;
	[SerializeField] TMPro.TMP_InputField itemIDInput;
	[SerializeField] FormSearchBar formSearch;

	public List<EvoTypeWindow> windows;

	private EvoTypeWindow current;
	bool updating = false;

	void SetInputEnabled(bool enabled)
	{
		targetInput.interactable		= enabled;
		targetFormInput.interactable	= enabled;
		evoTypeDropdown.interactable	= enabled;
		levelInput.interactable			= enabled;
		itemIDInput.interactable		= enabled;

		// Empty out fields if not valid selection
		if (!enabled)
		{
			targetInput.text = "";
			targetFormInput.text = "";

			evoTypeDropdown.value = 0;
			levelInput.text = "";
			itemIDInput.text = "";
		}
	}

	public void UpdateEvolution()
	{
		if (selectedEvolution == null)
			return;

		selectedEvolution.evoType = evoTypeDropdown.options[evoTypeDropdown.value].text;

		switch (selectedEvolution.evoType)
		{
			case "leveling":
				selectedEvolution.level = levelInput.text.ToIntegerOrNegativeOne();
				selectedEvolution.item = null;
				break;
			case "interact":
				selectedEvolution.item = new Item() { itemID = itemIDInput.text };
				selectedEvolution.level = 0;
				break;
		}

		SetEvolutionTarget();
	}

	void UpdateContent()
	{
		if (selectedEvolution == null)
		{
			SetInputEnabled(false);
			return;
		}

		SetInputEnabled(true);

		string[] split = selectedEvolution.to.Split(" form:");
		string species = split[0];
		string form = (split.Length > 1) ? split[1] : "";
		targetInput.text = species;
		targetFormInput.text = form;
		evoTypeDropdown.SetDropdownToStringValue(selectedEvolution.evoType);

		levelInput.text = (selectedEvolution.level == 0) ? "" : selectedEvolution.level.ToString();

		if (selectedEvolution.item != null)
			itemIDInput.text = selectedEvolution.item.itemID;
		else
			itemIDInput.text = "";

		Pokemon evoTarget = PokemonManager.instance.FindPokemon(species);
		if (evoTarget != null)
		{
			List<Form> forms = new List<Form>(evoTarget.forms);
			formSearch.forms = forms;
		}

		UpdateWindow(selectedEvolution.evoType);
	}

	void EvolutionChanged(Evolution e)
	{
		updating = true;
		selectedEvolution = e;
		UpdateContent();
		updating = false;
	}

	public void SetEvolutionTarget()
	{
		if (selectedEvolution == null)
			return;

		string toType = targetInput.text;
		if (targetFormInput.text != "")
		{
			toType += " form:" + targetFormInput.text;
		}

		selectedEvolution.to = toType;

		//PokemonManager.instance.SelectForm(PokemonManager.instance.selectedForm);
	}

	private void UpdateWindow(string evoType)
	{
		foreach (EvoTypeWindow window in windows)
		{
			if (evoType == window.type)
			{
				SetSelection(window);
				return;
			}
		}
	}

	public void EvoTypeSelected()
	{
		if (updating || selectedEvolution == null)
			return;

		string choice = evoTypeDropdown.options[evoTypeDropdown.value].text;
		foreach (EvoTypeWindow window in windows)
		{
			if (window.type == choice)
			{
				SetSelection(window);
			}
		}
		selectedEvolution.evoType = choice;
	}


	private void SetSelection(EvoTypeWindow window)
	{
		if (current.w1 != null)
		{
			current.w1.SetActive(false);
			current.w2.SetActive(false);
		}

		current = window;

		current.w1.SetActive(true);
		current.w2.SetActive(true);
	}
	
	private void OnEnable()
	{
		EvolutionEditor.OnEvolutionChanged += EvolutionChanged;
		current = windows[0];
	}

	private void OnDisable()
	{
		EvolutionEditor.OnEvolutionChanged -= EvolutionChanged;
	}

}

[Serializable]
public struct EvoTypeWindow
{
	public string type;
	public GameObject w1;
	public GameObject w2;
}