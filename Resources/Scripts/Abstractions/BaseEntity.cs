using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public abstract class BaseEntity : BaseXPManager, ICollisionAction
{
    // Finite state machine
    protected StateMachine _fsm;

    [Header("Main entity parametres:")]
    [SerializeField] protected bool _isActive = true;

    [SerializeField] protected string _stateNow = "base";
    [Header("-------------")]

    // Must have parametres
    protected SpriteRenderer _spriteRenderer;
    protected Collider2D _colider;
    protected ClassEntity _fraction;
    public ClassEntity Fraction => _fraction;

    private void ChangeState(string stateNow)
    {
        _stateNow = stateNow;
    }

    /// <summary>
    /// Init fsm
    /// </summary>
    protected virtual void FSMInit()
    {
        _fsm = new StateMachine();
        _fsm.AChangeState += ChangeState;
    }

    protected virtual void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _colider = GetComponent<Collider2D>();
    }


    private void OnDestroy()
    {
        if (_fsm != null)
        {
            _fsm.AChangeState -= ChangeState;
        }
        
    }

    public void RecolorThis(Color color)
    {
        _spriteRenderer.color = color; 
    }

    /// <summary>
    /// Update fsm
    /// </summary>
    protected virtual void Update()
    {
        if (_fsm != null)
        {
            _fsm.Update();
        }
    }


    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        var obj = collision.gameObject.GetComponent<BaseEntity>();
        if (obj != null)
        {
            switch (obj.Fraction)
            {
                case ClassEntity.Enemy:
                    CollisionWithEnemy(collision.gameObject);
                    break;
                case ClassEntity.Player:
                    CollisionWithPlayer(collision.gameObject);
                    break;
            }
        }
    }

    public virtual void CollisionWithPlayer(GameObject gameObject)
    {
    }

    public virtual void CollisionWithEnemy(GameObject gameObject)
    {
    }


    protected virtual void DestroyThisObject()
    {
        Destroy(gameObject);
    }
}
