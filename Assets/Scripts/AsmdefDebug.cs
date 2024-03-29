#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using UnityEditor;
using UnityEditor.Compilation;


// https://gist.github.com/karljj1/9c6cce803096b5cd4511cf0819ff517b
// Runs on Load
[InitializeOnLoad]
public class AsmdefDebug
{
    // Assembly Reload Events Time
    const string AssemblyReloadEventsEditorPref = "AssemblyReloadEventsTime";

    // Assembly Compilation Events
    const string AssemblyCompilationEventsEditorPref = "AssemblyCompilationEvents";

    // Script Assemblies Path Lenght
    static readonly int ScriptAssembliesPathLen = "Library/ScriptAssemblies/".Length;

    // Assembly Total Campilation Time
    private static string AssemblyTotalCompilationTimeEditorPref = "AssemblyTotalCompilationTime";

    // Local Start Times
    static Dictionary<string, DateTime> s_StartTimes = new Dictionary<string, DateTime>();

    // Build Events
    static StringBuilder s_BuildEvents = new StringBuilder();

    // Compilation Total Time
    static double s_CompilationTotalTime;

    // Static Constructor
    static AsmdefDebug()
    {
        // Get Assembly Compilation Data
#pragma warning disable CS0618
        CompilationPipeline.assemblyCompilationStarted += CompilationPipelineOnAssemblyCompilationStarted;
#pragma warning restore CS0618
        CompilationPipeline.assemblyCompilationFinished += CompilationPipelineOnAssemblyCompilationFinished;
        AssemblyReloadEvents.beforeAssemblyReload += AssemblyReloadEventsOnBeforeAssemblyReload;
        AssemblyReloadEvents.afterAssemblyReload += AssemblyReloadEventsOnAfterAssemblyReload;
    }

    // Calculate Assembly Start Times
    static void CompilationPipelineOnAssemblyCompilationStarted(string assembly)
    {
        s_StartTimes[assembly] = DateTime.UtcNow;
    }

    // Calculate Compilation Time and Set Report Text
    static void CompilationPipelineOnAssemblyCompilationFinished(string assembly, CompilerMessage[] arg2)
    {
        var timeSpan = DateTime.UtcNow - s_StartTimes[assembly];
        s_CompilationTotalTime += timeSpan.TotalMilliseconds;
        s_BuildEvents.AppendFormat("{0:0.00}s {1}\n", timeSpan.TotalMilliseconds / 1000f,
            assembly.Substring(ScriptAssembliesPathLen, assembly.Length - ScriptAssembliesPathLen));
    }

    // Assembly Reload Events Time Calculations
    static void AssemblyReloadEventsOnBeforeAssemblyReload()
    {
        var totalCompilationTimeSeconds = s_CompilationTotalTime / 1000f;
        s_BuildEvents.AppendFormat("compilation total: {0:0.00}s\n", totalCompilationTimeSeconds);
        EditorPrefs.SetString(AssemblyReloadEventsEditorPref, DateTime.UtcNow.ToBinary().ToString());
        EditorPrefs.SetString(AssemblyCompilationEventsEditorPref, s_BuildEvents.ToString());
        EditorPrefs.SetString(AssemblyTotalCompilationTimeEditorPref, totalCompilationTimeSeconds.ToString(CultureInfo.CurrentCulture));
    }

    // Assembly Reload Events
    static void AssemblyReloadEventsOnAfterAssemblyReload()
    {
        var binString = EditorPrefs.GetString(AssemblyReloadEventsEditorPref);
        var totalCompilationTimeSeconds = double.Parse(EditorPrefs.GetString(AssemblyTotalCompilationTimeEditorPref));

        long bin;
        if (long.TryParse(binString, out bin))
        {
            var date = DateTime.FromBinary(bin);
            var time = DateTime.UtcNow - date;
            var compilationTimes = EditorPrefs.GetString(AssemblyCompilationEventsEditorPref);
            var totalTimeSeconds = totalCompilationTimeSeconds + time.TotalSeconds;
            if (!string.IsNullOrEmpty(compilationTimes))
            {
                UnityEngine.Debug.Log($"Editor Game Loading Time: {totalTimeSeconds:F2} seconds\n");
                UnityEngine.Debug.Log($"Compilation Report: {totalTimeSeconds:F2} seconds\n" + compilationTimes + "Assembly Reload Time: " + time.TotalSeconds + "s\n");
            }
        }
    }
}
#endif