

using UnityEngine.Rendering.Universal;

/// <summary>
/// Говорит что обьект является источником света
/// </summary>
public interface ILightSource
{
    public Light2D Light { get; set; }
}

