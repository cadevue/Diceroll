using UnityEngine;

public enum DiceSide { One = 1, Two, Three, Four, Five, Six }
public enum DiceSideDirection { PlusX, MinusX, PlusY, MinusY, PlusZ, MinusZ }

[System.Serializable]
public class DiceFaceDirection
{
    public DiceSide Side;
    public DiceSideDirection Direction;
}

public class Dice : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private bool _isRolling = true;

    [SerializeField] private DiceFaceDirection[] _diceFaceDirections;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (_isRolling && _rigidbody.IsSleeping())
        {
            _isRolling = false;
            LogDiceResult();
        }
    }

    private Vector3 GetDirection(DiceSideDirection direction)
    {
        switch (direction)
        {
            case DiceSideDirection.PlusX: return transform.right;
            case DiceSideDirection.MinusX: return -transform.right;
            case DiceSideDirection.PlusY: return transform.up;
            case DiceSideDirection.MinusY: return -transform.up;
            case DiceSideDirection.PlusZ: return transform.forward;
            case DiceSideDirection.MinusZ: return -transform.forward;
            default: return Vector3.zero;
        }
    }

    private void LogDiceResult()
    {
        float maxDot = float.MinValue;
        DiceSide result = DiceSide.One;

        foreach (var face in _diceFaceDirections)
        {
            float dot = Vector3.Dot(Vector3.up, GetDirection(face.Direction));
            if (dot > maxDot)
            {
                maxDot = dot;
                result = face.Side;
            }
        }

        Debug.Log($"Dice result: {result}");
    }
}
