using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSpring : Metal
{
    public float maxHeight = 2f; // �V�����_�[�̍ő卂��
    public float minHeight = 0.5f; // �V�����_�[�̍ŏ�����
    public float increasingSpeed = 3f; // �L�k���x�i�L�т�Ƃ��j
    public float decreasingSpeed = 1f; // �L�k���x�i�k�ނƂ��j

    private float currentHeight; // ���݂̍���
    private bool increasing = true; // ���������ǂ�����\���t���O

    /// <summary>
    /// �p�����[�^�[�z�u
    /// </summary>
    /// <param name="paramList"></param>
    public override void SetParam(List<float> paramList)
    {
        maxHeight = paramList[0];
        minHeight = paramList[1];
        increasingSpeed = paramList[2];
        decreasingSpeed = paramList[3];
    }


    private void Start()
    {
        currentHeight = minHeight; // �����l��ݒ�
    }

    private void Update()
    {
        // ������ύX
        if (increasing)
        {
            currentHeight += increasingSpeed * Time.deltaTime; // ������
        }
        else
        {
            currentHeight -= decreasingSpeed * Time.deltaTime; // ������
        }

        // �ő卂���ɒB�����ꍇ�A�������̃t���O�𔽓]������
        if (currentHeight >= maxHeight)
        {
            increasing = false;
        }
        // �ŏ������ɒB�����ꍇ�A�������̃t���O�𔽓]������
        else if (currentHeight <= minHeight)
        {
            increasing = true;
        }

        // �V�����_�[�̃X�P�[����ύX����
        transform.localScale = new Vector3(1f, currentHeight, 1f);
    }
}