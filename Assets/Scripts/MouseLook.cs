using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float _sensitivity = 5f;
    [SerializeField] private float _smoothing = 2f;
    [SerializeField] private float _maxRotationY = 80f;
    [SerializeField] private float _minRotationY = -80f;
    [SerializeField] private Transform _characterBody;

    private Vector2 _mouseLook;
    private Vector2 _smoothV;

    private void OnValidate()
    {
        if (_maxRotationY < _minRotationY)
        {
            float tmp = _maxRotationY;
            _maxRotationY = _minRotationY;
            _minRotationY = tmp;
        }
    }

    private void Update()
    {
        Vector2 mouseDirection = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        mouseDirection = Vector2.Scale(mouseDirection, Vector2.one * (_sensitivity * _smoothing));

        _smoothV.x = Mathf.Lerp(_smoothV.x, mouseDirection.x, 1f / _smoothing);
        _smoothV.y = Mathf.Lerp(_smoothV.y, mouseDirection.y, 1f / _smoothing);
        
        _mouseLook += _smoothV;
        _mouseLook.y = Mathf.Clamp(_mouseLook.y, _minRotationY, _maxRotationY);

        transform.localRotation = Quaternion.AngleAxis(-_mouseLook.y, Vector3.right);
        _characterBody.transform.localRotation = Quaternion.AngleAxis(_mouseLook.x, _characterBody.transform.up);
    }
}
