using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;


[AttributeUsage(AttributeTargets.Field)]
public class IgnoreAttribute : Attribute
{
    public virtual bool DoIgnore(object obj)
	{
        return true;
	}
}
public class IgnoreEmptyStringAttribute : IgnoreAttribute
{
    public override bool DoIgnore(object obj)
    {
        string val = (string) obj;
        return val == "";
    }
}
public class IgnoreNullAttribute : IgnoreAttribute
{
    public override bool DoIgnore(object obj)
    {
        return obj == null;
    }
}
public class IgnoreZeroAttribute : IgnoreAttribute
{
    public override bool DoIgnore(object obj)
    {
        if (obj is int)
            return ((int) obj) == 0;

        if (obj is float)
            return ((float) obj) == 0f;

        throw new Exception("Invalid IgnoreZeroAttribute (not int or float)");
    }
}
public class IgnoreAllFieldsNullFalseOrZeroAttribute : IgnoreAttribute
{
    public override bool DoIgnore(object obj)
    {

        var fields = obj.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
        foreach (FieldInfo fieldInfo in fields)
        {
            object val = fieldInfo.GetValue(obj);
            
            // If val is null, move to next
            if (val == null)
                continue;
            
            // If val is False, move to next
            if (val is bool)
            {
                if ( !((bool) val))
                    continue;
            }

            // If val is 0, move to next
            if (val is int)
            {
                if ( ((int) val) == 0)
                    continue;
            }

            // If val is 0f, move to next
            if (val is float)
            {
                if ( ((float) val) == 0f)
                    continue;
            }

            // If we've reached here, we're not null, not True, and not 0 or 0f - so we need to not ignore.
            return false;
        }

        return true;
    }
}

[AttributeUsage(AttributeTargets.Field)]
public abstract class IgnoreArrayAttribute : Attribute
{
    public abstract bool DoIgnore(object[] objs);
}
public class IgnoreEmptyArrayAttribute : IgnoreArrayAttribute
{
    public override bool DoIgnore(object[] objs)
    {
        if (objs == null || objs.Length == 0)
            return true;
        return false;
    }
}
public class IgnoreEmptyStringArrayAttribute : IgnoreArrayAttribute
{
    public override bool DoIgnore(object[] objs)
    {
        if (objs == null)
            return true;

        foreach (object obj in objs)
        {
            string st = obj as string;
            if (st != "")
                return false;
        }
        return false;
    }
}

public class PokemonJsonConverter<T> : JsonConverter<T>
{

    public override T? ReadJson(JsonReader reader, Type objectType, T? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        JObject jsonObject = JObject.Load(reader);

        T result = existingValue ?? (T) Activator.CreateInstance(typeof(T), new object[] { });

        // Deserialize all fields
        var fields = typeof(Pokemon).GetFields(BindingFlags.Public | BindingFlags.Instance);

        foreach (var field in fields)
        {
            JToken value = jsonObject[field.Name];
            if (value != null && value.Type != JTokenType.Null)
            {
                field.SetValue(result, value.ToObject(field.FieldType, serializer));
            }
        }

        return result;
    }

    public override void WriteJson(JsonWriter writer, T? value, JsonSerializer serializer)
    {
        JObject jsonObject = new JObject();

        // Serialize all fields that do not are not marked for ignore
        var fields = value.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);

        foreach (var field in fields)
        {
            //Debug.Log(field.Name + " | " + field.GetValue(value));
            if (field.FieldType.IsArray)
            {
                List<IgnoreAttribute> attrs = new List<IgnoreAttribute>(field.GetCustomAttributes(typeof(IgnoreAttribute)).Cast<IgnoreAttribute>());
                bool ignore = attrs.Any(attr => attr != null && attr.DoIgnore(field.GetValue(value)));

                List<IgnoreArrayAttribute> arrayAttrs = new List<IgnoreArrayAttribute>(field.GetCustomAttributes(typeof(IgnoreArrayAttribute)).Cast<IgnoreArrayAttribute>());
                ignore = ignore || arrayAttrs.Any(attr => attr != null && attr.DoIgnore(field.GetValue(value) as object[]));

                if (!ignore)
                {
                    jsonObject.Add(field.Name, JToken.FromObject(field.GetValue(value), serializer));
                }
            }
            else
            {
                List<IgnoreAttribute> attrs = new List<IgnoreAttribute>(field.GetCustomAttributes(typeof(IgnoreAttribute)).Cast<IgnoreAttribute>());
                bool ignore = attrs.Any(attr => attr != null && attr.DoIgnore(field.GetValue(value)));

                if (!ignore)
                {
                    jsonObject.Add(field.Name, JToken.FromObject(field.GetValue(value), serializer));
                }
            }

        }

        jsonObject.WriteTo(writer);
    }
}
