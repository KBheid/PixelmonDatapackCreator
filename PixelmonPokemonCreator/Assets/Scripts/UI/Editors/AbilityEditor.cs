using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityEditor : MonoBehaviour
{
    Form selectedForm;

    [SerializeField] AbilityListEditor abilityListEditor;
    [SerializeField] AbilityListEditor hiddenAbiltiyListEditor;

    bool updating = false;

    public void UpdateContent()
    {
        if (updating || selectedForm == null)
            return;

        if (selectedForm.abilities == null && (abilityListEditor.currentAbilities.Count > 0 || hiddenAbiltiyListEditor.currentAbilities.Count > 0))
            selectedForm.abilities = new Abilities();

        selectedForm.abilities.abilities        = abilityListEditor.currentAbilities.ToArray();
        selectedForm.abilities.hiddenAbilities  = hiddenAbiltiyListEditor.currentAbilities.ToArray();
    }

    void SetForm(Form f)
    {
        if (f == null)
            return;

        updating = true;
        selectedForm = f;
        abilityListEditor.SetAbilities(f.abilities.abilities);
        hiddenAbiltiyListEditor.SetAbilities(f.abilities.hiddenAbilities);
        updating = false;
    }

    private void OnEnable()
    {
        selectedForm = PokemonManager.instance.selectedForm;
        PokemonManager.OnPokemonFormSwitched += SetForm;

        UpdateContent();
    }

    private void OnDisable()
    {
        PokemonManager.OnPokemonFormSwitched -= SetForm;
    }
}
