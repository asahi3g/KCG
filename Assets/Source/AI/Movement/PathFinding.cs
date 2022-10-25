//imports UnityEngine

using System;
using System.Collections.Generic;
using Enums.Tile;
using KMath;
using PlanetTileMap;

namespace AI.Movement
{
    public class PathFinding
    {
        public const int MAX_NUM_NODES = 2048; // Maximum size of open/closed Map.

        readonly PathAdjacency[] directions = new PathAdjacency[8]
            {   new PathAdjacency() { dir = new Vec2i(1, 0),  cost = 100 },   // Right
                new PathAdjacency() { dir = new Vec2i(-1, 0), cost = 100 },   // Left
                new PathAdjacency() { dir = new Vec2i(0, 1),  cost = 100 },   // Up
                new PathAdjacency() { dir = new Vec2i(0, -1), cost = 100 },   // Down
                // Used for flying paths only
                new PathAdjacency() { dir = new Vec2i(1, 1), cost = 144 },    // Up - Right
                new PathAdjacency() { dir = new Vec2i(-1, 1), cost = 144 },   // Up - Left
                new PathAdjacency() { dir = new Vec2i(1, -1), cost = 144 },   // Down - Right
                new PathAdjacency() { dir = new Vec2i(-1, -1), cost = 144 }    // Down - Left
            };

        Node[] openList;
        Node[] closedList;
        Node firstNode;
        int closeListLenght;
        int openListLenght;

        // Used to check if node exists in the list and how many of this node are there.. 
        // Todo: Use a binary grid to do the testing. 
        Dictionary<Vec2i, int> openSet;
        Dictionary<Vec2i, int> closedSet;

        CharacterMovement characterMovement;

        public void Initialize()
        {
            openList = new Node[MAX_NUM_NODES];
            closedList = new Node[MAX_NUM_NODES];
            firstNode = new Node();

            openSet = new Dictionary<Vec2i, int>();
            closedSet = new Dictionary<Vec2i, int>();
            openSet.EnsureCapacity(MAX_NUM_NODES);
            closedSet.EnsureCapacity(MAX_NUM_NODES);
        }

