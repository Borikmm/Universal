
/// <summary>
/// Говорит что у обьекта есть LightingCollider
/// </summary>
public interface IObjWithLightingCollider
{
    public Light2DCollision CollisionScript {  get; set; }
    public void InitLightingCollider();
}

