using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;

namespace loop
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("第一行有二个整数：n空格m。n表示图中的顶点数（在控制流图中将所有结点从1到n进行编号），m表示图中的边数。接下来m行，每行用二个整数a空格b，表示一条从结点a到b的有向边。");
            Console.WriteLine("请按以上格式输入数据，不要输入空行或多余空格：");
            string inputInfo = Console.ReadLine();
            int index = inputInfo.IndexOf(" ");
            string nodeNumStr = inputInfo.Substring(0, index);
            string edgeNumStr = inputInfo.Substring(index + 1);
            //string inputInfoTrim = inputInfo.Replace(" ", "");
            int nodeNum = int.Parse(nodeNumStr);
            int edgeNum = int.Parse(edgeNumStr);
            //Console.WriteLine(nodeNum + " " + edgeNum + " 请继续输入:");
            int[,] edge = new int[nodeNum, nodeNum];
            for (int i = 0; i < nodeNum; i++)
            {
                for (int j = 0; j < nodeNum; j++)
                {
                    edge[i, j] = 0;
                }
            }

            for (int i = 0; i < edgeNum; i++)
            {
                string inputEdge = Console.ReadLine();
                int indexOfSpace = inputEdge.IndexOf(" ");
                string startEdge = inputEdge.Substring(0, index);
                string endEdge = inputEdge.Substring(indexOfSpace + 1);
                int row = int.Parse(startEdge);
                int col = int.Parse(endEdge);
                edge[row - 1, col - 1] = 1;
            }

            /*for (int i = 0; i < nodeNum; i++)
            {

                for (int j = 0; j < nodeNum; j++)
                {
                    Console.Write(edge[i, j] + " ");
                }
                Console.WriteLine();
            }*/

            Console.WriteLine();   //准备输出结果
            
            List<Node> nodeList=new List<Node>();

            for (int i = 0; i < nodeNum; i++)
            {
                Node node=new Node(i+1);
                nodeList.Add(node);
            }

            foreach (Node perNode in nodeList)
            {
                for (int j = 0; j < nodeNum; j++)
                {
                    if (edge[perNode.no-1, j] == 1)
                    {
                        perNode.AddNextAndPre(nodeList[j]);
                    }
                }
            }
            
            bool isNeedToLoop = true;

            /*foreach (Node perNode in nodeList)
            {
                Console.Write(perNode.no+": ");
                foreach (Node nodeDom in perNode.preList)
                {
                   Console.Write(nodeDom.no+" "); 
                }
                Console.Write(" |||| ");
                foreach (Node nodeDom in perNode.nextList)
                {
                   Console.Write(nodeDom.no+" "); 
                }
                Console.WriteLine();
            }
            */
            for (int i = 0; i < nodeList.Count; i++)
            {
                if (i ==0)
                {
                    nodeList[0].doms.Add(nodeList[0]);
                }
                else
                {
                    nodeList[i].doms.AddRange(nodeList);
                }
                
            }

            
            while (isNeedToLoop)
            {
                isNeedToLoop = false;
                for (int i = 1; i < nodeList.Count; i++)
                {
                    List<Node> lastDoms = new List<Node>();
                    lastDoms.AddRange(nodeList[i].doms);
                    List<Node> nowDoms = new List<Node>();

                    foreach (Node preNode in nodeList[i].preList)
                    {
                        List<Node> preNodeDoms = preNode.doms;
                        if (nowDoms.Count == 0)
                        {
                            nowDoms.AddRange(preNodeDoms);
                        }
                        else
                        {
                            nowDoms=nowDoms.Intersect(preNodeDoms).ToList();
                        }
                        //Console.WriteLine(node.no);
                    }
                    nowDoms.Add(nodeList[i]);
                    
                    nowDoms=lastDoms.Intersect(nowDoms).ToList();
                    if (lastDoms.Count != nowDoms.Count)
                    {
                        nodeList[i].doms = nowDoms;
                        isNeedToLoop = true;
                    }
                    /*else
                    {
                        Console.WriteLine("false!");
                    }*/
                }
            }

/*
            foreach (Node perNode in nodeList)
            {
                Console.Write(perNode.no+": ");
                foreach (Node nodeDom in perNode.doms)
                {
                   Console.Write(nodeDom.no+" "); 
                }
                Console.WriteLine();
            }

            foreach (Node perNode in nodeList)
            {
                Console.WriteLine(perNode.no+": "+perNode.directDom.no);
                foreach (Node nodeDom in perNode.doms)
                {
                   Console.Write(nodeDom.no+" "); 
                }
                Console.WriteLine();
            }*/
            
            List<LoopEdge> loopEdgeList=new List<LoopEdge>();
            foreach (Node node in nodeList)
            {
                List<Node> nextNodes = new List<Node>();
                nextNodes.AddRange(node.nextList);
                nextNodes = nextNodes.Intersect(node.doms).ToList();
                if (nextNodes.Count > 0)
                {
                    foreach (Node toNode in nextNodes)
                    {
                        LoopEdge loopEdge = new LoopEdge(node, toNode);
                        loopEdgeList.Add(loopEdge);
                    }
                }
            }

            /*foreach (LoopEdge loopEdge in loopEdgeList)
            {
                Console.WriteLine("loopedge:"+loopEdge.fromNode.no+" "+loopEdge.toNode.no);
            }*/

            
            List<Loop> loopList = new List<Loop>();
            foreach (LoopEdge loopEdge in loopEdgeList)
            {
                Stack<Node> goToNodeStack = new Stack<Node>();
                Loop loop = new Loop(loopEdge);
                if (loop.nodesInLoop.Count != 1)
                {
                    goToNodeStack.Push(loopEdge.fromNode);
                    while (goToNodeStack.Count>0)
                    {
                        foreach (Node perNode in goToNodeStack.Pop().preList)
                        {
                            if (!loop.Contains(perNode))
                            {
                                loop.AddNodeToLoop(perNode);
                                goToNodeStack.Push(perNode);
                            }
                        }
                    }
                }
                
                loopList.Add(loop);
            }
            
            

            Console.WriteLine("各结点的支配节点集合：");
            foreach (Node perNode in nodeList)
            {
                Console.Write("D("+perNode.no+")={");
                for (int i=0;i<perNode.doms.Count;i++)
                {
                    if (i==perNode.doms.Count-1)
                    {
                       Console.Write(perNode.doms[i].no);
                    }
                    else
                    {
                       Console.Write(perNode.doms[i].no+", ");  
                    }
                }
                Console.WriteLine("}");
            }

            Console.WriteLine("回边及包含该回边的循环：");
            foreach (Loop perLoop in loopList)
            {
                Console.Write("("+perLoop.loopEdge.fromNode.no+", "+perLoop.loopEdge.toNode.no+")   ");
                Console.Write("{");
                List<Node> nodesInLoopOrderByNo= perLoop.nodesInLoop.OrderBy(t => t.no).ToList();
                for (int i=0;i<nodesInLoopOrderByNo.Count;i++)
                { 
                    if (i==nodesInLoopOrderByNo.Count-1)
                    {
                        Console.Write(nodesInLoopOrderByNo[i].no);
                    }
                    else
                    {
                        Console.Write(nodesInLoopOrderByNo[i].no + ", ");
                    }
                }
                Console.WriteLine("}");
            }

            Console.WriteLine("按任意键结束程序......");
            Console.ReadKey();

        }
        
    }
}