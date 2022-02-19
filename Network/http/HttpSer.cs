public class HttpSer : Server{
    public override void ServerHttp(HttpRequest req, HttpResponse resp)
    {
      // System.Console.WriteLine(req.URL);
        if(req.URL.Contains("/add")){
            string[] Val=req.URL.Split("/"); 
            string node= dist.GetNode(Val[1]);
            GetDistNodeCache.PutCache(dist.GetNode(node),Val[2],Val[3]);
            resp.Write("缓存成功"); 
        }else if(req.URL.Contains("/get")){
            string[] Val=req.URL.Split("/");

            string node= dist.GetNode(Val[1]);
            //从远程节点获取缓存值
           string val=GetDistNodeCache.GetCache(dist.GetNode(node),Val[2]);
           // System.Console.WriteLine("缓存值为"+val);
             resp.Write(val);
           // resp.Write(GetDistNodeCache.GetCache(dist.GetNode(node),Val[2]));
        }else if(req.URL.Contains("/regist")){
          //  System.Console.WriteLine("注册节点中");
            dist.AddNode(req.URL.Replace("/regist/",""));
            resp.Write("注册成功");
        }
      
    }
}