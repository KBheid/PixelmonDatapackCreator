using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvolutionConditionEditor : MonoBehaviour
{
	[SerializeField] EvolutionConditionListPanel editor;
    public TMPro.TMP_Dropdown evoConditionTypeDropdown;
    public List<EvoTypeWindow> windows;

	[SerializeField] TMPro.TMP_Dropdown timeDropdown;		// weather
	[SerializeField] BiomeSelectionEditor biomeEditor;		// biome
	[SerializeField] TMPro.TMP_Dropdown weatherDropdown;	// weather
	[SerializeField] TMPro.TMP_InputField chanceInput;		// chance
	[SerializeField] TMPro.TMP_InputField heldItemInput;	// heldItem
	[SerializeField] TMPro.TMP_Dropdown evolutionRockDropdown;			// evo rock
	[SerializeField] TMPro.TMP_InputField maxRangeSquaredRockInput;		// evo rock
	[SerializeField] TMPro.TMP_Dropdown evolutionScrollDropdown;		// evo scroll
	[SerializeField] TMPro.TMP_InputField maxRangeSquaredScrollInput;	// evo scroll
	[SerializeField] Toggle shinyToggle;					// shiny
	[SerializeField] TMPro.TMP_InputField friendshipInput;	// friendship
	[SerializeField] TMPro.TMP_InputField moveNameInput;	// move
	[SerializeField] TMPro.TMP_Dropdown attackTypeDropdown;	// moveType
	[SerializeField] TMPro.TMP_InputField moveUsesNameInput;// moveUses
	[SerializeField] TMPro.TMP_InputField moveUsesInput;	// moveUses
	//[SerializeField] string[] withPokemon;
	//[SerializeField] string[] withTypes;
	//[SerializeField] string[] withForms;
	//[SerializeField] string[] withPalettes;
	[SerializeField] TMPro.TMP_InputField minAltitudeInput;		// highAltitude
	[SerializeField] TMPro.TMP_InputField blocksToWalkInput;	// blocksWalkedOutsideBall
	//[SerializeField] string[] natures;
	[SerializeField] Toggle genderMaleToggle;					// gender
	[SerializeField] Toggle genderFemaleToggle;					// gender
	[SerializeField] TMPro.TMP_Dropdown stat1;					// statRatio
	[SerializeField] TMPro.TMP_Dropdown stat2;					// statRatio
	[SerializeField] TMPro.TMP_InputField ratio;				// statRatio
	[SerializeField] TMPro.TMP_Dropdown statusTypeInput;		// status
	[SerializeField] TMPro.TMP_InputField criticalInput;		// critical
	[SerializeField] TMPro.TMP_InputField recoil;				// recoil
	[SerializeField] TMPro.TMP_InputField nuggets;				// nuggets
	[SerializeField] TMPro.TMP_InputField health;				// healthAbsence


	[SerializeField]
	Condition condition;

    private EvoTypeWindow current;

	public void ConditionTypeSelected()
	{
		string choice = evoConditionTypeDropdown.options[evoConditionTypeDropdown.value].text;
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

	public void MarkComplete()
	{
		Condition newCondition = new Condition();
		newCondition.evoConditionType = evoConditionTypeDropdown.options[evoConditionTypeDropdown.value].text;

		switch(newCondition.evoConditionType)
		{
			case "time":
				newCondition.time = timeDropdown.options[timeDropdown.value].text;
				break;
			case "biome":
				List<string> biomes = biomeEditor.biomes.ConvertAll(biome => biome.textItem.text);
				newCondition.biomes = biomes.ToArray();
				break;
			case "weather":
				newCondition.weather = weatherDropdown.options[weatherDropdown.value].text;
				break;
			case "chance":
				newCondition.chance = chanceInput.text.ToFloatOrNegativeOne();
				break;
			case "heldItem":
				newCondition.item = new Item { itemID = heldItemInput.text };
				break;
			case "evolutionRock":
				newCondition.evolutionRock = evolutionRockDropdown.options[evolutionRockDropdown.value].text;
				newCondition.maxRangeSquared = maxRangeSquaredRockInput.text.ToIntegerOrNegativeOne();
				break;
			case "evolutionScroll":
				newCondition.evolutionScroll = evolutionScrollDropdown.options[evolutionScrollDropdown.value].text;
				newCondition.maxRangeSquared = maxRangeSquaredScrollInput.text.ToIntegerOrNegativeOne();
				break;
			case "shiny":
				newCondition.shiny = shinyToggle.isOn;
				break;
			case "friendship":
				newCondition.friendship = friendshipInput.text.ToIntegerOrNegativeOne();
				break;
			case "move":
				newCondition.attackName = moveUsesNameInput.text;
				break;
			case "moveType":
				newCondition.type = attackTypeDropdown.options[attackTypeDropdown.value].text;
				break;
			case "moveUses":
				newCondition.move = moveUsesNameInput.text;
				newCondition.uses = moveUsesInput.text.ToIntegerOrNegativeOne();
				break;
			case "party":
				// TODO:
				break;
			case "highAltitude":
				newCondition.minAltitude = minAltitudeInput.text.ToFloatOrNegativeOne();
				break;
			case "blocksWalkedOutsideBall":
				newCondition.blocksToWalk = blocksToWalkInput.text.ToIntegerOrNegativeOne();
				break;
			case "nature":
				// TODO:
				break;
			case "gender":
				List<string> genders = new List<string>();
				if (genderMaleToggle.isOn) genders.Add("MALE");
				if (genderFemaleToggle.isOn) genders.Add("FEMALE");

				newCondition.genders = genders.ToArray();
				break;
			case "statRatio":
				newCondition.stat1 = stat1.options[stat1.value].text;
				newCondition.stat2 = stat2.options[stat2.value].text;
				newCondition.ratio = ratio.text.ToFloatOrNegativeOne();
				break;
			case "status":
				newCondition.type = statusTypeInput.options[statusTypeInput.value].text;
				break;
			case "critical":
				newCondition.critical = criticalInput.text.ToIntegerOrNegativeOne();
				break;
			case "recoil":
				newCondition.recoil = recoil.text;
				break;
			case "nuggets":
				newCondition.nuggets = nuggets.text.ToIntegerOrNegativeOne();
				break;
			case "healthAbsence":
				newCondition.health = health.text;
				break;
		}

		editor.OverwriteCondition(condition, newCondition);
	}

	public void SetCondition(Condition c)
	{
		condition = c;

		evoConditionTypeDropdown.SetDropdownToStringValue(c.evoConditionType);
		biomeEditor.SetBiomes(new List<string>());

		switch (c.evoConditionType)
		{
			case "time":
				timeDropdown.SetDropdownToStringValue(c.time);
				break;
			case "biome":
				biomeEditor.SetBiomes(new List<string>(c.biomes));
				break;
			case "weather":
				weatherDropdown.SetDropdownToStringValue(c.weather);
				break;
			case "chance":
				chanceInput.text = c.chance.ToString();
				break;
			case "heldItem":
				heldItemInput.text = c.item.itemID;
				break;
			case "evolutionRock":
				evolutionRockDropdown.SetDropdownToStringValue(c.evolutionRock);
				maxRangeSquaredRockInput.text = c.maxRangeSquared.ToString();
				break;
			case "evolutionScroll":
				evolutionScrollDropdown.SetDropdownToStringValue(c.evolutionScroll);
				maxRangeSquaredScrollInput.text = c.maxRangeSquared.ToString();
				break;
			case "shiny":
				shinyToggle.isOn = c.shiny;
				break;
			case "friendship":
				friendshipInput.text = c.friendship.ToString();
				break;
			case "move":
				moveNameInput.text = c.attackName;
				break;
			case "moveType":
				attackTypeDropdown.SetDropdownToStringValue(c.type);
				break;
			case "moveUses":
				moveUsesNameInput.text = c.move;
				moveUsesInput.text = c.uses.ToString();
				break;
			case "party":
				// TODO:
				break;
			case "highAltitude":
				minAltitudeInput.text = c.minAltitude.ToString();
				break;
			case "blocksWalkedOutsideBall":
				blocksToWalkInput.text = c.blocksToWalk.ToString();
				break;
			case "nature":
				// TODO:
				break;
			case "gender":
				List<string> genders = new List<string>(c.genders);
				genderMaleToggle.isOn = genders.Contains("MALE");
				genderFemaleToggle.isOn = genders.Contains("FEMALE");
				break;
			case "statRatio":
				stat1.SetDropdownToStringValue(c.stat1);
				stat2.SetDropdownToStringValue(c.stat2);
				ratio.text = c.ratio.ToString();
				break;
			case "status":
				statusTypeInput.SetDropdownToStringValue(c.type);
				break;
			case "critical":
				criticalInput.text = c.critical.ToString();
				break;
			case "recoil":
				recoil.text = c.recoil;
				break;
			case "nuggets":
				nuggets.text = c.nuggets.ToString();
				break;
			case "healthAbsence":
				health.text = c.health;
				break;
		}
	}
}
