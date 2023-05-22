using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    // �G�t�F�N�g�̃f�[�^��ێ����邽�߂̃N���X
    [System.Serializable]
    public class EffectData
    {
        public GameObject effectPrefab; // �G�t�F�N�g�̃v���n�u
        public float duration; // �G�t�F�N�g�̍Đ�����
    }

    [SerializeField]
    private List<EffectData> effects = new List<EffectData>(); // �G�t�F�N�g�̃��X�g
    private GameObject currentEffect; // ���ݍĐ����̃G�t�F�N�g

    // �G�t�F�N�g���Đ����邽�߂̃��\�b�h
    public void PlayEffect(int index)
    {
        if (currentEffect != null)
        {
            Destroy(currentEffect); // ���ݍĐ����̃G�t�F�N�g��j������
        }

        // �w�肳�ꂽ�G�t�F�N�g�𐶐����A�Đ����Ԍ�ɔj������
        currentEffect = Instantiate(effects[index].effectPrefab, transform.position, Quaternion.identity);
        Destroy(currentEffect, effects[index].duration);
    }
}

