using DG.Tweening;
using Preliy.Flange;
using UnityEngine;

public class RoboticDiceRoller : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private TargetFollower _target;

    [Header("Checkpoints")]
    [SerializeField] private Transform _start;
    [SerializeField] private Transform _end;

    [Header("Tween")]
    [SerializeField] private float _duration = 1f;
    [SerializeField] private Ease _ease = Ease.Linear;

    [Header("Dice")]
    [SerializeField] private Transform _diceInArm;
    [SerializeField] private GameObject _dicePrefab;
    [SerializeField] private float _rollForce = 5f;
    [SerializeField] private float _rollTorque = 5f;


    public void RollDice()
    {
        Sequence sequence = DOTween.Sequence();

        // Move the robotic arm to the start position
        sequence.Append(_target.transform.DOMove(_start.position, _duration).SetEase(_ease));

        // Display the dice in the robotic arm
        sequence.AppendCallback(() => _diceInArm.gameObject.SetActive(true));
        sequence.AppendInterval(0.25f);

        // Move the robotic arm to the end position
        sequence.Append(_target.transform.DOMove(_end.position, _duration).SetEase(_ease));

        // Disable the dice in the robotic arm
        sequence.AppendInterval(0.25f);
        sequence.AppendCallback(() => _diceInArm.gameObject.SetActive(false));

        // Launch the dice
        sequence.AppendCallback(() =>
        {
            GameObject dice = Instantiate(_dicePrefab, _diceInArm.position, _diceInArm.rotation);

            // Add Random Launch force
            Rigidbody rb = dice.GetComponent<Rigidbody>();
            Vector3 direction = dice.transform.right;

            rb.AddForce(direction * _rollForce, ForceMode.Impulse);

            // Add Random Torque
            Vector3 torque = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            rb.AddTorque(torque * _rollTorque, ForceMode.Impulse);
        });
    }
}
