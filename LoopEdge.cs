namespace loop
{
    public class LoopEdge
    {
        public Node fromNode;
        public Node toNode;
    
        public LoopEdge(Node fromNode,Node toNode){
            this.fromNode=fromNode;
            this.toNode=toNode;
        }
    }
}