using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextAnimator : MonoBehaviour
{
	private static TextAnimator anim;
	public static TextAnimator instance { get { return anim; } }
	void OnEnable() { anim = this; }
	void OnDisable() { anim = null; }

	public void AnimateText(Text textObject, string text, float time, ShopItemBehaviour parent) {
		StartCoroutine(PlayText(textObject, text, time, parent));
    }


	IEnumerator PlayText(Text textObject, string txt, float time, ShopItemBehaviour parent) {
		float timePerChar = time / txt.Length;
		if (textObject == null) {
			parent.isAnimating = false;
			yield break;
		}
		textObject.text = "";
		foreach (char c in txt) {
			if (textObject == null) break;

			textObject.text += c;
			yield return new WaitForSeconds(timePerChar);
		}

		parent.isAnimating = false;
	}
}
