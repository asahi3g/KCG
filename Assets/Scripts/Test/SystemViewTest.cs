using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Collections.Generic;
using Source.SystemView;

namespace Scripts {
    namespace SystemView {

        public class SystemViewTest : MonoBehaviour {
            public SystemState       state;

            private float            LastTime;
            public System.Random     rnd;

            public  int              seed;

            public  int              StarCount        =                    1;
            public  int              InnerPlanets     =                    4;
            public  int              OuterPlanets     =                    6;
            public  int              FarOrbitPlanets  =                    2;
            public  int              SpaceStations    =                    0;

            public  float            system_scale     =                25.0f;

            public  float            SunMass          = 50000000000000000.0f;
            public  float            PlanetMass       =   100000000000000.0f;
            public  float            MoonMass         =    20000000000000.0f;
            public  float            StationMass      =        1000000000.0f;

            public  float            time_scale       =                 1.0f;

            public  float            acceleration     =               250.0f;
            public  float            drag_factor      =             10000.0f;
            public  float            sailing_factor   =                20.0f;

#pragma warning disable CS0414
            private float            CachedSunMass    = 50000000000000000.0f;

            private float            CachedPlanetMass =   100000000000000.0f;
            private float            CachedMoonMass   =    20000000000000.0f;

            public  bool             TrackingPlayer   =                false;
            public  bool             planet_movement  =                 true;
            public  bool             n_body_gravity   =                 true;

            public  ComputeShader    blur_noise_shader;
            public  ComputeShader    scale_noise_shader;
            public  ComputeShader    exponential_filter_shader;
            public  ComputeShader    distortion_shader;
            public  ComputeShader    circular_blur_shader;
            public  ComputeShader    circular_mask_shader;
            public  ComputeShader    pixelate_shader;
#pragma warning restore CS0414

            public  bool             pixelate;
            public  bool             smoothstepped_colors;
            public  int              pixelation_size;

            public  Color            rocky_planet_base_color;
            public  Color            gas_giant_base_color;
            public  Color            moon_base_color;

            public  Color[]          rocky_planet_colors;
            public  Color[]          gas_giant_colors;
            public  Color[]          moon_colors;

            public  int              rocky_planet_radius;
            public  int              gas_giant_radius;
            public  int              moon_radius;

            public  int              rocky_planet_layers;
            public  int              gas_giant_layers;
            public  int              moon_layers;

            public  CameraController Camera;

            public void setInnerPlanets(float f)    { InnerPlanets    =        (int)f; }
            public void setOuterPlanets(float f)    { OuterPlanets    =        (int)f; }
            public void setFarOrbitPlanets(float f) { FarOrbitPlanets =        (int)f; }

            public void setSystemScale(float f)     { system_scale    =             f; }

            public void setSunMass(float f)         { SunMass         =             f; }
            public void setPlanetMass(float f)      { PlanetMass      =             f; }
            public void setMoonMass(float f)        { MoonMass        =             f; }

            public void setTimeScale(float f)       { time_scale      =             f; }

            public void setAcceleration(float f)    { acceleration    =             f; }
            public void setDragFactor(float f)      { drag_factor     = 100000.0f - f; }
            public void setSailingFactor(float f)   { sailing_factor  =   1000.0f - f; }

            public void toggle_planet_movement(bool b) {
                planet_movement = b;
            }

            public void toggle_n_body_gravity(bool b) {
                n_body_gravity = b;
                gravity_renderer.n_body_gravity = b;
            }
                
            public void engage_autopilot() {
                state.player.ship.engage_orbital_autopilot();
            }

            public void circularize() {
                state.player.circularizing = true;
            }

            public void set_periapsis(string s) {
                state.player.periapsis = float.Parse(s);
            }

            public void set_apoapsis(string s) {
                state.player.apoapsis = float.Parse(s);
            }

            public void set_rotation(string s) { 
                state.player.rotation = Tools.normalize_angle(float.Parse(s) * Tools.pi / 180.0f);
            }

