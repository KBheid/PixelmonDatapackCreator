using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TagsEditor : MonoBehaviour
{
	Form selectedForm;

	[SerializeField] Toggle legendaryToggle;
	[SerializeField] Toggle mythicalToggle;
	[SerializeField] Toggle ultraBeastToggle;
	[SerializeField] Toggle threeIVsToggle;
	[SerializeField] Toggle alolanToggle;
	[SerializeField] Toggle galarianToggle;
	[SerializeField] Toggle hisuianToggle;
	[SerializeField] Toggle tempToggle;
	[SerializeField] Toggle undexableToggle;
	[SerializeField] Toggle walksOnWaterToggle;


	void UpdateValues()
	{
		SetInputActiveState(selectedForm != null);
		if (selectedForm == null)
			return;

		if (selectedForm.tags == null)
			return;

		List<string> tags = new List<string>(selectedForm.tags);
		legendaryToggle.isOn		= tags.Contains("legendary");
		mythicalToggle.isOn			= tags.Contains("mythical");
		ultraBeastToggle.isOn		= tags.Contains("ultrabeast");
		threeIVsToggle.isOn			= tags.Contains("threeivs");
		alolanToggle.isOn			= tags.Contains("alolan");
		galarianToggle.isOn			= tags.Contains("galarian");
		hisuianToggle.isOn			= tags.Contains("hisuian");
		tempToggle.isOn				= tags.Contains("temp");
		undexableToggle.isOn		= tags.Contains("undexable");
		walksOnWaterToggle.isOn		= tags.Contains("walksonwater");
	}

	void SetInputActiveState(bool active)
	{
		legendaryToggle.interactable = active;
		mythicalToggle.interactable = active;
		ultraBeastToggle.interactable = active;
		threeIVsToggle.interactable = active;
		alolanToggle.interactable = active;
		galarianToggle.interactable = active;
		hisuianToggle.interactable = active;
		tempToggle.interactable = active;
		undexableToggle.interactable = active;
		walksOnWaterToggle.interactable = active;
	}

	public void UpdateFormTags()
	{
		if (selectedForm == null)
			return;

		List<string> tags = new List<string>();
		if (legendaryToggle.isOn) tags.Add("legendary");
		if (mythicalToggle.isOn) tags.Add("mythical");
		if (ultraBeastToggle.isOn) tags.Add("ultrabeast");
		if (threeIVsToggle.isOn) tags.Add("threeivs");
		if (alolanToggle.isOn) tags.Add("alolan");
		if (galarianToggle.isOn) tags.Add("galarian");
		if (hisuianToggle.isOn) tags.Add("hisuian");
		if (tempToggle.isOn) tags.Add("temp");
		if (undexableToggle.isOn) tags.Add("undexable");
		if (walksOnWaterToggle.isOn) tags.Add("walksonwater");

		selectedForm.tags = tags.ToArray();
	}

	public void SetForm(Form f)
	{
		selectedForm = f;
		UpdateValues();
	}

}
