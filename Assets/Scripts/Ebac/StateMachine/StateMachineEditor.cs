using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(FSMExample))]
public class StateMachineEditor : Editor
{
    public bool showFoldout;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        FSMExample fse = (FSMExample)target;

        EditorGUILayout.Space(30);
        EditorGUILayout.LabelField("State Machine");

        if (fse.stateMachine == null) return;

        if (fse.stateMachine.CurrentState != null)
            EditorGUILayout.LabelField("Current State: ", fse.stateMachine.CurrentState.ToString());

        showFoldout = EditorGUILayout.Foldout(showFoldout, "Available States: ");

        if (showFoldout)
        {
            if(fse.stateMachine.dictionaryState != null)
            {
                var keys = fse.stateMachine.dictionaryState.Keys.ToArray();
                var vals = fse.stateMachine.dictionaryState.Values.ToArray();

                for(int i = 0; i < keys.Length; i++)
                {
                    EditorGUILayout.LabelField(string.Format("{0} :: {1}", keys[i], vals[i]));
                }
            }
        }

    }


}
