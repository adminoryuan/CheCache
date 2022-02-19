using System.Collections.Generic;


public class Lru{
    private Dictionary<string,CacheNode> map;
    private LinkedList<CacheNode> link;
    private int Cap;
    public Lru(int cap)
    {
        Cap=cap;
        map=new Dictionary<string, CacheNode>();
        link=new LinkedList<CacheNode>();
    }
    public CacheNode Get(string key){
        if(!map.ContainsKey(key)){
            return null;
        }
        Put(key,map[key]);
        return map[key];
    }
    
    public void Put(string key,CacheNode node){
        if(map.ContainsKey(key)){
            link.Remove(node);
        }else if(link.Count==Cap){
            CacheNode nodes=link.Last.Value;
            link.RemoveLast();
            map.Remove(nodes.key);       
        }
         link.AddFirst(node);
         map[key]=node;
    }

}