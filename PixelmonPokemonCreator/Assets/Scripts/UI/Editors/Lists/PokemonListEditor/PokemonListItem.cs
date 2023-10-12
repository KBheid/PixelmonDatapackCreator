using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonListItem : MonoBehaviour
{
	public PokemonSelectionEditor editor;
	public TMPro.TMP_Text textItem;

	public void OnClick()
	{
		editor.SelectPokemon(this);
	}
}
