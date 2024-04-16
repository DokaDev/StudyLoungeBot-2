using ServerCore;

CoreModule coreModule = new();
await coreModule.StartServeAsync()!;

while(true);