        /// <summary>
        /// Path doesn't guarantee shortest possible path for jump movement Type.
        /// Todo: Look into Jump point search and Hierarchical Pathfinding A* to optimize path 
        /// https://www.researchgate.net/publication/315509846_Simulation_and_Comparison_of_Efficency_in_Pathfinding_algorithms_in_Games
        /// Vec2f[] has closest node at the end of the list.
        /// </summary>
        public Vec2f[] getPath(TileMap tileMap, Vec2f start, Vec2f end, Enums.AgentMovementType movType, CharacterMovement characterMov)
        {
            if (tileMap.GetFrontTileID((int)end.X, (int)end.Y) != TileID.Air)
            {
                UnityEngine.Debug.Log("Not possible path. Endpoint is solid(unreacheable)");
            }

            Passable passable = PassableJump;
            Heuristics.distance heuristics = Heuristics.manhattan_distance;
            int numDirection = 4;
            if (movType == Enums.AgentMovementType.FlyingMovemnt)
            {
                passable = PassableFly;
                heuristics = Heuristics.manhattan_distance;
                numDirection = 8;
            }

            openSet.Clear();
            closedSet.Clear();
            closeListLenght = 0;
            openListLenght = 0;


            characterMovement = characterMov;
            Vec2i startPos = new Vec2i((int)start.X, (int)start.Y);
            Vec2i endPos = new Vec2i((int)end.X, (int)end.Y);

            // Check max distance here.
            SetFirstNode(startPos, endPos, heuristics);
            SetFirstNodeJumpValue(startPos, tileMap);

            // Todo: Profile sorting against searching, Gnomescroll sort the nodes. I am not sure its the fatest way.
            // It's possible that 
            int sortStartPost = 0; // If 0 no sorting needed.
            while (true)
            {
                if (openListLenght >= MAX_NUM_NODES || closeListLenght >= MAX_NUM_NODES)
                {
                    UnityEngine.Debug.Log("The path is taking too long. Giving up.");
                    return null;
                }

                // We failed to find a path if open list is empty.
                if (openListLenght == 0)
                {
                    UnityEngine.Debug.Log("Couldn't find a path to the destination.");
                    return null;
                }

                if (sortStartPost > 0)
                {
                    SortOpenList(sortStartPost);
                    sortStartPost = 0;
                }

                Node current = MoveNodeToCloseList();

                if (current.pos == endPos) // Goal Reached.
                    break;


                for (int i = 0; i < numDirection; i++)
                {
                    Node node = current;
                    node.parentID = current.id;

                    if (!passable(tileMap, ref node, i))
                        continue;

                    node.UpdateCost(endPos, heuristics);

                    // Todo allow have more than one node in the list.
                    int numberOf;
                    if (openSet.TryGetValue(node.pos, out numberOf))
                    {
                        // Todo(Performace): Only search among nodes with cheaper cost.
                        int indexMinCost = -1;
                        int indexMaxHorizontalMovement = -1;

                        bool added = false;
                        for (int index = 0, k = 0; k < numberOf; index++) // Remove all nodes with all attributes worst than node.
                        {
                            if (openList[index].pos == node.pos)
                            {
                                k++;
                                if (node.totalCost < openList[index].totalCost && node.horizontalMovemnt >= openList[index].horizontalMovemnt)
                                {
                                    if (added)
                                    {
                                        RemoveNodeOpenList(index);

                                        if (sortStartPost > index)
                                            sortStartPost = index;

                                        continue;
                                    }
                                    openList[index] = node;
                                    
                                    if (sortStartPost > index)
                                        sortStartPost = index;

                                    added = true;
                                    continue;
                                }

                                if (added)
                                    continue;

                                if (indexMinCost == -1)
                                {
                                    indexMinCost = index;
                                    indexMaxHorizontalMovement = index;
                                }
                                else
                                {
                                    if (openList[indexMinCost].totalCost > openList[index].totalCost)
                                        indexMinCost = index;

                                    if (openList[indexMaxHorizontalMovement].horizontalMovemnt < openList[index].horizontalMovemnt)
                                        indexMaxHorizontalMovement = index;
                                }

                            }
                        }

                        if (added)
                            continue;

                        if (indexMinCost != -1)
                        {
                            if (node.horizontalMovemnt <= openList[indexMinCost].horizontalMovemnt 
                                    && node.totalCost >= openList[indexMinCost].totalCost
                                || (node.horizontalMovemnt <= openList[indexMaxHorizontalMovement].horizontalMovemnt
                                    && node.totalCost >= openList[indexMaxHorizontalMovement].horizontalMovemnt)
                                )
                                continue;
                        }
                    }

                    if (closedSet.TryGetValue(node.pos, out numberOf))
                    {
                        bool betterNodeExists = false;// is better than node.
                        for (int index = 0, k = 0; k < numberOf; index++)
                        {
                            if (closedList[index].pos == node.pos)
                            {
                                k++;
                                if (node.totalCost >= closedList[index].totalCost && node.horizontalMovemnt <= closedList[index].horizontalMovemnt)
                                {
                                    betterNodeExists = true;
                                    continue;
                                }
                            }
                        }
                        if (betterNodeExists)
                            continue;

                    }

                    AddNodeOpenList(ref node);
                    if (sortStartPost == 0)
                        sortStartPost = openListLenght - 1;
                }
            }

            // Todo filter nodes befores returning path.
            return constructPath(end, movType != Enums.AgentMovementType.FlyingMovemnt ? FilterNodeJump : FilterNodeFly);
        }

        Vec2f[] constructPath(Vec2f end, FilterNode filterNode)
        {
            int first = closedList[closeListLenght - 1].id;
            int length = 0;
            int lengthFiltered = 0; // length after filtering.

            Utility.BitSet filterInNodes = new Utility.BitSet((uint)closeListLenght);
            ref Node child = ref closedList[first];
            ref Node node = ref closedList[first];
            ref Node parent = ref closedList[first];

            // Get path lenth.
            while (first >= 0)
            {
                node = ref closedList[first];
                if (node.parentID >= 0)
                    parent = ref closedList[node.parentID];

                // Filter node.
                filterInNodes[length] = filterNode(ref child, ref node, ref parent);
                if (filterInNodes[length])
                    lengthFiltered++;

                first = node.parentID;
                child = ref node;
                length++;
            }

            Vec2f[] path = new Vec2f[lengthFiltered];

            // Add to path array starting form last element.
            first = closedList[closeListLenght - 1].id;
            for (int i = 0, j = 0; i < length; i++)
            {
                if (filterInNodes[i])
                    path[j++] = new Vec2f(closedList[first].pos.X, closedList[first].pos.Y);
                first = closedList[first].parentID;
            }

            //path[0].X = end.X;

            return path;
        }


