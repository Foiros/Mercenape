using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Created by Arttu Paldán on 5.11.2020: 
public class PlayerSpeech: MonoBehaviour
{
    GameObject bubbleObject;
    TextMeshProUGUI speechBubble;

    public string howToAttack, howToBlock;
    public float messageTime;

    void Awake()
    {
        bubbleObject = GameObject.FindGameObjectWithTag("PlayerSpeechBubble");
        speechBubble = bubbleObject.GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        speechBubble.text = "";

        StartCoroutine(Wait(messageTime));
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);

        StartCoroutine(SpawnMessage(speechBubble, howToAttack, howToBlock, messageTime));
    }

    IEnumerator SpawnMessage(TextMeshProUGUI messageObject, string message1, string message2, float time)
    {
        messageObject.text = message1;

        yield return new WaitForSeconds(time);

        messageObject.text = message2;

        yield return new WaitForSeconds(time);

        messageObject.text = "";
    }
}
