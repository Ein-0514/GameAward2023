using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreSetting_iwata : MonoBehaviour
{
    const float ROTATION = 90.0f;   // ��]�p�x
    const float DAMPING_RATE = 0.5f;   // ��]������
    const float ENLARGE_SiZE = 1.25f;   //�I�𒆂�Core�̑傫��
    const float ORIZIN_SiZE = 1.00f;   //�I���O��Core�̑傫��

    List<Transform> m_attachFaces;	// �g�ݗ��Ă����
    int m_SelectFaceNum;     // �I��ʂ̔ԍ�
    int m_timeToRotate;             // ��]����
    float m_rotateY, m_rotateX;     // �p�x
    float m_lateY, m_lateX;         // �x���p�x
    public int m_rotateFrameCnt;           // ��]�t���[���̃J�E���g
    bool m_isDepath;        // �ʏ����擾�������t���O

    //[SerializeReference] AudioClip m_RotSound;  //�I�[�f�B�I�t�@�C���̏��
    //AudioSource audioSource;    //�Đ����邽�߂̃n���h��

    // Start is called before the first frame update
    void Start()
    {
        m_attachFaces = GetAttachFace();	// �g�ݗ��Ă���ʂ��擾
        m_SelectFaceNum = 0;
        m_rotateY = m_rotateX = 0.0f;
        m_lateY = m_lateX = 0.0f;

        // ��]���Ԃ��v�Z
        m_timeToRotate = (int)(Mathf.Log(0.00001f) / Mathf.Log(1.0f - DAMPING_RATE));

        //�I�΂�Ă���Core��傫������
        EnlargeSizeCore();

        //�Č������K�v�Ȏ��ɗ��Ă�Flag��ݒ�
        m_isDepath = false;

        //audioSource = GetComponent<AudioSource>();


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
            EnlargeSizeCore();

            m_isDepath = false;
        }
    }

    List<Transform> GetAttachFace()
    {
        // �ʂ̊i�[���p��
        List<Transform> attachFaces = new List<Transform>();

        //--- �g�ݗ��Ă���ʂ����ԂɊi�[
        foreach (Transform child in this.transform)
        {
            if (child.tag != "Player") continue;

            // ��O�ɐL�т郌�C��p��
            Ray ray = new Ray(child.position, Vector3.back);
            RaycastHit hit;

            // �g�ݗ��Ă��Ȃ��ʂ̓X���[
            // ��O�ɕ�����������X�L�b�v
            if (Physics.Raycast(ray, out hit, 10.0f)) continue;

            attachFaces.Add(child); // �ʂ��i�[
        }

        //--- �\�[�g
        attachFaces.Sort((a, b) => {
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

    void EnlargeSizeCore()
    {
        m_attachFaces[m_SelectFaceNum].localScale = new Vector3(ENLARGE_SiZE, ENLARGE_SiZE, ENLARGE_SiZE);
    }

    void UndoSizeCore()
    {
        m_attachFaces[m_SelectFaceNum].localScale = new Vector3(ORIZIN_SiZE, ORIZIN_SiZE, ORIZIN_SiZE);
    }

    public void ChangeFaceX(float axis)
    {
        UndoSizeCore();
        Vector3 pos = m_attachFaces[m_SelectFaceNum].position;
        pos.x += axis;
        for(int i = 0; i < m_attachFaces.Count; i++)
        {
            //--- ���݂̖ʂƎ��̖ʂ�XY���W��Vector2�Ɋi�[
            Vector2 currentFacePos = new Vector2(m_attachFaces[i].position.x, m_attachFaces[i].position.y);
            Vector2 newxtFacePos = new Vector2(pos.x, pos.y);

            // XY���ʂł̋��������ꂷ���Ă�����X���[
            if (Vector2.Distance(currentFacePos, newxtFacePos) > 0.05f) continue;

            m_SelectFaceNum = i;
            EnlargeSizeCore();
            return;
        }

        m_rotateY += ROTATION * (int)axis;  // �p�x��ݒ�
        m_rotateFrameCnt = 1;	// �ŏ��̃J�E���g
        m_isDepath = true;
        //audioSource.PlayOneShot(m_RotSound);    //SE�̍Đ�
    }

    public void ChangeFaceY(float axis)
    {
        UndoSizeCore();
        Vector3 pos = m_attachFaces[m_SelectFaceNum].position;
        pos.y += axis;
        for (int i = 0; i < m_attachFaces.Count; i++)
        {
            //--- ���݂̖ʂƎ��̖ʂ�XY���W��Vector2�Ɋi�[
            Vector2 currentFacePos = new Vector2(m_attachFaces[i].position.x, m_attachFaces[i].position.y);
            Vector2 newxtFacePos = new Vector2(pos.x, pos.y);

            // XY���ʂł̋��������ꂷ���Ă�����X���[
            if (Vector2.Distance(currentFacePos, newxtFacePos) > 0.05f) continue;

            m_SelectFaceNum = i;
            EnlargeSizeCore();
            return;
        }

        m_rotateX -= ROTATION * (int)axis;  // �p�x��ݒ�
        m_rotateFrameCnt = 1;   // �ŏ��̃J�E���g
        m_isDepath = true;
        //audioSource.PlayOneShot(m_RotSound);    //SE�̍Đ�
    }

    //Core����]�����
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
            m_SelectFaceNum = 0;    // �I��ʂ̔ԍ������Z�b�g
            m_attachFaces[m_SelectFaceNum].localScale = new Vector3(1.25f, 1.25f, 1.25f);   // ���݂̖ʂ�����
        }
    }
}
