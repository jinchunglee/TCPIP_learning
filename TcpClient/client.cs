using System;
using System.Net.Sockets;
using System.Text;

class ClientProgram
{
    static void Main(string[] args)
    {
        // 1. 連接到伺服端
        TcpClient client = new TcpClient("127.0.0.1", 12345);
        Console.WriteLine("已連接到伺服端");

        // 2. 向伺服端發送訊息
        NetworkStream stream = client.GetStream();
        string message = "Hello from client!";
        byte[] messageBytes = Encoding.UTF8.GetBytes(message);
        stream.Write(messageBytes, 0, messageBytes.Length);
        Console.WriteLine("已發送訊息");

        // 3. 接收伺服端的回應
        byte[] buffer = new byte[1024];
        int bytesRead = stream.Read(buffer, 0, buffer.Length);
        string responseMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
        Console.WriteLine($"收到的回應: {responseMessage}");

        // 4. 關閉連接
        client.Close();
    }
}
