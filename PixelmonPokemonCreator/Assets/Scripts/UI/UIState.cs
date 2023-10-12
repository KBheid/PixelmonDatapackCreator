using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIState : MonoBehaviour
{
    public static UIState instance;

	private void Awake()
	{
		instance = this;
	}

	public UIMode state = UIMode.Pokemon;
	public string dataPath = "";

    public void SetState(string mode)
	{
        UIMode newMode = (UIMode) Enum.Parse(typeof(UIMode), mode);
        state = newMode;
	}
}

public enum UIMode
{
    Spawning,
    Pokemon,
    Moves
}