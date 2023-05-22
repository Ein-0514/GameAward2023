using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParent : MonoBehaviour
{
    Transform parent;

    private void Awake()
    {
        if(!parent)
        {
            parent = GameObject.Find("PlayStage").transform.Find("StageObject").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!this.GetComponent<FixedJoint>())
        {
            this.gameObject.transform.parent = parent;
            // Rigidbody�R���|�[�l���g���擾
            Rigidbody rb = GetComponent<Rigidbody>();

            // Rigidbody��constraints�v���p�e�B���擾���āAx���Ay���Az����FreezePosition�t���O��false�ɐݒ�
            rb.constraints &= ~RigidbodyConstraints.FreezePositionX;
            rb.constraints &= ~RigidbodyConstraints.FreezePositionY;
            rb.constraints &= ~RigidbodyConstraints.FreezePositionZ;
            rb.constraints &= ~RigidbodyConstraints.FreezeRotationX;
            rb.constraints &= ~RigidbodyConstraints.FreezeRotationY;
            rb.constraints &= ~RigidbodyConstraints.FreezeRotationZ;
        }
    }
}
