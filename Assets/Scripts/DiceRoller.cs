using UnityEngine;

public class DiceRoller : MonoBehaviour
{
    [SerializeField] private GameObject _dicePrefab;
    [SerializeField] private float _rollForce = 5f;
    [SerializeField] private float _rollTorque = 5f;

    public void RollDice()
    {
        // Instantiate a new dice object
        GameObject dice = Instantiate(_dicePrefab, transform.position, Quaternion.identity);

        // Add Random Launch force
        Rigidbody rb = dice.GetComponent<Rigidbody>();
        Vector3 direction = new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(0.5f, 1f)).normalized;

        rb.AddForce(direction * _rollForce, ForceMode.Impulse);

        // Add Random Torque
        Vector3 torque = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        rb.AddTorque(torque * _rollTorque, ForceMode.Impulse);

        Debug.DrawRay(transform.position, direction * _rollForce, Color.red, 2f);
    }
}
