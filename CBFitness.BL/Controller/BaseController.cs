using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CBFitness.BL.Controller
{
    public abstract class BaseController
    {
        protected IDataSaver saver = new BaseDataSaver();
        //protected IDataSaver saver = new SerializeDataSaver();
        protected void Save(string fileName, object item)
        {
            saver.Save(fileName,item);
        }
        protected T Load<T>(string fileName) where T:class
        {
            return saver.Load<T>(fileName);
        }
    }
}
