using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueController : PlayerAttributes
{
    public void Continue()
    {
        StateControllerRef.GetComponent<GamestateController>().state = GamestateController.GameState.Play;
        PlayerRagdoll.SetActive(false);
        PlayerModel.SetActive(true);
        PlayerRagdoll.transform.localPosition = new Vector3(0,-2,0);
        transform.rotation = Quaternion.identity;
        RB.constraints = RigidbodyConstraints.FreezeRotationY;
        RB.constraints = RigidbodyConstraints.FreezeRotationZ;
        transform.position = new Vector3(0, 1, transform.position.z);
        continuePanel.gameObject.SetActive(false);
    }
}
