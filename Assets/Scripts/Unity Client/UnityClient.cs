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

        //    // ���� �޽����� ���� �� �ֵ��� �ݺ��� ���
        //    for (int i = 0; i < 10; i++)
        //    {
        //        stream.WriteLine($"Hello {i}");
        //        stream.Flush(); // ���۸� ��� ��� ����
        //        Thread.Sleep(1000); // 1�� ��� (�ɼ�)
        //    }

        //    stream.Close(); // ��Ʈ���� �ݰ�
        //    pipe.Close(); // �������� ����
        //}

        using (var pipe = new NamedPipeClientStream(".", "ServerRead", PipeDirection.In))
        using (var stream = new StreamReader(pipe))
        {
            pipe.Connect();
            print("Connection");


            while (pipe.IsConnected) // Ŭ���̾�Ʈ�� ����� ���� ��� �б�
            {

                string message = stream.ReadLine();
                if (message != null)
                {
                    print(message);
                }
            }

            stream.Close(); // ��Ʈ���� �ݰ�
            pipe.Close(); // �������� ����
        }
    }
}