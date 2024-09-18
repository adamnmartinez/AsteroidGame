using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class LifeTally : MonoBehaviour
{
    public PlanetHitbox planet;
    public GameObject token;
    public Stack<GameObject> tokenStack;

    private Vector3 tokenPos;

    void Awake()
    {
        tokenStack = new Stack<GameObject>();
        tokenPos = gameObject.transform.localPosition;
        for (int i = 0; i < planet.health; i++)
        {
            AddToken();
        }
    }

    public void AddToken()
    {
        tokenStack.Push(Instantiate(token, tokenPos, Quaternion.identity, gameObject.transform));
        tokenPos.x -= 0.8f;

        if (tokenStack.Count % 5 == 0)
        {
            tokenPos.x = gameObject.transform.localPosition.x;
            tokenPos.y -= 0.6f;
        }
    }

    public void SubtractToken()
    {
        if (tokenStack.Count != 0) Destroy(tokenStack.Pop());
    }
}
