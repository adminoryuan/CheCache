using System.Collections.Generic;
public class HttpRequest{
    class httpUrl{
        private string[] url;
        public void Parms(){
            
        }
    }
    public string URL{get;set;}
    // public string Host{get;set;}
    // public string User_Agent{get;set;}
    private Dictionary<string,string> headers=new Dictionary<string, string>();
    public void Put(string key,string head){
        headers[key]=head;
    }
}