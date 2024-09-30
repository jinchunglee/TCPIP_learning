using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class ServerProgram
{
    static void Main(string[] args)
    {
        // 1. 建立 TCP 伺服器
        TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 12345);
        server.Start();
        Console.WriteLine("伺服端正在等待連接...");

        // 2. 接受客戶端連接
        TcpClient client = server.AcceptTcpClient();
        Console.WriteLine("客戶端已連線");

        // 3. 接收客戶端的訊息
        NetworkStream stream = client.GetStream();
        byte[] buffer = new byte[1024];
        int bytesRead = stream.Read(buffer, 0, buffer.Length);
        string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
        Console.WriteLine($"收到的訊息: {receivedMessage}");

        // 4. 回傳訊息給客戶端
        string responseMessage = "伺服端已收到訊息";
        byte[] responseBytes = Encoding.UTF8.GetBytes(responseMessage);
        stream.Write(responseBytes, 0, responseBytes.Length);

        // 5. 關閉連接
        client.Close();
        server.Stop();
    }
}
