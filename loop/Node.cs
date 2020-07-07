using System;
using System.Collections.Generic;


namespace loop
{
    public class Node
    {
        public int no;
        public List<Node> nextList ;
        public List<Node> preList ;
        public List<Node> doms ;

        public Node(int no) {
            this.no = no;
            nextList = new List<Node>();
            preList = new List<Node>();
            doms = new List<Node>();
        }
     
        public void AddNextAndPre(Node n){
            nextList.Add(n);
            n.preList.Add(this);
        }

        /*
        public void DfsTh(int[,] num,int nodeNum,int start,int des)
        {
            
            for (int i = 0; i<nodeNum; i++)
            {
                if (num[start, i] == 1)
                {
                    if (i == des)
                    {
                       ;
                    }
                    else
                    {
                        DfsTh(num,nodeNum,i,des);
                    }
                }
            }
        }
        */
    }
}