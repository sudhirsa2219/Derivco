using Derivco.Core.Exceptions;

namespace Derivco.Core.Utils
{
    public class ExceptionThrower
    {

        public static void Throws<T>()
            where T : CoreException, new()
        {
            throw new T();
        }
    }
}
