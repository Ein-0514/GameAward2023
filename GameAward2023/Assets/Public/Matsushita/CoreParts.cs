using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CoreParts : Metal
{
    private FixedJoint fixedJoint;
    private int frameCount = 0;
    private const int MAX_FRAME_COUNT = 60; // 1�b������̃t���[������60�̏ꍇ

    void Start()
    {
        fixedJoint = GetComponent<FixedJoint>();
    }

    void Update()
    {
        // FixedJoint���폜���ꂽ�ꍇ�̏���
        if (fixedJoint == null)
        {
            frameCount++;

            // �P�b�o�߂�����Q�[���V�[���Ɉړ�
            if (frameCount > MAX_FRAME_COUNT)
            {
                SceneManager.LoadScene("GameScene");
            }
        }
        else
        {
            frameCount = 0;
        }
    }

    /// <summary>
    /// �B�R���N���[�g�ɂ��������Ƃ�
    /// </summary>
    public override void HitNailConcrete()
    {
    }

    /// <summary>
    /// �S�[���ɐG�ꂽ�Ƃ�
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Goal"))
        {
            Debug.Log("�Q�[���N���A");
        }
    }
}
