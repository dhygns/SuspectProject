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

        private Vector2 historyScrollPosition = Vector3.zero;


        void OnEnable()
        {
            instance = target as Game;
        }

        public void DrawDataBase(GameDataBase dataBase, string title)
        {
            if(!_foldStatus.ContainsKey(dataBase))
            {
                _foldStatus[dataBase] = false;
            }

            _foldStatus[dataBase] = EditorGUILayout.Foldout(_foldStatus[dataBase], $"{title}");

            EditorGUI.indentLevel++;
            EditorGUILayout.BeginVertical();

            if (_foldStatus[dataBase])
            {   
                Type type = dataBase.GetType();
                foreach (var propertyInfo in type.GetProperties())
                {
                    object value = propertyInfo.GetValue(dataBase);

                    if (value is DataPrimitive dataPrimitive)
                    {
                        DrawDataPrimitive(dataPrimitive, propertyInfo.Name);
                    }

                    if (value is GameDataBase childDataBase)
                    {
                        DrawDataBase(childDataBase, propertyInfo.Name);
                    }
                }
            }

            EditorGUILayout.EndVertical();
            EditorGUI.indentLevel--;

        }

        public void DrawDataEnumerable(DataEnumerable dataEnumerable, string title)
        {
            if (!_foldStatus.ContainsKey(dataEnumerable))
            {
                _foldStatus[dataEnumerable] = false;
            }

            _foldStatus[dataEnumerable] = EditorGUILayout.Foldout(_foldStatus[dataEnumerable], $"{title}");

            EditorGUI.indentLevel++;
            EditorGUILayout.BeginVertical();

            if (_foldStatus[dataEnumerable])
            {
                dynamic typedDataEnumerable = Convert.ChangeType(dataEnumerable, dataEnumerable.GetType());

                if (typedDataEnumerable is DataDictionary)
                {
                    foreach (var kv in typedDataEnumerable)
                    {
                        DrawDataBase(kv.Value, $"{kv.Key}");
                    }
                }

                if (typedDataEnumerable is DataList)
                {
                    foreach (var value in typedDataEnumerable)
                    {
                        EditorGUILayout.TextArea(value, GUILayout.Width(150.0f));
                    }
                }

            }

            EditorGUILayout.EndVertical();
            EditorGUI.indentLevel--;
        }

        public void DrawDataPrimitive(DataPrimitive dataPrimitive, string title)
        {
            if (dataPrimitive is DataEnumerable dataEnumerable)
            {
                DrawDataEnumerable(dataEnumerable, title);
            }
            else
            {
                dynamic data = Convert.ChangeType(dataPrimitive, dataPrimitive.GetType());
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField($"{title}", GUILayout.Width(150.0f));
                EditorGUILayout.TextArea($"{data.value}");
                EditorGUILayout.EndHorizontal();
            }
        }

        public override void OnInspectorGUI()
        {
            GUI.Box(EditorGUILayout.BeginVertical(GUILayout.MaxHeight(500.0f)), "Action History");
            EditorGUILayout.Space();
            EditorGUI.indentLevel++;

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            historyScrollPosition = EditorGUILayout.BeginScrollView(historyScrollPosition);

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
            EditorGUILayout.EndScrollView();

            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();

            EditorGUILayout.Space();
            EditorGUILayout.Space();

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
