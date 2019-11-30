using static SuspectProject.Data.Game;

using UnityEditor;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SuspectProject.Data
{
    [CustomEditor(typeof(Game))]
    [CanEditMultipleObjects]
    public class GameEditor : Editor
    {
        private Game instance = null;

        private Dictionary<object, bool> _foldStatus = new Dictionary<object, bool>();


        void OnEnable()
        {
            instance = target as Game;
        }

        public void DrawDataBase(GameDataBase dataBase, string title)
        {
            if(!_foldStatus.ContainsKey(dataBase))
                _foldStatus[dataBase] = false;

            _foldStatus[dataBase] = EditorGUILayout.Foldout(_foldStatus[dataBase], $"{title}");


            EditorGUI.indentLevel++;
            EditorGUILayout.BeginVertical();

            if (_foldStatus[dataBase])
            {   
                Type type = dataBase.GetType();
                foreach (var propertyInfo in type.GetProperties())
                {
                    DrawDataPrimitive((GameDataPrimitive)propertyInfo.GetValue(dataBase), propertyInfo.Name);
                }
            }

            EditorGUILayout.EndVertical();
            EditorGUI.indentLevel--;

        }

        public void DrawDataEnumerable(GameDataEnumerable dataEnumerable, string title)
        {
            if (!_foldStatus.ContainsKey(dataEnumerable))
                _foldStatus[dataEnumerable] = false;

            _foldStatus[dataEnumerable] = EditorGUILayout.Foldout(_foldStatus[dataEnumerable], $"{title}");

            EditorGUI.indentLevel++;
            EditorGUILayout.BeginVertical();

            if (_foldStatus[dataEnumerable])
            {
                dynamic dataDictionary = Convert.ChangeType(dataEnumerable, dataEnumerable.GetType());
                foreach (var kv in dataDictionary)
                {
                    DrawDataBase(kv.Value, $"{kv.Key}");
                }
            }

            EditorGUILayout.EndVertical();
            EditorGUI.indentLevel--;
        }

        public void DrawDataPrimitive(GameDataPrimitive dataPrimitive, string title)
        {

            if (dataPrimitive is GameDataEnumerable)
            {
                DrawDataEnumerable(dataPrimitive as GameDataEnumerable, title);
            }
            else
            {
                dynamic data = Convert.ChangeType(dataPrimitive, dataPrimitive.GetType());

                EditorGUI.indentLevel++;
                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.PrefixLabel(title);
                    EditorGUILayout.SelectableLabel($"{data.value}");
                }
                EditorGUILayout.EndHorizontal();
                EditorGUI.indentLevel--;
            }
        }

        public override void OnInspectorGUI()
        {
            GUI.Box(EditorGUILayout.BeginVertical(), "Action History");
            EditorGUILayout.Space();
            EditorGUI.indentLevel++;

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            if (instance != null)
            {
                foreach(var historyAction in instance.historyActionQ)
                {
                    GUI.Box(EditorGUILayout.BeginVertical(), "");
                    EditorGUILayout.Space();
                    EditorGUILayout.LabelField(historyAction.ToString());
                    EditorGUILayout.HelpBox(historyAction.Description(), MessageType.Info);
                    EditorGUILayout.Space();
                    EditorGUILayout.EndVertical();
                }
            }

            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();


            GUI.Box(EditorGUILayout.BeginVertical(), "Data Hierarchy");
            EditorGUILayout.Space();
            EditorGUI.indentLevel++;

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            DrawDataBase(state, "state");

            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();
        }
    }
}
