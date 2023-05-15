using UnityEngine;
using UnityEngine.SceneManagement;

public class RaycastSceneSwitch : MonoBehaviour
{
    public Camera camera; // ���C���΂��J����
    public float maxDistance = 100f; // ���C���͂��ő勗��

    void Update()
    {
        // �}�E�X�̍��N���b�N�Ń��C���΂�
        if (Input.GetMouseButtonDown(0))
        {
            // �X�N���[�����W�����[���h���W�ɕϊ�����
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            // ���C���Փ˂����I�u�W�F�N�g���擾����
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                // �Փ˂����I�u�W�F�N�g���ړI�̃I�u�W�F�N�g�������ꍇ�A�V�[����؂�ւ���
                if (hit.collider.CompareTag("Goal"))
                {
                    SceneManager.LoadScene("ClearScene");
                }
            }
        }
    }
}
