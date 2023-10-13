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

	private void OnEnable()
	{
		EvolutionEditor.OnEvolutionChanged += EvolutionChanged;
		current = windows[0];
	}

	void EvolutionChanged(Evolution e)
	{
		selectedEvolution = e;
		if (e == null)
		{
			targetInput.interactable = false;
			targetFormInput.interactable = false;
			evoTypeDropdown.interactable = false;
			levelInput.interactable = false;
			itemIDInput.interactable = false;

			targetInput.text = "";
			targetFormInput.text = "";

			evoTypeDropdown.value = 0;
			levelInput.text = "";
			itemIDInput.text = "";
			return;
		}

		targetInput.interactable = true;
		targetFormInput.interactable = true;
		evoTypeDropdown.interactable = true;
		levelInput.interactable = true;
		itemIDInput.interactable = true;

		string[] split = e.to.Split(" form:");
		string species = split[0];
		string form = (split.Length > 1) ? split[1] : "";
		targetInput.text = species;
		targetFormInput.text = form;

		List<string> options = evoTypeDropdown.options.ConvertAll(option => option.text);
		evoTypeDropdown.value = options.IndexOf(e.evoType);

		levelInput.text = (e.level == 0) ? "" : e.level.ToString();

		if (e.item != null)
			itemIDInput.text = e.item.itemID;
		else
			itemIDInput.text = "";

		Pokemon evoTarget = PokemonManager.instance.FindPokemon(species);
		if (evoTarget != null)
		{
			List<Form> forms = new List<Form>(evoTarget.forms);
			formSearch.forms = forms;
		}

		UpdateWindow(e.evoType);
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

		PokemonManager.instance.SelectForm(PokemonManager.instance.selectedForm);
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
		string choice = evoTypeDropdown.options[evoTypeDropdown.value].text;
		foreach (EvoTypeWindow window in windows)
		{
			if (window.type == choice)
			{
				SetSelection(window);
			}
		}
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
}

[Serializable]
public struct EvoTypeWindow
{
	public string type;
	public GameObject w1;
	public GameObject w2;
}