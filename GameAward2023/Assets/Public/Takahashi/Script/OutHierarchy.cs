using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OutHierarchy : MonoBehaviour
{
    [SerializeReference] GameSceneController gSC;
    private GameObject core;
    private HashSet<Transform> Junks;
    private bool isStart;
    // Start is called before the first frame update
    void Start()
    {
        isStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        isStart = gSC.IsStart;
        if (isStart)
        {
            for (int i = 0; i < this.gameObject.transform.childCount; i++)
                Junks.Add(transform.GetChild(i));
            isStart = false;
            SearchJunks(Junks);
            // Hierarchy���Core����O���
            foreach (Transform obj in Junks)
                obj.transform.parent = null;
        }
    }

    /// <summary>
    /// �K���N�^���o�H�T���ŒT�����߂̊֐�
    /// </summary>
    private void SearchJunks(HashSet<Transform> Junks)
    {
        // �T���ς݃K���N�^���i�[
        HashSet<Transform> searchedJunks = new HashSet<Transform>();
        HashSet<Transform> connectToCoreJunks = new HashSet<Transform>();

        // �o�H��̃K���N�^���ꎞ�G�Ɋi�[
        HashSet<Transform> junks = new HashSet<Transform>();

        FixedJoint[] fixedJoints = Junks.First().GetComponents<FixedJoint>();

        // ���ӂ̃K���N�^��1�����ׂ�
        foreach (FixedJoint fixedJoint in fixedJoints)
        {
            // ��������Ă���K���N�^���擾
            Transform connectJunk = fixedJoint.connectedBody.transform;

            // �T���ς݂ł���Ώ������Ȃ�
            if (searchedJunks.Contains(connectJunk)) continue;

            // �T���Ώۂ��R�A�Ȃ珈�����Ȃ�
            if (connectJunk.tag == "Core") continue;

            // �o�H��ɂ���I�u�W�F�N�g���i�[
            junks.Add(connectJunk);

            // �R�A�ɍs��������܂ŒT��
            if (SearchJunksLoop(Junks.First(), connectJunk, junks, connectToCoreJunks))
            {
                //--- �R�A�܂ł̌o�H�����X�g�ɒǉ�
                foreach (Transform junk in junks)
                    connectToCoreJunks.Add(junk);

                junks.Clear();  // �R�A�܂Ōq�����Ă���΃��X�g���폜
                continue;
            }

            //--- �R�A�Ɍq����Ȃ��o�H�����X�g�ɒǉ�
            foreach (Transform junk in junks)
                Junks.Add(junk);

            junks.Clear();  // �ꎞ�I�ȃ��X�g���폜
        }

        // �폜�Ώۂ̃K���N�^���ǉ�
        Junks.Add(Junks.First());
    }

    /// <summary>
    /// �K���N�^���ċA�I�ɒT�����߂̊֐�
    /// </summary>
    private bool SearchJunksLoop(Transform selectJunk, Transform junk, HashSet<Transform> searchJunks, HashSet<Transform> connectToCoreJunks)
    {
        // �T���Ώۂ�FixedJoint���擾
        FixedJoint[] fixedJoints = junk.GetComponents<FixedJoint>();

        foreach (FixedJoint fixedJoint in fixedJoints)
        {
            // ��������Ă���K���N�^���擾
            Transform connectJunk = fixedJoint.connectedBody.transform;

            // �T���ς݂ł���Ώ������Ȃ�
            if (searchJunks.Contains(connectJunk)) continue;

            // �폜�Ώۂ̃K���N�^�͏������Ȃ�
            if (connectJunk == selectJunk) continue;

            // �R�A�Ɍq����o�H�Ɍq�����Ă���ΒT�����I��
            if (connectToCoreJunks.Contains(connectJunk)) return true;

            // �T���Ώۂ��R�A�Ȃ�T�����I��
            if (connectJunk.tag == "Core") return true;

            // �o�H��ɂ���I�u�W�F�N�g���i�[
            searchJunks.Add(connectJunk);

            // �R�A�ɍs��������܂ōċA�I�ɒT��
            if (SearchJunksLoop(selectJunk, connectJunk, searchJunks, connectToCoreJunks)) return true;
        }
        return false;
    }

}
