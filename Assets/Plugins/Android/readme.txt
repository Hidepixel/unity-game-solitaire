This manifest and .aar files have to be dragged (replace game manifest) onto each games' Plugins/Android folder. These override Unity's activity (entry point for the android version) so we can change stuff on the OnCreate() event.

- Current Features:
  - Hide Android UI as soon as the game is booted. This makes it so the games now have the same behavior	 as the rest of the Seninc applications. You need to disable "Start in fullscreen mode" because it will conflict with the included .aar file implementation.