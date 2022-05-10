warning: CRLF will be replaced by LF in Assets/Scripts/Buildings/Tower.cs.
The file will have its original line endings in your working directory
warning: CRLF will be replaced by LF in Assets/Scripts/Cameras/CameraController.cs.
The file will have its original line endings in your working directory
[1mdiff --git a/Assets/Scripts/Buildings/Tower.cs b/Assets/Scripts/Buildings/Tower.cs[m
[1mindex cd0aa17..1658d5b 100644[m
[1m--- a/Assets/Scripts/Buildings/Tower.cs[m
[1m+++ b/Assets/Scripts/Buildings/Tower.cs[m
[36m@@ -13,6 +13,7 @@[m [mnamespace Pandaria.Buildings[m
 [m
         public void Activate()[m
         {[m
[32m+[m[32m            Debug.Log("Activating");[m
             character.SetActive(false);[m
             characterCamera.enabled = false;[m
             turretCamera.enabled = true;[m
[1mdiff --git a/Assets/Scripts/Cameras/CameraController.cs b/Assets/Scripts/Cameras/CameraController.cs[m
[1mindex 3d551f7..f60e735 100644[m
[1m--- a/Assets/Scripts/Cameras/CameraController.cs[m
[1m+++ b/Assets/Scripts/Cameras/CameraController.cs[m
[36m@@ -13,11 +13,12 @@[m [mnamespace Pandaria.Cameras[m
 [m
         void Update()[m
         {[m
[32m+[m[41m            [m
             if (this.targetTransform == null)[m
             {[m
                 return;[m
             }[m
[31m-[m
[32m+[m[32m            Debug.Log("Updating");[m
             var vector = Quaternion.AngleAxis(this.verticalAngle, Vector3.forward) * Vector3.right;[m
             vector = Quaternion.AngleAxis(this.hortizontalAngle, Vector3.up) * vector;[m
 [m
