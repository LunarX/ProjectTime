// Source: https://github.com/off99555/Unity3D-Python-Communication

using AsyncIO;
using NetMQ;
using NetMQ.Sockets;
using UnityEngine;
using System.Threading;
using System.Globalization;

/// <summary>
///     To use this class, you just instantiate, call Start() when you want to start and Stop() when you want to stop.
/// </summary>
public class EquipementMesures : RunAbleThread
{
    public float bpm { get; private set; } = 0;
    public bool faceDetected { get; private set; } = false;

    private int samplesAmount = 150;
    private int count = 0;

    public float averageBpm = 0;

    /// <summary>
    ///     Receives bmp from python program.
    ///     Stop requesting when Running=false.
    /// </summary>
    protected override void Run()
    {
        ForceDotNet.Force(); // this line is needed to prevent unity freeze after one use, not sure why yet


        using (var server = new ResponseSocket())
        {
            server.Bind("tcp://*:9701");

            while (Running)
            {
                //Debug.Log("Running ...");
                if (server.TryReceiveFrameString(out string msg))
                {
                    //Debug.Log("From Client: " + msg);
                    server.SendFrame("G");

                    var msgs = msg.Split(';');
                    //bpm = float.Parse(msgs[1]);
                    CultureInfo ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
                    ci.NumberFormat.CurrencyDecimalSeparator = ".";
                    bpm = float.Parse(msgs[1], NumberStyles.Any, ci);
                    faceDetected = msgs[0] == "T";

                    // Computes mean value of bmp right after the first results are received
                    if(count < samplesAmount && bpm != 0)
                    {
                        count += 1;
                        averageBpm += (1.0f / samplesAmount) * bpm;
                    }
                }
                Thread.Sleep(5);
            }
            //Debug.Log("Not running anymore !");
        }

        //Debug.Log("Cleanup");
        NetMQConfig.Cleanup(); // this line is needed to prevent unity freeze after one use, not sure why yet

    }
}