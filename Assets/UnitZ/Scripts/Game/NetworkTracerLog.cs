using UnityEngine;
using System;
using System.IO;
using UnityEngine.Networking;

public class NetworkTracerLog : MonoBehaviour {

    private TextWriter tw;
    private string TW_Filename;

    void Start () {
        // Open a log file
        TW_Filename = String.Format("NetworkBPS_{0}.log", DateTime.Now.ToString("yyyy.MM.dd.hh.mm"));
        tw = new StreamWriter(TW_Filename, true);
        tw.Close();
    }

    private int PrevFullBytesCount = 0;
    private int FullBPS = 0;
    private int FullBPSMin = 0;
    private int FullBPSMax = 0;

    private int PrevUserBytesCount = 0;
    private int UserBPS = 0;
    private int UserBPSMin = 0;
    private int UserBPSMax = 0;

    private string LogText;
    private float timeTmp;

    private void OnGUI()
    {
        // If a network game
        if (NetworkTransport.IsStarted)
        {
            // Get Full & User bytes count since start of game
            int FullBytesCount = NetworkTransport.GetOutgoingFullBytesCount();
            int UserBytesCount = NetworkTransport.GetOutgoingUserBytesCount();
            //  If it's time to recalculate
            if (Time.time >= timeTmp + 1)
            {
                // Set recording time
                timeTmp = Time.time;
                // Calculate bytes sent since the previous second
                FullBPS = FullBytesCount - PrevFullBytesCount;
                UserBPS = UserBytesCount - PrevUserBytesCount;
                // Store the current values
                PrevFullBytesCount = FullBytesCount;
                PrevUserBytesCount = UserBytesCount;
                // Record the minimum BPS
                if ((FullBPS < FullBPSMin) && (FullBPSMin != 0))
                {
                    FullBPSMin = FullBPS;
                }
                if ((UserBPS < UserBPSMin) && (UserBPSMin != 0))
                {
                    UserBPSMin = UserBPS;
                }
                // Record the maximum BPS
                if (FullBPS > FullBPSMax)
                {
                    FullBPSMax = FullBPS;
                }
                if (UserBPS > UserBPSMax)
                {
                    UserBPSMax = UserBPS;
                }
                // Save findings to disk
                LogText = string.Format("{0:00000000}: Full: {2} ({3}/{4}) ... User: {5} ({6}/{7})", timeTmp, DateTime.Now.ToString(), FullBytesCount, FullBPSMin, FullBPSMax, UserBytesCount, UserBPSMin, UserBPSMax);
                File.AppendAllText(TW_Filename, LogText + Environment.NewLine);
            }
            // Output usage
            GUI.skin.label.fontSize = 16;
            GUI.skin.label.alignment = TextAnchor.UpperLeft;
            GUI.Label(new Rect(5, 5, Screen.width / 2, 100), LogText);
        }
    }
}
