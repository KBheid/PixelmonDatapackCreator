using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwimmingEditor : MonoBehaviour
{
    Form selectedForm;

    [SerializeField] TMPro.TMP_InputField depthStartInput;
    [SerializeField] TMPro.TMP_InputField depthEndInput;
    [SerializeField] TMPro.TMP_InputField swimSpeedInput;
    [SerializeField] TMPro.TMP_InputField decayRateInput;
    [SerializeField] TMPro.TMP_InputField refreshRateInput;
    [SerializeField] TMPro.TMP_InputField minStopTimeInput;
    [SerializeField] TMPro.TMP_InputField maxStopTimeInput;
    [SerializeField] TMPro.TMP_InputField minStopCooldownInput;
    [SerializeField] TMPro.TMP_InputField maxStopCooldownInput;
    [SerializeField] Toggle               canRotateWhileStoppedToggle;
    [SerializeField] Toggle               shouldSinkToggle;
    [SerializeField] TMPro.TMP_InputField chanceToStopOnblocKInput;
    // TODO: Some list for blocksToStopOn

    bool updating = false;

    public void UpdateForm()
    {
        if (selectedForm == null || updating)
            return;

        // TODO: Is this what we want to do?
        if (selectedForm.movement == null)
            selectedForm.movement = new Movement();

        if (selectedForm.movement.swimmingParameters == null)
            selectedForm.movement.swimmingParameters = new Swimmingparameters();

        Swimmingparameters swimParams = selectedForm.movement.swimmingParameters;

        swimParams.depthRangeStart      = depthStartInput.text.ToIntegerOrDefault(-5000);
        swimParams.depthRangeEnd        = depthEndInput.text.ToIntegerOrDefault(-5000);
        swimParams.swimSpeed            = swimSpeedInput.text.ToIntegerOrNegativeOne();
        swimParams.decayRate            = decayRateInput.text.ToFloatOrNegativeOne();
        swimParams.refreshRate          = refreshRateInput.text.ToIntegerOrNegativeOne();
        swimParams.minStopTime          = minStopTimeInput.text.ToIntegerOrNegativeOne();
        swimParams.maxStopTime          = maxStopTimeInput.text.ToIntegerOrNegativeOne();
        swimParams.minStopCooldownTime  = minStopCooldownInput.text.ToIntegerOrNegativeOne();
        swimParams.maxStopCooldownTime  = maxStopCooldownInput.text.ToIntegerOrNegativeOne();
        
        swimParams.canRotateWhileStopped    = canRotateWhileStoppedToggle.isOn;
        swimParams.shouldSink               = shouldSinkToggle.isOn;

        swimParams.chanceToStopOnBlock  = chanceToStopOnblocKInput.text.ToFloatOrNegativeOne();

        // TODO blocks to stop on
    }

    void UpdateContent()
    {
        if (selectedForm == null || selectedForm.movement == null || selectedForm.movement.swimmingParameters == null)
            return;

        updating = true;

        depthStartInput.text        = selectedForm.movement.swimmingParameters.depthRangeStart.ToString();
        depthEndInput.text          = selectedForm.movement.swimmingParameters.depthRangeEnd.ToString();
        swimSpeedInput.text         = selectedForm.movement.swimmingParameters.swimSpeed.ToString();
        decayRateInput.text         = selectedForm.movement.swimmingParameters.decayRate.ToString();
        refreshRateInput.text       = selectedForm.movement.swimmingParameters.refreshRate.ToString();
        minStopTimeInput.text       = selectedForm.movement.swimmingParameters.minStopTime.ToString();
        maxStopTimeInput.text       = selectedForm.movement.swimmingParameters.maxStopTime.ToString();
        minStopCooldownInput.text   = selectedForm.movement.swimmingParameters.minStopCooldownTime.ToString();
        maxStopCooldownInput.text   = selectedForm.movement.swimmingParameters.maxStopCooldownTime.ToString();
        
        canRotateWhileStoppedToggle.isOn    = selectedForm.movement.swimmingParameters.canRotateWhileStopped;
        shouldSinkToggle.isOn               = selectedForm.movement.swimmingParameters.shouldSink;

        chanceToStopOnblocKInput.text = selectedForm.movement.swimmingParameters.chanceToStopOnBlock.ToString();

        updating = false;
    }

    void SetForm(Form f)
    {
        selectedForm = f;

        UpdateContent();
    }

    private void OnEnable()
    {
        PokemonManager.OnPokemonFormSwitched += SetForm;
        SetForm(PokemonManager.instance.selectedForm);
    }

    private void OnDisable()
    {
        PokemonManager.OnPokemonFormSwitched -= SetForm;
    }

}
