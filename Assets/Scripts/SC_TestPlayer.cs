using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using Spine.Unity.Examples;
using UnityEngine;

public class SC_TestPlayer : MonoBehaviour {
	[SpineSlot]
	public string slotproperty = "body-down";

	[SpineAttachment]
	public string attachmentproperty = "body-down-3";


	private SkeletonRenderer skeltonRenderer;
	private SkeletonAnimation skeltonAnim;
	string skin;

	// Start is called before the first frame update
	void Start() {
		//skin = GetComponent<SkeletonAnimation>().skeleton.Skin.ToString();
		//skeltonRenderer = GetComponent<SkeletonRenderer>();
		skeltonAnim = GetComponent<SkeletonAnimation>();

		//print(skeltonRenderer.skeleton.Slots.Count);
		//for (int i = 0; i < skeltonRenderer.skeleton.Slots.Count; i++) {
		//	print(i + "/" + skeltonRenderer.skeleton.Slots.Items[i]);
		//}


		skin = "3";
		slotproperty = "body-down";
		attachmentproperty = "body-down-" + 6;
		skeltonAnim.skeleton.SetAttachment(slotproperty, attachmentproperty);


		slotproperty = "hand";
		attachmentproperty = "hand-" + skin;
		skeltonAnim.skeleton.SetAttachment(slotproperty, attachmentproperty);


		slotproperty = "arm-down";
		attachmentproperty = "arm-down-" + skin;
		skeltonAnim.skeleton.SetAttachment(slotproperty, attachmentproperty);


		slotproperty = "arm-up";
		attachmentproperty = "arm-up-" + skin;
		skeltonAnim.skeleton.SetAttachment(slotproperty, attachmentproperty);


		slotproperty = "head";
		attachmentproperty = "head-" + 6;
		skeltonAnim.skeleton.SetAttachment(slotproperty, attachmentproperty);

		slotproperty = "body-up";
		attachmentproperty = "body-up-" + 6;
		skeltonAnim.skeleton.SetAttachment(slotproperty, attachmentproperty);

		slotproperty = "neck";
		attachmentproperty = "neck-" + skin;
		skeltonAnim.skeleton.SetAttachment(slotproperty, attachmentproperty);

		slotproperty = "leg-up";
		attachmentproperty = "leg-up-" + skin;
		skeltonAnim.skeleton.SetAttachment(slotproperty, attachmentproperty);

		slotproperty = "leg-down";
		attachmentproperty = "leg-down-" + skin;
		skeltonAnim.skeleton.SetAttachment(slotproperty, attachmentproperty);

		slotproperty = "foot";
		attachmentproperty = "foot-" + skin;
		skeltonAnim.skeleton.SetAttachment(slotproperty, attachmentproperty);

		slotproperty = "leg-up2";
		attachmentproperty = "leg-up-" + skin;
		skeltonAnim.skeleton.SetAttachment(slotproperty, attachmentproperty);

		slotproperty = "leg-down2";
		attachmentproperty = "leg-down-" + skin;
		skeltonAnim.skeleton.SetAttachment(slotproperty, attachmentproperty);

		slotproperty = "foot2";
		attachmentproperty = "foot-" + skin;
		skeltonAnim.skeleton.SetAttachment(slotproperty, attachmentproperty);

		slotproperty = "hand2";
		attachmentproperty = "hand-" + skin;
		skeltonAnim.skeleton.SetAttachment(slotproperty, attachmentproperty);

		slotproperty = "arm-down2";
		attachmentproperty = "arm-down-" + skin;
		skeltonAnim.skeleton.SetAttachment(slotproperty, attachmentproperty);

		slotproperty = "arm-up2";
		attachmentproperty = "arm-up-" + skin;
		skeltonAnim.skeleton.SetAttachment(slotproperty, attachmentproperty);
	}
}
