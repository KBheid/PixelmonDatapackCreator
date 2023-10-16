using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelMoveListEditor : MonoBehaviour
{
    public List<LevelMoveListItem> items;
    public Transform itemHolder;
    public LevelMoveListItem itemPrefab;

    Form selectedForm;

    public void SetForm(Form f)
    {
        ClearItems();

        selectedForm = f;
        if (selectedForm == null || selectedForm.moves == null || selectedForm.moves.levelUpMoves == null)
            return;

        foreach (Levelupmove lum in f.moves.levelUpMoves)
        {
            foreach (string move in lum.attacks)
            {
                AddItem(new LevelMove() { level = lum.level, move = move });
            }
        }
    }

    public void UpdateContent()
    {
        Sort();

        Dictionary<int, List<string>> levelToMoves = new Dictionary<int, List<string>>();
        foreach (LevelMoveListItem item in items)
        {
            if (levelToMoves.TryGetValue(item.levelMove.level, out List<string> moveList))
                moveList.Add(item.levelMove.move);
            else
                levelToMoves.Add(item.levelMove.level, new List<string>() { item.levelMove.move } );
        }


        List<Levelupmove> outMoves = new List<Levelupmove>();
        foreach (KeyValuePair<int, List<string>> kvp in levelToMoves)
        {
            Levelupmove move = new Levelupmove
            {
                level = kvp.Key,
                attacks = kvp.Value.ToArray()
            };
            outMoves.Add(move);
        }

        selectedForm.moves.levelUpMoves = outMoves.ToArray();
    }

    public void AddItem()
    {
        LevelMove newMove = new LevelMove() { level = 0, move = "Absorb" };
        AddItem(newMove);

    }

    void AddItem(LevelMove lm)
    {
        LevelMoveListItem newItem = Instantiate(itemPrefab, itemHolder);
        newItem.SetLevelItem(lm);
        newItem.editor = this;
        items.Add(newItem);
        UpdateContent();
    }

    public void Sort() 
    {
        List<LevelMoveListItem> sorted = new List<LevelMoveListItem>(items.OrderBy(item => item.levelMove.level));

        for (int i=0; i<sorted.Count; i++)
        {
            sorted[i].transform.SetSiblingIndex(i);
        }
    }

    public void RemoveItem(LevelMoveListItem item)
    {
        items.Remove(item);
        Destroy(item.gameObject);
        UpdateContent();
    }

    public struct LevelMove
    {
        public int level;
        public string move;
    }

    void ClearItems()
    {
        foreach (LevelMoveListItem item in items)
        {
            Destroy(item.gameObject);
        }
        items.Clear();
    }

    private void OnEnable()
    {
        SetForm(PokemonManager.instance.selectedForm);
        PokemonManager.OnPokemonFormSwitched += SetForm;
    }

    private void OnDisable()
    {
        PokemonManager.OnPokemonFormSwitched -= SetForm;
    }
}