using System.Threading;

/// <summary>
/// 并发安全的缓存
/// </summary>
public class gloabCache{
    private gloabCache(){

    }
    private static gloabCache _cache;
    public  static gloabCache GetGloabCache(){
        if(_cache==null)_cache=new gloabCache();
        return _cache;
    }
    private const int TIMEOUT=100;
    private Lru lru=new Lru(100);
    private ReaderWriterLock L=new ReaderWriterLock();
    public void PutCache(string key,string val){
        L.AcquireWriterLock(TIMEOUT );
        try{
       //     System.Console.WriteLine("本地节点缓存了"+key+":"+val);
            lru.Put(key,new CacheNode(key,val));
        }finally{ 
            L.ReleaseWriterLock();
        }
    }   
    public string GetCache(string key){
        L.AcquireReaderLock(TIMEOUT);
        try{
            var CacheNode=lru.Get(key);
            return CacheNode!=null?CacheNode.val:"";
        }
        finally{

            L.ReleaseReaderLock();
        }
       
    }

}