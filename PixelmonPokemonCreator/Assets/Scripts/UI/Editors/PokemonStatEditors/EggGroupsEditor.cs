using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EggGroupsEditor : MonoBehaviour
{
    [SerializeField] Toggle amorphousToggle;
    [SerializeField] Toggle bugToggle;
    [SerializeField] Toggle dittoToggle;
    [SerializeField] Toggle dragonToggle;
    [SerializeField] Toggle fairyToggle;
    [SerializeField] Toggle feildToggle;
    [SerializeField] Toggle flyingToggle;
    [SerializeField] Toggle grassToggle;
    [SerializeField] Toggle humanLikeToggle;
    [SerializeField] Toggle mineralToggle;
    [SerializeField] Toggle monsterToggle;
    [SerializeField] Toggle undiscoveredToggle;
    [SerializeField] Toggle water1Toggle;
    [SerializeField] Toggle water2Toggle;
    [SerializeField] Toggle water3Toggle;

    Form selectedForm;

    public void SetForm(Form f)
	{
        selectedForm = f;
        UpdateContents();
	}

    private void UpdateContents()
	{
        SetButtonsActiveState(selectedForm != null);
        if (selectedForm == null)
            return;

        if (selectedForm.eggGroups == null)
            return;

        List<string> groups = new List<string>(selectedForm.eggGroups);

        amorphousToggle.isOn    = groups.Contains("AMORPHOUS");
        bugToggle.isOn          = groups.Contains("BUG");
        dittoToggle.isOn        = groups.Contains("DITTO");
        dragonToggle.isOn       = groups.Contains("DRAGON");
        fairyToggle.isOn        = groups.Contains("FAIRY");
        grassToggle.isOn        = groups.Contains("GRASS");
        humanLikeToggle.isOn    = groups.Contains("HUMAN_LIKE");
        mineralToggle.isOn      = groups.Contains("MINERAL");
        monsterToggle.isOn      = groups.Contains("MONSTER");
        undiscoveredToggle.isOn = groups.Contains("UNDISCOVERED");
        water1Toggle.isOn       = groups.Contains("WATER_ONE");
        water2Toggle.isOn       = groups.Contains("WATER_TWO");
        water3Toggle.isOn       = groups.Contains("WATER_THREE");
    }

    public void SetButtonsActiveState(bool active)
	{
        amorphousToggle.interactable = active;
        bugToggle.interactable = active;
        dittoToggle.interactable = active;
        dragonToggle.interactable = active;
        fairyToggle.interactable = active;
        grassToggle.interactable = active;
        humanLikeToggle.interactable = active;
        mineralToggle.interactable = active;
        monsterToggle.interactable = active;
        undiscoveredToggle.interactable = active;
        water1Toggle.interactable = active;
        water2Toggle.interactable = active;
        water3Toggle.interactable = active;
    }

    public void UpdateFormEggs()
	{
        List<string> eggGroups = new List<string>();

        if (amorphousToggle.isOn)
            eggGroups.Add("AMORPHOUS");
        if (bugToggle.isOn)
            eggGroups.Add("BUG");
        if (dittoToggle.isOn)
            eggGroups.Add("DITTO");
        if (dragonToggle.isOn)
            eggGroups.Add("DRAGON");
        if (fairyToggle.isOn)
            eggGroups.Add("FAIRY");
        if (grassToggle.isOn)
            eggGroups.Add("GRASS");
        if (humanLikeToggle.isOn)
            eggGroups.Add("HUMAN_LIKE");
        if (mineralToggle.isOn)
            eggGroups.Add("MINERAL");
        if (monsterToggle.isOn)
            eggGroups.Add("MONSTER");
        if (undiscoveredToggle.isOn)
            eggGroups.Add("UNDISCOVERED");
        if (water1Toggle.isOn)
            eggGroups.Add("WATER_ONE");
        if (water2Toggle.isOn)
            eggGroups.Add("WATER_TWO");
        if (water3Toggle.isOn)
            eggGroups.Add("WATER_THREE");

        selectedForm.eggGroups = eggGroups.ToArray();
    }
}
