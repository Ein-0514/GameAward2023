using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public string sceneName = "GameOver";  // �ړ���̃V�[����

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // �Փ˂����I�u�W�F�N�g���v���C���[�̏ꍇ
        {
            SceneManager.LoadScene(sceneName); // �w�肵���V�[���Ɉړ�����
        }
    }
}

