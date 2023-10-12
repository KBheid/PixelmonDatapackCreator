using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
	public static int ToIntegerOrNegativeOne(this string str)
	{
		if (int.TryParse(str, out int res))
			return res;

		return -1;
	}

	public static float ToFloatOrNegativeOne(this string str)
	{
		if (float.TryParse(str, out float res))
			return res;

		return -1;
	}

	public static void SetDropdownToStringValue(this TMPro.TMP_Dropdown dropdown, string value)
	{
		List<string> options = dropdown.options.ConvertAll(option => option.text);

		int index = options.IndexOf(value);
		dropdown.value = index;
	}

	public static void SetDropdownToStringValueOrDefault(this TMPro.TMP_Dropdown dropdown, string value, string def)
	{
		List<string> options = dropdown.options.ConvertAll(option => option.text);

		int index = options.IndexOf(value);
		if (index >= 0)
			dropdown.value = index;
		else
			dropdown.SetDropdownToStringValue(def);
	}
}
