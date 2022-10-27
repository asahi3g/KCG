//import UntiyEngine

using Enums.PlanetTileMap;
using KMath;
using System.Linq;
using System.Collections.Generic;
using Collisions;
using PlanetTileMap;
using Enums;

namespace Planet.Unity
{
    class MovementSceneScript : UnityEngine.MonoBehaviour
    {
        [UnityEngine.SerializeField] UnityEngine.Material Material;



        AgentEntity Player;
        int PlayerID;

        int CharacterSpriteId;
        int inventoryID;

        bool showMechInventory = false;

        private int totalMechs;
        private int selectedMechIndex;
        public Utility.FrameMesh HighliterMesh;
        private UnityEngine.Color wrongHlColor = UnityEngine.Color.red;
        private UnityEngine.Color correctHlColor = UnityEngine.Color.green;


        //KGui.CharacterDisplay CharacterDisplay;

        Vec2f[] Shape1;

        Vec2f[] ShapeTemp;
        Vec2f[] Shape2;

        Vec2f LastMousePosition;

        bool IsShapeColliding = false;

        Vec2f CircleCenter = new Vec2f(12, 12);


        Vec2f CircleVelocity = new Vec2f();

        float CollisionDistance = 0.0f;


        float CircleRadius = 0.5f;
        Vec2f P1 = new Vec2f(15, 15);
        Vec2f P2 = new Vec2f(18, 15);

        Vec2f P3 = new Vec2f(18, 18);

        Vec2f collisionPoint;

        static bool Init = false;

        public void Start()
        {
            if (!Init)
            {

                Initialize();
                Init = true;
            }
        }

        Vec2f rotatePoint(Vec2f pos, float angle) 
        {
            Vec2f newv;
            newv.X = pos.X * System.MathF.Cos(angle) - pos.Y * System.MathF.Sin(angle);
            newv.Y = pos.X * System.MathF.Sin(angle) + pos.Y * System.MathF.Cos(angle);

            return newv;
        }

                // create the sprite atlas for testing purposes
        public void Initialize()
        {

           /* Vec2f slope = P2 - P1;
            float mag = slope.Magnitude;
            P2.X = P1.X;
            P2.Y = P1.Y + mag;*/


            Shape1 = new Vec2f[4];
            ShapeTemp = new Vec2f[4];
            Shape1[0] = new Vec2f(12.0f, 12.0f);
            Shape1[1] = new Vec2f(13.0f, 12.0f);
            Shape1[2] = new Vec2f(13.0f, 13.0f);
            Shape1[3] = new Vec2f(12.0f, 13.0f);

            /*
            float epsilon = 0.01f;
            Shape2 = new Vec2f[5];
            Shape2[0] = new Vec2f(15.0f, 15.0f);
            Shape2[1] = new Vec2f(16.0f, 15.0f);
            Shape2[2] = new Vec2f(16.0f, 16.0f);
            Shape2[3] = new Vec2f(16.0f - epsilon, 16.0f);
            Shape2[4] = new Vec2f(15.0f, 15.0f + epsilon);*/

            Shape2 = new Vec2f[4];

            Shape2[0] = new Vec2f(15.0f, 15.0f);
            Shape2[1] = new Vec2f(16.0f, 15.0f);
            Shape2[2] = new Vec2f(16.0f, 16.0f);
            Shape2[3] = new Vec2f(15.0f, 15.2f);


           // Shape2[3] = new Vec2f(15.0f, 16.0f);


            LastMousePosition = Shape1[0];

            UnityEngine.Application.targetFrameRate = 60;

            GameResources.Initialize();

            // Generating the map
            ref var planet = ref GameState.Planet;
            Vec2i mapSize = new Vec2i(128, 32);
            planet.Init(mapSize);

            int playerFaction = 0;

            Player = planet.AddPlayer(new Vec2f(3.0f, 20), playerFaction);
            PlayerID = Player.agentID.ID;
            inventoryID = Player.agentInventory.InventoryID;

            planet.InitializeSystems(Material, transform);
            planet.InitializeHUD();
            GameState.MechGUIDrawSystem.Initialize();
            //GenerateMap();
            var camera = UnityEngine.Camera.main;
            UnityEngine.Vector3 lookAtPosition = camera.ScreenToWorldPoint(new UnityEngine.Vector3(UnityEngine.Screen.width / 2, UnityEngine.Screen.height / 2, camera.nearClipPlane));

            GenerateMap();

            planet.TileMap.UpdateBackTileMapPositions((int)lookAtPosition.x, (int)lookAtPosition.y);
            planet.TileMap.UpdateMidTileMapPositions((int)lookAtPosition.x, (int)lookAtPosition.y);
            planet.TileMap.UpdateFrontTileMapPositions((int)lookAtPosition.x, (int)lookAtPosition.y);

            AddItemsToPlayer();

            totalMechs = GameState.MechCreationApi.PropertiesArray.Where(m => m.Name != null).Count();

            HighliterMesh = new Utility.FrameMesh("HighliterGameObject", Material, transform,
                GameState.SpriteAtlasManager.GetSpriteAtlas(AtlasType.Generic), 30);



           // CharacterDisplay = new KGui.CharacterDisplay();
            //CharacterDisplay.setPlayer(Player);

          //  UpdateMode(ref Planet, Player);
        }
        Collisions.Box2D otherBox = new Box2D {x = 7, y = 21, w = 1.0f, h = 1.0f};
        Collisions.Box2D orrectedBox = new Box2D {x = 0, y = 17, w = 1.0f, h = 1.0f};

