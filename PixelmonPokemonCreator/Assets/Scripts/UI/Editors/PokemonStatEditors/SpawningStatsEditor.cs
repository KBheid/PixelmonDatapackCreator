using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawningStatsEditor : MonoBehaviour
{
    [SerializeField] TMPro.TMP_InputField baseExpInput;
    [SerializeField] TMPro.TMP_InputField friendshipInput;
    [SerializeField] TMPro.TMP_InputField spawnLevelInput;
    [SerializeField] TMPro.TMP_InputField levelRangeInput;

    [SerializeField] Toggle spawnLandToggle;
    [SerializeField] Toggle spawnWaterToggle;
    [SerializeField] Toggle spawnUndergroundToggle;
    [SerializeField] Toggle spawnAirToggle;
    [SerializeField] Toggle spawnAirPersistentToggle;

    Form selectedForm;
    bool updating = false;

    void UpdateContent()
	{
        updating = true;

        SetInputActiveState(selectedForm != null);
        if (selectedForm == null)
            return;

        if (selectedForm.spawn == null)
            return;

        baseExpInput.text = selectedForm.spawn.baseExp.ToString();
        friendshipInput.text = selectedForm.spawn.baseFriendship.ToString();
        spawnLevelInput.text = selectedForm.spawn.spawnLevel.ToString();
        levelRangeInput.text = selectedForm.spawn.spawnLevelRange.ToString();

        if (selectedForm.spawn.spawnLocations == null)
            return;

        List<string> locations          = new List<string>(selectedForm.spawn.spawnLocations);
        spawnLandToggle.isOn            = locations.Contains("LAND");
        spawnWaterToggle.isOn           = locations.Contains("WATER");
        spawnUndergroundToggle.isOn     = locations.Contains("UNDERGROUND");
        spawnAirToggle.isOn             = locations.Contains("AIR");
        spawnAirPersistentToggle.isOn   = locations.Contains("AIR_PERSISTENT");

        updating = false;
	}

    void SetInputActiveState(bool active)
	{
        baseExpInput.interactable = active;
        friendshipInput.interactable = active;
        spawnLevelInput.interactable = active;
        levelRangeInput.interactable = active;
        spawnLandToggle.interactable = active;
        spawnWaterToggle.interactable = active;
        spawnUndergroundToggle.interactable = active;
        spawnAirToggle.interactable = active;
        spawnAirPersistentToggle.interactable = active;
    }

    public void SetFormSpawnData()
	{
        if (selectedForm == null || updating)
            return;

        selectedForm.spawn.baseExp          = baseExpInput.text.ToIntegerOrNegativeOne();
        selectedForm.spawn.baseFriendship   = friendshipInput.text.ToIntegerOrNegativeOne();
        selectedForm.spawn.spawnLevel       = spawnLevelInput.text.ToIntegerOrNegativeOne();
        selectedForm.spawn.spawnLevelRange  = levelRangeInput.text.ToIntegerOrNegativeOne();

        List<string> locations = new List<string>();

        if (spawnLandToggle.isOn) locations.Add("LAND");
        if (spawnWaterToggle.isOn) locations.Add("WATER");
        if (spawnUndergroundToggle.isOn) locations.Add("UNDERGROUND");
        if (spawnAirToggle.isOn) locations.Add("AIR");
        if (spawnAirPersistentToggle.isOn) locations.Add("AIR_PERSISTENT");
    }

    public void SetForm(Form f)
	{
        selectedForm = f;
        UpdateContent();
	}
}
