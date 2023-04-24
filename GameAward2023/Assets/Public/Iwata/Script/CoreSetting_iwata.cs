using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreSetting_iwata : MonoBehaviour
{
    public struct AttachFace
    {
        public Transform Trans;
        public bool isAttach;
        public bool isRelease;
    };

    public enum RotateFlag
    {
        E_ROTATE_FLAG_NULL = 0,
        E_ROTATE_FLAG_X_P,
        E_ROTATE_FLAG_X_M,
        E_ROTATE_FLAG_Y_P,
        E_ROTATE_FLAG_Y_M,

        E_ROTATE_FLAG_Y_MAX
    }
    
    const float ROTATION = 90.0f;   // ��]�p�x
    const float DAMPING_RATE = 0.5f;   // ��]������
    const float ENLARGE_SiZE = 1.25f;   //�I�𒆂�Core�̑傫��
    const float ORIZIN_SiZE = 1.00f;   //�I���O��Core�̑傫��

    List<AttachFace> m_attachFaces;	// �g�ݗ��Ă����
    List<Transform> hoge;
    int m_SelectFaceNum;     // �I��ʂ̔ԍ�
    int m_timeToRotate;             // ��]����
    float m_rotateY, m_rotateX;     // �p�x
    float m_lateY, m_lateX;         // �x���p�x
    public int m_rotateFrameCnt;    // ��]�t���[���̃J�E���g
    RotateFlag m_rotFlag;           //�ǂ����ɉ�]���Ă��邩
    bool m_isDepath;        // �ʏ����擾�������t���O
    public PlayerController_iwata PController;
    public GameManager GMManager;
    Vector3 RotVectorX;
    static int num = 0;

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
        //EnlargeSizeCore();
        m_attachFaces[m_SelectFaceNum].Trans.GetComponent<JankStatus>().PickupSize();


        //�Č������K�v�Ȏ��ɗ��Ă�Flag��ݒ�
        m_isDepath = false;

        m_rotFlag = RotateFlag.E_ROTATE_FLAG_NULL;

        RotVectorX = this.transform.up;

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
            ResetAttachFace();
        }
    }

    private void ResetAttachFace()
    {
        m_attachFaces = GetAttachFace();    // ���̑g�ݗ��Ă���ʂ��擾

        if (m_SelectFaceNum > m_attachFaces.Count - 1)
        {
            m_SelectFaceNum = 0;
        }

        // �I�𒆂̖ʂ�傫����������
        m_attachFaces[m_SelectFaceNum].Trans.GetComponent<JankStatus>().PickupSize();

        m_isDepath = false;
    }

    List<AttachFace> GetAttachFace()
    {
        // �ʂ̊i�[���p��
        List<AttachFace> attachFaces = new List<AttachFace>();
        AttachFace TempFace = new AttachFace();

        //--- �g�ݗ��Ă���ʂ����ԂɊi�[
        foreach (Transform child in this.transform)
        {
            //if (child.tag != "Player") continue;

            // ��O�ɐL�т郌�C��p��
            Ray ray = new Ray(child.position, Vector3.back);
            RaycastHit hit;

            // �g�ݗ��Ă��Ȃ��ʂ̓X���[
            // ��O�ɕ�����������X�L�b�v
            if (Physics.Raycast(ray, out hit, 10.0f)) continue;

            //Transform�̏��o�^
            TempFace.Trans = child;

            //�A�^�b�`�ł���ʂ��𔻒f����t���O���i�[
            if(!child.GetComponent<IsAttachFace_iwata>())
            {
                TempFace.isAttach = true;
            }
            else
            {
                TempFace.isAttach = child.GetComponent<IsAttachFace_iwata>().CanAttach(Vector3.back);
            }

            //���O����ʂ��𔻒f����t���O���i�[
            if(child.name.Contains("Core_Child"))
            {
                TempFace.isRelease = false;
            }
            else
            {
                TempFace.isRelease = true;
            }

            attachFaces.Add(TempFace); // �ʂ��i�[
        }

        //--- �\�[�g
        attachFaces.Sort((a, b) => {
            if (Mathf.Abs(a.Trans.position.y - b.Trans.position.y) > 0.75f)
            {
                // Y���W���قȂ�ꍇ��Y���W�Ŕ�r����
                return b.Trans.position.y.CompareTo(a.Trans.position.y);
            }
            else
            {
                // Y���W�������ꍇ��X���W�Ŕ�r����
                return a.Trans.position.x.CompareTo(b.Trans.position.x);
            }
        });

        return attachFaces;
    }

    //void EnlargeSizeCore()
    //{
    //    m_attachFaces[m_SelectFaceNum].Trans.localScale *= ENLARGE_SiZE;
    //}

    //void UndoSizeCore()
    //{
    //    m_attachFaces[m_SelectFaceNum].Trans.localScale = m_attachFaces[m_SelectFaceNum].Trans.GetComponent<JankStatus>().OrizinSize;
    //}

    public void ResetCoreSize()
    {
        Debug.Log(m_attachFaces[m_SelectFaceNum].Trans.name);
        m_attachFaces[m_SelectFaceNum].Trans.GetComponent<JankStatus>().UndoSize();
    }

    public void ChangeFaceX(float axis)
    {
        this.transform.Rotate(-10.0f, 0.0f, 0.0f, Space.World);
        m_attachFaces[m_SelectFaceNum].Trans.GetComponent<JankStatus>().UndoSize();
        Vector3 pos = m_attachFaces[m_SelectFaceNum].Trans.position;
        pos.x += axis;
        for(int i = 0; i < m_attachFaces.Count; i++)
        {
            //--- ���݂̖ʂƎ��̖ʂ�XY���W��Vector2�Ɋi�[
            Vector2 currentFacePos = new Vector2(m_attachFaces[i].Trans.position.x, m_attachFaces[i].Trans.position.y);
            Vector2 newxtFacePos = new Vector2(pos.x, pos.y);

            //Debug.Log(m_attachFaces[m_SelectFaceNum].Trans.name + ":" + m_attachFaces[i].Trans.name + " = " + Vector2.Distance(currentFacePos, newxtFacePos) + "(" + (bool)!(Vector2.Distance(currentFacePos, newxtFacePos) > 0.05f) + ")");

            // XY���ʂł̋��������ꂷ���Ă�����X���[
            if (Vector2.Distance(currentFacePos, newxtFacePos) > 0.05f) continue;

            m_SelectFaceNum = i;
            m_attachFaces[m_SelectFaceNum].Trans.GetComponent<JankStatus>().PickupSize();
            this.transform.Rotate(10.0f, 0.0f, 0.0f, Space.World);
            return;
        }

        m_attachFaces[m_SelectFaceNum].Trans.GetComponent<JankStatus>().UndoSize();
        m_rotateY += ROTATION * (int)axis;  // �p�x��ݒ�
        m_rotateFrameCnt = 1;	// �ŏ��̃J�E���g
        if(axis < 0)
        {
            m_rotFlag = RotateFlag.E_ROTATE_FLAG_X_M;
            Debug.Log("XM");
        }
        else
        {
            m_rotFlag = RotateFlag.E_ROTATE_FLAG_X_P;
            Debug.Log("XP");
        }
        m_isDepath = true;
        this.transform.Rotate(10.0f, 0.0f, 0.0f, Space.World);
        //audioSource.PlayOneShot(m_RotSound);    //SE�̍Đ�
    }

    public void ChangeFaceY(float axis)
    {
        this.transform.Rotate(-10.0f, 0.0f, 0.0f, Space.World);
        m_attachFaces[m_SelectFaceNum].Trans.GetComponent<JankStatus>().UndoSize();
        Vector3 pos = m_attachFaces[m_SelectFaceNum].Trans.position;
        pos.y += axis;
        for (int i = 0; i < m_attachFaces.Count; i++)
        {
            //--- ���݂̖ʂƎ��̖ʂ�XY���W��Vector2�Ɋi�[
            Vector2 currentFacePos = new Vector2(m_attachFaces[i].Trans.position.x, m_attachFaces[i].Trans.position.y);
            Vector2 newxtFacePos = new Vector2(pos.x, pos.y);

            // XY���ʂł̋��������ꂷ���Ă�����X���[
            if (Vector2.Distance(currentFacePos, newxtFacePos) > 0.05f) continue;

            m_attachFaces[m_SelectFaceNum].Trans.GetComponent<JankStatus>().UndoSize();
            m_SelectFaceNum = i;
            m_attachFaces[m_SelectFaceNum].Trans.GetComponent<JankStatus>().PickupSize();
            this.transform.Rotate(10.0f, 0.0f, 0.0f, Space.World);
            return;
        }

        m_rotateX -= ROTATION * (int)axis;  // �p�x��ݒ�
        m_rotateFrameCnt = 1;   // �ŏ��̃J�E���g
        if (axis < 0)
        {
            m_rotFlag = RotateFlag.E_ROTATE_FLAG_Y_M;
            Debug.Log("YM");
        }
        else
        {
            m_rotFlag = RotateFlag.E_ROTATE_FLAG_Y_P;
            Debug.Log("YP");
        }
        m_isDepath = true;
        this.transform.Rotate(10.0f, 0.0f, 0.0f, Space.World);
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
        this.transform.Rotate(RotVectorX, m_lateY - lastY, Space.World);
        this.transform.Rotate(Vector3.right, m_lateX - lastX, Space.World);

        m_rotateFrameCnt++; // ��]�t���[���J�E���g

        //--- ��]�I�����̏���
        if (m_rotateFrameCnt > m_timeToRotate)
        {
            //Transform RotStack = m_attachFaces[m_SelectFaceNum].Trans;

            m_attachFaces = GetAttachFace();    // ���̑g�ݗ��Ă���ʂ��擾
            m_rotateFrameCnt = 0;   // ��]�t���[�������Z�b�g

            float hogepos;
            int nextnum = 0;
            List<int> selectnum = new List<int>();

            switch (m_rotFlag)
            {
                case RotateFlag.E_ROTATE_FLAG_X_P:
                    hogepos = m_attachFaces[m_SelectFaceNum].Trans.position.y;
                    Debug.Log("target:" + hogepos);
                    for (int i = 0; i < m_attachFaces.Count; i++)
                    {
                        Debug.Log(m_attachFaces[i].Trans.name + ":" + Mathf.Abs(m_attachFaces[i].Trans.position.y - hogepos));

                        //if (m_attachFaces[i].Trans.position.y != hogepos) continue;

                        if (Mathf.Abs(m_attachFaces[i].Trans.position.y - hogepos) > 0.05f) continue;

                        selectnum.Add(i);

                        if (m_attachFaces[nextnum].Trans.position.x >= m_attachFaces[i].Trans.position.x) nextnum = i;
                    }
                    foreach (int child in selectnum)
                    {
                        Debug.Log(child);
                    }
                    break;

                case RotateFlag.E_ROTATE_FLAG_X_M:
                    hogepos = m_attachFaces[m_SelectFaceNum].Trans.position.y;
                    for (int i = 0; i < m_attachFaces.Count; i++)
                    {
                        //if (m_attachFaces[i].Trans.position.y != hogepos) continue;
                        if (Mathf.Abs(m_attachFaces[i].Trans.position.y - hogepos) > 0.05f) continue;

                        selectnum.Add(i);

                        if (m_attachFaces[nextnum].Trans.position.x <= m_attachFaces[i].Trans.position.x) nextnum = i;
                    }
                    foreach (int child in selectnum)
                    {
                        Debug.Log(child);
                    }
                    break;

                case RotateFlag.E_ROTATE_FLAG_Y_P:
                    hogepos = m_attachFaces[m_SelectFaceNum].Trans.position.x;
                    for (int i = 0; i < m_attachFaces.Count; i++)
                    {
                        Debug.Log(m_attachFaces[i].Trans.name + ":" + Mathf.Abs(m_attachFaces[i].Trans.position.x - hogepos));


                        //if (m_attachFaces[i].Trans.position.x != hogepos) continue;
                        if (Mathf.Abs(m_attachFaces[i].Trans.position.x - hogepos) > 0.05f) continue;

                        selectnum.Add(i);

                        if (m_attachFaces[nextnum].Trans.position.y >= m_attachFaces[i].Trans.position.y) nextnum = i;
                    }
                    foreach (int child in selectnum)
                    {
                        Debug.Log(child);
                    }
                    break;

                case RotateFlag.E_ROTATE_FLAG_Y_M:
                    hogepos = m_attachFaces[m_SelectFaceNum].Trans.position.x;
                    for (int i = 0; i < m_attachFaces.Count; i++)
                    {
                        //if (m_attachFaces[i].Trans.position.x != hogepos) continue;
                        if (Mathf.Abs(m_attachFaces[i].Trans.position.x - hogepos) > 0.05f) continue;

                        selectnum.Add(i);

                        if (m_attachFaces[nextnum].Trans.position.y <= m_attachFaces[i].Trans.position.y) nextnum = i;
                    }
                    foreach (int child in selectnum)
                    {
                        Debug.Log(child);
                    }
                    break;
            }


            m_SelectFaceNum = nextnum;

            //---------------------

            m_rotFlag = RotateFlag.E_ROTATE_FLAG_NULL;
            m_attachFaces[m_SelectFaceNum].Trans.GetComponent<JankStatus>().PickupSize();   // ���݂̖ʂ�����
        }
    }

    public bool AttachCore(GameObject obj)
    {
        Debug.Log("a");
        if(m_attachFaces[m_SelectFaceNum].isAttach)
        {//�g�ݗ��Ă鏈��
            Debug.Log("b");
            m_attachFaces[m_SelectFaceNum].Trans.GetComponent<JankStatus>().UndoSize();
            Debug.Log("c");
            obj.GetComponent<JankBase>().JointJank(m_attachFaces[m_SelectFaceNum].Trans);
            Debug.Log("d");
            m_isDepath = true;
            return true;
        }
        else
        {//�g�ݗ��Ă��Ȃ������Ƃ��ɖ炷SE
            Debug.Log("�g�߂Ȃ���I");
            return false;
        }
    }

    public void ReleaseCore()
    {
        if(m_attachFaces[m_SelectFaceNum].isRelease)
        {
            //�O������
            Debug.Log(m_attachFaces[m_SelectFaceNum].Trans.name + "�O����");
            m_attachFaces[m_SelectFaceNum].Trans.GetComponent<SpownJank_iwata>().RemoveCore();
            m_isDepath = true;
        }
        else
        {
            //�O���Ȃ������Ƃ��ɖ炷SE
            Debug.Log(m_attachFaces[m_SelectFaceNum].Trans.name + "�O���Ȃ�");
        }
    }

    public void JointToRot()
    {
        m_attachFaces[m_SelectFaceNum].Trans.GetComponent<JankStatus>().UndoSize();
        Debug.Log(num);
        num++;
        GameObject clone = Instantiate(this.gameObject, new Vector3(-9.0f, 1.5f, -9.0f), Quaternion.identity);
        clone.AddComponent<RotationCore>();
        clone.AddComponent<CloneCoreMove>();
    }

    public void RotToJoint()
    {
        m_isDepath = true;
        this.transform.position = new Vector3(-4.0f, 11.0f, -38.0f);
        this.transform.rotation = Quaternion.identity;
        foreach (Transform child in this.transform)
        {
            child.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    public void RotToPlay()
    {
        foreach (Transform child in this.transform)
        {
            child.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezeAll;
        }
    }

    public void PlayToJoint()
    {
        m_isDepath = true;
        this.transform.position = new Vector3(-4.0f, 11.0f, -38.0f);
        this.transform.rotation = Quaternion.identity;
        foreach (Transform child in this.transform)
        {
            child.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    public void PlayToRot()
    {
        Debug.Log("��]������");
        this.transform.rotation = Quaternion.identity;
    }

    public Transform SelectFace
    {
        get { return m_attachFaces[m_SelectFaceNum].Trans; }
    }
}
