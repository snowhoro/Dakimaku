using System;
using System.Text.RegularExpressions;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(EnumFlagAttribute))]
public class EnumFlagDrawer : PropertyDrawer {
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		Enum targetEnum = GetBaseProperty<Enum>(property);
		string propName = FormatName(property.name);
		EditorGUI.BeginProperty(position, label, property);
		Enum enumNew = EditorGUI.EnumMaskField(position, propName, targetEnum);
		property.intValue = (int) Convert.ChangeType(enumNew, targetEnum.GetType());
		EditorGUI.EndProperty();
	}

	private static T GetBaseProperty<T>(SerializedProperty prop)
	{
		// Separate the steps it takes to get to this property
		string[] separatedPaths = prop.propertyPath.Split('.');
		
		// Go down to the root of this serialized property
		System.Object reflectionTarget = prop.serializedObject.targetObject as object;
		// Walk down the path to get the target object
		foreach (var path in separatedPaths)
		{
			FieldInfo fieldInfo = reflectionTarget.GetType().GetField(path, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
			reflectionTarget = fieldInfo.GetValue(reflectionTarget);
		}
		return (T) reflectionTarget;
	}

    /// <summary>
    /// This format the name in the same way Unity formats every property name
    /// </summary>
    private static string FormatName(string propertyName)
    {
        if (string.IsNullOrEmpty(propertyName))
        {
            return propertyName;
        }

        int index = propertyName.IndexOf("_");
        if (index != -1 && (index != 1 || index != 2) && propertyName.Length > (index + 1))
        {
            propertyName = propertyName.Substring(index + 1);
        }
        string formattedPropertyName = char.ToUpper(propertyName[0]).ToString();
        if (propertyName.Length > 1)
        {
            formattedPropertyName = string.Concat(formattedPropertyName, Regex.Replace(propertyName.Substring(1), "([a-z])([A-Z])", "$1 $2"));
        }
        return formattedPropertyName;
    }
}
