using UnityEngine;

public class CameraFollowTouch : MonoBehaviour
{
    public Transform player;
    public float sensitivity = 2.0f; // 控制相机旋转灵敏度

    private Vector2 rotation = Vector2.zero;
    public RectTransform uiAreaRect; // 引用UI区域的RectTransform

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // 检查触摸是否在UI区域内
                    if (IsTouchInUIArea(touch.position))
                    {
                        rotation = Vector2.zero;
                    }
                    break;

                case TouchPhase.Moved:
                    // 检查触摸是否在UI区域内
                    if (IsTouchInUIArea(touch.position))
                    {
                        // 获取触摸滑动的距离
                        Vector2 deltaPosition = touch.deltaPosition;

                        // 根据触摸滑动的距离来计算相机旋转
                        rotation.x += deltaPosition.x * sensitivity;
                        rotation.y -= deltaPosition.y * sensitivity;

                        // 使用 Mathf.Clamp 限制旋转的角度，防止相机超出范围
                        rotation.y = Mathf.Clamp(rotation.y, -180f, 180f);

                        // 将旋转应用到相机和玩家对象上
                        transform.localRotation = Quaternion.Euler(rotation.y, rotation.x, 0);
                        player.rotation = Quaternion.Euler(0, rotation.x, 0);
                    }
                    break;
            }
        }
    }

    // 检查触摸是否在UI区域内
    private bool IsTouchInUIArea(Vector2 touchPosition)
    {
        if (uiAreaRect == null)
        {
            return false; // 如果没有指定UI区域，则默认为false
        }

        // 检查触摸位置是否在UI区域内
        return RectTransformUtility.RectangleContainsScreenPoint(uiAreaRect, touchPosition);
    }
}
