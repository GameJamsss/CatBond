using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outliner : MonoBehaviour
{
    private Material _outlineMaterial;
    private Material _defaultMatetial;

    private void Start()
    {
        _outlineMaterial = Resources.Load<Material>("Material/OutlineMaterial");
        _defaultMatetial = new Material(Shader.Find("Sprites/Default"));
    }

    void OnMouseEnter()
    {
        SetMaterial(_outlineMaterial);
    }

    void OnMouseExit()
    {
        SetMaterial(_defaultMatetial);
    }

    private void SetMaterial(Material material)
    {
        if (gameObject.GetComponent<SpriteRenderer>() != null)
            gameObject.GetComponent<SpriteRenderer>().material = material;

        foreach (Transform child in gameObject.transform)
        {
            if (child.GetComponent<SpriteRenderer>() != null)
                child.GetComponent<SpriteRenderer>().material = material;
        }
    }
}
