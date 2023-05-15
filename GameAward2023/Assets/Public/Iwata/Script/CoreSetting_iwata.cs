using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreSetting_iwata : ObjectBase
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

        E_ROTATE_FLAG_MAX
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
    Vector3 AxisRotX;       //�R�A�̉�]�p��X��
    Vector3 AxisRotY;       //�R�A�̉�]�p��Y��
    bool m_CanAttach;

    [SerializeField] PlayerController_iwata PController;
    [SerializeField] GameManager GM;
    [SerializeField] GameObject m_AttachJank;
    [SerializeField] float m_BreakForce = 1500.0f;

    // Start is called before the first frame update
    void Start()
    {
        AxisRotX = this.transform.right;        //�c��]���邽�߂̎��o�^
        AxisRotY = this.transform.up;           //���J�X���邽�߂̎��o�^
        
        m_rotateY = m_rotateX = 0.0f;       //�p�x������
        m_lateY = m_lateX = 0.0f;       //�x���p�x������

        // ��]���Ԃ��v�Z
        m_timeToRotate = (int)(Mathf.Log(0.00001f) / Mathf.Log(1.0f - DAMPING_RATE));
        
        //�Č������K�v�Ȏ��ɗ��Ă�Flag��ݒ�
        m_isDepath = false;

        //��]�̃t���O��������
        m_rotFlag = RotateFlag.E_ROTATE_FLAG_NULL;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //--- ��]��
        if (m_rotateFrameCnt > 0)
        {
            RotateCore();   // ��]����			
            return;
        }
    }

    List<AttachFace> GetAttachFace()
    {
        // �ʂ̊i�[���p��
        List<AttachFace> attachFaces = new List<AttachFace>();
        AttachFace TempFace = new AttachFace();

        //--- �g�ݗ��Ă���ʂ����ԂɊi�[
        foreach (Transform child in this.transform)
        {
            // ���u�����Ă�����̂ƈꏏ�Ȃ�X�L�b�v����
            if (child.gameObject == m_AttachJank) continue;
            if (child.gameObject.name == "CoreCenter") continue;

            // ��O�ɐL�т郌�C��p��
            Ray ray = new Ray(child.position, Vector3.back);
            RaycastHit hit;

            // �g�ݗ��Ă��Ȃ��ʂ̓X���[
            // ��O�ɕ�����������X�L�b�v
            if (Physics.Raycast(ray, out hit, 10.0f))
            {
                if (hit.transform.gameObject != m_AttachJank)    //���������̂����u�����Ă���̈ȊO�Ȃ�
                {
                    continue;
                }
            }
            

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

        transform.RotateAround(transform.position, AxisRotY, m_lateY - lastY);
        transform.RotateAround(transform.position, AxisRotX, m_lateX - lastX);

        m_rotateFrameCnt++; // ��]�t���[���J�E���g

        //--- ��]�I�����̏���
        if (m_rotateFrameCnt > m_timeToRotate)
        {
            transform.rotation = Quaternion.Euler(new Vector3(Mathf.Round(this.transform.rotation.eulerAngles.x), Mathf.Round(this.transform.rotation.eulerAngles.y), Mathf.Round(this.transform.rotation.eulerAngles.z)));

            m_rotateFrameCnt = 0;   // ��]�t���[�������Z�b�g
            if (m_rotFlag == RotateFlag.E_ROTATE_FLAG_NULL) return;

            
            float hogepos = 0f;      //��ɂȂ邽�߂̍��W
            switch (m_rotFlag)
            {
                case RotateFlag.E_ROTATE_FLAG_R:        //�E�ɉ�]����
                    hogepos = m_AttachFaces[m_SelectFaceNum].Trans.position.y;      //���Y���̍��W���擾
                    break;

                case RotateFlag.E_ROTATE_FLAG_L:        //���ɉ�]����
                    hogepos = m_AttachFaces[m_SelectFaceNum].Trans.position.y;      //���Y���̍��W���擾
                    break;

                case RotateFlag.E_ROTATE_FLAG_U:        //��ɉ�]����
                    hogepos = m_AttachFaces[m_SelectFaceNum].Trans.position.x; ;      //���X���̍��W���擾
                    break;

                case RotateFlag.E_ROTATE_FLAG_D:
                    hogepos = m_AttachFaces[m_SelectFaceNum].Trans.position.x;
                    break;
            }
            
            m_AttachFaces = GetAttachFace();    // ���̑g�ݗ��Ă���ʂ��擾
            int nextnum = 0;    //��]��̑I��ʂ̓Y�����������p
            List<int> numList = new List<int>();
            
            //��]�̌����ɉ������������s��
            switch (m_rotFlag)
            {
                case RotateFlag.E_ROTATE_FLAG_R:        //�E�ɉ�]����
                    for (int i = 0; i < m_AttachFaces.Count; i++)
                    {
                        //Debug.Log(m_AttachFaces[i].Trans.name + ":" + Mathf.Abs(m_AttachFaces[i].Trans.position.y - hogepos));
                        if (Mathf.Abs(m_AttachFaces[i].Trans.position.y - hogepos) > 0.2f) continue;       //���������ʂƊ��Y���W���r����

                        numList.Add(i);
                    }
                    nextnum = numList[0];

                    foreach(int child in numList)
                    {
                        Debug.Log(m_AttachFaces[child].Trans.name);
                        if (m_AttachFaces[nextnum].Trans.position.x > m_AttachFaces[child].Trans.position.x) nextnum = child;      //���̌��̖ʂ̍��W���E�Ɍ��������ʂ�����Ȃ����ς���
                    }

                    m_AttachJank.transform.Rotate(0.0f, -90.0f, 0.0f);
                    break;

                case RotateFlag.E_ROTATE_FLAG_L:        //���ɉ�]����
                    
                    for (int i = 0; i < m_AttachFaces.Count; i++)
                    {
                        Debug.Log(m_AttachFaces[i].Trans.name + ":" + Mathf.Abs(m_AttachFaces[i].Trans.position.y - hogepos));
                        if (Mathf.Abs(m_AttachFaces[i].Trans.position.y - hogepos) > 0.2f) continue;       //���������ʂƊ��Y���W���r����

                        numList.Add(i);
                    }
                    nextnum = numList[0];

                    foreach (int child in numList)
                    {
                        Debug.Log(m_AttachFaces[child].Trans.name);
                        if (m_AttachFaces[nextnum].Trans.position.x < m_AttachFaces[child].Trans.position.x) nextnum = child;      //���̌��̖ʂ̍��W���E�Ɍ��������ʂ�����Ȃ����ς���
                    }
                    
                    m_AttachJank.transform.Rotate(0.0f, 90.0f, 0.0f);
                    break;

                case RotateFlag.E_ROTATE_FLAG_U:        //��ɉ�]����

                    for (int i = 0; i < m_AttachFaces.Count; i++)
                    {
                        Debug.Log(m_AttachFaces[i].Trans.name + ":" + Mathf.Abs(m_AttachFaces[i].Trans.position.x - hogepos));
                        if (Mathf.Abs(m_AttachFaces[i].Trans.position.x - hogepos) > 0.2f) continue;       //���������ʂƊ��Y���W���r����

                        numList.Add(i);
                    }
                    nextnum = numList[0];

                    foreach (int child in numList)
                    {
                        Debug.Log(m_AttachFaces[child].Trans.name);
                        if (m_AttachFaces[nextnum].Trans.position.y > m_AttachFaces[child].Trans.position.y) nextnum = child;      //���̌��̖ʂ̍��W���E�Ɍ��������ʂ�����Ȃ����ς���
                    }

                    m_AttachJank.transform.Rotate(90.0f, 0.0f, 0.0f);
                    break;

                case RotateFlag.E_ROTATE_FLAG_D:

                    for (int i = 0; i < m_AttachFaces.Count; i++)
                    {
                        Debug.Log(m_AttachFaces[i].Trans.name + ":" + Mathf.Abs(m_AttachFaces[i].Trans.position.x - hogepos));
                        if (Mathf.Abs(m_AttachFaces[i].Trans.position.x - hogepos) > 0.2f) continue;       //���������ʂƊ��Y���W���r����

                        numList.Add(i);
                    }
                    nextnum = numList[0];

                    foreach (int child in numList)
                    {
                        if (m_AttachFaces[nextnum].Trans.position.y < m_AttachFaces[child].Trans.position.y) nextnum = child;      //���̌��̖ʂ̍��W���E�Ɍ��������ʂ�����Ȃ����ς���
                    }

                    m_AttachJank.transform.Rotate(-90.0f, 0.0f, 0.0f);
                    break;
            }
            
            m_SelectFaceNum = nextnum;
            m_AttachJank.GetComponent<JankBase_iwata>().PutJank(m_AttachFaces[m_SelectFaceNum].Trans, this.transform);
            CheckCanAttach();
            
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
            m_rotateX -= ROTATION * axisY;  // �p�x��ݒ�
            m_rotateFrameCnt = 1;   // �ŏ��̃J�E���g
            AudioMane.PlaySE(AudioManager.SEKind.E_SE_KIND_PREV_KEYMOVE);
        }
        else if(GM.JointStage.GetComponent<JointStageManager>().JSStatus == JointStageManager.eJointStageStatus.E_JOINTSTAGE_STATUS_PUT)
        {
            this.transform.Rotate(0.0f, -10.0f, 0.0f, Space.World);      //�R�A�̌X�����ꎞ�I��0�C0�C0�ɖ߂�
            Vector3 pos = m_AttachFaces[m_SelectFaceNum].Trans.position;
            pos.x += axisX;
            pos.y += axisY;
            Vector2 newxtFacePos = new Vector2(pos.x, pos.y);
            for (int i = 0; i < m_AttachFaces.Count; i++)
            {
                //--- ���݂̖ʂƎ��̖ʂ�XY���W��Vector2�Ɋi�[
                Vector2 currentFacePos = new Vector2(m_AttachFaces[i].Trans.position.x, m_AttachFaces[i].Trans.position.y);

                // XY���ʂł̋��������ꂷ���Ă�����X���[
                if (Vector2.Distance(currentFacePos, newxtFacePos) > 0.05f) continue;
                
                this.transform.Rotate(0.0f, 10.0f, 0.0f, Space.World);      //�R�A�̌X�������Ƃɖ߂�

                m_SelectFaceNum = i;
                m_AttachJank.GetComponent<JankBase_iwata>().PutJank(m_AttachFaces[i].Trans, this.transform);
                CheckCanAttach();
                
                return;
            }
            
            this.transform.Rotate(0.0f, 10.0f, 0.0f, Space.World);      //�R�A�̌X�������Ƃɖ߂�

            m_rotateY += ROTATION * (int)axisX;  // �p�x��ݒ�
            m_rotateX -= ROTATION * (int)axisY;  // �p�x��ݒ�
            m_rotateFrameCnt = 1;   // �ŏ��̃J�E���g
            if (axisX < 0)
            {
                m_rotFlag = RotateFlag.E_ROTATE_FLAG_L;
            }
            else if(axisX > 0)
            {
                m_rotFlag = RotateFlag.E_ROTATE_FLAG_R;
            }
            else if (axisY < 0)
            {
                m_rotFlag = RotateFlag.E_ROTATE_FLAG_D;
            }
            else if(axisY > 0)
            {
                m_rotFlag = RotateFlag.E_ROTATE_FLAG_U;
            }
        }
    }

    /// <summary>
    /// �g�ݗ��Ă邱�Ƃ��ł��邩���肷��
    /// </summary>
    public void CheckCanAttach()
    {
        if(m_AttachFaces[m_SelectFaceNum].Trans.GetComponent<JankStatus>().CanColliderFlags(this.transform) && m_AttachJank.GetComponent<JankStatus>().CanCollisionFlags(this.transform))
        {
            //Debug.Log("�ł����[");
            m_CanAttach = true;
        }
        else
        {
            //Debug.Log("�ނ肾��[");
            m_CanAttach = false;
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
        CheckCanAttach();
    }

    /// <summary>
    /// ���u�����Ă���I�u�W�F�N�g���m�肳����
    /// </summary>
    /// <returns></returns>
    public bool JointCore()
    {
        if(m_CanAttach)
        {
            m_AttachJank.GetComponent<JankBase_iwata>().Orizin.SetActive(false);
            FixedJoint comp = m_AttachJank.AddComponent<FixedJoint>();
            comp.connectedBody = m_AttachFaces[m_SelectFaceNum].Trans.GetComponent<Rigidbody>();
            comp.breakForce = m_BreakForce;
            m_AttachFaces[m_SelectFaceNum].Trans.GetComponent<JankStatus>().ConnectedChild = m_AttachJank;
            m_AttachJank = null;
            m_AttachFaces.Clear();
            m_SelectFaceNum = 0;
            Debug.Log("������");
            AudioMane.PlaySE(AudioManager.SEKind.E_SE_KIND_ASSEMBLE);
            return true;
        }
        else
        {
            Debug.Log("�ނ���Ă����Ă񂾂�");
            return false;
        }
    }

    public void CanselCore()
    {
        Destroy(m_AttachJank);
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
        this.transform.rotation = Quaternion.identity;
    }

    public GameObject AttachJank
    {
        get { return m_AttachJank; }
    }

    public Transform SelectFace
    {
        get { return m_AttachFaces[m_SelectFaceNum].Trans; }
    }

}
