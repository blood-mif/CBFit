using System.Runtime.InteropServices.ComTypes;

namespace CBFitness.BL.Controller
{
    public interface IDataSaver
    {
        void Save(string fileName, object item);
        T Load<T>(string fileName) where T : class;
    }
}
