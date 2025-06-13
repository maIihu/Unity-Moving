using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float maxDistance = 30f;

    private Vector3 _startPos;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _startPos = transform.position;
        _rb.velocity = Vector2.left * speed; 
    }

    private void Update()
    {
        if (Vector3.Distance(_startPos, transform.position) > maxDistance)
        {
            ReturnToPool();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ContainString.BorderTag) || other.CompareTag(ContainString.PlayerTag)) 
        {
            ReturnToPool();
        }
    }

    private void ReturnToPool()
    {
        _rb.velocity = Vector3.zero;
        gameObject.SetActive(false);

        Level6 level6 = FindObjectOfType<Level6>();
        if (level6 != null)
        {
            level6.ReturnArrowToPool(gameObject);
        }
    }
}