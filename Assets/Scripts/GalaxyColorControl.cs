using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;


public class GalaxyColorControl : MonoBehaviour
{
    public GalaxyColor m_galaxyColor;
    public VisualEffect orbitalVisualEffect;

    // Start is called before the first frame update
    void Start()
    {
        orbitalVisualEffect = gameObject.GetComponent<VisualEffect>();

        orbitalVisualEffect.SetVector3("EdgeMistColor", m_galaxyColor.EdgeMistColor);
        orbitalVisualEffect.SetGradient("CenterMistColor",m_galaxyColor.CenterMistColor);
        orbitalVisualEffect.SetGradient("SpiralColor", m_galaxyColor.SpiralColor);
        orbitalVisualEffect.SetVector2("BodyColor_R",m_galaxyColor.BodyColor.r);
        orbitalVisualEffect.SetVector2("BodyColor_G", m_galaxyColor.BodyColor.g);
        orbitalVisualEffect.SetVector2("BodyColor_B", m_galaxyColor.BodyColor.b);

        Debug.Log(m_galaxyColor.EdgeMistColor);
        Debug.Log(m_galaxyColor.CenterMistColor);
        Debug.Log(m_galaxyColor.SpiralColor);
        Debug.Log(m_galaxyColor.BodyColor);
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        orbitalVisualEffect.SetVector3("EdgeMistColor", m_galaxyColor.EdgeMistColor);
        orbitalVisualEffect.SetGradient("CenterMistColor", m_galaxyColor.CenterMistColor);
        orbitalVisualEffect.SetGradient("SpiralColor", m_galaxyColor.SpiralColor);
        orbitalVisualEffect.SetVector2("BodyColor_R", m_galaxyColor.BodyColor.r);
        orbitalVisualEffect.SetVector2("BodyColor_G", m_galaxyColor.BodyColor.g);
        orbitalVisualEffect.SetVector2("BodyColor_B", m_galaxyColor.BodyColor.b);
#endif
    }
}
