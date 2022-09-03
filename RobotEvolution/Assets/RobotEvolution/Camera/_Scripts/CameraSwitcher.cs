using Cinemachine;
using System.Collections.Generic;

public class CameraSwitcher
{
    private static List<CinemachineVirtualCamera> t_cameraList = new();
    public static CinemachineVirtualCamera t_ActiveCmCamera = null;

    public static void Register(CinemachineVirtualCamera camera)
    {
        t_cameraList.Add(camera);
    }

    public static bool isActiveCamera(CinemachineVirtualCamera camera)
    {
        return camera == t_ActiveCmCamera;
    }

    public static void SwitchCamera(CinemachineVirtualCamera camera)
    {
        camera.Priority = 10;
        t_ActiveCmCamera = camera;

        foreach (CinemachineVirtualCamera c in t_cameraList)
        {
            if (c != t_ActiveCmCamera)
                c.Priority = 0;
        }
    }
}
