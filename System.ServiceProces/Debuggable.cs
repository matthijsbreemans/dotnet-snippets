//source unknown

  public partial class SnippetService: ServiceBase
  {
   ...
        internal void TestStartupAndStop()
        {
            OnStart(null);
            Console.ReadLine();
            OnStop();
        }
   ...
   }


Program.cs
...
 if (Environment.UserInteractive)
            {
                SnippetService taskSchedulerService = new SnippetService();
                taskSchedulerService.TestStartupAndStop();
            }
            else
            {
                ServiceBase.Run(new ServiceBase[]
                {
      new SnippetService()
                });
            }
...