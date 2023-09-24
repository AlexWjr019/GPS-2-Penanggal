using UnityEngine;

public class CameraFollowTouch : MonoBehaviour
{
    public Transform player;
    public float sensitivity = 2.0f; // ���������ת������

    private Vector2 rotation = Vector2.zero;
    public RectTransform uiAreaRect; // ����UI�����RectTransform

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // ��鴥���Ƿ���UI������
                    if (IsTouchInUIArea(touch.position))
                    {
                        rotation = Vector2.zero;
                    }
                    break;

                case TouchPhase.Moved:
                    // ��鴥���Ƿ���UI������
                    if (IsTouchInUIArea(touch.position))
                    {
                        // ��ȡ���������ľ���
                        Vector2 deltaPosition = touch.deltaPosition;

                        // ���ݴ��������ľ��������������ת
                        rotation.x += deltaPosition.x * sensitivity;
                        rotation.y -= deltaPosition.y * sensitivity;

                        // ʹ�� Mathf.Clamp ������ת�ĽǶȣ���ֹ���������Χ
                        rotation.y = Mathf.Clamp(rotation.y, -180f, 180f);

                        // ����תӦ�õ��������Ҷ�����
                        transform.localRotation = Quaternion.Euler(rotation.y, rotation.x, 0);
                        player.rotation = Quaternion.Euler(0, rotation.x, 0);
                    }
                    break;
            }
        }
    }

    // ��鴥���Ƿ���UI������
    private bool IsTouchInUIArea(Vector2 touchPosition)
    {
        if (uiAreaRect == null)
        {
            return false; // ���û��ָ��UI������Ĭ��Ϊfalse
        }

        // ��鴥��λ���Ƿ���UI������
        return RectTransformUtility.RectangleContainsScreenPoint(uiAreaRect, touchPosition);
    }
}
