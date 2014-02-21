using Cio.UI;
using Cio.UI.Composition.Default;
using Cio.UI.Services;
using Cio.UI.Wpf;
using Cio.UI.Wpf.ServiceVisitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FullCioApp
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {

#if use_config_file
			CioConfiguration config = ConfigurationLoader.Load();
#else
            CioConfiguration config = new CioConfiguration();
            config.RegisterServiceVisitor(new DisplayNameServiceVisitor());
            config.RegisterServiceVisitor(new EditableServiceVisitor());
            config.RegisterService(CreateDisplayNameService());

            config.Elements = new ElementConfiguration();
#endif
            config.Elements.RegisterDefaultControls();

            CioWindow wnd = new CioWindow(config, new WpfWindowBuilder());

            wnd.Render();
            new Application().Run();
        }

        private static IDisplayNameService CreateDisplayNameService()
        {
            IDisplayNameService propertyNameDisplayService = new PropertyDisplayNameService();

            return new AttributedDisplayNameService(propertyNameDisplayService);
        }
    }
}
