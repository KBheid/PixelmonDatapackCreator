using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CommonPropertyEditor : MonoBehaviour
{
	[SerializeField] TMPro.TMP_InputField pokemonNameInput;
	[SerializeField] TMPro.TMP_InputField dexNumberInput;
	[SerializeField] TMPro.TMP_InputField genNumberInput;
	[SerializeField] TMPro.TMP_Dropdown formDropdown;

	[SerializeField] TMPro.TMP_InputField formNameInput;
	[SerializeField] TMPro.TMP_Dropdown expGroupDropdown;
	[SerializeField] TMPro.TMP_Dropdown type1Dropdown;
	[SerializeField] TMPro.TMP_Dropdown type2Dropdown;
	[SerializeField] TMPro.TMP_InputField eggCyclesInput;
	[SerializeField] TMPro.TMP_InputField weightInput;
	[SerializeField] TMPro.TMP_InputField catchRateInput;
	[SerializeField] TMPro.TMP_InputField malePercentInput;

	private Form selectedForm;
	private Pokemon selectedPokemon;

	bool updating = false;

	public void UpdateName()
	{
		string pokeName = pokemonNameInput.text;
		selectedPokemon.name = pokeName;
		PokemonManager.instance.SelectPokemon(selectedPokemon);
	}
	public void UpdateDexNumber()
	{
		selectedPokemon.dex = dexNumberInput.text.ToIntegerOrNegativeOne();
	}
	public void UpdateGenNumber()
	{
		selectedPokemon.generation = genNumberInput.text.ToIntegerOrNegativeOne();
	}
	public void UpdateFormName()
	{
		string formName = formNameInput.text;
		selectedForm.name = formName;
		PokemonManager.instance.SelectForm(selectedForm);
	}
	public void UpdateExpGroup()
	{
		selectedForm.experienceGroup = expGroupDropdown.options[expGroupDropdown.value].text;
	}
	public void UpdateTypes()
	{
		if (updating)
			return;

		string type1 = type1Dropdown.options[type1Dropdown.value].text;
		string type2 = type2Dropdown.options[type2Dropdown.value].text;

		if (type1 == "[ NONE ]")
		{
			type2Dropdown.value = 0;
			selectedForm.types = new string[] { };
			return;
		}

		if (type2 == "[ NONE ]")
		{
			selectedForm.types = new string[] { type1 };
			return;
		}

		selectedForm.types = new string[] { type1, type2 };
	}
	public void UpdateEggCycles()
	{
		selectedForm.eggCycles = eggCyclesInput.text.ToIntegerOrNegativeOne();
	}
	public void UpdateWeight()
	{
		selectedForm.weight = weightInput.text.ToIntegerOrNegativeOne();
	}
	public void UpdateCatchRate()
	{
		selectedForm.catchRate = catchRateInput.text.ToIntegerOrNegativeOne();
	}
	public void UpdateMalePercent()
	{
		selectedForm.malePercentage = malePercentInput.text.ToFloatOrNegativeOne();
	}


	// TODO: Form selection
	public void UpdateFormSelection()
	{
		if (updating)
			return;

		string formSelection = formDropdown.options[formDropdown.value].text;
		if (formSelection == "[ Add Form ]")
		{
			// add new form stuff
			List<Form> forms = new List<Form>(selectedPokemon.forms);
			Form added = new Form()
			{
					name = "unnamed",
					experienceGroup = "SLOW",
					dimensions = new Dimensions(),
					moves = new Moves(),
					abilities = new Abilities(),
					movement = new Movement(),
					aggression = new Aggression(),
					battleStats = new Battlestats(),
					tags = new string[] { },
					spawn = new Spawn(),
					possibleGenders = new string[] { "MALE", "FEMALE" },
					genderProperties = new Genderproperty[] { selectedForm.genderProperties[0].Copy() },
					eggGroups = new string[] { },
					types = new string[] { "GRASS" },
					preEvolutions = new string[] { },
					defaultBaseForm = "",
					megaItems = new string[] { },
					megas = new string[] { },
					gigantamax = new Gigantamax(),
					eggCycles = 21,
					weight = 30,
					catchRate = 20,
					malePercentage = -1f,
					evolutions = null,
					evYields = new Evyields()
			};
			forms.Add(added);

			selectedPokemon.forms = forms.ToArray();

			formSelection = added.name;
		}

		Form selection = selectedPokemon.forms.Where(form => form.name == formSelection).First();

		PokemonManager.instance.SelectPokemon(selectedPokemon);
		PokemonManager.instance.SelectForm(selection);
	}


	private void FormSwitched(Form m)
	{
		if (m != selectedForm)
		{
			selectedForm = m;
			UpdateUIValues();
		}
	}

	private void PokemonSwitched(Pokemon p)
	{
		// Update forms, even if it's same pokemon

		if (p != selectedPokemon)
		{
			selectedPokemon = p;
			
			if (selectedForm != null)
				UpdateUIValues();
		}
	}

	private void UpdateUIValues()
	{
		updating = true;

		pokemonNameInput.text = selectedPokemon.name;
		dexNumberInput.text = selectedPokemon.dex.ToString();
		genNumberInput.text = selectedPokemon.generation.ToString();
		formDropdown.SetDropdownToStringValue(selectedForm.name);
		formNameInput.text = selectedForm.name;
		expGroupDropdown.SetDropdownToStringValue(selectedForm.experienceGroup);

		if (selectedForm.types != null && selectedForm.types.Length > 0)
			type1Dropdown.SetDropdownToStringValue(selectedForm.types[0]);
		else
			type1Dropdown.value = 0;
		if (selectedForm.types != null && selectedForm.types.Length > 1)
			type2Dropdown.SetDropdownToStringValue(selectedForm.types[1]);
		else
			type2Dropdown.value = 0;

		eggCyclesInput.text = selectedForm.eggCycles.ToString();
		weightInput.text = selectedForm.weight.ToString();
		catchRateInput.text = selectedForm.catchRate.ToString();
		malePercentInput.text = selectedForm.malePercentage.ToString();

		// Forms
		List<Form> forms = new List<Form>(selectedPokemon.forms);
		List<TMPro.TMP_Dropdown.OptionData> formNames = forms.ConvertAll(form => new TMPro.TMP_Dropdown.OptionData(form.name));
		formDropdown.options = formNames;
		formDropdown.options.Add(new TMPro.TMP_Dropdown.OptionData("[ Add Form ]"));

		updating = false;
	}


	private void OnEnable()
	{
		PokemonManager.OnPokemonSwitched += PokemonSwitched;
		PokemonManager.OnPokemonFormSwitched += FormSwitched;

		if (PokemonManager.instance.selectedPokemon != null && PokemonManager.instance.selectedForm != null)
		{
			selectedPokemon = PokemonManager.instance.selectedPokemon;
			selectedForm = PokemonManager.instance.selectedForm;
			UpdateUIValues();
		}
	}

	private void OnDisable()
	{
		PokemonManager.OnPokemonSwitched -= PokemonSwitched;
		PokemonManager.OnPokemonFormSwitched -= FormSwitched;
	}
}
