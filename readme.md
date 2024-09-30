# C# TCP 伺服器與客戶端重建步驟

這是如何從頭建立 C# TCP 伺服器與客戶端的完整步驟。

---

## 1. 建立 TCP 伺服器

### 步驟 1: 創建新的伺服器專案

1. 打開命令提示字元，移動到你想要建立專案的資料夾。例如：
   ```bash
   cd C:\Delta\TCP
   ```

2. 創建一個新的 .NET 控制台專案，名稱為 TcpServer：

    ```bash
    dotnet new console -n TcpServer
    ```

3. 進入 TcpServer 專案資料夾：
   ```bash
    cd TcpServer
    ```

4. 刪除系統自動生成的 Program.cs：
   ```bash
   del Program.cs
   ```


### 步驟 2: 撰寫伺服器代碼

5. 在 TcpServer 資料夾內新建一個 server.cs 文件，並將以下 TCP 伺服器代碼貼上
   ```csharp
   using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;

    class ServerProgram
    {
        static void Main(string[] args)
        {
            TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 12345);
            server.Start();
            Console.WriteLine("伺服端正在等待連接...");

            TcpClient client = server.AcceptTcpClient();
            Console.WriteLine("客戶端已連線");

            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Console.WriteLine($"收到的訊息: {receivedMessage}");

            string responseMessage = "伺服端已收到訊息";
            byte[] responseBytes = Encoding.UTF8.GetBytes(responseMessage);
            stream.Write(responseBytes, 0, responseBytes.Length);

            client.Close();
            server.Stop();
        }
    }
    ```

### 步驟 3: 編譯並運行伺服器
6. 在 TcpServer 資料夾內運行伺服器：

    ```bash
    dotnet run
    ```

7. 伺服器應該顯示「伺服端正在等待連接...」，表示伺服器已經開始監聽並等待客戶端的連接。


## 2. 建立 TCP 客戶端

### 步驟 1:創建新的客戶端專案
1. 打開一個新的命令提示字元，並移動到你想要建立專案的資料夾：
   ```bash
   cd C:\Delta\TCP
    ```

2. 創建一個新的 .NET 控制台專案，名稱為 TcpClient：
   ```bash
   dotnet new console -n TcpClient
    ```
3. 進入 TcpClient 專案資料夾：
   ```bash
   cd TcpClient
    ```
4. 刪除自動生成的 Program.cs：
   ```bash
   del Program.cs
   ```

### 步驟 2: 撰寫客戶端代碼
5. 在 TcpClient 資料夾內新建一個 client.cs 文件，並將以下 TCP 客戶端代碼貼上：
    ```csharp
    using System;
    using System.Net.Sockets;
    using System.Text;

    class ClientProgram
    {
        static void Main(string[] args)
        {
            TcpClient client = new TcpClient("127.0.0.1", 12345);
            Console.WriteLine("已連接到伺服端");

            NetworkStream stream = client.GetStream();
            string message = "Hello from client!";
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            stream.Write(messageBytes, 0, messageBytes.Length);
            Console.WriteLine("已發送訊息");

            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string responseMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Console.WriteLine($"收到的回應: {responseMessage}");

            client.Close();
        }
    }
    ```

### 步驟 3: 編譯並運行客戶端
6. 在 TcpClient 資料夾內運行客戶端：
    ```bash
    dotnet run
    ```

7. 客戶端應該顯示「已連接到伺服端」，並成功傳送訊息。


## 總結
1. 創建專案：使用 dotnet new console -n <ProjectName> 創建伺服器和客戶端專案。
2. 刪除 Program.cs：刪除自動生成的 Program.cs 文件，避免頂級語句衝突。
3. 撰寫代碼：將 TCP 伺服器和客戶端代碼分別寫入 server.cs 和 client.cs。
4. 運行專案：分別運行伺服器和客戶端，觀察它們的互動。

