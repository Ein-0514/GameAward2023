using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// �ʂ̃X�N���v�g����G�t�F�N�g���Đ������
public class OtherScript : MonoBehaviour
{
    [SerializeField]
    private Manager effectManager;

    private void Start()
    {
        // �G�t�F�N�g�ԍ� 0 ���Đ�����
        effectManager.PlayEffect(0);

        effectManager.PlayEffect(1);
        effectManager.PlayEffect(2);
    }
}
