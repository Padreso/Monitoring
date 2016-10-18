﻿using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;

namespace MonitoringAgent
{
    public class PluginLoader
    {
        private CompositionContainer _container;

        public void Loader()
        {
            if (!Directory.Exists("Plugins"))
            {
                Directory.CreateDirectory("Plugins");
            }

            //An aggregate catalog that combines multiple catalogs
            var catalog = new AggregateCatalog();
            //Adds all the parts found in the same assembly as the Program class
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(Program).Assembly));
            catalog.Catalogs.Add(new DirectoryCatalog(Path.GetFullPath(@"Plugins")));

            //Create the CompositionContainer with the parts in the catalog
            _container = new CompositionContainer(catalog);

            //Fill the imports of this object
            try
            {
                this._container.ComposeParts(this);
                foreach (var item in catalog.Catalogs)
                {
                    if(item is DirectoryCatalog)
                    {
                        foreach (var loadedFile in ((DirectoryCatalog)item).LoadedFiles)
                        {
                            Console.WriteLine(loadedFile.ToString());
                            var DLL = Assembly.LoadFile(loadedFile);

                            foreach (Type type in DLL.GetExportedTypes())
                            {
                                dynamic c = Activator.CreateInstance(type);
                                c.Output();
                            }
                        }
                    }
                }
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }

           
        }

    }
}
