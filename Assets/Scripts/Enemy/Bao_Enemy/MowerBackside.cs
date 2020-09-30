using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MowerBackside : MonoBehaviour
{
    public enum GeneratorState { Inactive, Generating, Active }

    private GeneratorState currentState;

    private SpriteRenderer sprite;

    private void Start()
    {     
        currentState = GeneratorState.Inactive;

        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        switch (currentState)
        {
            case GeneratorState.Inactive:
                {
                    sprite.color = Color.white;

                    break;
                }
            case GeneratorState.Generating:
                {
                    sprite.color = Color.yellow;

                    break;
                }
            case GeneratorState.Active:
                {
                    sprite.color = Color.red;

                    break;
                }
        }
    }
}
