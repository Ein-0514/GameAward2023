using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TakaCoreSetting : MonoBehaviour
{
    const float ROTATION = 90.0f;   // ��]�p�x
    const float DAMPING_RATE = 0.5f;   // ��]������

    List<Transform> m_attachFaces;  // �g�ݗ��Ă����
    int m_selectFaceNum;            // �I��ʂ̔ԍ�
    int m_timeToRotate;             // ��]����
    float m_rotateY, m_rotateX;     // �p�x
    float m_lateY, m_lateX;         // �x���p�x
    int m_rotateFrameCnt;           // ��]�t���[���̃J�E���g]
    bool m_isDepath;		// �ʏ����擾�������t���O

    [SerializeReference] AudioClip m_RotSound;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        m_attachFaces = GetAttachFace();    // �g�ݗ��Ă���ʂ��擾
        m_selectFaceNum = 0;
        m_rotateY = m_rotateX = 0.0f;
        m_lateY = m_lateX = 0.0f;

        // ��]���Ԃ��v�Z
        m_timeToRotate = (int)(Mathf.Log(0.00001f) / Mathf.Log(1.0f - DAMPING_RATE));

        // �I�𒆂̖ʂ�傫����������
        m_attachFaces[m_selectFaceNum].localScale = new Vector3(1.25f, 1.25f, 1.25f);

        m_isDepath = false;

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //--- ��]��
        if (m_rotateFrameCnt > 0)
        {
            RotateCore();   // ��]����			
            return;
        }

        if (m_isDepath)
        {
            m_attachFaces = GetAttachFace();    // ���̑g�ݗ��Ă���ʂ��擾

            // �I�𒆂̖ʂ�傫����������
            m_attachFaces[m_selectFaceNum].localScale = new Vector3(1.25f, 1.25f, 1.25f);
            Debug.Log(m_selectFaceNum);
            Debug.Log(m_attachFaces.Count);
            for (int i = 0; i < m_attachFaces.Count; i++)
            {
                Vector3 pos = m_attachFaces[i].position;
                Debug.Log("X:" + pos.x + " Y:" + pos.y + " Z:" + pos.z);
            }

            m_isDepath = false;
        }

        SelectFace();   // �ʂ�I��
    }

    /// <summary>
    /// �g�ݗ��Ă���ʂ�Ԃ�
    /// </summary>
    List<Transform> GetAttachFace()
    {
        // �ʂ̊i�[���p��
        List<Transform> attachFaces = new List<Transform>();

        //--- �g�ݗ��Ă���ʂ����ԂɊi�[
        foreach (Transform child in this.transform)
        {
            // ��O�ɐL�т郌�C��p��
            Ray ray = new Ray(child.position, Vector3.back);
            RaycastHit hit;

            // �g�ݗ��Ă��Ȃ��ʂ̓X���[
            if (Physics.Raycast(ray, out hit, 10.0f)) continue;

            attachFaces.Add(child); // �ʂ��i�[
        }

        //--- �\�[�g
        attachFaces.Sort((a, b) =>
        {
            //if (a.position.y != b.position.y)
            if (Mathf.Abs(a.position.y - b.position.y) > 0.75f)
            {
                // Y���W���قȂ�ꍇ��Y���W�Ŕ�r����
                return b.position.y.CompareTo(a.position.y);
            }
            else
            {
                // Y���W�������ꍇ��X���W�Ŕ�r����
                return a.position.x.CompareTo(b.position.x);
            }
        });

        return attachFaces;
    }

    /// <summary>
    /// �ʂ̑I��
    /// </summary>
    void SelectFace()
    {
        int lastSelectFaceNum = m_selectFaceNum;    // �ߋ��̑I��ʔԍ����i�[

        float axisX = AxisInput.GetAxisRawRepeat("Horizontal");         // ���Ɉړ�

        //--- ���ɓ��͂����������̂ݏ���
        if (axisX != 0)
        {
            Vector3 facePos = m_attachFaces[m_selectFaceNum].position;  // ���݂̖ʂ̍��W���擾
            facePos.x += axisX; // �ړ���̖ʂ̍��W�ɏC��

            bool isRotate = true;

            //--- ��v����ʂ�T��
            for (int i = 0; i < m_attachFaces.Count; i++)
            {
                //--- ���݂̖ʂƎ��̖ʂ�XY���W��Vector2�Ɋi�[
                Vector2 currentFacePos = new Vector2(m_attachFaces[i].position.x, m_attachFaces[i].position.y);
                Vector2 newxtFacePos = new Vector2(facePos.x, facePos.y);

                // XY���ʂł̋��������ꂷ���Ă�����X���[
                if (Vector2.Distance(currentFacePos, newxtFacePos) > 0.05f) continue;

                m_selectFaceNum = i;
                isRotate = false;
                break;
            }

            if (isRotate)
            {
                m_attachFaces[lastSelectFaceNum].localScale = new Vector3(1.0f, 1.0f, 1.0f);    // �ߋ��̖ʂ�������
                StartRotateY((int)axisX);
                return;
            }
        }

        float axisY = (float)AxisInput.GetAxisRawRepeat("Vertical");    // �c�Ɉړ�

        //--- �c�ɓ��͂����������̂ݏ���
        if (axisY != 0)
        {
            Vector3 facePos = m_attachFaces[m_selectFaceNum].position;  // ���݂̖ʂ̍��W���擾
            facePos.y += axisY; // �ړ���̖ʂ̍��W�ɏC��

            bool isRotate = true;

            //--- ��v����ʂ�T��
            for (int i = 0; i < m_attachFaces.Count; i++)
            {
                //--- ���݂̖ʂƎ��̖ʂ�XY���W��Vector2�Ɋi�[
                Vector2 currentFacePos = new Vector2(m_attachFaces[i].position.x, m_attachFaces[i].position.y);
                Vector2 newxtFacePos = new Vector2(facePos.x, facePos.y);

                // XY���ʂł̋��������ꂷ���Ă�����X���[
                if (Vector2.Distance(currentFacePos, newxtFacePos) > 0.05f) continue;

                m_selectFaceNum = i;
                isRotate = false;
                break;
            }

            if (isRotate)
            {
                m_attachFaces[lastSelectFaceNum].localScale = new Vector3(1.0f, 1.0f, 1.0f);    // �ߋ��̖ʂ�������
                StartRotateX((int)axisY);
                return;
            }
        }

        //--- �I��ʂ��ύX���ꂽ�ꍇ
        if (m_selectFaceNum != lastSelectFaceNum)
        {
            m_attachFaces[lastSelectFaceNum].localScale = new Vector3(1.0f, 1.0f, 1.0f);    // �ߋ��̖ʂ�������
            m_attachFaces[m_selectFaceNum].localScale = new Vector3(1.25f, 1.25f, 1.25f);   // ���݂̖ʂ�����
        }

    }

    /// <summary>
    /// �R�A�̉�]����
    /// </summary>
    void RotateCore()
    {
        float lastY = m_lateY;
        float lastX = m_lateX;

        //--- �x������
        m_lateY = (m_rotateY - m_lateY) * DAMPING_RATE + m_lateY;
        m_lateX = (m_rotateX - m_lateX) * DAMPING_RATE + m_lateX;

        //--- ���W�v�Z
        this.transform.Rotate(Vector3.up, m_lateY - lastY, Space.World);
        this.transform.Rotate(Vector3.right, m_lateX - lastX, Space.World);

        m_rotateFrameCnt++; // ��]�t���[���J�E���g

        //--- ��]�I�����̏���
        if (m_rotateFrameCnt > m_timeToRotate)
        {
            m_attachFaces = GetAttachFace();    // ���̑g�ݗ��Ă���ʂ��擾
            m_rotateFrameCnt = 0;   // ��]�t���[�������Z�b�g
            m_selectFaceNum = 0;    // �I��ʂ̔ԍ������Z�b�g
            m_attachFaces[m_selectFaceNum].localScale = new Vector3(1.25f, 1.25f, 1.25f);   // ���݂̖ʂ�����
        }
    }

    /// <summary>
    /// �R�A��Y����]�J�n
    /// </summary>
    void StartRotateY(int direction)
    {
        if (direction == 0) return;

        m_rotateY += ROTATION * direction;  // �p�x��ݒ�
        m_rotateFrameCnt = 1;	// �ŏ��̃J�E���g
        audioSource.PlayOneShot(m_RotSound);    //SE�̍Đ�
    }

    /// <summary>
    /// �R�A��X����]�J�n
    /// </summary>
    void StartRotateX(int direction)
    {
        if (direction == 0) return;

        m_rotateX -= ROTATION * direction;  // �p�x��ݒ�
        m_rotateFrameCnt = 1;   // �ŏ��̃J�E���g
        audioSource.PlayOneShot(m_RotSound);    //SE�̍Đ�
    }

    /// <summary>
    /// FixedJoint��ǉ�
    /// </summary>
    void AddFixedJoint(GameObject junk, Vector3 junkPos, Vector3 direction)
    {
        // �w������ɐL�т郌�C��p��
        Ray ray = new Ray(junkPos, direction);
        RaycastHit hit;

        // �w������ɃK���N�^�����݂��Ȃ��Ȃ�X���[
        if (!Physics.Raycast(ray, out hit, 1.0f)) return;

        //--- �K���N�^�̏ꍇ�A�g�ݗ��Ă���ʂ��𔻒肷��
        if (hit.transform.tag == "Junk")
        {
            JunkSetting junkSetting = hit.transform.GetComponent<JunkSetting>();
            if (!junkSetting.CanAttach(-direction)) return;
        }

        //--- FixedJoint��ݒ�
        FixedJoint fixedJoint = junk.AddComponent<FixedJoint>();
        fixedJoint.connectedBody = hit.rigidbody;
        fixedJoint.breakForce = 2000.0f;
        fixedJoint.breakTorque = 2000.0f;

        Rigidbody junkRigidbody = junk.GetComponent<Rigidbody>();
        junkRigidbody.constraints = RigidbodyConstraints.FreezeAll; // ���W�E�p�x���Œ肷��
        junkRigidbody.isKinematic = false;  // �������Z��L����

        //--- �A�^�b�`����鑤��FixedJoint��ݒ�
        FixedJoint otherFixedJoint = hit.transform.gameObject.AddComponent<FixedJoint>();
        otherFixedJoint.connectedBody = junkRigidbody;
        otherFixedJoint.breakForce = 2000.0f;
        otherFixedJoint.breakTorque = 2000.0f;

        Rigidbody attachedRigidbody = hit.rigidbody;
        attachedRigidbody.constraints = RigidbodyConstraints.FreezeAll; // ���W�E�p�x���Œ肷��
        attachedRigidbody.isKinematic = false;  // �������Z��L����
    }

    /// <summary>
    /// FixedJoint���폜
    /// </summary>
    void RemoveFixedJoint(GameObject junk, Vector3 direction)
    {
        // �w������ɐL�т郌�C��p��
        Ray ray = new Ray(junk.transform.position, direction);
        RaycastHit hit;

        // �w������ɃK���N�^�����݂��Ȃ��Ȃ�X���[
        if (!Physics.Raycast(ray, out hit, 1.0f)) return;

        //--- �K���N�^�̏ꍇ�A�g�ݗ��Ă���ʂ��𔻒肷��
        if (hit.transform.tag == "Junk")
        {
            JunkSetting junkSetting = hit.transform.GetComponent<JunkSetting>();
            if (!junkSetting.CanAttach(-direction)) return;
        }

        //--- ���͂̎��g���q���ł���FixedJoint���폜
        Rigidbody rigidbody = junk.GetComponent<Rigidbody>();
        FixedJoint[] fixedJoints = hit.transform.GetComponents<FixedJoint>();
        for (int i = 0; i < fixedJoints.Length; i++)
        {
            if (rigidbody != fixedJoints[i].connectedBody) continue;

            Destroy(fixedJoints[i]);
        }
    }

    /// <summary>
    /// ��]���t���O���擾
    /// </summary>
    public bool IsRotate()
    {
        return (m_rotateFrameCnt > 0);
    }

    /// <summary>
    /// �K���N�^���A�^�b�`����
    /// �A�^�b�`�̐����t���O��Ԃ�
    /// </summary>
    public bool AttachJunk(GameObject junk)
    {
        //--- �g�ݗ��Ă��Ȃ��ʂł���΃L�����Z��
        if (m_attachFaces[m_selectFaceNum].transform.tag == "Junk")
        {
            //--- �g�ݗ��Ă��Ȃ��ꍇ
            if (!m_attachFaces[m_selectFaceNum].GetComponent<JunkSetting>().CanAttach(Vector3.back)) return false;
        }

        m_attachFaces[m_selectFaceNum].localScale = new Vector3(1.0f, 1.0f, 1.0f);

        //--- �A�^�b�`������W���w��
        Vector3 junkPos = m_attachFaces[m_selectFaceNum].position;
        //Vector3 junkPos = face.position;
        junkPos -= Vector3.forward.normalized * 1.0f;
        junk.transform.position = junkPos;

        JunkSetting junkSetting = junk.GetComponent<JunkSetting>();
        List<Vector3> searchDirects = junkSetting.GetAttachVector();

        //--- �e������FixedJoint��ݒ�
        for (int i = 0; i < searchDirects.Count; i++)
            AddFixedJoint(junk, junkPos, searchDirects[i]);

        junk.transform.SetParent(this.transform);   // �R�A�̎q�ɂ���

        m_attachFaces = GetAttachFace();    // ���̑g�ݗ��Ă���ʂ��擾

        // �I�𒆂̖ʂ�傫����������
        m_attachFaces[m_selectFaceNum].localScale = new Vector3(1.25f, 1.25f, 1.25f);

        return true;
    }

    public void DetachJunk()
    {
        // �K���N�^�łȂ��ꍇ�X���[
        if (m_attachFaces[m_selectFaceNum].transform.tag != "Junk") return;

        // Core�܂�FixedJoint�Ōq����Ă��Ȃ��K���N�^���X�g
        HashSet<GameObject> unconnectedJunks = new HashSet<GameObject>();
        // �폜�Ώۂ̃K���N�^
        GameObject selectJunk = m_attachFaces[m_selectFaceNum].gameObject;
        // Core�܂�FixedJoint�Ōq����Ă��Ȃ��K���N�^���擾
        FindUnconnectedJunkToCore(selectJunk, unconnectedJunks);

        // unconnectedJunk�̃K���N�^��FixedJoint���폜����
        foreach (GameObject junk in unconnectedJunks)
        {
            FixedJoint[] unconnectedJoints = junk.GetComponents<FixedJoint>();
            for (int i = 0; i < unconnectedJoints.Length; i++)
                Destroy(unconnectedJoints[i]);
        }

        // �S�~�R�ɖ߂�
        foreach (GameObject junk in unconnectedJunks)
        {
            JunkController junkController = junk.GetComponent<JunkController>();
            if (junkController != null)
                junkController.ResetTransform(); // �S�~�R�ɖ߂�����
        }

        //--- �S�~�R�ɖ߂�
        Transform junkTransform = m_attachFaces[m_selectFaceNum].transform;
        junkTransform.GetComponent<JunkController>().ResetTransform();

        m_isDepath = true;  // �ʏ����擾�������t���O�𗧂Ă�

        // HACK:�����s����Ɩʂ̏�񂪏�肭�擾����Ȃ���
        /**
		 *	if (m_isDepath)
		 *	{
		 *		m_attachFaces = GetAttachFace();    // ���̑g�ݗ��Ă���ʂ��擾
		 *
		 *		// �I�𒆂̖ʂ�傫����������
		 *		m_attachFaces[m_selectFaceNum].localScale = new Vector3(1.25f, 1.25f, 1.25f);
		 *		Debug.Log(m_selectFaceNum);
		 *		Debug.Log(m_attachFaces.Count);
		 *		for (int i = 0; i < m_attachFaces.Count; i++)
		 *		{
		 *			Vector3 pos = m_attachFaces[i].position;
		 *			Debug.Log("X:" + pos.x + " Y:" + pos.y + " Z:" + pos.z);
		 *		}
		 *
		 *		m_isDepath = false;
		 *	}
		 */
    }

    /// <summary>
    /// �R�A���g�̏���
    /// </summary>
    public void CoreReady()
    {
        m_attachFaces[m_selectFaceNum].localScale = new Vector3(1.0f, 1.0f, 1.0f);

        foreach (Transform child in this.transform)
        {
            // �Œ������
            child.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

            // �X�^�[�g�ʒu�ֈړ�
            this.transform.position = new Vector3(-10.0f, 2.5f, -10.0f);
        }
    }

    /// <summary>
    /// FixedJoint�Ōq����Ă���K���N�^���ċA�I�ɒT��
    /// </summary>
    private void FindConnectedJunk(GameObject obj, HashSet<GameObject> connectedJunk)
    {
        FixedJoint[] fixedJoints = obj.GetComponents<FixedJoint>();

        // FixedJoint������ꍇ�A�q�����Ă���I�u�W�F�N�g����������
        foreach (FixedJoint fixedJoint in fixedJoints)
        {
            GameObject connectedObj = fixedJoint.connectedBody.gameObject;

            // �q�����Ă���I�u�W�F�N�g���K���N�^�ŁA���X�g�ɒǉ�����Ă��Ȃ��ꍇ�A���X�g�ɒǉ�����
            if (connectedObj.tag == "Junk" && !connectedJunk.Contains(connectedObj))
            {
                connectedJunk.Add(connectedObj);

                // �q�����Ă���I�u�W�F�N�g�̎���ɂ��Ă�����������J��Ԃ�
                FindConnectedJunk(connectedObj, connectedJunk);
            }
        }
    }

    /// <summary>
    /// core�܂�FixedJoint�Ōq�����Ă��Ȃ��K���N�^��Ԃ�
    /// </summary>
    private void FindUnconnectedJunkToCore(GameObject selectJunk, HashSet<GameObject> unconnectedJunk)
    {
        // �o�H�T�����ɃI�u�W�F�N�g���i�[����ϐ�
        HashSet<GameObject> junkList = new HashSet<GameObject>();
        // ���g��FixedJoint
        FixedJoint[] fixedJoints = selectJunk.GetComponents<FixedJoint>();
        // ���ӂ̃K���N�^��1�����ׂ�
        foreach (FixedJoint fixedJoint in fixedJoints)
        {
            GameObject connectedObj = fixedJoint.connectedBody.gameObject;

            // �폜�Ώۂ̃K���N�^�Ȃ�X�L�b�v
            if (connectedObj == selectJunk)
                continue;

            // �o�H��ɂ���I�u�W�F�N�g����U�i�[����
            junkList.Add(connectedObj);

            // �^�O��"Core"�̃I�u�W�F�N�g�ɗאڂ��Ă��邩���ׂ�
            if (connectedObj.tag == "Core")
            {
                junkList.Clear();
                continue;
            }

            FindOtherJunk(selectJunk, connectedObj, junkList);

            // �ŏI�I��list�ɓ����Ă���K���N�^��ǉ�
            foreach (GameObject obj in junkList)
                unconnectedJunk.Add(obj);
        }
        // �Ō�ɍ폜�Ώۂ̃K���N�^��ǉ�
        unconnectedJunk.Add(selectJunk);
    }


    /// <summary>
    /// �K���N�^���ċA�I�ɒT�����߂̊֐�
    /// </summary>
    private bool FindOtherJunk(GameObject selectJunk, GameObject junk, HashSet<GameObject> junkList)
    {
        FixedJoint[] fixedJoints = junk.GetComponents<FixedJoint>();
        foreach (FixedJoint fixedJoint in fixedJoints)
        {
            GameObject connectedObj = fixedJoint.connectedBody.gameObject;

            // �폜�Ώۂ̃K���N�^�A�������͒T���ς݂̕����X�L�b�v
            if (connectedObj == selectJunk || junkList.Contains(connectedObj))
                continue;

            // �o�H��ɂ���I�u�W�F�N�g����U�i�[����
            junkList.Add(connectedObj);

            // ��Core�ɗאڂ��Ă���Ȃ炻��ȏセ�̕����֒T������K�v�͂Ȃ�
            // ���ŏ�i�܂Ŗ߂��č폜�Ώۂ���V���ȕ����ւƒT�����n�߂�K�v������
            // �^�O��"Core"�̃I�u�W�F�N�g�ɗאڂ��Ă��邩���ׂ�
            if (connectedObj.tag == "Core")
            {
                junkList.Clear();
                break;
            }
            if(FindOtherJunk(selectJunk, connectedObj, junkList)) return true;
        }
        return false;
    }
}