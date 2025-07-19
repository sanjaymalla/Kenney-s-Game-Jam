using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image healthBar;
    public float healthAmount = 100f;

    public float baseDecayRate = 1f;
    public float moveDecayRate = 3f;
    public float jumpDecayRate = 5f;

    private float currentDecayRate;

    private Player player;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindAnyObjectByType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDecayRate();

        healthAmount -= currentDecayRate * Time.deltaTime;
        healthAmount = Mathf.Clamp(healthAmount, 0f, 100f);

        healthBar.fillAmount = healthAmount / 100f;

        if (healthAmount <= 0)
        {
            HandleDeathOrZeroPower();
        }
    }

    void UpdateDecayRate()
    {
        if (player == null) return;

        if (player.isJumping)
            currentDecayRate = jumpDecayRate;
        else if (player.isMoving)
            currentDecayRate = moveDecayRate;
        else
            currentDecayRate = baseDecayRate;
    }

    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / 100f;
    }

    public void Heal(float healingAmount)
    {
        healthAmount += healingAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);

        healthBar.fillAmount = healthAmount / 100f;
    }

    void HandleDeathOrZeroPower()
    {
        // You can disable movement, show Game Over screen, etc.
        Debug.Log("Power depleted. Player is dead or disabled.");
        // Example: Disable player controller
        // GetComponent<PlayerController>().enabled = false;
    }
}