            public  Dropdown DockingTargetSelector;
            private SpaceStation DockingTarget;
            public  GravityRenderer gravity_renderer;

            private void Start() {
                RegenerateSystem();

                var StarObject = new GameObject();
                StarObject.name = "Star Renderer";

                Camera = GameObject.Find("Main Camera").GetComponent<CameraController>();
            }

            public void CenterCamera() {
                Camera.set_position(-state.player.ship.self.posx, -state.player.ship.self.posy, 0.25f / system_scale);
            }

            public void TogglePlayerTracking() {
                if(TrackingPlayer = !TrackingPlayer) CenterCamera();                
            }

            public void RegenerateSystem() {
                LastTime = Time.time;

                // delete previous system

                state.cleanup();

                rnd = new System.Random(seed);

                for(int i = 0; i < StarCount; i++) {

                    state.stars.Add(new());

                    state.stars[i].obj.self.mass                    = SunMass * (float)rnd.NextDouble() * (i + 1);
                    state.stars[i].obj.self.posx                    = ((float)rnd.NextDouble() * 16.0f - 64.0f) * system_scale;
                    state.stars[i].obj.self.posy                    = ((float)rnd.NextDouble() * 16.0f -  8.0f) * system_scale;
                    state.stars[i].obj.render_orbit                 = StarCount > 1;

                }

                if(StarCount > 1)
                    for(int i = 0; i < StarCount; i++) {

                        int j;
                        do j = rnd.Next(StarCount);
                        while(j == i);

                        state.stars[i].obj.descriptor.semiminoraxis = (float)rnd.NextDouble() * 32.0f * system_scale;
                        state.stars[i].obj.descriptor.semimajoraxis = (float)rnd.NextDouble() * 32.0f * system_scale + state.stars[i].obj.descriptor.semiminoraxis;
                        state.stars[i].obj.descriptor.rotation      = (float)rnd.NextDouble() * Tools.twopi;
                        state.stars[i].obj.descriptor.rotation      = (float)rnd.NextDouble() * Tools.twopi;
                        state.stars[i].obj.descriptor.mean_anomaly  = (float)rnd.NextDouble() * Tools.twopi;
                        state.stars[i].obj.descriptor.central_body  = state.stars[j].obj.self;

                    }

                for(int i = 0; i < InnerPlanets; i++) {

                    SystemPlanet Planet = new SystemPlanet();

                    Planet.descriptor.central_body  = state.stars[0].obj.self;
                    Planet.descriptor.semiminoraxis = (30.0f + (i + 1) * (i + 1) * 10) * system_scale;
                    Planet.descriptor.semimajoraxis = Planet.descriptor.semiminoraxis + ((float)rnd.NextDouble() * (i + 5) * system_scale);
                    Planet.descriptor.rotation      = (float)rnd.NextDouble() * Tools.twopi;
                    Planet.descriptor.mean_anomaly  = (float)rnd.NextDouble() * Tools.twopi;
                    Planet.descriptor.self.mass     = PlanetMass;
                    Planet.type                     = PlanetType.PLANET_ROCKY;

                    state.planets.Add(new());
                    var p = state.planets[state.planets.Count - 1];
                    p.obj = Planet;

                }

                for(int i = 0; i < OuterPlanets; i++) {

                    SystemPlanet Planet = new SystemPlanet();

                    Planet.descriptor.central_body  = state.stars[0].obj.self;
                    Planet.descriptor.semiminoraxis = state.planets[InnerPlanets - 1].obj.descriptor.semimajoraxis + ((i + 3) * (i + 3) * 10 * system_scale);
                    Planet.descriptor.semimajoraxis = Planet.descriptor.semiminoraxis + ((float)rnd.NextDouble() * i / 20.0f) * system_scale;
                    Planet.descriptor.rotation      = (float)rnd.NextDouble() * Tools.twopi;
                    Planet.descriptor.mean_anomaly  = (float)rnd.NextDouble() * Tools.twopi;
                    Planet.descriptor.self.mass     = PlanetMass;
                    Planet.type                     = PlanetType.PLANET_GAS_GIANT;

                    state.planets.Add(new());
                    var p = state.planets[state.planets.Count - 1];
                    p.obj = Planet;

                    for(int j = 0; j < rnd.Next(i + 1); j++) {

                        SystemPlanet Moon = new SystemPlanet();

                        Moon.descriptor.self.mass     = MoonMass;
                        Moon.descriptor.central_body  = Planet.descriptor.self;
                        Moon.descriptor.semiminoraxis = ((float)rnd.NextDouble() * (j + 1) + 5.0f) * system_scale;
                        Moon.descriptor.semimajoraxis = Moon.descriptor.semiminoraxis + ((float)rnd.NextDouble() * 2.0f) * system_scale;
                        Moon.descriptor.rotation      = (float)rnd.NextDouble() * Tools.twopi;
                        Moon.descriptor.mean_anomaly  = (float)rnd.NextDouble() * Tools.twopi;
                        Moon.type                     = PlanetType.PLANET_MOON;

                        state.planets.Add(new());
                        var m = state.planets[state.planets.Count - 1];
                        m.obj = Moon;

                    }

                }

                for(int i = 0; i < FarOrbitPlanets; i++) {

                    SystemPlanet Planet = new SystemPlanet();

                    Planet.descriptor.central_body  = state.stars[0].obj.self;
                    Planet.descriptor.semiminoraxis = state.planets[InnerPlanets + OuterPlanets - 1].obj.descriptor.semimajoraxis
                                                    + ((i + 3) * (i + 3) * 31 * system_scale);
                    Planet.descriptor.semimajoraxis = Planet.descriptor.semiminoraxis + (float)rnd.NextDouble() * (i + 1) * 82 * system_scale;
                    Planet.descriptor.rotation      = (float)rnd.NextDouble() * Tools.twopi;
                    Planet.descriptor.mean_anomaly  = (float)rnd.NextDouble() * Tools.twopi;
                    Planet.descriptor.self.mass     = PlanetMass;
                    Planet.type                     = PlanetType.PLANET_ROCKY;

                    state.planets.Add(new());
                    var p = state.planets[state.planets.Count - 1];
                    p.obj = Planet;

                }

                for(int i = 0; i < SpaceStations; i++) {

                    state.stations.Add(new());

                    state.stations[i].obj.descriptor.central_body  =  state.stars[0].obj.self;
                    state.stations[i].obj.descriptor.semiminoraxis = ((float)rnd.NextDouble()
                                                                   *  state.planets[InnerPlanets + OuterPlanets - 1].obj.descriptor.semimajoraxis + 4.0f);
                    state.stations[i].obj.descriptor.semimajoraxis =  (float)rnd.NextDouble() * system_scale
                                                                   +  state.stations[i].obj.descriptor.semiminoraxis;
                    state.stations[i].obj.descriptor.rotation      =  (float)rnd.NextDouble() * Tools.twopi;
                    state.stations[i].obj.descriptor.mean_anomaly  =  (float)rnd.NextDouble() * Tools.twopi;
                    state.stations[i].obj.descriptor.self.mass     =  StationMass;

                }

                state.create_renderers();

                foreach(var planet in state.planets) {
                    planet.renderer.blur_noise_shader         = blur_noise_shader;
                    planet.renderer.scale_noise_shader        = scale_noise_shader;
                    planet.renderer.exponential_filter_shader = exponential_filter_shader;
                    planet.renderer.distortion_shader         = distortion_shader;
                    planet.renderer.circular_blur_shader      = circular_blur_shader;
                    planet.renderer.circular_mask_shader      = circular_mask_shader;
                    planet.renderer.pixelate_shader           = pixelate_shader;

                    planet.renderer.seed                      = rnd.Next();
                    planet.renderer.pixelate                  = pixelate;
                    planet.renderer.pixelation_size           = pixelation_size;
                    planet.renderer.smoothstepped_colors      = smoothstepped_colors;

                    switch(planet.obj.type) {

                        case PlanetType.PLANET_ROCKY:
                            planet.renderer.radius    = rocky_planet_radius;
                            planet.renderer.layers    = rocky_planet_layers;
                            planet.renderer.basecolor = rocky_planet_base_color;
                            planet.renderer.colors    = rocky_planet_colors;
                            break;

                        case PlanetType.PLANET_GAS_GIANT:
                            planet.renderer.radius    = gas_giant_radius;
                            planet.renderer.layers    = gas_giant_layers;
                            planet.renderer.basecolor = gas_giant_base_color;
                            planet.renderer.colors    = gas_giant_colors;
                            break;

                        case PlanetType.PLANET_MOON:
                            planet.renderer.radius    = moon_radius;
                            planet.renderer.layers    = moon_layers;
                            planet.renderer.basecolor = moon_base_color;
                            planet.renderer.colors    = moon_colors;
                            break;

                    }
                }

                state.generate_renderers();

                state.player              = gameObject.AddComponent<PlayerShip>();
                state.player.system_scale = system_scale;
            }

