using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogPositionChanges : MonoBehaviour
{
    private Vector3 lastPosition;

    private void Start()
    {
        // �����ʒu��ۑ�
        lastPosition = transform.position;
    }

    private void Update()
    {
        // �ʒu���ύX���ꂽ�ꍇ
        if (transform.position != lastPosition)
        {
            // �ύX�O�ƕύX��̈ʒu�����O�o��
            Debug.Log("Position changed from " + lastPosition + " to " + transform.position);

            // �ǂ̃X�N���v�g�̂ǂ̊֐��ŕύX���ꂽ�������O�o��
            Debug.Log("Changed by " + UnityEngine.StackTraceUtility.ExtractStackTrace());

            // �ʒu���X�V
            lastPosition = transform.position;
        }
    }
}
