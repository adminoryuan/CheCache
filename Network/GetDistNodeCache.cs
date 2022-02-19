using System.Net;
using System.Net.Sockets;
using Probuf;
using Google.Protobuf;
public class GetDistNodeCache{
    public static void PutCache(string node,string key,string val){
        //System.Console.WriteLine($"节点{node}"+node+"ke"+key+"va"+val);
        Socket socket=new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
        socket.Connect(IPEndPoint.Parse(node));
        Cache r=new Cache();
        r.IsPut=true;
        r.Key=key;
        r.Val=val;
        byte[] data=new byte[r.CalculateSize()];
        using(CodedOutputStream str=new CodedOutputStream(data)){
                r.WriteTo(str);
        }
        socket.Send(data);
    }
    
    public static string GetCache(string node,string key){
        Socket socket=new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
        socket.Connect(IPEndPoint.Parse(node));
        Cache cache=new Cache();
        cache.IsPut=false;
        cache.Key=key;
        byte[] data=new byte[cache.CalculateSize()];
        using(CodedOutputStream str=new CodedOutputStream(data)){
            cache.WriteTo(str);
        }
        socket.Send(data);
        Result cache1;
      
        byte[] Revc=new byte[512];
        int len=socket.Receive(Revc);
        cache1=Result.Parser.ParseFrom(Revc,0,len); 
        //  System.Console.WriteLine(cache1.Result_);
        return cache1.Result_;

    }
}