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

    /// <summary>
    /// ��]�����Ƃ��̃{�^��������������
    /// </summary>
    public enum RotateFlag
    {
        E_ROTATE_FLAG_NULL = 0,
        E_ROTATE_FLAG_R,
        E_ROTATE_FLAG_L,
        E_ROTATE_FLAG_U,
        E_ROTATE_FLAG_D,

        E_ROTATE_FLAG_Y_MAX
    }
    
    const float ROTATION = 90.0f;   // ��]�p�x
    const float DAMPING_RATE = 0.5f;   // ��]������
    const float ENLARGE_SiZE = 1.25f;   //�I�𒆂�Core�̑傫��
    const float ORIZIN_SiZE = 1.00f;   //�I���O��Core�̑傫��

    List<AttachFace> m_AttachFaces;	// �g�ݗ��Ă����
    List<Transform> hoge;
    int m_SelectFaceNum;     // �I��ʂ̔ԍ�
    int m_timeToRotate;             // ��]����
    float m_rotateY, m_rotateX;     // �p�x
    float m_lateY, m_lateX;         // �x���p�x
    public int m_rotateFrameCnt;    // ��]�t���[���̃J�E���g
    RotateFlag m_rotFlag;           //�ǂ����ɉ�]���Ă��邩
    bool m_isDepath;        // �ʏ����擾�������t���O
    public PlayerController_iwata PController;
    public GameManager GM;
    static int num = 0;
    Vector3 AxisRotX;
    Vector3 AxisRotY;
    [SerializeField] GameObject m_AttachJank;

    //[SerializeReference] AudioClip m_RotSound;  //�I�[�f�B�I�t�@�C���̏��
    //AudioSource audioSource;    //�Đ����邽�߂̃n���h��

    // Start is called before the first frame update
    void Start()
    {
        AxisRotY = this.transform.right;        //�c��]���邽�߂̎��o�^
        AxisRotX = this.transform.up;           //���J�X���邽�߂̎��o�^
        
        m_rotateY = m_rotateX = 0.0f;       //�p�x������
        m_lateY = m_lateX = 0.0f;       //�x���p�x������

        // ��]���Ԃ��v�Z
        m_timeToRotate = (int)(Mathf.Log(0.00001f) / Mathf.Log(1.0f - DAMPING_RATE));
        
        //�Č������K�v�Ȏ��ɗ��Ă�Flag��ݒ�
        m_isDepath = false;

        m_rotFlag = RotateFlag.E_ROTATE_FLAG_NULL;
        
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

        //if (m_isDepath)
        //{
        //    ResetAttachFace();
        //}
    }

    private void ResetAttachFace()
    {
        m_AttachFaces = GetAttachFace();    // ���̑g�ݗ��Ă���ʂ��擾

        if (m_SelectFaceNum > m_AttachFaces.Count - 1)
        {
            m_SelectFaceNum = 0;
        }

        // �I�𒆂̖ʂ�傫����������
        m_AttachFaces[m_SelectFaceNum].Trans.GetComponent<JankStatus>().PickupSize();

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

            if (child.gameObject == m_AttachJank) continue;

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

            //Debug.Log(TempFace.Trans.name);
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

    public void ResetCoreSize()
    {
        m_AttachFaces[m_SelectFaceNum].Trans.GetComponent<JankStatus>().UndoSize();
    }

    /// <summary>
    /// �ʏ�̃R�A�̉�]
    /// </summary>
    void RotateCore()
    {
        float lastY = m_lateY;
        float lastX = m_lateX;

        //--- �x������
        m_lateY = (m_rotateY - m_lateY) * DAMPING_RATE + m_lateY;
        m_lateX = (m_rotateX - m_lateX) * DAMPING_RATE + m_lateX;

        transform.RotateAround(transform.position, AxisRotX, m_lateY - lastY);
        transform.RotateAround(transform.position, AxisRotY, m_lateX - lastX);

        m_rotateFrameCnt++; // ��]�t���[���J�E���g

        //--- ��]�I�����̏���
        if (m_rotateFrameCnt > m_timeToRotate)
        {
            m_AttachFaces = GetAttachFace();    // ���̑g�ݗ��Ă���ʂ��擾
            m_rotateFrameCnt = 0;   // ��]�t���[�������Z�b�g

            float hogepos;      //��ɂȂ邽�߂̍��W
            int nextnum = 0;    //��]��̑I��ʂ̓Y�����������p

            //��]�̌����ɉ������������s��
            switch (m_rotFlag)
            {
                case RotateFlag.E_ROTATE_FLAG_R:        //�E�ɉ�]����
                    hogepos = m_AttachFaces[m_SelectFaceNum].Trans.position.y;      //���Y���̍��W���擾
                    for (int i = 0; i < m_AttachFaces.Count; i++)       //���̖ʂ����ׂČ�������
                    {
                        if (Mathf.Abs(m_AttachFaces[i].Trans.position.y - hogepos) > 0.05f) continue;       //���������ʂƊ��Y���W���r����
                        Debug.Log(m_AttachFaces[nextnum].Trans.name + ":" + m_AttachFaces[nextnum].Trans.position.x);
                        Debug.Log(m_AttachFaces[i].Trans.name + ":" + m_AttachFaces[i].Trans.position.x);
                        if (m_AttachFaces[nextnum].Trans.position.x > m_AttachFaces[i].Trans.position.x) nextnum = i;      //���̌��̖ʂ̍��W���E�Ɍ��������ʂ�����Ȃ����ς���
                    }
                    m_AttachJank.transform.Rotate(0.0f, -90.0f, 0.0f);
                    break;

                case RotateFlag.E_ROTATE_FLAG_L:        //���ɉ�]����
                    hogepos = m_AttachFaces[m_SelectFaceNum].Trans.position.y;      //���Y���̍��W���擾
                    for (int i = 0; i < m_AttachFaces.Count; i++)       //���̖ʂ����ׂČ�������
                    {
                        if (Mathf.Abs(m_AttachFaces[i].Trans.position.y - hogepos) > 0.05f) continue;       //���������ʂƊ��Y���W���r����
                        if (m_AttachFaces[nextnum].Trans.position.x < m_AttachFaces[i].Trans.position.x) nextnum = i;      //���̌��̖ʂ̍��W��荶�Ɍ��������ʂ�����Ȃ����ς���
                    }
                    m_AttachJank.transform.Rotate(0.0f, 90.0f, 0.0f);
                    break;

                case RotateFlag.E_ROTATE_FLAG_U:        //��ɉ�]����
                    hogepos = m_AttachFaces[m_SelectFaceNum].Trans.position.x; ;      //���X���̍��W���擾
                    for (int i = 0; i < m_AttachFaces.Count; i++)       //���̖ʂ����ׂČ�������
                    {
                        if (Mathf.Abs(m_AttachFaces[i].Trans.position.x - hogepos) > 0.05f) continue;       //���������ʂƊ��X���W���r����
                        if (m_AttachFaces[nextnum].Trans.position.y > m_AttachFaces[i].Trans.position.y) nextnum = i;      //���̌��̖ʂ̍��W��荶�Ɍ��������ʂ�����Ȃ����ς���
                    }
                    m_AttachJank.transform.Rotate(90.0f, 0.0f, 0.0f);
                    break;

                case RotateFlag.E_ROTATE_FLAG_D:
                    hogepos = m_AttachFaces[m_SelectFaceNum].Trans.position.x;
                    for (int i = 0; i < m_AttachFaces.Count; i++)
                    {
                        if (Mathf.Abs(m_AttachFaces[i].Trans.position.x - hogepos) > 0.05f) continue;
                        
                        if (m_AttachFaces[nextnum].Trans.position.y < m_AttachFaces[i].Trans.position.y) nextnum = i;
                    }
                    m_AttachJank.transform.Rotate(-90.0f, 0.0f, 0.0f);
                    break;
            }
            
            m_SelectFaceNum = nextnum;
            m_AttachJank.GetComponent<JankBase_iwata>().PutJank(m_AttachFaces[m_SelectFaceNum].Trans, this.transform);

            //---------------------

            m_rotFlag = RotateFlag.E_ROTATE_FLAG_NULL;
        }
    }

    /// <summary>
    /// ���͂̑΂��ẴR�A�̏���
    /// </summary>
    /// <param name="axisX">������</param>
    /// <param name="axisY">�c����</param>
    public void InputAxisCore(int axisX, int axisY)
    {
        if(GM.JointStage.GetComponent<JointStageManager>().JSStatus == JointStageManager.eJointStageStatus.E_JOINTSTAGE_STATUS_SELECT)
        {
            m_rotateY += ROTATION * axisX;  // �p�x��ݒ�
            m_rotateX += ROTATION * axisY;  // �p�x��ݒ�
            m_rotateFrameCnt = 1;	// �ŏ��̃J�E���g
        }
        else if(GM.JointStage.GetComponent<JointStageManager>().JSStatus == JointStageManager.eJointStageStatus.E_JOINTSTAGE_STATUS_PUT)
        {
            Vector3 pos = m_AttachFaces[m_SelectFaceNum].Trans.position;
            pos.x += axisX;
            pos.y += axisY;
            for (int i = 0; i < m_AttachFaces.Count; i++)
            {
                //--- ���݂̖ʂƎ��̖ʂ�XY���W��Vector2�Ɋi�[
                Vector2 currentFacePos = new Vector2(m_AttachFaces[i].Trans.position.x, m_AttachFaces[i].Trans.position.y);
                Vector2 newxtFacePos = new Vector2(pos.x, pos.y);

                // XY���ʂł̋��������ꂷ���Ă�����X���[
                if (Vector2.Distance(currentFacePos, newxtFacePos) > 0.05f) continue;
                
                m_SelectFaceNum = i;
                m_AttachJank.GetComponent<JankBase_iwata>().PutJank(m_AttachFaces[i].Trans, this.transform);
                return;
            }

            m_rotateY += ROTATION * (int)axisX;  // �p�x��ݒ�
            m_rotateX -= ROTATION * (int)axisY;  // �p�x��ݒ�
            m_rotateFrameCnt = 1;   // �ŏ��̃J�E���g
            if (axisX < 0)
            {
                m_rotFlag = RotateFlag.E_ROTATE_FLAG_L;
                Debug.Log("L");
            }
            else if(axisX > 0)
            {
                m_rotFlag = RotateFlag.E_ROTATE_FLAG_R;
                Debug.Log("R");
            }
            else if (axisY < 0)
            {
                m_rotFlag = RotateFlag.E_ROTATE_FLAG_D;
                Debug.Log("D");
            }
            else if(axisY > 0)
            {
                m_rotFlag = RotateFlag.E_ROTATE_FLAG_U;
                Debug.Log("U");
            }
            //m_isDepath = true;
        }
    }

    /// <summary>
    /// �J�[�\���őI�����ꂽ�K���N�^���R�A�ɔz�u����
    /// </summary>
    /// <param name="jank">�J�[�\���őI�����ꂽ�K���N�^�̏��</param>
    public void PutJank(GameObject jank)
    {
        m_AttachFaces = GetAttachFace();    // ���̑g�ݗ��Ă���ʂ��擾
        m_SelectFaceNum = 0;        //�I�����Ă���ꏊ������ɏ�����
        m_AttachJank = jank;        //���u������Ă���K���N�^��o�^
        m_AttachJank.GetComponent<JankBase_iwata>().SetJank(m_AttachFaces[m_SelectFaceNum].Trans);        //�K���N�^�̉��u���̏���
    }

    public void JointCore()
    {
        if(true)
        {
            m_AttachJank.GetComponent<JankBase_iwata>().Orizin.SetActive(false);
            m_AttachJank = null;
            m_AttachFaces.Clear();
            m_SelectFaceNum = 0;
        }
        else
        {

        }
    }

    public bool AttachCore(GameObject obj)
    {
        Debug.Log("a");
        if(m_AttachFaces[m_SelectFaceNum].isAttach)
        {//�g�ݗ��Ă鏈��
            Debug.Log("b");
            m_AttachFaces[m_SelectFaceNum].Trans.GetComponent<JankStatus>().UndoSize();
            Debug.Log("c");
            obj.GetComponent<JankBase_iwata>().JointJank(m_AttachFaces[m_SelectFaceNum].Trans);
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
        if(m_AttachFaces[m_SelectFaceNum].isRelease)
        {
            //�O������
            Debug.Log(m_AttachFaces[m_SelectFaceNum].Trans.name + "�O����");
            m_AttachFaces[m_SelectFaceNum].Trans.GetComponent<SpownJank_iwata>().RemoveCore();
            m_isDepath = true;
        }
        else
        {
            //�O���Ȃ������Ƃ��ɖ炷SE
            Debug.Log(m_AttachFaces[m_SelectFaceNum].Trans.name + "�O���Ȃ�");
        }
    }

    public void JointToRot()
    {
        m_AttachFaces[m_SelectFaceNum].Trans.GetComponent<JankStatus>().UndoSize();
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
        get { return m_AttachFaces[m_SelectFaceNum].Trans; }
    }
}
