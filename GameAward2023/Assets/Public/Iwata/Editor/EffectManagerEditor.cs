//#if UNITY_EDITOR
//using UnityEditor;
//using UnityEngine;

//[CustomEditor(typeof(EffectManager_iwata))]
//public class EffectManagerEditor : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        float originalLabelWidth = EditorGUIUtility.labelWidth;

//        base.OnInspectorGUI();

//        EffectManager_iwata effectManager = (EffectManager_iwata)target;

//        EditorGUILayout.Space();

//        EditorGUILayout.LabelField("Effect List", EditorStyles.boldLabel);

//        // �G�t�F�N�g�̐�������EffectInfo��ǉ�����
//        int EffectCount = (int)EffectType.E_EFFECT_KIND_MAX;
//        while (EffectManager_iwata.effectList.Count < EffectCount)
//        {
//            EffectManager_iwata.effectList.Add(new EffectInfo());
//        }

//        for (int i = 0; i < EffectManager_iwata.effectList.Count; i++)
//        {
//            EditorGUILayout.BeginHorizontal();

//            GUILayout.Label(EffectManager_iwata.EffectTypeToString((EffectType)i), GUILayout.Width(120f)); // ���x���̕��𒲐�����

//            GUILayout.Label("Prefab", GUILayout.Width(60f)); // ���x���̕��𒲐�����
//            EffectManager_iwata.effectList[i].effectPrefab = (GameObject)EditorGUILayout.ObjectField(EffectManager_iwata.effectList[i].effectPrefab, typeof(GameObject), false);

//            EditorGUILayout.EndHorizontal();

//            EditorGUIUtility.labelWidth = originalLabelWidth; // ���̃��x�����ɖ߂�
//        }
//    }
//}
//#endif