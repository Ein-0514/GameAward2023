using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public Camera mainCamera;
    public Camera subCamera;
    private bool switchCamera; 

    void Start()
    {
        // �ŏ��̓T�u�J�������\���ɂ���
        subCamera.enabled = false;
    }

    void Update()
    {
        // V�L�[�������ꂽ��T�u�J�����̕\����؂�ւ���
        switchCamera = Input.GetMouseButtonDown(0);
        if (switchCamera)
        {
            switchCamera &= subCamera.enabled;
            subCamera.enabled = !subCamera.enabled;
        }
    }
    
    public bool OffPrevCamera()
    {
        return switchCamera; 
    }
}
