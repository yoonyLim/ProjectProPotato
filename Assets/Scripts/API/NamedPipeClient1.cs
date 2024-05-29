using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class NamedPipeClient1 : MonoBehaviour
{
    public static NamedPipeClient1 Instance;

    private const string PipeName = "pipe1";
    private NamedPipeClientStream pipeClient;

    private int ProInt1;
    private int PotInt1;
    public int ProDiff;
    public int PotDiff;

    public bool ProBlowing = false;
    public bool PotBlowing = false;



    private const int maxNumCounts = 2; // count up to 0.4 seconds; tics every 0.2 second
    private int numCounts = 0;

    void Start()
    {
        pipeClient = new NamedPipeClientStream(".", PipeName, PipeDirection.In, PipeOptions.Asynchronous);
        Task.Run(() => ConnectToPipeAsync());
    }

    async Task ConnectToPipeAsync()
    {
        await pipeClient.ConnectAsync();
        Debug.Log("Connected to " + PipeName);

        await ReadFromPipeAsync();
    }

    async Task ReadFromPipeAsync()
    {
        byte[] buffer = new byte[256];
        while (pipeClient.IsConnected)
        {
            int bytesRead = await pipeClient.ReadAsync(buffer, 0, buffer.Length);
            if (bytesRead > 0)
            {
                string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                if (numCounts == 0)
                {
                    ProInt1 = int.Parse(message.Split(',')[0]);
                    PotInt1 = int.Parse(message.Split(',')[1]);
                }
                

                if (numCounts == maxNumCounts)
                {
                    if (ProDiff >= 3)
                    {
                        ProBlowing = true;
                        Debug.Log("Pro is Blowing");
                    }
                    else
                    {
                        ProBlowing = false;
                        Debug.Log("is not Blowing");
                    }

                    if (PotDiff >= 3)
                    {
                        PotBlowing = true;
                        Debug.Log("Pot is Blowing");
                    }
                    else
                    {
                        PotBlowing = false;
                        Debug.Log("Pot is not Blowing");
                    }

                    ProDiff = int.Parse(message.Split(',')[0]) - ProInt1;
                    PotDiff = int.Parse(message.Split(',')[1]) - PotInt1;
                    ProInt1 = int.Parse(message.Split(',')[0]);
                    PotInt1 = int.Parse(message.Split(',')[1]);

                    numCounts = 0;
                }
                else
                    numCounts++;
            }
        }
    }



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    

    void OnDestroy()
    {
        pipeClient?.Close();
        pipeClient?.Dispose();
    }

    
    
}