            private SpaceObject gravity_cycle(SpaceObject self, float current_time) {
                SpaceObject strongest_body = null;
                float       maxg           = 0.0f;
                float       grav_velx       = 0.0f;
                float       grav_vely       = 0.0f;

                foreach(SpaceObject body in state.objects) {

                    if(body == self) continue;

                    float dx = body.posx - self.posx;
                    float dy = body.posy - self.posy;

                    float d2 = dx * dx + dy * dy;
                    float d = (float)Math.Sqrt(d2);

                    float g = Tools.gravitational_constant * body.mass / d2;

                    if(g > maxg) strongest_body = body;

                    if(n_body_gravity) {

                        float Velocity = g * current_time;

                        grav_velx += Velocity * dx / d;
                        grav_vely += Velocity * dy / d;

                    } else {

                        if(g > maxg) {
                            maxg = g;
                            float vel = g * current_time;

                            grav_velx = vel * dx / d;
                            grav_vely = vel * dy / d;
                        }

                    }

                }

                self.posx += self.velx * current_time + 0.5f * grav_velx * current_time;
                self.posy += self.vely * current_time + 0.5f * grav_vely * current_time;

                self.velx += grav_velx;
                self.vely += grav_vely;

                return strongest_body;
            }
            void Update() {
                float current_time = (Time.time - LastTime) * time_scale;
                LastTime = Time.time;

                /*if(CachedSunMass != SunMass) {
                    State.stars[0].self.mass = CachedSunMass = SunMass;

                    for(int i = 0; i < Planets.Count; i++)
                        Planets.ElementAt(i).Key.descriptor.compute();

                    foreach(SystemShip ship in State.ships)
                        ship.descriptor.compute();
                }

                if(CachedPlanetMass != PlanetMass) {
                    CachedPlanetMass = PlanetMass;

                    for(int i = 0; i < Planets.Count; i++)
                        Planets.ElementAt(i).Key.descriptor.self.mass = PlanetMass;

                    for(int i = 0; i < Moons.Count; i++)
                        Moons.ElementAt(i).Key.descriptor.compute();
                }

                if(CachedMoonMass != MoonMass) {
                    CachedPlanetMass = MoonMass;

                    for(int i = 0; i < Moons.Count; i++)
                        Moons.ElementAt(i).Key.descriptor.self.mass = MoonMass;
                }*/
                
                foreach(var ship in state.ships) {
                    SystemShip s = ship.obj;

                    if(!s.path_planned)
                        s.path_planned = s.descriptor.plan_path(s.destination, 0.1f * system_scale);
                    else if(s.descriptor.get_distance_from(s.destination) < system_scale) {
                        s.descriptor.copy(s.destination);
                        s.path_planned = false;
                        (s.start, s.destination) = (s.destination, s.start);
                    }

                    s.descriptor.update_position(current_time);
                }

                state.player.stations_orbiting = planet_movement;

                if(planet_movement) {

                    if(StarCount <= 1 || !n_body_gravity) {

                        foreach(var p in state.planets)
                            p.obj.descriptor.update_position(current_time);

                        foreach(var s in state.stations)
                            s.obj.descriptor.update_position(current_time);

                    } else {

                        foreach(var star in state.stars) {

                            SpaceObject strongest_body = gravity_cycle(star.obj.self, current_time);

                            if(strongest_body != null)
                                star.obj.descriptor.change_frame_of_reference(strongest_body);

                        }

                        foreach(var planet in state.planets) {

                            SpaceObject strongest_body = gravity_cycle(planet.obj.self, current_time);

                            if(strongest_body != null)
                                planet.obj.descriptor.change_frame_of_reference(strongest_body);

                        }

                        foreach(var station in state.stations) {

                            SpaceObject strongest_body = gravity_cycle(station.obj.self, current_time);

                            if(strongest_body != null)
                                station.obj.descriptor.change_frame_of_reference(strongest_body);

                        }

                    }

                }

                if(!state.player.ship.ignore_gravity) {
                    float maxg = 0.0f;
                    float GravVelX = 0.0f;
                    float GravVelY = 0.0f;

                    // this behaves weird when getting really close to central body --- is float too inaccurate?
                    foreach(SpaceObject body in state.objects) {

                        float dx = body.posx - state.player.ship.self.posx;
                        float dy = body.posy - state.player.ship.self.posy;

                        float d2 = dx * dx + dy * dy;
                        float d = (float)Math.Sqrt(d2);

                        float g = Tools.gravitational_constant * body.mass / d2;

                        if(n_body_gravity) {

                            float Velocity = g * current_time;

                            GravVelX += Velocity * dx / d;
                            GravVelY += Velocity * dy / d;

                        } else {

                            if(g > maxg) {
                                maxg = g;
                                float vel = g * current_time;

                                GravVelX = vel * dx / d;
                                GravVelY = vel * dy / d;
                            }

                        }

                    }

                    state.player.gravitational_strength = (float)Math.Sqrt(GravVelX * GravVelX + GravVelY * GravVelY) * 0.4f / current_time;

                    state.player.ship.self.velx   += GravVelX;
                    state.player.ship.self.vely   += GravVelY;

                    // For some reason this messes stuff up?!

                    //State.Player.ship.self.posx   += GravVelX * CurrentTime * 0.5f;
                    //State.Player.ship.self.posy   += GravVelY * CurrentTime * 0.5f;
                }

                state.player.ship.acceleration = acceleration;
                state.player.drag_factor       = drag_factor;
                state.player.sailing_factor    = sailing_factor;
                state.player.time_scale        = time_scale;

                UpdateDropdownMenu();

                if(TrackingPlayer) {
                    if(Input.GetMouseButton(1)) TrackingPlayer = false; // Disable camera tracking if user is manually moving camera
                    else
                        Camera.set_position(-state.player.ship.self.posx, -state.player.ship.self.posy, Camera.scale);
                }
            }

            private void UpdateDropdownMenu() {
                DockingTargetSelector.ClearOptions();

                List<string> Options = new();

                if(state.stations.Count == 0) {
                    Options.Add("-- No stations --");

                    DockingTargetSelector.interactable = false;
                } else {
                    Options.Add("-- Select a station --");

                    for(int i = 0; i < state.stations.Count;)
                        Options.Add("Station " + ++i);

                    DockingTargetSelector.interactable = true;
                }

                DockingTargetSelector.AddOptions(Options);

                DockingTargetSelector.value = 0;

                for(int i = 0; i < state.stations.Count; i++)
                    if(state.stations[i].obj == DockingTarget) {
                        DockingTargetSelector.value = i + 1;
                        break;
                    }
            }

            public void SelectDockingTarget(int i) {
                if(i == 0 || i > state.stations.Count) {
                    DockingTarget = null;
                    state.player.ship.disengage_docking_autopilot();
                } else if(DockingTarget != state.stations[i - 1].obj)
                    state.player.ship.engage_docking_autopilot(DockingTarget = state.stations[i - 1].obj);
            }
        }
    }
}
