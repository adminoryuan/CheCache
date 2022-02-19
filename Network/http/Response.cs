using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Net.Sockets;
public class HttpResponse{
    private Socket socket;
    public HttpResponse(Socket s)
    {
        socket=s;
    }
    public void Write(string val){
      //  System.Console.WriteLine(val);
        string response="HTTP/1.1 200 OK\r\n Content-Length:length \r\nContent-Type: text/html; charset=utf-8\r\n\r\n\r\n";
        response+=val;
        response= response.Replace("length",Encoding.UTF8.GetBytes(response).Length.ToString());
        socket.Send(Encoding.UTF8.GetBytes(response));
        socket.Close();
    }
}