using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
//分配分布式节点
public  class DistNode{
   private static SortedDictionary<ulong,string> hashd=new SortedDictionary<ulong,string>();
          
   /// <summary>
   /// 添加一个分布式节点
   /// </summary>
   /// <param name="node">节点地址</param>       
   public  void AddNode(string node){
       hashd.Add(GetHash(node),node);
   }
   private  ulong GetHash(string key)
    {            
         key=key.GetHashCode().ToString();
         using (var hash = System.Security.Cryptography.MD5.Create())
            {
                byte[] data = hash.ComputeHash(Encoding.UTF8.GetBytes(key));
                var a = BitConverter.ToUInt64(data, 0);
                var b = BitConverter.ToUInt64(data, 8);
                ulong hashCode = a ^ b;
                return hashCode;
            }
        }

    /// <summary>
    /// 获取要存储值的分布式节点 
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
   public string GetNode(string val){
       return hashd[ModifiedBinarySearch(hashd.Keys.ToArray(),GetHash(val))];
   }  
   public static ulong ModifiedBinarySearch(ulong[] sortedArray, ulong val)
    {

            int min = 0;
            int max = sortedArray.Length - 1;

            if (val < sortedArray[min] || val > sortedArray[max])
                return sortedArray[0];

            while (max - min > 1)
            {
                int mid = (max + min) / 2;
                if (sortedArray[mid] >= val)
                {
                    max = mid;
                }
                else
                {
                    min = mid;
                }
            }

            return sortedArray[max];
        }
   
 
}