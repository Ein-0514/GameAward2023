using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    private GameManager m_GameManager;

    // Start is called before the first frame update
    void Start()
    {
        // GameManager��T��
        m_GameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("�Ԃ�����" + collision.transform.name);
        if (collision.transform.name.Contains("Core_Child"))
        {
            Debug.Log("�S�[������");
            m_GameManager.GameStatus = GameManager.eGameStatus.E_GAME_STATUS_END;
        }
    }
}