        bool EmptySpace(TileMap tileMap, ref Node current)
        {
            // Check if tile is inside the map.
            if (current.pos.X < 0 || current.pos.X + characterMovement.Size.X >= tileMap.MapSize.X ||
                current.pos.Y < 0 || current.pos.Y + characterMovement.Size.Y >= tileMap.MapSize.Y)
                return false;

            // If solid return false.
            for (int i = 0; i < characterMovement.Size.X; i++)
            {
                for (int j = 0; j < characterMovement.Size.Y; j++)
                {
                    TileID frontTileID = tileMap.GetFrontTileID((current.pos.X + i), (current.pos.Y + j));
                    if (frontTileID != TileID.Air && frontTileID != TileID.Platform)
                        return false;
                }
            }

            return true;
        }

        delegate bool Passable(TileMap tileMap, ref Node current, int indDir);
        delegate bool FilterNode(ref Node child, ref Node node, ref Node parent);

        bool PassableFly(TileMap tileMap, ref Node current, int indDir)
        {
            current.pos = current.pos + directions[indDir].dir;
            current.pathCost += directions[indDir].cost;

            if (!EmptySpace(tileMap, ref current))
                return false;

            if (indDir > 3) // Diagonals 
            {

                if (indDir > 5) // Diagonals Down
                {
                    TileID frontTileIDY = tileMap.GetFrontTileID(current.pos.X, (current.pos.Y + characterMovement.Size.Y - directions[indDir].dir.Y));
                    if (frontTileIDY != TileID.Air && frontTileIDY != TileID.Platform)
                        return false;
                }
                else  // Diagonals Up
                {
                    TileID frontTileIDY = tileMap.GetFrontTileID(current.pos.X, (current.pos.Y - directions[indDir].dir.Y));
                    if (frontTileIDY != TileID.Air && frontTileIDY != TileID.Platform)
                        return false;
                }
            }

            if (indDir == 5 || indDir == 7) // Left
            {
                TileID frontTileIDX = tileMap.GetFrontTileID((current.pos.X + (characterMovement.Size.X - 1) - directions[indDir].dir.X), current.pos.Y);
                if (frontTileIDX != TileID.Air && frontTileIDX != TileID.Platform)
                    return false;
            }
            else // Right
            {
                TileID frontTileIDX = tileMap.GetFrontTileID((current.pos.X - directions[indDir].dir.X), current.pos.Y);
                if (frontTileIDX != TileID.Air && frontTileIDX != TileID.Platform)
                    return false;
            }


            return true;
        }

        /// <summary>
        /// Check if player can reach the space. 
        /// Return false if tile is either too high or two low. 
        /// Return false if block is solid.
        /// </summary>
        bool PassableJump(TileMap tileMap, ref Node current, int indDir)
        {
            const int HOR_MOV_COST = 100; // How much horizontal movement is necessary to move on tile to the right/left while jumping 
            Int16 maxJump = characterMovement.JumpMaxHeight;
            Int16 maxDown = characterMovement.FallMaxHeight;

            current.pos = current.pos + directions[indDir].dir;
            current.pathCost += directions[indDir].cost;

            if (!EmptySpace(tileMap, ref current))
                return false;

            // Jump and falling paths:
            if (indDir > 1)
            {
                if (current.horizontalMovemnt >= HOR_MOV_COST)
                    current.horizontalMovemnt -= characterMovement.HorizontalRateMovement;

                if (indDir == 2) // UP
                {
                    if (current.jumpValue >= maxJump)
                        return false;
                    
                    if (current.jumpValue == 0)  // Make the path less jumpy.
                        current.pathCost += 250; // Do more test to find ideal value here. (Note)Maybe it should change for each on agent Type?

                    current.jumpValue++;
                }
                else // Dowm
                {
                    // Todo deals with falling speed. When falling speed it too high diagonals may be impossible.
                    if (current.jumpValue >= maxDown)
                        return false;
                    current.jumpValue = (current.jumpValue < maxJump) ? maxJump : current.jumpValue;
                    current.jumpValue++;
                }
                current.horizontalMovemnt += characterMovement.HorizontalRateMovement;

            }
            else
            {
                if (current.jumpValue != 0)
                {
                    if (current.horizontalMovemnt < HOR_MOV_COST && (current.jumpValue > maxJump || current.horizontalMovemnt < 0))
                        return false;
                    current.horizontalMovemnt -= HOR_MOV_COST;
                }
            }


            // Set jump to zero when tile it on ground.
            if (tileMap.GetFrontTileID(current.pos.X, current.pos.Y - 1) != TileID.Air)
            {
                current.jumpValue = 0;
                current.horizontalMovemnt = 0;
            }
            else
            {
                if (current.jumpValue == 0)
                {
                    current.jumpValue = maxJump;
                    current.horizontalMovemnt -= HOR_MOV_COST;
                }
            }

            return true;
        }

