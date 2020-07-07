using System.Collections.Generic;

namespace loop
{
    public class Loop
    {
        public LoopEdge loopEdge;
        public List<Node> nodesInLoop; 
         
         
         public Loop(LoopEdge edge){
            loopEdge=edge;
            nodesInLoop=new List<Node>();
            if (loopEdge.fromNode == loopEdge.toNode)
            {
                nodesInLoop.Add(loopEdge.fromNode);
            }
            else
            {
                nodesInLoop.Add(loopEdge.fromNode);
                nodesInLoop.Add(loopEdge.toNode);
            }
            
         }
         
         public void AddNodeToLoop(Node node){
             nodesInLoop.Add(node); 
         }
         
         public bool Contains(Node node){
             if(nodesInLoop.Contains(node))
             {
                return true; 
             }
             return false; 
         }
    }
}