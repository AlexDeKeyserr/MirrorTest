using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;

public class Score : NetworkBehaviour
{
    private Dictionary<NetworkConnection, int> playerscore = new Dictionary<NetworkConnection, int>();
    private TextMeshProUGUI scoreTMP;

    public override void OnStartClient()
    {
        base.OnStartClient();
        scoreTMP = GetComponent<TextMeshProUGUI>();
        scoreTMP.text = "score: " + 0;
    }

    [Command(requiresAuthority = false)]
    public void CmdAddNewPlayer(NetworkConnectionToClient sender = null) => playerscore.Add(sender, 0);
    [TargetRpc]
    private void TargetChangeScoreOnPlayer(NetworkConnection target, int score) => scoreTMP.text = "score: " + score;
    [Server]
    public void ChangePoint(NetworkConnection id, int change)
    {
        playerscore[id] += change;
        TargetChangeScoreOnPlayer(id, playerscore[id]);
    }
}