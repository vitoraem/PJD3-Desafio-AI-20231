using UnityEngine;
using UnityEditor;
namespace ChallengeAI {
  [CustomEditor(typeof(PlayerState))]
  public class PlayerStateEditor : Editor {
    SerializedProperty energy;

    private void OnEnable() {
      energy = serializedObject.FindProperty("Energy");
    }
    public override void OnInspectorGUI()
    {
      serializedObject.Update();
      EditorGUILayout.PropertyField(energy);
      // EditorGUILayout.FloatField()
      
      serializedObject.ApplyModifiedProperties();
    }
    
  }
}