                // It's much easier to write a path folowing AI after filtering all unnecessary nodes.
        bool FilterNodeJump(ref Node child, ref Node node, ref Node parent)
        {
            if (child.id == node.id                                                                         // Last node.
                || child.parentID == -1                                                                     // First Node
                || (child.jumpValue != 0 && node.jumpValue == 0)                                            // First jump Node.
                || (node.jumpValue == 0 && parent.jumpValue != 0)                                           // Land Node Add child.
                // Todo: Account for change Direction in mid air.
                ) 
                return true;

            //  Check Jump Highest point.
            if (child.jumpValue != 0 && child.pos.Y >= node.pos.Y || node.jumpValue == 0)
                return false;

            ref Node nextParent = ref parent;
            while (nextParent.parentID != -1) 
            {
                if (node.pos.Y > nextParent.pos.Y || nextParent.jumpValue == 0)
                    return true;

                if (node.pos.Y < nextParent.pos.Y)
                    return false;

                nextParent = ref closedList[nextParent.parentID];
            }

            return false;

        }

        bool FilterNodeFly(ref Node child, ref Node node, ref Node parent)
        {
            if (child.id == node.id                                     // First node.
                || node.id < 0                                          // Last node.
                || (parent.pos - node.pos != node.pos - child.pos))     // Change direction
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Uses insertion sort. It's the fastest algorithm for almost sorted data.
        /// startPos is an optimization. Every element before startPos is sorted.
        /// </summary>
        void SortOpenList(int startPos)
        {
            for (int i = startPos; i < openListLenght; i++)
            {
                Node current = openList[i];
                int j = i - 1;
                while (j >= 0 && current.totalCost > openList[j].totalCost)
                {
                    openList[j + 1] = openList[j];
                    j--;
                }
                openList[j + 1] = current;
            }
        }

        void AddNodeOpenList(ref Node node)
        {
            openList[openListLenght++] = node;
            if (!openSet.TryAdd(node.pos, 1))
                openSet[node.pos]++;
        }

        void RemoveNodeOpenList(int index)
        {
            openList[index] = openList[--openListLenght];
        }

        Node MoveNodeToCloseList()
        {
            Node node = openList[--openListLenght];
            openSet.Remove(node.pos); // Remove only if there is only one of it in the 

            node.id = (Int16)closeListLenght;
            AddNodeCloseList(ref node);

            return node;
        }

        void AddNodeCloseList(ref Node node)
        {
            closedList[closeListLenght++] = node;
            if (!closedSet.TryAdd(node.pos, 1))
                closedSet[node.pos]++;
        }

        void SetFirstNode(Vec2i start, Vec2i end, Heuristics.distance heuristcs)
        {
            firstNode.parentID = -1;
            firstNode.id = 0;
            firstNode.pathCost = 0;
            firstNode.pos = start;
            firstNode.UpdateCost(end, heuristcs);
            AddNodeOpenList(ref firstNode);
        }
        void SetFirstNodeJumpValue(Vec2i start, TileMap tileMap)
        {
            // Is tile on ground.
            if (tileMap.GetFrontTileID(start.X, start.Y - 1) != TileID.Air)
            {
                firstNode.jumpValue = 0;
                return;
            }

            // If its on the ceiling
            if (tileMap.GetFrontTileID(start.X, start.Y + 1) != TileID.Air)
            {
                firstNode.jumpValue = characterMovement.JumpMaxHeight; // Next node needs to be down.
                return;
            }
        }
    }
}
