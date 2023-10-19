using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MountedFlyingEditor : MonoBehaviour
{

    Form selectedForm;

    /*
	public string type;
	public int upper_angle_limit;
	public int lower_angle_limit;
	public float max_fly_speed;
	public float deceleration_rate;
	public float hover_deceleration_rate;
	public float acceleration_rate;
	public float strafe_acceleration_rate;
	public int strafe_roll_conversion;
	public float turn_rate;
	public float pitch_rate;
	public bool stays_horizontal_flying;
	public float gravity_drop_per_tick;
	public bool continuous_forward_motion;
	public int continuous_forward_motion_ticks;
	public int flying_stamina_charges;
	public int hover_ticks;
     */

    [SerializeField] TMPro.TMP_Dropdown   typeDropdown;
    [SerializeField] TMPro.TMP_InputField upperAngleInput;
    [SerializeField] TMPro.TMP_InputField lowerAngleInput;
    [SerializeField] TMPro.TMP_InputField maxFlySpeedInput;
    [SerializeField] TMPro.TMP_InputField decelerationRateInput;
    [SerializeField] TMPro.TMP_InputField hoverDecelerationInput;
    [SerializeField] TMPro.TMP_InputField accelerationInput;
    [SerializeField] TMPro.TMP_InputField strafeAccelerationInput;
    [SerializeField] TMPro.TMP_InputField strafeRollConversionInput;
    [SerializeField] TMPro.TMP_InputField turnRateInput;
    [SerializeField] TMPro.TMP_InputField pitchRateInput;
    [SerializeField] Toggle               staysHorizontalToggle;
    [SerializeField] TMPro.TMP_InputField gravityDropPerTickInput;
    [SerializeField] Toggle               continuousForwardMotionToggle;
    [SerializeField] TMPro.TMP_InputField continuousForwardMotionTicksInput;
    [SerializeField] TMPro.TMP_InputField flyingStaminaChargesInput;
    [SerializeField] TMPro.TMP_InputField hoverTicksInput;



    bool updating = false;

    public void UpdateForm()
    {
        if (updating) return;

        Mountedflyingparameters mfp = selectedForm.movement.mountedFlyingParameters;

        mfp.type = typeDropdown.options[typeDropdown.value].text;
        mfp.upper_angle_limit = upperAngleInput.text.ToIntegerOrNegativeOne();
        mfp.lower_angle_limit = lowerAngleInput.text.ToIntegerOrNegativeOne();
        mfp.max_fly_speed = maxFlySpeedInput.text.ToFloatOrNegativeOne();
        mfp.deceleration_rate = decelerationRateInput.text.ToFloatOrNegativeOne();
        mfp.hover_deceleration_rate = hoverDecelerationInput.text.ToFloatOrNegativeOne();
        mfp.acceleration_rate = accelerationInput.text.ToFloatOrNegativeOne();
        mfp.strafe_acceleration_rate = strafeAccelerationInput.text.ToFloatOrNegativeOne();
        mfp.strafe_roll_conversion = strafeRollConversionInput.text.ToIntegerOrNegativeOne();
        mfp.turn_rate = turnRateInput.text.ToFloatOrNegativeOne();
        mfp.pitch_rate = turnRateInput.text.ToFloatOrNegativeOne();
        mfp.stays_horizontal_flying = staysHorizontalToggle.isOn;
        mfp.gravity_drop_per_tick = gravityDropPerTickInput.text.ToFloatOrNegativeOne();
        mfp.continuous_forward_motion = continuousForwardMotionToggle.isOn;
        mfp.continuous_forward_motion_ticks = continuousForwardMotionTicksInput.text.ToIntegerOrNegativeOne();
        mfp.flying_stamina_charges = flyingStaminaChargesInput.text.ToIntegerOrNegativeOne();
        mfp.hover_ticks = hoverTicksInput.text.ToIntegerOrNegativeOne();

    }

    void UpdateContent()
    {
        if (selectedForm == null) return;
        updating = true;

        // TODO: Is this what we want?
        if (selectedForm.movement == null)
            selectedForm.movement = new Movement();

        Mountedflyingparameters mountFlyParams = selectedForm.movement.mountedFlyingParameters;
        if (mountFlyParams == null)
        {
            mountFlyParams = new Mountedflyingparameters();
            selectedForm.movement.mountedFlyingParameters = mountFlyParams;
        }

        
        typeDropdown.SetDropdownToStringValue(mountFlyParams.type);
        upperAngleInput.text            = mountFlyParams.upper_angle_limit.ToString();
        lowerAngleInput.text            = mountFlyParams.lower_angle_limit.ToString();
        maxFlySpeedInput.text           = mountFlyParams.max_fly_speed.ToString();
        decelerationRateInput.text      = mountFlyParams.deceleration_rate.ToString();
        hoverDecelerationInput.text     = mountFlyParams.hover_deceleration_rate.ToString();
        accelerationInput.text          = mountFlyParams.acceleration_rate.ToString();
        strafeAccelerationInput.text    = mountFlyParams.strafe_acceleration_rate.ToString();
        strafeRollConversionInput.text  = mountFlyParams.strafe_roll_conversion.ToString();
        turnRateInput.text              = mountFlyParams.turn_rate.ToString();
        pitchRateInput.text             = mountFlyParams.pitch_rate.ToString();
        gravityDropPerTickInput.text    = mountFlyParams.gravity_drop_per_tick.ToString();

        staysHorizontalToggle.isOn          = mountFlyParams.stays_horizontal_flying;
        continuousForwardMotionToggle.isOn  = mountFlyParams.continuous_forward_motion;
        
        continuousForwardMotionTicksInput.text  = mountFlyParams.continuous_forward_motion_ticks.ToString();
        flyingStaminaChargesInput.text          = mountFlyParams.flying_stamina_charges.ToString();
        hoverTicksInput.text                    = mountFlyParams.hover_ticks.ToString();

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
