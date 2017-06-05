using UnityEngine;
using UnityEditor;

namespace UnityEngine
{
   [CustomPropertyDrawer(typeof(Vector2Int))]
    public class Vector2IntDrawer : PropertyDrawer
    {

        SerializedProperty x, y;
        string name;


        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
   
                //get the name before it's gone
                name = property.displayName;

                //get the X and Y values
                property.Next(true);
                x = property.Copy();
                property.Next(true);
                y = property.Copy();


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
            EditorGUIUtility.labelWidth = 14f;
            contentPosition.width *= 0.5f;
            EditorGUI.indentLevel = 0;

            // Begin/end property & change check make each field
            // behave correctly when multi-object editing.
            EditorGUI.BeginProperty(contentPosition, label, x);
            {
                EditorGUI.BeginChangeCheck();
                int newVal = EditorGUI.IntField(contentPosition, new GUIContent("X"), x.intValue);
                if (EditorGUI.EndChangeCheck())
                    x.intValue = newVal;
            }
            EditorGUI.EndProperty();

            contentPosition.x += half;

            EditorGUI.BeginProperty(contentPosition, label, y);
            {
                EditorGUI.BeginChangeCheck();
                int newVal = EditorGUI.IntField(contentPosition, new GUIContent("Y"), y.intValue);
                if (EditorGUI.EndChangeCheck())
                    y.intValue = newVal;
            }
            EditorGUI.EndProperty();

        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return Screen.width < 333 ? (16f + 18f) : 16f;
        }

    }
}


