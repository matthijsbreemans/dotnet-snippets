public class SimpleInjectorActivator : JobActivator
    {
        private readonly Container _container;
        private readonly Lifestyle _lifestyle;

        public SimpleInjectorActivator(Container container)
        {
            _container = container ?? throw new ArgumentNullException("container");
        }

        public SimpleInjectorActivator(Container container, Lifestyle lifestyle)
        {
            _container = container ?? throw new ArgumentNullException("container");
            _lifestyle = lifestyle ?? throw new ArgumentNullException("lifestyle");
        }

        public override object ActivateJob(Type jobType)
        {
            return _container.GetInstance(jobType);
        }

        public override JobActivatorScope BeginScope(JobActivatorContext context)
        {
            if (_lifestyle == null || _lifestyle != Lifestyle.Scoped)
            {
                return new SimpleInjectorScope(_container, SimpleInjector.Lifestyles.AsyncScopedLifestyle.BeginScope(_container));
            }

            
            return new SimpleInjectorScope(_container, new SimpleInjector.Lifestyles.AsyncScopedLifestyle().GetCurrentScope(_container));
        }
    }

    internal class SimpleInjectorScope : JobActivatorScope
    {
        private readonly Container _container;
        private readonly Scope _scope;

        public SimpleInjectorScope(Container container, Scope scope)
        {
            _container = container;
            _scope = scope;
        }

        public override object Resolve(Type type)
        {
            return _container.GetInstance(type);
        }

        public override void DisposeScope()
        {
            if (_scope != null)
            {
                _scope.Dispose();
            }
        }
    }
}