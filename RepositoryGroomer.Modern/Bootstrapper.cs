using System;
using System.Collections.Generic;
using Caliburn.Micro;
using System.Windows;
using RepositoryGroomer.Core;

namespace RepositoryGroomer.Modern
{
    public class Bootstrapper : BootstrapperBase
    {
        readonly SimpleContainer _container = new SimpleContainer();

        public Bootstrapper()
        {
            Initialize();
        }
        
        protected override void Configure()
        {
            log4net.Config.XmlConfigurator.Configure();

            _container.Singleton<IWindowManager, WindowManager>();
            _container.Singleton<IEventAggregator, EventAggregator>();
            _container.Singleton<IAmConfigurationProvider, ConfigurationProvider>();
            _container.Singleton<IAmProjectFileFinder, ProjectFileFinder>();
            _container.Singleton<IAmFileReader, FileReader>();
            _container.Singleton<MainWindowViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<MainWindowViewModel>();
        }
    }
}