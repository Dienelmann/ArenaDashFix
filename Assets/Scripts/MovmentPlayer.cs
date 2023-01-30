using System.Collections;
using UnityEngine;

public class MovmentPlayer : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] float DashTime = 1f;
    [SerializeField, Range(0f, 100f)] float dashSpeed = 1f;
    [SerializeField, Range(0f, 100f)] float speed = 5f;
    [SerializeField, Range(0f, 100f)] private float thrust = 10f;
    private Rigidbody2D rb;
    private float horizontal, vertical;
    public string AxisNameH, AxisNameV;
    public LifePoints TakeDamage;
    private int startDir;
    

    [SerializeField] bool _canDash = true, _isStuned = false, _isStunedLast;
    
    private Vector2 beschleunigung, desiredVelocity;
    [SerializeField, Range(0f,10f)] private float maxspeedchange = 1f;
    private Coroutine _currentStunTimer;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (gameObject.CompareTag("Player1"))
        {
            rb.position = new Vector2(12.5f, -2.5f);
        }

        if (gameObject.CompareTag("Player2"))
        {
            rb.position = new Vector2(-12.5f, -2.5f);
        }
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw(AxisNameH);
        vertical = Input.GetAxisRaw(AxisNameV);
        beschleunigung = new Vector2(horizontal, vertical).normalized * speed;

        if (_canDash && Input.GetKeyDown(KeyCode.Space) && gameObject.CompareTag("Player2"))
        {
            StartCoroutine(Dassh(beschleunigung));
        }
        if (_canDash && Input.GetKeyDown(KeyCode.Keypad0) && gameObject.CompareTag("Player1"))
        {
            StartCoroutine(Dassh(beschleunigung));
        }
    }

    private void FixedUpdate()
    {
        if(_isStuned) { rb.velocity *= 0.98f; return;}
        if (!_canDash)
        {
            rb.velocity = new Vector2(Mathf.MoveTowards(rb.velocity.x, desiredVelocity.x, maxspeedchange),
                        Mathf.MoveTowards(rb.velocity.y, desiredVelocity.y, maxspeedchange));
        }
        else
        {
            rb.velocity = beschleunigung;
        }

        StunIndicator();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.CompareTag("DeadZone") && !_isStuned || col.gameObject.CompareTag("DeadZone") && _isStuned)
        {
            ResetPlayer();
        }

        if (col.gameObject.CompareTag("Player2") && !_canDash || col.gameObject.CompareTag("Player1") && !_canDash)
        {
            col.gameObject.GetComponent<MovmentPlayer>().Stun(1f);
            col.gameObject.GetComponent<Rigidbody2D>().AddForce(rb.velocity * thrust);
            rb.velocity = Vector2.zero;
            desiredVelocity = beschleunigung * -1f;
            Stun(0.5f);
            StopCoroutine("Dassh");
            _canDash = true;
        }
    }
    private void ResetPlayer()
    {
        rb.velocity = Vector2.zero;
        startDir = Random.Range(0,4);
        switch (startDir)
        {
            case 0:
                rb.position = new Vector2(-13.5f, 4f);
                break;
            case 1:
                rb.position = new Vector2(13.5f, 4f);
                break;
            case 2:
                rb.position = new Vector2(13.5f, -9);
                break;
            case 3:
                rb.position = new Vector2(-13.5f, 9f);
                break;
        }
        Stun(1f);
    }

    private IEnumerator Dassh(Vector2 direction)
    {
        _canDash = false;
        desiredVelocity = direction * dashSpeed;
        
        yield return new WaitForSeconds(DashTime / 2);

        desiredVelocity = Vector2.zero;

        yield return new WaitForSeconds(DashTime / 2);

        _canDash = true;
    }

    private IEnumerator StunTimer(float stunTime)
    {
        yield return new WaitForSeconds(stunTime);
        _isStuned = false;
        _currentStunTimer = null;
    }

    private void StunIndicator()
    {
        if (_isStunedLast != _isStuned)
        {
            _isStunedLast = _isStuned;
            if (_isStuned)
            {
                print($"Player \"{transform.name}\" is stuned");
            }
            else
            {
                print($"Player \"{transform.name}\" is vulnerable again");
            }
        }
    }

    public void Stun(float stunTime)
    {
        if(_currentStunTimer != null){StopCoroutine(_currentStunTimer);}
        _isStuned = true;
        _currentStunTimer = StartCoroutine(StunTimer(stunTime));
    }
}


