using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomPropertyDrawer(typeof(GameStateAttribute))]
public class GameStateDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var gameStateAttribute = attribute as GameStateAttribute;
        if (gameStateAttribute.states != null && gameStateAttribute.states.Count > 0)
        {
            var propertyName = ToUnderscoreCase(property.name);
            var selectedIndex = Mathf.Max(0, gameStateAttribute.states.IndexOf(property.stringValue));
            selectedIndex = EditorGUI.Popup(position, propertyName, selectedIndex, gameStateAttribute.states.ToArray());
            property.stringValue = gameStateAttribute.states[selectedIndex];
        }
        else
        {
            EditorGUI.PropertyField(position, property, label);
        }
    }

    public static string ToUnderscoreCase(string str)
    {
        str = char.ToUpper(str[0]) + str.Substring(1);
        return string.Concat(str.Select((x) => char.IsUpper(x) ? " " + x.ToString() : x.ToString()));
    }
}
