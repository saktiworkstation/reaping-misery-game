using UnityEngine;
using UnityEngine.SceneManagement;

public class Pet : Mover
{
    [Header("Pet Stats")]
    public int healAmount = 5;           // Berapa poin HP yang dipulihkan per heal
    public float healCooldown = 10f;     // Waktu tunggu (detik) antar heal

    [Header("Follow Distance")]
    public float minFollowDistance = 1.5f;   // Jarak minimal sebelum pet mundur
    public float maxFollowDistance = 3f;     // Jarak maksimal sebelum pet mendekat
    [Header("Follow Offset")]
    public Vector2 followOffset = new Vector2(1.5f, 0f); // Offset posisi relatif ke player

    private Transform playerTransform;
    private float lastHealTime;
    private Vector3 initialScale;

    protected override void Start()
    {
        base.Start();

        playerTransform = Player.instance.transform;
        initialScale = transform.localScale;
        lastHealTime = Time.time;  // mulai hitung cooldown dari spawn
    }

    void Update()
    {
        MaintainScale();
        FollowPlayer();
        TryHealPlayer();
    }

    // Agar pet tidak ikut ter-flip saat player flip
    private void MaintainScale()
    {
        transform.localScale = initialScale;
    }

    // Logika mengikuti player dengan batas jarak
    private void FollowPlayer()
    {
        // Target world pos = player + offset
        Vector3 targetPos = playerTransform.position + (Vector3)followOffset;
        float dist = Vector3.Distance(transform.position, targetPos);

        if (dist > maxFollowDistance)
        {
            Vector3 dir = (targetPos - transform.position).normalized;
            UpdateMotor(dir);
        }
        else if (dist < minFollowDistance)
        {
            Vector3 dir = (transform.position - targetPos).normalized;
            UpdateMotor(dir);
        }
        else
        {
            // Diam jika sudah dalam zona toleransi
            UpdateMotor(Vector3.zero);
        }
    }

    // Logika heal dengan cooldown
    private void TryHealPlayer()
    {
        if (Time.time - lastHealTime < healCooldown)
            return;

        // Panggil method Heal pada Player
        Player.instance.Heal(healAmount);

        // Update timestamp terakhir heal
        lastHealTime = Time.time;
    }

    // Jika nanti ada death logic
    protected override void Death()
    {
        Destroy(gameObject);
    }

    // Untuk debugging range follow di SceneView
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, minFollowDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, maxFollowDistance);
    }
}
