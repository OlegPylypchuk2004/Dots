using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ReverseMaskUI : Image
{
    private Material _cachedMaterial;

    public override Material materialForRendering
    {
        get
        {
            if (_cachedMaterial == null)
            {
                _cachedMaterial = new Material(base.materialForRendering);
                _cachedMaterial.SetInt("_StencilComp", (int)CompareFunction.NotEqual);
            }

            return _cachedMaterial;
        }
    }

    protected override void OnDestroy()
    {
        if (_cachedMaterial != null)
        {
            Destroy(_cachedMaterial);
        }

        base.OnDestroy();
    }
}