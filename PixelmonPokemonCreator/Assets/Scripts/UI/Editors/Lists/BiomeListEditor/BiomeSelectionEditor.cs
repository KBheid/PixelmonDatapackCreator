using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeSelectionEditor : MonoBehaviour
{
	public TMPro.TMP_InputField input;
	public Transform biomeListHolder;
	public BiomeListItem biomeListItemPrefab;

	public List<BiomeListItem> biomes = new List<BiomeListItem>();
	private BiomeListItem selectedBiome;

	public void SetBiomes(List<string> biomes)
	{
		Clear();
		foreach (string b in biomes)
		{
			AddBiome(b);
		}
	}

	public void SelectBiome(BiomeListItem selected)
	{
		selectedBiome = selected;
	}

	void Clear()
	{
		foreach (Transform t in biomeListHolder)
		{
			Destroy(t.gameObject);
		}

		foreach (BiomeListItem item in biomes)
		{
			Destroy(item.gameObject);
		}
		biomes.Clear();
	}


	// Called from button
    public void AddBiome()
	{
		string biomeText = input.text;
		selectedBiome = AddBiome(biomeText);
		input.text = "";
	}

	BiomeListItem AddBiome(string biomeName)
	{
		BiomeListItem added = Instantiate(biomeListItemPrefab, biomeListHolder);
		biomes.Add(added);
		added.textItem.text = biomeName;
		added.editor = this;
		return added;
	}

    public void RemoveBiome()
	{
		if (selectedBiome == null)
			return;

		int index = biomes.IndexOf(selectedBiome);

		biomes.Remove(selectedBiome);
		Destroy(selectedBiome.gameObject);

		index = (index - 1 >= 0) ? index - 1 : 0;
		
		if (index+1 < biomes.Count)
			selectedBiome = biomes[index];
	}

}
