using Foster.Framework;
using FosterImGui;
using ImGuiNET;
using Pine.Core.Components;

namespace Pine.Sandbox.Scripts.Core.Scenes;

public class TestImGUI : Scene
{
    public override void Startup()
    {
        Renderer.Startup();
    }

    public override void Update()
    {
        Renderer.BeginLayout();
        if (ImGui.BeginMainMenuBar())
        {

            if (ImGui.BeginMenu("File"))
            {
                if (ImGui.MenuItem("Hello"))
                {

                }

                if (ImGui.MenuItem("World"))
                {

                }

                if (ImGui.MenuItem("Goodbye"))
                {

                }

                if (ImGui.MenuItem("World"))
                {

                }
                ImGui.EndMenu();
            }

            if (ImGui.BeginMenu("Edit"))
            {
                if (ImGui.MenuItem("Create"))
                {

                }

                ImGui.EndMenu();
            }

            if (ImGui.BeginMenu("View"))
            {
                if (ImGui.MenuItem("Create"))
                {

                }

                ImGui.EndMenu();
            }

            ImGui.EndMainMenuBar();
        }


        Renderer.EndLayout();
    }

    public override void Render(Batcher batcher)
    {
        Renderer.Render();
    }

    public override void Shutdown()
    {
        Renderer.Shutdown();
    }
}
