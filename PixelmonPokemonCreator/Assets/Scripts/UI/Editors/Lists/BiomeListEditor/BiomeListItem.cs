using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeListItem : MonoBehaviour
{
	public BiomeSelectionEditor editor;
	public TMPro.TMP_Text textItem;

	public void OnClick()
	{
		editor.SelectBiome(this);
	}
}
