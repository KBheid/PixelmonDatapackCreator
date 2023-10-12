using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonAndFormSelectionTest : MonoBehaviour
{
	private void Start()
	{
		StartCoroutine(nameof(DelayFor5Seconds));
	}


	IEnumerator DelayFor5Seconds()
	{
		yield return new WaitForSeconds(5f);


		bool ranAny = false;
		foreach (Pokemon p in PokemonManager.instance.pokemon)
		{
			ranAny = true;
			Debug.Log("Selecting Pokemon: \"" + p.name + "\"");
			PokemonManager.instance.SelectPokemon(p);

			foreach (Form f in p.forms)
			{
				Debug.Log("Selecting Form: \"" + f.name + "\"");
				PokemonManager.instance.SelectForm(f);
				yield return new WaitForEndOfFrame();
			}

		}

		Debug.Log("Test Complete!");
		if (!ranAny)
			Debug.Log("\t Did not run any test cases.");

	}
}
