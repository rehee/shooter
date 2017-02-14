using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace System
{
    public static class G
    {
        private static int count = 0;
        public static object CreateGeneralType(Type type)
        {
            try
            {
                var result = Activator.CreateInstance(type);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }
        public static object CreateGeneralType(Type type, params object[] args)
        {
            try
            {
                var result = Activator.CreateInstance(type, args);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }
        public static string ToJson(this Object input)
        {
            var count = 0;
            start:
            try
            {
                return JsonConvert.SerializeObject(input);
            }
            catch { count++; if (count < 100) goto start; return ""; }

        }
        public static int GetRandomInt(int max, int min = 0)
        {
            count++;
            if (count > 3000)
                count = 0;
            var random = new Random(DateTime.Now.Millisecond + count);
            return random.Next(min, max);
            
        }
    }
}
