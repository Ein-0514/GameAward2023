using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�v���n�u�ɓ���Ȃ��ƃI�u�W�F�N�g���Ə�����̂Œ���
public class Delete : MonoBehaviour
{
    public float duration = 2.0f; // �G�t�F�N�g�̍Đ�����
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject, duration);
    }
}
