using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EffectObject
{
    public string name; // �G�t�F�N�g�̖��O
    public GameObject effectPrefab; // �v���n�u�Ƃ��Đݒ肵���G�t�F�N�g
}

public class EffectManager : MonoBehaviour
{
    [SerializeField] private List<EffectObject> effectList; // �G�t�F�N�g�̃��X�g

    // �G�t�F�N�g���Đ�����֐�
    public void PlayEffect(string effectName, Vector3 position)
    {
        // �G�t�F�N�g���ƈ�v����G�t�F�N�g������
        EffectObject effectObject = effectList.Find(e => e.name == effectName);

        // �G�t�F�N�g���Đ�
        if (effectObject != null)
        {
            GameObject effect = Instantiate(effectObject.effectPrefab, position, Quaternion.identity);
            Destroy(effect, 3f); // �G�t�F�N�g�̍Đ����Ԃ��w�肷��
        }
        else
        {
            Debug.LogWarning("Effect not found: " + effectName);
        }
    }
}
