using UnityEngine;

public class MovingEffect : MonoBehaviour
{
    public float speed = 1f;
    public float resetPosition = -20f;
    public float startingPosition = 20f;

    private Vector3 currentPosition;

    private void Start()
    {
        // �G�t�F�N�g�̏����ʒu��ݒ肷��
        currentPosition = transform.position;
        currentPosition.x = startingPosition;
        transform.position = currentPosition;
    }

    private void Update()
    {
        // �G�t�F�N�g�����ɓ�����
        currentPosition.x -= speed * Time.deltaTime;
        transform.position = currentPosition;

        // ��ʊO�ɏo����A�E�[�ɖ߂�
        if (transform.position.x < resetPosition)
        {
            currentPosition.x = startingPosition;
            transform.position = currentPosition;
        }
    }
}
