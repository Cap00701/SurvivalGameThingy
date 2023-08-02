using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkSyncObject : NetworkBehaviour
{

	public PlayersManager playersManager;
	public ScoreManager scoreManager;
	public ChatLog chatLog;
	public EnvironmentManager environment;

	void Awake ()
	{
		if (playersManager == null)
			playersManager = this.GetComponent<PlayersManager> ();
		if (scoreManager == null)
			scoreManager = this.GetComponent<ScoreManager> ();
		if (chatLog == null)
			chatLog = this.GetComponent<ChatLog> ();
		if (environment == null)
			environment = this.GetComponent<EnvironmentManager> ();

        UnitZ.NetworkGameplay = this;
	}
}
