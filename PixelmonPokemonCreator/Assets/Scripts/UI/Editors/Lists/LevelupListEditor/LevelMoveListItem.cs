using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMoveListItem : MonoBehaviour
{
    public LevelMoveListEditor editor;
    public LevelMoveListEditor.LevelMove levelMove;
    public TMPro.TMP_InputField levelInput;
    public TMPro.TMP_InputField moveInput;

    public void SetLevelItem(LevelMoveListEditor.LevelMove lm)
    {
        levelMove = lm;
        levelInput.text = lm.level.ToString();
        moveInput.text = lm.move;
    }

    public void UpdateContent() 
    {
        if (int.TryParse(levelInput.text, out int newLevel))
        {
            levelMove.level = newLevel;
        }
        else
        {
            levelMove.level = -1;
            moveInput.text = "-1";
        }
        levelMove.move = moveInput.text;

        editor.UpdateContent();
    }

    public void RemoveItem()
    {
        editor.RemoveItem(this);
    }
}
