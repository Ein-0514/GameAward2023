using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OutHierarchy_iwata : MonoBehaviour
{
    private HashSet<Transform> Junks;
    private HashSet<Transform> brokeJunks;
    
    // Start is called before the first frame update
    void Start()
    {
        GetJunks(Junks);
    }

    // Update is called once per frame
    void Update()
    {
        // �R�A�Ɍq�����Ă��Ȃ��K���N�^���i�[
        SearchJunks(Junks, brokeJunks);

        foreach (Transform outJunk in brokeJunks)
            outJunk.transform.parent = null;
        if(brokeJunks != null)
            brokeJunks.Clear();
    }

    /// <summary>
    /// �K���N�^���o�H�T���ŒT�����߂̊֐�
    /// </summary>
    private void SearchJunks(HashSet<Transform> Junks, HashSet<Transform> brokeJunks)
    {
        // �T���ς݃K���N�^���i�[
        HashSet<Transform> searchedJunks = new HashSet<Transform>();
        HashSet<Transform> connectToCoreJunks = new HashSet<Transform>();

        // �o�H��̃K���N�^���ꎞ�G�Ɋi�[
        HashSet<Transform> junks = new HashSet<Transform>();

        //ErrorPoint
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
                brokeJunks.Add(junk);

            junks.Clear();  // �ꎞ�I�ȃ��X�g���폜
        }
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

    /// <summary>
    /// �A�^�b�`�����I�u�W�F�N�g�̎q�I�u�W�F�N�g���擾����֐�
    /// </summary>
    public void GetJunks(HashSet<Transform>List)
    {
        for (int i = 0; i < this.gameObject.transform.childCount; i++)
            List.Add(transform.GetChild(i));
    }

}
