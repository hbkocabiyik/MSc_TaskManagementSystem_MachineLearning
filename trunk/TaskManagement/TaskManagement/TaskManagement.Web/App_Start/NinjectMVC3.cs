using System.Collections.Generic;
using System.Configuration;
using Agh.DecisionTree.Algorithm;
using Agh.DecisionTree.Entity;
using Agh.DecisionTree.ID3;
using Agh.DecisionTree.Loading;
using TaskManagement.Web.Models;
using TaskManagement.Web.Models.Data;

[assembly: WebActivator.PreApplicationStartMethod(typeof(TaskManagement.Web.App_Start.NinjectMVC3), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(TaskManagement.Web.App_Start.NinjectMVC3), "Stop")]

[assembly: WebActivator.PreApplicationStartMethod(typeof(TaskManagement.Web.App_Start.ID3DecisionTreeEngine), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(TaskManagement.Web.App_Start.ID3DecisionTreeEngine), "Stop")]

namespace TaskManagement.Web.App_Start
{
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
	using Ninject.Web.Mvc;

    public static class ID3DecisionTreeEngine
    {
        private static IDecisionTreeAlgorithm _algorithm;
        private static IDecisionTreeAlgorithm _algorithmOptimized;

        public static void Start()
        {
            var dbManagementXmlFileName = ConfigurationManager.AppSettings["DBManagementXmlFileName"];
            
            IDataLoader loader = DataLoader.CreateIt(EntityDataValidator.CreateIt());
            
            var data = loader.LoadFromFile(dbManagementXmlFileName);
            _algorithm = ID3Algorithm.CreateIt(data);
            _algorithm.BuildDecisionTree();

            var dataForOptimized = loader.LoadFromFile(dbManagementXmlFileName);
            _algorithmOptimized = ID3Algorithm.CreateIt(dataForOptimized, true);
            _algorithmOptimized.BuildDecisionTree();
        }

        public static void Stop()
        {
            _algorithm = null;
            _algorithmOptimized = null;
        }

        public static string Classify(IList<string> data)
        {
            return _algorithm.Classify(data);
        }

        public static string ClassifyOptimized(IList<string> data)
        {
            return _algorithmOptimized.Classify(data);
        }
    }

    public static class NinjectMVC3 
	{
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
		{
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestModule));
			bootstrapper.Initialize(CreateKernel);
        }
		
        /// <summary>
        /// Stops the application.
        /// </summary>
		public static void Stop()
		{
			bootstrapper.ShutDown();
		}
		
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<ITaskManagementContext>().To<TaskManagementContext>();

            kernel.Bind<IAreaRepository>().To<AreaRepository>();
            kernel.Bind<ISkillRepository>().To<SkillRepository>();
            kernel.Bind<IEmployeeRepository>().To<EmployeeRepository>();
            kernel.Bind<ITaskRepository>().To<TaskRepository>();
        }		
    }
}
