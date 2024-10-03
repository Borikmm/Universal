using UnityEngine;
using UnityEngine.Rendering.Universal;

public abstract class BaseEntity : BaseXPManager
{
    // Must have parametres
    protected SpriteRenderer _spriteRenderer;
    [SerializeField] protected Collider2D _collisionColider;
    protected EntityFraction _fraction;
    public EntityFraction Fraction => _fraction;

    public Collider2D Collider => _collisionColider;

    // Light Mechanic
/*
    private int _inTheLight = 0;
    public int InTheLight
    {
        get { return _inTheLight; }
        set 
        {
            _inTheLight = value;
        }
    }*/

    protected override void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if ( _spriteRenderer == null )
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        CheckThisObject();
    }



    private void CheckThisObject()
    {
        if (this is IObjWithLightingCollider)
        {
            ((IObjWithLightingCollider)this).InitLightingCollider();
        }

        if (this is BaseCube cube)
        {
            GetComponentInChildren<CubeColliderCollision>().Init(cube);
        }
    }

    public void RecolorThis(Color color)
    {
        _spriteRenderer.color = color; 
    }
}
