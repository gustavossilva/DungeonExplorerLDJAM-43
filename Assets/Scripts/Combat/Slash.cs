using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

[RequireComponent(typeof(SkeletonAnimation))]
public class Slash : MonoBehaviour {

	// Use this for initialization
	public SkeletonAnimation _skeletonAnimation;
	public AnimationReferenceAsset slash;
	void Start () {
		_skeletonAnimation = GetComponent<SkeletonAnimation>();
	}

	public void DoSlash()
	{
		_skeletonAnimation.AnimationState.SetAnimation(0,slash,false);
	}
}
