using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Entity : MonoBehaviour
{

    private List<EntityPart> parts;
    protected virtual void Start()
    {
        parts = GetComponents<EntityPart>().ToList();

        foreach (EntityPart part in parts)
        {
            part.Init();
        }
    }
}
