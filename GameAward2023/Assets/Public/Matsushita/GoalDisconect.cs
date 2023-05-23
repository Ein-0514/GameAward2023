using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalDisconect : MonoBehaviour
{ 
    void Update()
    {
        if (GameManager.Instance.GameStatus != GameManager.eGameStatus.E_GAME_STATUS_END)
            return;

        // "fixjoint"�łȂ����Ă���I�u�W�F�N�g���擾
        FixedJoint[] joints = FindObjectsOfType<FixedJoint>();

        foreach (FixedJoint joint in joints)
        {
            // �ڑ����ꂽ�I�u�W�F�N�g�̃^�O��"Player"�łȂ���ΐؒf����
            if (joint.connectedBody.gameObject.tag != "Player")
            {
                Destroy(joint);
            }
        }
    }
}