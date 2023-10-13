using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsEditor : MonoBehaviour
{
    [SerializeField] TagsEditor tagsEditor;
    [SerializeField] BattleStatsEditor battleStatsEditor;
    [SerializeField] EVYieldsEditor evYieldEditor;
    [SerializeField] AggressionEditor aggressionEditor;
    [SerializeField] SpawningStatsEditor spawnStatsEditor;
    [SerializeField] DimensionsEditor dimensionsEditor;
    [SerializeField] EggGroupsEditor eggGroupsEditor;
    [SerializeField] GigantamaxEditor gigantamaxEditor;

    Pokemon selectedPokemon;
    Form selectedForm;

	private void OnEnable()
	{
        selectedPokemon = PokemonManager.instance.selectedPokemon;
        selectedForm = PokemonManager.instance.selectedForm;

		PokemonManager.OnPokemonSwitched += PokemonSwitched;
		PokemonManager.OnPokemonFormSwitched += FormSwitched;

		PokemonSwitched(selectedPokemon);
		FormSwitched(selectedForm);
	}

	private void OnDisable()
	{
		PokemonManager.OnPokemonSwitched -= PokemonSwitched;
		PokemonManager.OnPokemonFormSwitched -= FormSwitched;
	}

	private void FormSwitched(Form m)
	{
		if (m == null)
			return;

		selectedForm = m;
		tagsEditor.SetForm(m);
		battleStatsEditor.SetForm(m);
		evYieldEditor.SetForm(m);
		aggressionEditor.SetForm(m);
		eggGroupsEditor.SetForm(m);
		dimensionsEditor.SetForm(m);
		gigantamaxEditor.SetForm(m);
		spawnStatsEditor.SetForm(m);
	}

	private void PokemonSwitched(Pokemon p)
	{
		selectedPokemon = p;
		gigantamaxEditor.SetPokemon(p);
	}
}