        public void Update()
        {

            if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.LeftArrow))
            {
                CircleCenter.X -= 1.0f;
                for( int i = 0; i < Shape1.Length; i++)
                {
                    Shape1[i].X -= 1.0f;
                }
            }

            if(UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.RightArrow))
            {
                CircleCenter.X += 1.0f;
                for( int i = 0; i < Shape1.Length; i++)
                {
                    Shape1[i].X += 1.0f;
                }
            }

            if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.UpArrow))
            {
                CircleCenter.Y += 1.0f;
                for( int i = 0; i < Shape1.Length; i++)
                {
                    Shape1[i].Y += 1.0f;
                }
            }

            if(UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.DownArrow))
            {
                CircleCenter.Y -= 1.0f;
                for( int i = 0; i < Shape1.Length; i++)
                {
                    Shape1[i].Y -= 1.0f;
                }
            }

            UnityEngine.Vector3 p = UnityEngine.Input.mousePosition;
            p.z = 20;
            UnityEngine.Vector3 mouse = UnityEngine.Camera.main.ScreenToWorldPoint(p);

            Vec2f shapeVelocity = new Vec2f(mouse.x, mouse.y) - LastMousePosition;
            LastMousePosition = new Vec2f(mouse.x, mouse.y);

            bool sat = true;
            if (sat)
            {
                for( int i = 0; i < Shape1.Length; i++)
                {
                    Shape1[i] += shapeVelocity;
                    ShapeTemp[i] = Shape1[i];
                }

           
                Collisions.MinimumMagnitudeVector mtv = Collisions.SAT.CollisionDetection(Shape1, Shape2);
                if (mtv.Value > 0.0f)
                {
                    for( int i = 0; i < Shape1.Length; i++)
                    {
                        Shape1[i] -= shapeVelocity.Normalize() * mtv.Value;
                    }
                }
            }
            else
            {
                var result = Collisions.PolygonSweepTest.TestCollision(Shape1, shapeVelocity, Shape2);
                float distance = result.CollisionTime;
                UnityEngine.Debug.Log(distance);
                UnityEngine.Debug.Log(result.CollisionNormal);

                Vec2f shape1Center = new Vec2f();
                Vec2f shape2Center = new Vec2f();

                for( int i = 0; i < Shape1.Length; i++)
                {
                    shape1Center += Shape1[i] / (float)Shape1.Length;
                }
                
                for( int i = 0; i < Shape2.Length; i++)
                {
                    shape2Center += Shape2[i] / (float)Shape2.Length;
                }

                 for( int i = 0; i < Shape1.Length; i++)
                {
                    if (distance == 1.0f)
                    {
                        ShapeTemp[i] = Shape1[i] + shapeVelocity * distance;
                        Shape1[i] = Shape1[i] + shapeVelocity * distance;
                    }
                    else
                    {
                        ShapeTemp[i] = Shape1[i] + shapeVelocity * distance;
                        Shape1[i] = Shape1[i] + shapeVelocity * distance;

                       // ShapeTemp[i] = Shape1[i] + new Vec2f(Mathf.Abs(shapeVelocity.X), Mathf.Abs(shapeVelocity.Y)) * (1.0f - distance) * result.CollisionNormal;
                       // Shape1[i] = Shape1[i] + new Vec2f(Mathf.Abs(shapeVelocity.X), Mathf.Abs(shapeVelocity.Y)) * (1.0f - distance) * result.CollisionNormal;

                        ShapeTemp[i] -= shapeVelocity * 0.01f;
                        Shape1[i] -= shapeVelocity * 0.01f;

                        Vec2f reflectVelocity = shapeVelocity - 1.0f * Vec2f.Dot(shapeVelocity, result.CollisionNormal) * result.CollisionNormal;

                        ShapeTemp[i] = Shape1[i] + reflectVelocity * (1.0f - distance);
                        Shape1[i] = Shape1[i] + reflectVelocity * (1.0f - distance);

     
                    }
                }
            }

            LastMousePosition = Shape1[0];


            CircleVelocity = new Vec2f(mouse.x, mouse.y) - CircleCenter;


            var circleCollisionResult = Collisions.CirclePolygonSweepTest.TestCollision(CircleCenter, CircleRadius, CircleVelocity, Shape2);
          /*  var rs1 = Collisions.CircleLineCollision.TestCollision(CircleCenter, CircleRadius, CircleVelocity, P1 + 0.001f, P2 - 0.001f);
            var rs2 = Collisions.CircleLineCollision.TestCollision(CircleCenter, CircleRadius, CircleVelocity, P2 + 0.001f, P3 - 0.001f);
            var rs3 = Collisions.CircleLineCollision.TestCollision(CircleCenter, CircleRadius, CircleVelocity, P3 + 0.001f, P1 - 0.001f);


            float minTime = 1.0f;
            Vec2f minNormal = new Vec2f();
            float minCollisionTime = 1.0f;
            if (rs1.Time < rs2.Time)
            {
                if (rs1.Time < rs3.Time)
                {
                    minTime = rs1.Time;
                    minNormal = rs1.Normal;
                }
                else
                {
                    minTime = rs3.Time;
                    minNormal = rs3.Normal;
                }
            }
            else
            {
                if (rs2.Time < rs3.Time)
                {
                    minTime = rs2.Time;
                    minNormal = rs2.Normal;
                }
                else
                {
                    minTime = rs3.Time;
                    minNormal = rs3.Normal;
                }
            }
           // CollisionDistance = Mathf.Min(rs1.Time, Mathf.Min(rs2.Time, rs3.Time));

           Debug.Log("normal 1" + rs1.Normal);
           Debug.Log("normal 2" + rs2.Normal);
           Debug.Log("normal 3" + rs3.Normal);*/

           CollisionDistance = circleCollisionResult.CollisionTime;

            //CollisionDistance -= 0.01f;

            //collisionPoint = CircleCenter + CircleVelocity * CollisionDistance;

            CircleCenter = CircleCenter + CircleVelocity * CollisionDistance;

            //minNormal = (collisionPoint - CircleCenter).Normalize();

            CircleCenter = CircleCenter - CircleVelocity * 0.01f;

            UnityEngine.Debug.Log("normal " + circleCollisionResult.CollisionNormal);



            Vec2f refl = CircleVelocity - 1.0f * Vec2f.Dot(CircleVelocity, circleCollisionResult.CollisionNormal) * circleCollisionResult.CollisionNormal;

            CircleCenter = CircleCenter + refl * (1.0f - CollisionDistance);

            //distance1 = Collisions.CircleLineCollision.TestCollision(CircleCenter + new Vec2f(1.0f, 0.0f), CircleRadius, CircleVelocity, P1, P2);
           // distance2 = Collisions.CircleLineCollision.TestCollision(CircleCenter + new Vec2f(1.0f, 0.0f), CircleRadius, CircleVelocity, P3, P4);


            //CollisionDistance = Mathf.Min(CollisionDistance, Mathf.Min(distance1, distance2));

            //distance1 = Collisions.CircleLineCollision.TestCollision(P1, CircleRadius, -CircleVelocity, CircleCenter, CircleCenter + new Vec2f(1.0f, 0.0f));;
           // distance2 = Collisions.CircleLineCollision.TestCollision(P2, CircleRadius, -CircleVelocity, CircleCenter, CircleCenter + new Vec2f(1.0f, 0.0f));
          //  CollisionDistance = Mathf.Min(CollisionDistance, Mathf.Min(distance1, distance1));

           /* distance1 = 1.0f - Collisions.CircleLineCollision.TestCollision(P3, CircleRadius, -CircleVelocity, CircleCenter, CircleCenter + new Vec2f(1.0f, 0.0f));
            distance2 = 1.0f - Collisions.CircleLineCollision.TestCollision(P4, CircleRadius, -CircleVelocity, CircleCenter, CircleCenter + new Vec2f(1.0f, 0.0f));
            CollisionDistance = Mathf.Min(CollisionDistance, Mathf.Min(distance1, distance2));*/




            /*CollisionDistance = rs.CollisionTime;
            NewPos = rs.NewPos;

            CircleA = CircleCenter + new Vec2f (CircleVelocity.Y, -CircleVelocity.X).Normalize() * CircleRadius;
            CircleB = CircleCenter - new Vec2f (CircleVelocity.Y, -CircleVelocity.X).Normalize() * CircleRadius;

            Line2D Line = new Line2D(P1, P2);
            Line2D LineA = new Line2D(CircleA, CircleA + CircleVelocity * 10000.0f);
            Line2D LineB = new Line2D(CircleB, CircleB + CircleVelocity * 10000.0f);

            if (!Line.Intersects(LineB) && !Line.Intersects(LineA))
            {
                CollisionDistance = 1.0f;
            }

            ImpactPoint = CircleCenter + CircleVelocity * rs.ImpactTime;*/






            
            var playerPhysicsState = Player.agentPhysicsState;
            Vec2f playerPosition = playerPhysicsState.Position;
            var playerCollider = Player.physicsBox2DCollider;

            orrectedBox.w = playerCollider.Size.X;
            orrectedBox.h = playerCollider.Size.Y;

            
            Vec2f velocity = new Vec2f(mouse.x - orrectedBox.x, mouse.y - orrectedBox.y);
            Collisions.Collisions.SweptBox2dCollision(ref orrectedBox, velocity, otherBox, false);




            ref var planet = ref GameState.Planet;
            ref var tileMap = ref planet.TileMap;
            UnityEngine.Material material = Material;

            if(UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.RightArrow))
            {
                selectedMechIndex++;
            } else if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.LeftArrow))
            {
                selectedMechIndex--;
            }

            selectedMechIndex = UnityEngine.Mathf.Clamp(selectedMechIndex, 0, totalMechs);

            if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.F1))
            {
                PlanetManager.Save(planet, "generated-maps/movement-map.kmap");
                UnityEngine.Debug.Log("saved!");
            }

            if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.F2))
            {
                var camera = UnityEngine.Camera.main;
                UnityEngine.Vector3 lookAtPosition = camera.ScreenToWorldPoint(new UnityEngine.Vector3(UnityEngine.Screen.width / 2, UnityEngine.Screen.height / 2, camera.nearClipPlane));

                planet.Destroy();
                planet = PlanetManager.Load("generated-maps/movement-map.kmap", (int)lookAtPosition.x, (int)lookAtPosition.y);
                planet.TileMap.UpdateBackTileMapPositions((int)lookAtPosition.x, (int)lookAtPosition.y);
                planet.TileMap.UpdateMidTileMapPositions((int)lookAtPosition.x, (int)lookAtPosition.y);
                planet.TileMap.UpdateFrontTileMapPositions((int)lookAtPosition.x, (int)lookAtPosition.y);


               planet.InitializeSystems(Material, transform);
               planet.InitializeHUD();
               GameState.MechGUIDrawSystem.Initialize();

               Player = planet.AddPlayer(new Vec2f(3.0f, 20));
               PlayerID = Player.agentID.ID;
               inventoryID = Player.agentInventory.InventoryID;

               AddItemsToPlayer();

                UnityEngine.Debug.Log("loaded!");
            }


           // CharacterDisplay.Update();
            planet.Update(UnityEngine.Time.deltaTime, Material, transform);

        }
        UnityEngine.Texture2D texture;

        private void OnGUI()
        {
            if (!Init)
                return;

            GameState.Planet.DrawHUD(Player); 
            GameState.MechGUIDrawSystem.Draw(Player);

            if (showMechInventory)
            {
                DrawCurrentMechHighlighter();
            }

            
          //  CharacterDisplay.Draw();

                 
        }

        private void DrawQuad(UnityEngine.GameObject gameObject, float x, float y, float w, float h, UnityEngine.Color color)
        {
            var mr = gameObject.GetComponent<UnityEngine.MeshRenderer>();
            mr.sharedMaterial.color = color;

            var mf = gameObject.GetComponent<UnityEngine.MeshFilter>();
            var mesh = mf.sharedMesh;

            List<int> triangles = new List<int>();
            List<UnityEngine.Vector3> vertices = new List<UnityEngine.Vector3>();

            Vec2f topLeft = new Vec2f(x, y + h);
            Vec2f BottomLeft = new Vec2f(x, y);
            Vec2f BottomRight = new Vec2f(x + w, y);
            Vec2f TopRight = new Vec2f(x + w, y + h);

            var p0 = new UnityEngine.Vector3(BottomLeft.X, BottomLeft.Y, 0);
            var p1 = new UnityEngine.Vector3(TopRight.X, TopRight.Y, 0);
            var p2 = new UnityEngine.Vector3(topLeft.X, topLeft.Y, 0);
            var p3 = new UnityEngine.Vector3(BottomRight.X, BottomRight.Y, 0);

            vertices.Add(p0);
            vertices.Add(p1);
            vertices.Add(p2);
            vertices.Add(p3);

            triangles.Add(vertices.Count - 4);
            triangles.Add(vertices.Count - 2);
            triangles.Add(vertices.Count - 3);
            triangles.Add(vertices.Count - 4);
            triangles.Add(vertices.Count - 3);
            triangles.Add(vertices.Count - 1);

            mesh.SetVertices(vertices);
            mesh.SetTriangles(triangles.ToArray(), 0);
        }

        private void DrawCurrentMechHighlighter()
        {
            UnityEngine.Vector3 worldPosition = UnityEngine.Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            int x = (int)worldPosition.x;
            int y = (int)worldPosition.y;

            ref var planet = ref GameState.Planet;
            //var viewportPos = Camera.main.WorldToViewportPoint(new Vector3(x, y));

            if (x >= 0 && x < planet.TileMap.MapSize.X &&
            y >= 0 && y < planet.TileMap.MapSize.Y)
            {
                //TODO: SET TO Get(selectedMechIndex)
                var mech = GameState.MechCreationApi.Get((MechType)selectedMechIndex);
                var xRange = UnityEngine.Mathf.CeilToInt(mech.SpriteSize.X);
                var yRange = UnityEngine.Mathf.CeilToInt(mech.SpriteSize.Y);

                var allTilesAir = true;

                var w = mech.SpriteSize.X;
                var h = mech.SpriteSize.Y;

                for (int i = 0; i < xRange; i++)
                {
                    for (int j = 0; j < yRange; j++)
                    {
                        if (planet.TileMap.GetMidTileID(x + i, y + j) != TileID.Air)
                        {
                            allTilesAir = false;
                            DrawQuad(HighliterMesh.obj, x, y, w, h, wrongHlColor);
                            break;
                        }
                        if (planet.TileMap.GetFrontTileID(x + i, y + j) != TileID.Air)
                        {
                            allTilesAir = false;
                            DrawQuad(HighliterMesh.obj, x, y, w, h, wrongHlColor);
                            break;
                        }
                    }

                    if (!allTilesAir)
                        break;
                }

                if (allTilesAir)
                {
                    DrawQuad(HighliterMesh.obj, x, y, w, h, correctHlColor);
                }

            }
        }

    


        private void OnDrawGizmos()
        {
            ref var planet = ref GameState.Planet;
            planet.DrawDebug();

            // Set the color of gizmos
            UnityEngine.Gizmos.color = UnityEngine.Color.green;
            
            // Draw a cube around the map
            if(planet.TileMap != null)
                UnityEngine.Gizmos.DrawWireCube(UnityEngine.Vector3.zero, new UnityEngine.Vector3(planet.TileMap.MapSize.X, planet.TileMap.MapSize.Y, 0.0f));

            // Draw lines around player if out of bounds
            if (Player != null)
                if(Player.agentPhysicsState.Position.X -10.0f >= planet.TileMap.MapSize.X)
                {
                    // Out of bounds

                    // X+
                    UnityEngine.Gizmos.DrawLine(new UnityEngine.Vector3(Player.agentPhysicsState.Position.X, Player.agentPhysicsState.Position.Y, 0.0f), new UnityEngine.Vector3(Player.agentPhysicsState.Position.X + 10.0f, Player.agentPhysicsState.Position.Y));

                    // X-
                    UnityEngine.Gizmos.DrawLine(new UnityEngine.Vector3(Player.agentPhysicsState.Position.X, Player.agentPhysicsState.Position.Y, 0.0f), new UnityEngine.Vector3(Player.agentPhysicsState.Position.X - 10.0f, Player.agentPhysicsState.Position.Y));

                    // Y+
                    UnityEngine.Gizmos.DrawLine(new UnityEngine.Vector3(Player.agentPhysicsState.Position.X, Player.agentPhysicsState.Position.Y, 0.0f), new UnityEngine.Vector3(Player.agentPhysicsState.Position.X, Player.agentPhysicsState.Position.Y + 10.0f));

                    // Y-
                    UnityEngine.Gizmos.DrawLine(new UnityEngine.Vector3(Player.agentPhysicsState.Position.X, Player.agentPhysicsState.Position.Y, 0.0f), new UnityEngine.Vector3(Player.agentPhysicsState.Position.X, Player.agentPhysicsState.Position.Y - 10.0f));
                }

            // Draw Chunk Visualizer
            ChunkVisualizer.Draw(0.5f, 0.0f);


            bool drawRayCast = false;


            if (drawRayCast)
            {
                UnityEngine.Vector3 p = UnityEngine.Input.mousePosition;
                p.z = 20;
                UnityEngine.Vector3 mouse = UnityEngine.Camera.main.ScreenToWorldPoint(p);

                var rayCastResult = Collisions.Collisions.RayCastAgainstTileMap(new Line2D(Player.agentPhysicsState.Position, new Vec2f(mouse.x, mouse.y)));
                
                Vec2f startPos = Player.agentPhysicsState.Position;
                Vec2f endPos = new Vec2f(mouse.x, mouse.y);
                UnityEngine.Gizmos.DrawLine(new UnityEngine.Vector3(startPos.X, startPos.Y, 20), new UnityEngine.Vector3(endPos.X, endPos.Y, 20));

                if (rayCastResult.Intersect)
                {
                    UnityEngine.Gizmos.DrawWireCube(new UnityEngine.Vector3(rayCastResult.Point.X, rayCastResult.Point.Y, 20),
                    new UnityEngine.Vector3(0.3f, 0.3f, 0.3f));
                }
            }

            
            bool testCircleCollision = false;
            bool testRectangleCollision = false;


            UnityEngine.Vector3 worldPosition = UnityEngine.Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            worldPosition.z = 20.0f;

            if (testCircleCollision)
            {
                int[] agentIds = Collisions.Collisions.BroadphaseAgentCircleTest(new Vec2f(worldPosition.x, worldPosition.y), 0.5f);

                UnityEngine.Gizmos.color = UnityEngine.Color.yellow;
                if (agentIds != null && agentIds.Length > 0)
                {
                    UnityEngine.Gizmos.color = UnityEngine.Color.red;
                }

                UnityEngine.Gizmos.DrawSphere(worldPosition, 0.5f);
            }

            if (testRectangleCollision)
            {
                int[] agentIds = Collisions.Collisions.BroadphaseAgentBoxTest(new AABox2D(new Vec2f(worldPosition.x, worldPosition.y), new Vec2f(0.5f, 0.75f)));

                UnityEngine.Gizmos.color = UnityEngine.Color.yellow;
                if (agentIds != null && agentIds.Length > 0)
                {
                    UnityEngine.Gizmos.color = UnityEngine.Color.red;
                }

                UnityEngine.Gizmos.DrawWireCube(worldPosition + new UnityEngine.Vector3(0.25f, 0.75f * 0.5f, 0), new UnityEngine.Vector3(0.5f, 0.75f, 0.5f));
            }

            bool testRayAgainstCircle = false;

            if (testRayAgainstCircle)
            {
                UnityEngine.Vector3 p = UnityEngine.Input.mousePosition;
                p.z = 20;
                UnityEngine.Vector3 mouse = UnityEngine.Camera.main.ScreenToWorldPoint(p);

                var rayCastResult = Collisions.Collisions.RayCastAgainstCircle(new Line2D(Player.agentPhysicsState.Position, new Vec2f(mouse.x, mouse.y)),
                 new Vec2f(9, 19), 1.0f);

                Vec2f startPos = Player.agentPhysicsState.Position;
                Vec2f endPos = new Vec2f(mouse.x, mouse.y);
                UnityEngine.Gizmos.DrawLine(new UnityEngine.Vector3(startPos.X, startPos.Y, 20), new UnityEngine.Vector3(endPos.X, endPos.Y, 20));

                UnityEngine.Gizmos.color = UnityEngine.Color.yellow;
                if (rayCastResult.Intersect)
                {
                    UnityEngine.Gizmos.DrawWireCube(new UnityEngine.Vector3(rayCastResult.Point.X, rayCastResult.Point.Y, 20),
                    new UnityEngine.Vector3(0.3f, 0.3f, 0.3f));

                    UnityEngine.Gizmos.color = UnityEngine.Color.red;
                }

                UnityEngine.Gizmos.DrawSphere(new UnityEngine.Vector3(9, 19, 20.0f), 1.0f);
            }


            bool testSweptCollision = false;
            if (testSweptCollision)
            {
                //var playerCollider = Player.physicsBox2DCollider;
                UnityEngine.Gizmos.color = UnityEngine.Color.red;
                UnityEngine.Gizmos.DrawWireCube(new UnityEngine.Vector3(otherBox.x, otherBox.y, 1.0f), new UnityEngine.Vector3(otherBox.w, otherBox.h, 0.5f));
                UnityEngine.Gizmos.color = UnityEngine.Color.green;
                UnityEngine.Gizmos.DrawWireCube(new UnityEngine.Vector3(orrectedBox.x, orrectedBox.y, 1.0f), new UnityEngine.Vector3(orrectedBox.w, orrectedBox.h, 0.5f));
            }


            bool shapeCollision = false;

            if (shapeCollision)
            {
                UnityEngine.Gizmos.color = UnityEngine.Color.green;
                if (IsShapeColliding)
                {
                    UnityEngine.Gizmos.color = UnityEngine.Color.red;
                }

                for(int i = 0; i < Shape1.Length; i++)
                {
                    int nextPosition = i + 1;
                    if (i == Shape1.Length - 1)
                    {
                        nextPosition = 0;
                    }
                    UnityEngine.Gizmos.color = UnityEngine.Color.green;
                    UnityEngine.Gizmos.DrawLine(new UnityEngine.Vector3(Shape1[i].X, Shape1[i].Y, 1), new UnityEngine.Vector3(Shape1[nextPosition].X, Shape1[nextPosition].Y, 1));

                    UnityEngine.Gizmos.color = UnityEngine.Color.red;
                    UnityEngine.Gizmos.DrawLine(new UnityEngine.Vector3(ShapeTemp[i].X, ShapeTemp[i].Y, 1), new UnityEngine.Vector3(ShapeTemp[nextPosition].X, ShapeTemp[nextPosition].Y, 1));
                }

                UnityEngine.Gizmos.color = UnityEngine.Color.green;

                for(int i = 0; i < Shape2.Length; i++)
                {
                    int nextPosition = i + 1;
                    if (i == Shape2.Length - 1)
                    {
                        nextPosition = 0;
                    }
                    UnityEngine.Gizmos.DrawLine(new UnityEngine.Vector3(Shape2[i].X, Shape2[i].Y, 1), new UnityEngine.Vector3(Shape2[nextPosition].X, Shape2[nextPosition].Y, 1));
                }
                
                
            }


            bool circleLineCollision = true;

            if (circleLineCollision)
            {
                UnityEngine.Gizmos.color = UnityEngine.Color.red;
                UnityEngine.Gizmos.DrawLine(new UnityEngine.Vector3(P1.X, P1.Y + 100, 1), new UnityEngine.Vector3(P2.X, P2.Y - 100, 1));
                UnityEngine.Gizmos.color = UnityEngine.Color.green;
                /* Gizmos.DrawLine(new Vector3(P1.X, P1.Y, 1), new Vector3(P2.X, P2.Y, 1));
                 Gizmos.DrawLine(new Vector3(P2.X, P2.Y, 1), new Vector3(P3.X, P3.Y, 1));
                 Gizmos.DrawLine(new Vector3(P3.X, P3.Y, 1), new Vector3(P1.X, P1.Y, 1));*/

                UnityEngine.Gizmos.color = UnityEngine.Color.green;

                for(int i = 0; i < Shape2.Length; i++)
                {
                    int nextPosition = i + 1;
                    if (i == Shape2.Length - 1)
                    {
                        nextPosition = 0;
                    }
                    UnityEngine.Gizmos.DrawLine(new UnityEngine.Vector3(Shape2[i].X, Shape2[i].Y, 1), new UnityEngine.Vector3(Shape2[nextPosition].X, Shape2[nextPosition].Y, 1));
                }
                
                UnityEngine.Gizmos.DrawSphere(new UnityEngine.Vector3(CircleCenter.X, CircleCenter.Y, 1.0f), CircleRadius);
                UnityEngine.Gizmos.DrawSphere(new UnityEngine.Vector3(collisionPoint.X, collisionPoint.Y, 1.0f), 0.2f);

                UnityEngine.Gizmos.DrawLine(new UnityEngine.Vector3(CircleCenter.X, CircleCenter.Y, 1), new UnityEngine.Vector3(CircleCenter.X + 1.0f, CircleCenter.Y, 1));

                UnityEngine.Gizmos.color = UnityEngine.Color.blue;
               
                UnityEngine.Gizmos.color = UnityEngine.Color.red;
                UnityEngine.Gizmos.DrawLine(new UnityEngine.Vector3(CircleCenter.X, CircleCenter.Y, 1.0f), new UnityEngine.Vector3(CircleCenter.X + CircleVelocity.X, CircleCenter.Y + CircleVelocity.Y, 1.0f));

                /*if (CollisionDistance < 1.0f)
                {*/
                    UnityEngine.Gizmos.color = UnityEngine.Color.green;
                UnityEngine.Gizmos.DrawLine(new UnityEngine.Vector3(CircleCenter.X, CircleCenter.Y, 1.0f), new UnityEngine.Vector3(CircleCenter.X + CircleVelocity.X * CollisionDistance, CircleCenter.Y + CircleVelocity.Y * CollisionDistance, 1.0f));
                //Gizmos.DrawSphere(new Vector3(CircleCenter.X + CircleVelocity.X * CollisionDistance, CircleCenter.Y + CircleVelocity.Y * CollisionDistance, 1.0f), CircleRadius + 0.05f);


                //   Gizmos.DrawSphere(new Vector3(CircleCenter.X + 1.0f + CircleVelocity.X * CollisionDistance, CircleCenter.Y + CircleVelocity.Y * CollisionDistance, 1.0f), CircleRadius + 0.05f);
                //   Gizmos.DrawLine(new Vector3(CircleCenter.X + CircleVelocity.X * CollisionDistance, CircleCenter.Y + CircleVelocity.Y * CollisionDistance, 1),
                //    new Vector3(CircleCenter.X + 1.0f + + CircleVelocity.X * CollisionDistance, CircleCenter.Y + CircleVelocity.Y * CollisionDistance, 1));
                //
                UnityEngine.Gizmos.color = UnityEngine.Color.yellow;
                   
               // }


            }
        }


        public void AddItemsToPlayer()
        {
            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, ItemType.PlacementTool);
            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, ItemType.RemoveTileTool);
            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, ItemType.SpawnEnemyGunnerTool);
            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, ItemType.SpawnEnemySwordmanTool);
            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, ItemType.ConstructionTool);
            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, ItemType.RemoveMech);
            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, ItemType.HealthPositon);
            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, ItemType.SMG);
            Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, ItemType.Pistol);
            //Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.Sword);
           // Admin.AdminAPI.AddItem(GameState.InventoryManager, inventoryID, Enums.ItemType.FragGrenade);

        }



        void GenerateMap()
        {
            KMath.Random.Mt19937.init_genrand((ulong) System.DateTime.Now.Ticks);
            
            ref var planet = ref GameState.Planet;
            
            ref var tileMap = ref planet.TileMap;

            
            for (int j = 0; j < tileMap.MapSize.Y / 2; j++)
            {
                for(int i = 0; i < tileMap.MapSize.X; i++)
                {
                    tileMap.GetTile(i, j).FrontTileID = TileID.Moon;
                    tileMap.GetTile(i, j).BackTileID = TileID.Background;
                }
            }

            

            for(int i = 0; i < tileMap.MapSize.X; i++)
            {
                tileMap.GetTile(i, 0).FrontTileID =  TileID.Bedrock;
                tileMap.GetTile(i, tileMap.MapSize.Y - 1).FrontTileID = TileID.Bedrock;
            }

            for(int j = 0; j < tileMap.MapSize.Y; j++)
            {
                tileMap.GetTile(0, j).FrontTileID = TileID.Bedrock;
                tileMap.GetTile(tileMap.MapSize.X - 1, j).FrontTileID = TileID.Bedrock;
            }

            tileMap.GetTile(8, 18).FrontTileID = TileID.Platform;
            tileMap.GetTile(9, 18).FrontTileID = TileID.Platform;
            tileMap.GetTile(10, 18).FrontTileID = TileID.Platform;
            tileMap.GetTile(11, 18).FrontTileID = TileID.Platform;
            tileMap.GetTile(12, 18).FrontTileID = TileID.Platform;
            tileMap.GetTile(13, 18).FrontTileID = TileID.Platform;

            tileMap.GetTile(12, 21).FrontTileID = TileID.Platform;
            tileMap.GetTile(13, 21).FrontTileID = TileID.Platform;
            tileMap.GetTile(14, 21).FrontTileID = TileID.Platform;

            tileMap.GetTile(14, 24).FrontTileID = TileID.Platform;
            tileMap.GetTile(15, 24).FrontTileID = TileID.Platform;
            tileMap.GetTile(16, 24).FrontTileID = TileID.Platform;


            tileMap.GetTile(19, 24).FrontTileID = TileID.Platform;
            tileMap.GetTile(20, 24).FrontTileID = TileID.Platform;
            tileMap.GetTile(21, 24).FrontTileID = TileID.Platform;



            tileMap.GetTile(26, 26).FrontTileID = TileID.Platform;

            tileMap.GetTile(29, 26).FrontTileID = TileID.Platform;

            tileMap.GetTile(32, 26).FrontTileID = TileID.Platform;

            tileMap.GetTile(36, 26).FrontTileID = TileID.Platform;

            tileMap.GetTile(40, 26).FrontTileID = TileID.Platform;


            tileMap.GetTile(16, 26).FrontTileID = TileID.Platform;


            tileMap.GetTile(12, 27).FrontTileID = TileID.Platform;

            tileMap.GetTile(8, 27).FrontTileID = TileID.Platform;
            tileMap.GetTile(7, 27).FrontTileID = TileID.Platform;



            for(int i = 0; i < 5; i++)
            {
                tileMap.GetTile(20, i + 16).FrontTileID = TileID.Moon;
            }

            for(int i = 0; i < 10; i++)
            {
                tileMap.GetTile(24, i + 16).FrontTileID = TileID.Moon;
            }


            tileMap.GetTile(26, 21).FrontTileID = TileID.Moon;
            tileMap.GetTile(27, 21).FrontTileID = TileID.Moon;
            tileMap.GetTile(26, 22).FrontTileID = TileID.Moon;
            tileMap.GetTile(27, 22).FrontTileID = TileID.Moon;
        }

            private void UpdateMode(AgentEntity agentEntity)
        {
            ref var planet = ref GameState.Planet;
            agentEntity.agentPhysicsState.Invulnerable = false;
            UnityEngine.Camera.main.gameObject.GetComponent<CameraMove>().enabled = false;
            planet.CameraFollow.canFollow = false;

            UnityEngine.Camera.main.gameObject.GetComponent<CameraMove>().enabled = false;
            planet.CameraFollow.canFollow = true;
        }

    }

}
