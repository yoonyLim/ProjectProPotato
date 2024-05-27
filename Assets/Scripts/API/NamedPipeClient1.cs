using System;
using System.IO.Pipes;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class NamedPipeClient1 : MonoBehaviour
{
    public static NamedPipeClient1 Instance;

    private const string PipeName = "pipe1";
    private NamedPipeClientStream pipeClient;

    public int ProInt;
    public int PotInt;
    public int AvgPro;
    public int AvgPot;

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
                ProInt = int.Parse(message.Split(',')[0]);
                PotInt = int.Parse(message.Split(',')[1]);
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

    private void Update()
    {
        
    }

    void OnDestroy()
    {
        pipeClient?.Close();
        pipeClient?.Dispose();
    }
}
