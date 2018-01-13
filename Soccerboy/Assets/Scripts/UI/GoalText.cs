using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalText : MonoBehaviour {

    [Range(0f, 10f)]
    public float rotationFrequency = 6f;
    [Range(0f, 10f)]
    public float rotationMagnitude = 2f;

    RectTransform rectTransform;
    Text text;

	void Awake () {
        rectTransform = GetComponent<RectTransform>();
        text = GetComponent<Text>();
	}

    void Start() {
        Hide();
    }

    void Update () {
        rectTransform.rotation = Quaternion.Euler(0f, 0f, Mathf.Sin(Time.time * rotationFrequency) * rotationMagnitude);
	}

    public void Show() {
        text.enabled = true;
    }

    public void Hide() {
        text.enabled = false;
    }
}
