// Source: https://github.com/off99555/Unity3D-Python-Communication

using AsyncIO;
using NetMQ;
using NetMQ.Sockets;
using UnityEngine;
using System.Threading;

/// <summary>
///     Example of requester who only sends Hello. Very nice guy.
///     You can copy this class and modify Run() to suits your needs.
///     To use this class, you just instantiate, call Start() when you want to start and Stop() when you want to stop.
/// </summary>
public class EquipementMesures : RunAbleThread
{
    public float bpm { get; private set; } = 0;
    public bool faceDetected { get; private set; } = false;

    /// <summary>
    ///     Request Hello message to server and receive message back. Do it 10 times.
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
                    bpm = float.Parse(msgs[1]);
                    //if (msgs[0] == "T")
                    //    faceDetected = true;
                    //else
                    //    faceDetected = false;
                     //= msgs[0] == "T" ? true : false;
                }
                Thread.Sleep(5);
            }
            //Debug.Log("Not running anymore !");
        }

        //Debug.Log("Cleanup");
        NetMQConfig.Cleanup(); // this line is needed to prevent unity freeze after one use, not sure why yet

    }
}