using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class EvolutionEditor : MonoBehaviour
{
	public delegate void EvolutionChanged(Evolution e);
	public static EvolutionChanged OnEvolutionChanged;

	public Evolution selectedEvolution;

	private Form selectedForm;

	private void OnEnable()
	{
		PokemonManager.OnPokemonSwitched += PokemonSwitched;
		PokemonManager.OnPokemonFormSwitched += PokemonFormSwitched;

		selectedForm = PokemonManager.instance.selectedForm;
	}

	private void PokemonSwitched(Pokemon p)
	{
		PokemonFormSwitched(p.forms[0]);
	}

	private void PokemonFormSwitched(Form m)
	{
		selectedForm = m;
		SelectEvolution(null);
	}

	public void SelectEvolution(Evolution e)
	{
		selectedEvolution = e;
		OnEvolutionChanged?.Invoke(e);
	}

	public void AddEvolution()
	{
		if (selectedForm.evolutions == null)
			selectedForm.evolutions = new Evolution[] { };

		List<Evolution> evos = new List<Evolution>(selectedForm.evolutions);
		
		Evolution added = new Evolution();
		added.to = "";
		added.evoType = "leveling";
		added.level = 1;
		added.moves = null;
		added.with = null;
		added.conditions = new Condition[0];

		evos.Add(added);

		selectedForm.evolutions = evos.ToArray();

		SelectEvolution(added);
		PokemonManager.instance.SelectForm(selectedForm);
	}

	public void RemoveEvolution()
	{
		if (selectedEvolution == null)
			return;

		List<Evolution> evos = new List<Evolution>(selectedForm.evolutions);
		evos.Remove(selectedEvolution);

		selectedForm.evolutions = evos.ToArray();

		SelectEvolution(null);

		PokemonManager.instance.SelectForm(selectedForm);
	}


}
