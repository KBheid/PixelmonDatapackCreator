using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EvolutionPreview : MonoBehaviour
{
    public EvolutionEditor editor;

    public Pokemon viewingPokemon;
    public Form viewingForm;

    public EvolutionPreviewItem evoPreviewPrefab;

    [SerializeField]
    float radius = 4f;

    [Header("Line")]
    [SerializeField] Sprite lineImage;
    [SerializeField] float lineWidth;

    private EvolutionPreviewItem root;
    private List<EvolutionPreviewItem> leaves;

	private void OnEnable()
    {
        leaves = new List<EvolutionPreviewItem>();

        PokemonManager.OnPokemonFormSwitched += OnPokemonFormChange;
        PokemonManager.OnPokemonSwitched += OnPokemonChange;

        viewingPokemon = PokemonManager.instance.selectedPokemon;
        viewingForm = PokemonManager.instance.selectedForm;

        UpdateContent();
    }

	private void OnPokemonChange(Pokemon p) { 
        viewingPokemon = p;
        viewingForm = PokemonManager.instance.selectedForm;
        UpdateContent();  
    }

    private void OnPokemonFormChange(Form f) {
        viewingPokemon = PokemonManager.instance.selectedPokemon;
        viewingForm = f; 
        UpdateContent();  
    }


    public void SelectEvolution(Evolution evo)
	{
        editor.SelectEvolution(evo);
	}

	public void UpdateContent()
	{
        ClearItems();

        if (viewingForm == null || viewingPokemon == null)
            return;

        AddRootItem(viewingForm, viewingPokemon.name);

        if (viewingForm.evolutions == null)
            return;
        foreach (Evolution evo in viewingForm.evolutions)
		{
            string[] split = evo.to.Split(" form:");

            string species = split[0];
            string form = (split.Length > 1) ? split[1] : "";

            bool found = false;
            foreach (Pokemon p in PokemonManager.instance.pokemon)
			{
                if (p.name == species) {
                    var result = p.forms.Where(selForm => selForm.name == form);

                    Form toPokemonForm = (result.Count() == 0) ? p.forms[0] : result.First();
                    AddLeafItem(toPokemonForm, species, evo);
                    found = true;
                    break;
                }
			}
            if (!found)
            {
                print("Potential error: Could not find form \"" + form + "\" for species: " + species);
                AddEmptyItem(species, evo);
            }
		}

        BalanceLeaves();
	}

    void BalanceLeaves()
	{
        float angleOffset = 360f / (float)leaves.Count() * Mathf.Deg2Rad;

        int count = 0;
        foreach (EvolutionPreviewItem leaf in leaves)
		{
            float x, y;
            x = Mathf.Sin(count * angleOffset);
            y = Mathf.Cos(count * angleOffset);


            leaf.GetComponent<RectTransform>().anchoredPosition = radius * new Vector3(x, y, 0);

            count++;

            MakeLine(0, 0, x*radius, y*radius, Color.green);
		}
	}

    void AddRootItem(Form f, string species)
	{
        root = AddItem(f, species, null);
        root.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
	}

    void AddLeafItem(Form f, string species, Evolution evo)
	{
        EvolutionPreviewItem added = AddItem(f, species, evo);
        leaves.Add(added);
    }

    void AddEmptyItem(string species, Evolution evo)
    {
        EvolutionPreviewItem added = Instantiate(evoPreviewPrefab, transform);

        string dataPath = UIState.instance.dataPath + "assets\\";

        Texture2D loadTexture = new Texture2D(1, 1);
        loadTexture.LoadImage(File.ReadAllBytes(dataPath + "editor\\unknown.png"));

        added.evo = evo;

        added.image.texture = loadTexture;
        added.text.text = species + "()";

        added.editor = this;

        leaves.Add(added);
    }

    EvolutionPreviewItem AddItem(Form f, string species, Evolution evo)
	{
        EvolutionPreviewItem added = Instantiate(evoPreviewPrefab, transform);

        string dataPath = UIState.instance.dataPath + "assets\\";

        string spriteLocalPath;
        if (f.genderProperties == null || 
                f.genderProperties[0].palettes == null || 
                f.genderProperties[0].palettes[0].sprite == null)
            spriteLocalPath = "editor\\unknown.png";
        else
            spriteLocalPath = f.genderProperties[0].palettes[0].sprite.Split(":")[1];

        string spritePath = dataPath + spriteLocalPath;

        Texture2D loadTexture = new Texture2D(1, 1);
        if (File.Exists(spritePath))
            loadTexture.LoadImage(File.ReadAllBytes(spritePath));
        else
            loadTexture.LoadImage(File.ReadAllBytes(dataPath + "editor\\unknown.png"));

        if (evo != null)
            added.evo = evo;

        added.image.texture = loadTexture;
        added.text.text = species + "(" + f.name + ")";

        added.editor = this;

        return added;
	}

    void ClearItems()
	{
        if (root != null)
            Destroy(root.gameObject);
        foreach (EvolutionPreviewItem leaf in leaves)
        {
            Destroy(leaf.gameObject);
        }
        leaves.Clear();
    }

    GameObject MakeLine(float ax, float ay, float bx, float by, Color col)
    {
        GameObject NewObj = new GameObject();
        NewObj.name = "line from " + ax + " to " + bx;
        Image NewImage = NewObj.AddComponent<Image>();
        NewImage.sprite = lineImage;
        NewImage.color = col;
        RectTransform rect = NewObj.GetComponent<RectTransform>();
        rect.SetParent(root.transform);
        rect.localScale = Vector3.one;

        Vector3 a = new Vector3(ax, ay, 0);
        Vector3 b = new Vector3(bx, by, 0);


        rect.localPosition = (a + b) / 2;
        Vector3 dif = a - b;
        rect.sizeDelta = new Vector3(dif.magnitude, lineWidth);
        rect.rotation = Quaternion.Euler(new Vector3(0, 0, 180 * Mathf.Atan(dif.y / dif.x) / Mathf.PI));

        rect.anchorMin = Vector2.one * 0.5f;
        rect.anchorMax = Vector2.one * 0.5f;

        return NewObj;
    }

	private void OnDisable()
	{
        ClearItems();

        PokemonManager.OnPokemonFormSwitched -= OnPokemonFormChange;
        PokemonManager.OnPokemonSwitched -= OnPokemonChange;
    }
}
