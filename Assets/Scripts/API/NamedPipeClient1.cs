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

    private List<int> proValues = new List<int>();
    private List<int> potValues = new List<int>();
    private int maxValuesToStore = 5; // Number of values to consider for analysis
    private int blowThreshold = 4; // Threshold for detecting blowing
    private int stopThreshold = 2; // Threshold for detecting stop of blowing

    // State to keep track of blowing
    public bool proBlowing = false;
    public bool potBlowing = false;

    //private const int maxNumCounts = 4; // count up to 0.4 seconds; tics every 0.2 second
    //private int numCounts = 0;

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
                int proValue = int.Parse(message.Split(',')[0]);
                int potValue = int.Parse(message.Split(',')[1]);

                // Add the new value to the list
                proValues.Add(proValue);
                potValues.Add(potValue);

                // Keep only the latest 'maxValuesToStore' values
                if (proValues.Count > maxValuesToStore)
                {
                    proValues.RemoveAt(0);
                }

                if (potValues.Count > maxValuesToStore)
                {
                    potValues.RemoveAt(0);
                }

                // Check if blowing is detected
                if (IsBlowingDetected(proValues))
                {
                    if (!proBlowing)
                    {
                        proBlowing = true;
                        Debug.Log("Professor Blowing detected!");
                    }
                }
                else
                {
                    if (proBlowing)
                    {
                        proBlowing = false;
                        Debug.Log("Professor Blowing stopped!");
                    }
                }
                //if (numCounts == 0)
                //{
                //    ProInt1 = int.Parse(message.Split(',')[0]);
                //    PotInt1 = int.Parse(message.Split(',')[1]);
                //}

                //if (numCounts == maxNumCounts)
                //{
                //    ProDiff = int.Parse(message.Split(',')[0]) - ProInt1;
                //    PotDiff =  int.Parse(message.Split(',')[1]) - PotInt1;
                //    ProInt1 = int.Parse(message.Split(',')[0]);
                //    PotInt1 = int.Parse(message.Split(',')[1]);

                //    numCounts = 0;
                //}
                //else
                //    numCounts++;
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

    // Check if blowing is detected based on the recent sensor values
    bool IsBlowingDetected(List<int> sensorValues)
    {
        if (sensorValues.Count < maxValuesToStore)
        {
            return false;
        }

        int maxVal = int.MinValue;
        int minVal = int.MaxValue;

        foreach (int value in sensorValues)
        {
            if (value > maxVal)
            {
                maxVal = value;
            }

            if (value < minVal)
            {
                minVal = value;
            }
        }

        if ((maxVal - minVal) > blowThreshold)
        {
            return true;
        }

        if ((maxVal - minVal) < stopThreshold)
        {
            return false;
        }

        return false;
    }
}
