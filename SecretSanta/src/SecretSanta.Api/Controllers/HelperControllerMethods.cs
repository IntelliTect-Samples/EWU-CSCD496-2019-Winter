using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Api.Controllers
{
    public class HelperControllerMethods
    {
        public bool IsValidId(int value)
        {
            return value <= 0;
        }

        public bool IsNull(Object obj)
        {
            return obj == null;
        }
    }
}
