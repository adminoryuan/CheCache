using System.Text;
using System.Threading.Tasks;
using System.Net;
using System;
using System.Net.Sockets;
using Probuf;
using Google.Protobuf;
public abstract class Server{    
    private Socket ser;
    protected DistNode dist=new DistNode();
    public void Start(EndPoint point,string a){
        ser=new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        ser.Bind(point);
        dist.AddNode(a);
        ser.Listen(10);
        System.Console.WriteLine("服务正在启动...."+((int)point.AddressFamily));
        while(true){
            Socket cli=ser.Accept(); 
            HandleRequest(cli);
        }
    }
    public   void HandleRequest(Socket s){  
        byte[] msg=new byte[1024];
        int len= s.Receive(msg);
        Task.Run(new System.Action(()=>{
            if(len>512){
                //大于512使用http 
                string reqStr=Encoding.UTF8.GetString(msg);
                ServerHttp(GetRequest(reqStr),new HttpResponse(s));
            }else{
                //使用probuf协议 
               Cache ch= Cache.Parser.ParseFrom(msg,0,len);
               Result r=new Result();
               if(ch.IsPut){
                    //注册到本地缓存中 
                   gloabCache.GetGloabCache().PutCache(ch.Key,ch.Val);
                   
               }else{
                   r.Key=ch.Key;

                  // System.Console.WriteLine(ch.Key);
                   r.Result_= gloabCache.GetGloabCache().GetCache(ch.Key);
               }
               byte[] data=new byte[r.CalculateSize()];
               using(CodedOutputStream str=new CodedOutputStream(data)){
                        r.WriteTo(str);
               }
               
              s.Send(data); 
            }
        }));

    }
    public HttpRequest GetRequest(string str) {
        string[] https=str.Split("\r\n");

        HttpRequest req=new HttpRequest();
        if(https[0].Split(" ").Length<2)return req;
        req.URL=https[0].Split(" ")[1];
        req.Put("Method",req.URL);
        for(int i=1;i<https.Length;i++){
            string[] heads=https[i].Split(":");
            //System.Console.WriteLine(https[i]);
            if(heads.Length<2)
                continue;
            req.Put(heads[0],heads[1]);
        }
        
        return req;
    }
    public abstract void ServerHttp(HttpRequest req,HttpResponse resp);
    
}