using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Threading;
using UnityEngine;

public class UnityClient : MonoBehaviour
{
    public static UnityClient instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //using (var pipe = new NamedPipeClientStream(".", "ServerRead", PipeDirection.Out))
        //using (var stream = new StreamWriter(pipe))
        //{
        //    pipe.Connect();
        //    Console.WriteLine("Connection");

        //    // 여러 메시지를 보낼 수 있도록 반복문 사용
        //    for (int i = 0; i < 10; i++)
        //    {
        //        stream.WriteLine($"Hello {i}");
        //        stream.Flush(); // 버퍼를 비워 즉시 전송
        //        Thread.Sleep(1000); // 1초 대기 (옵션)
        //    }

        //    stream.Close(); // 스트림을 닫고
        //    pipe.Close(); // 파이프를 닫음
        //}

        using (var pipe = new NamedPipeClientStream(".", "ServerRead", PipeDirection.In))
        using (var stream = new StreamReader(pipe))
        {
            pipe.Connect();
            print("Connection");


            while (pipe.IsConnected) // 클라이언트가 연결된 동안 계속 읽기
            {

                string message = stream.ReadLine();
                if (message != null)
                {
                    print(message);
                }
            }

            stream.Close(); // 스트림을 닫고
            pipe.Close(); // 파이프를 닫음
        }
    }
}