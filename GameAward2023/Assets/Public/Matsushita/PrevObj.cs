using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrevObj : MonoBehaviour
{
    private GameObject clonedObject;
    private GameObject originalObject;
    private Quaternion rotation;

    [SerializeField] GameObject target;
    private CameraSwitch cameraSwitchScript;
    private bool onCamera;

    void Start()
    {
        cameraSwitchScript = GetComponent<CameraSwitch>();
        target = GameObject.Find("core");
    }

    void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Clicked on " + hit.collider.gameObject.name);

                // �������̃I�u�W�F�N�g���L��
                GameObject originalObject = hit.collider.gameObject;

                // �����ȑO�ɕ��������I�u�W�F�N�g������΁A�قȂ�ꍇ�͍폜����
                if (clonedObject != null)
                {
                    Destroy(clonedObject);
                    clonedObject = null;
                }

                // ������̍��W���v�Z
                Vector3 newPosition = target.transform.position;
                newPosition.z -= 3.0f;

                // �I�u�W�F�N�g�𕡐�
                clonedObject = Instantiate(originalObject, newPosition, originalObject.transform.rotation);

                // ���������I�u�W�F�N�g�ɖ��O��t����
                clonedObject.name = originalObject.name + "_clone";
            }
        }

        // �������ꂽ�I�u�W�F�N�g�����݂���ꍇ�ɉ�]��K�p����
        if (clonedObject != null)
        {
            if (Input.GetKeyDown(KeyCode.E)) // E�L�[�������ꂽ�ꍇ
            {
                // ��]��K�p
                clonedObject.transform.RotateAround(clonedObject.transform.position, Vector3.up, 90f);

            }
            else if (Input.GetKeyDown(KeyCode.Q)) // Q�L�[�������ꂽ�ꍇ
            {
                // ��]��K�p
                clonedObject.transform.RotateAround(clonedObject.transform.position, Vector3.up, -90f);

            }
        }
    }
}
