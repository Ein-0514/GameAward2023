#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StageDataToJson_araki))]
public class StageJsonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("�f�[�^�쐬"))
        {
            // �{�^�����N���b�N���ꂽ���̏���
            var hoge = target as StageDataToJson_araki;
            hoge.Work();
        }
    }
}

#endif