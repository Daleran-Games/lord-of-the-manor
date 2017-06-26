using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace DaleranGames.TBSFramework
{
    [CustomPropertyDrawer(typeof(Stat))]
    public class StatDrawer : PropertyDrawer
    {

        SerializedProperty t, v;
        string name;

       

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {

            
            //get the name before it's gone
            name = property.displayName;

            t = property.FindPropertyRelative("_type");
            v = property.FindPropertyRelative("_value");


            Rect contentPosition = EditorGUI.PrefixLabel(position, new GUIContent(name));

            //Check if there is enough space to put the name on the same line (to save space)
            if (position.height > 16f)
            {
                position.height = 16f;
                EditorGUI.indentLevel += 1;
                contentPosition = EditorGUI.IndentedRect(position);
                contentPosition.y += 18f;
            }

            float half = contentPosition.width / 2;
            GUI.skin.label.padding = new RectOffset(3, 3, 6, 6);

            //show the X and Y from the point
            EditorGUIUtility.labelWidth = 40f;
            contentPosition.width *= 0.5f;
            EditorGUI.indentLevel = 0;

            // Begin/end property & change check make each field
            // behave correctly when multi-object editing.
            EditorGUI.BeginProperty(contentPosition, label, t);
            {
                EditorGUI.PropertyField(contentPosition, t, new GUIContent("Type"));
            }
            EditorGUI.EndProperty();

            contentPosition.x += half;

            EditorGUI.BeginProperty(contentPosition, label, v);
            {
                EditorGUI.PropertyField(contentPosition, v, new GUIContent("Value"));
            }
            EditorGUI.EndProperty();

        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return Screen.width < 333 ? (16f + 18f) : 16f;
        }

    }
}
